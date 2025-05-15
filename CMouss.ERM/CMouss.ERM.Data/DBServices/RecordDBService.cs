using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace CMouss.ERM.Data.DBServices
{
    public class RecordDBService
    {
        private readonly ERMDBContext _context;

        public RecordDBService(ERMDBContext context)
        {
            _context = context;
        }

        public async Task<List<Record>> GetAllAsync()
        {
            var records = await _context.Records
                .Include(x => x.EntityType)
                .ToListAsync();
            return records;
        }

        public async Task<List<Record>> GetByEntityTypeIdAsync(int entityTypeId)
        {
            var records = await _context.Records
                .Include(x => x.EntityType)
                .Include(x => x.RecordFieldValues)
                    .ThenInclude(rfv => rfv.EntityField)
                .Where(x => x.EntityTypeId == entityTypeId)
                .ToListAsync();
            return records;
        }

        public async Task<List<Record>> GetByEntityTypeIdAsync(int entityTypeId, string searchFor)
        {
            var records = await _context.Records
                .Include(x => x.EntityType)
                .Include(x => x.RecordFieldValues)
                    .ThenInclude(rfv => rfv.EntityField)
                .Where(x => x.EntityTypeId == entityTypeId && x.RecordFieldValues.Any(rfv => rfv.FieldValue.Contains(searchFor)))
                .ToListAsync();
            return records;
        }

        public async Task<List<Record>> GetByEntityTypeIdAsync(int entityTypeId, RecordFilter recordFilter, string orderBy, int page, int pageSize, bool reverseOrder)
        {
            string sortOrder = reverseOrder ? "descending" : "ascending";
            bool sortIsByFieldValues = !IsPrimaryRecordField(orderBy);
            string sortByEntityFieldName = sortIsByFieldValues ? orderBy : null;

            // Start base query
            var query = _context.Records
                .Include(x => x.EntityType)
                .Include(x => x.RecordFieldValues)
                    .ThenInclude(rfv => rfv.EntityField)
                .Where(x => x.EntityTypeId == entityTypeId);

            // Add search term filter
            if (!string.IsNullOrWhiteSpace(recordFilter.SearchFor))
            {
                query = query.Where(x => x.RecordFieldValues
                    .Any(rfv => rfv.FieldValue.Contains(recordFilter.SearchFor)));
            }

            // Add RecordFilterItems dynamically
            foreach (var filterItem in recordFilter.RecordFilterItems)
            {
                string dynamicCondition;

                if (filterItem.OperatorValue == "Contains")
                {
                    dynamicCondition = "RecordFieldValues.Any(EntityFieldId == @0 && FieldValue.Contains(@1))";
                }
                else
                {
                    string op = filterItem.OperatorValue switch
                    {
                        "Equal" => "==",
                        "NotEqual" => "!=",
                        "GreaterThan" => ">",
                        "LessThan" => "<",
                        "GreaterThanOrEqual" => ">=",
                        "LessThanOrEqual" => "<=",
                        _ => throw new InvalidOperationException($"Unsupported operator: {filterItem.OperatorValue}")
                    };

                    dynamicCondition = $"RecordFieldValues.Any(EntityFieldId == @0 && FieldValue {op} @1)";
                }

                query = query.Where(dynamicCondition, filterItem.EntityFieldId, filterItem.FieldValue);
            }

            // Projection for sorting
            var projectedQuery = query.Select(r => new
            {
                Record = r,
                FieldValueForSorting = sortByEntityFieldName == null
                    ? null
                    : r.RecordFieldValues
                        .Where(rfv => rfv.EntityField.Name == sortByEntityFieldName)
                        .Select(rfv => rfv.FieldValue)
                        .FirstOrDefault()
            });

            string dynamicSortProperty = sortByEntityFieldName != null ? "FieldValueForSorting" : $"Record.{orderBy}";
            string dynamicSortQuery = $"{dynamicSortProperty} {sortOrder}";

            var ordered = projectedQuery
                .OrderBy(dynamicSortQuery)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var result = await ordered.Select(x => x.Record).ToListAsync();
            return result;
        }



        public async Task<Record> GetByIdAsync(int id)
        {
            Record response = new();
            var record = await _context.Records
                .Include(x => x.EntityType)
                .Include(x => x.RecordFieldValues)
                    .ThenInclude(rfv => rfv.EntityField)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (record != null)
            {
                response = record;
            }
            return response;
        }

        public async Task<Record> GetByIdWithRelationsAsync(int id)
        {
            Record response = new();
            var record = await _context.Records
                .Include(x => x.EntityType)
                .Include(x => x.RecordFieldValues)
                    .ThenInclude(rfv => rfv.EntityField)
                .Include(x => x.RecordRelations).ThenInclude(o => o.RightRecord)
                .Include(x => x.RecordRelations).ThenInclude(o => o.EntityRelation)
                .Include(x => x.RecordInverseRelations).ThenInclude(o => o.LeftRecord)
                .Include(x => x.RecordInverseRelations).ThenInclude(o => o.EntityRelation)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (record != null)
            {
                response = record;
            }
            return response;
        }



        public async Task<Record> AddAsync(int entityTypeId, string name, List<RecordValue_Save> recordValues, List<RecordRelation_Save> recordRelations,string ownerUserId, string userId)
        {
            //Check if entity type exists
            var entityType = await _context.EntityTypes
                .Include(x => x.EntityFields)
                .FirstOrDefaultAsync(x => x.Id == entityTypeId);
            if (entityType == null)
            {
                throw new Exception("Entity Type not found");
            }

            //Make sure that all required fields are set
            foreach (var field in entityType.EntityFields.Where(x => x.IsRequired))
            {
                var recordValue = recordValues.FirstOrDefault(x => x.EntityFieldId == field.Id);
                if (recordValue == null || string.IsNullOrWhiteSpace(recordValue.Value))
                {
                    throw new Exception($"Field {field.Name} is required");
                }
            }

            //Check if all fields are valid
            foreach (var recordValue in recordValues)
            {
                var field = await _context.EntityFields
                    .FirstOrDefaultAsync(x => x.Id == recordValue.EntityFieldId && x.EntityTypeId == entityTypeId);
                if (field == null)
                {
                    throw new Exception($"Field with ID {recordValue.EntityFieldId} does not exist in Entity Type {entityTypeId}");
                }
            }

            //TODO: Check if the record name is unique

            //TODO: Check if the record values are valid, by checking the data type of the field and the value




            //Create new record
            Record record = new()
            {
                EntityTypeId = entityTypeId,
                CreateUserId = userId,
                CreateDateTime = DateTime.UtcNow,
                LastUpdateUserId = userId,
                LastUpdate = DateTime.UtcNow,
                IterationId = 1,
                OwnerUserId = ownerUserId,
                IsDeleted = false,
                Name = name 

            };
            await _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();

            //Add field values to the new record by sending the range of values
            List<RecordFieldValue> recordFieldValues = new();
            foreach (var recordValue in recordValues)
            {
                var fieldValue = new RecordFieldValue()
                {
                    RecordId = record.Id,
                    EntityFieldId = recordValue.EntityFieldId,
                    FieldValue = recordValue.Value
                };
                recordFieldValues.Add(fieldValue);
            }
            await _context.RecordFieldValues.AddRangeAsync(recordFieldValues);
            await _context.SaveChangesAsync();


            //Add relations to the new record by sending the range of values
            List<RecordRelation> recordRelationsList = new();
            foreach (var relation in recordRelations)
            {
                foreach (var relationRecord in relation.RecordIds)
                {
                    var recordRelation = new RecordRelation()
                    {
                        EntityRelationId = relation.EntityRelationId,
                        LeftRecordId = record.Id,
                        RightRecordId = relationRecord,
                        CreateUserId = userId,
                        CreateDateTime = DateTime.UtcNow
                    };
                    recordRelationsList.Add(recordRelation);
                }
            }
            await _context.RecordRelations.AddRangeAsync(recordRelationsList);
            _context.SaveChanges();

            return record;
            }

        public async Task<Record> UpdateAsync(int id, string name, List<RecordValue_Save> newRecordValues, List<RecordRelation_Save> recordRelations, string ownerUserId, string userId)
        {
            var record = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new Exception("Record not found");
            }
            if (newRecordValues == null) {
                throw new Exception("Record values cannot be null");
            }

            //Make sure that all required fields are set
            var entityType = await _context.EntityTypes
                .Include(x => x.EntityFields)
                .FirstOrDefaultAsync(x => x.Id == record.EntityTypeId);
            if (entityType == null)
                {
                throw new Exception("Entity Type not found");
            }
            foreach (var field in entityType.EntityFields.Where(x => x.IsRequired))
            {
                var recordValue = newRecordValues.FirstOrDefault(x => x.EntityFieldId == field.Id);
                if (recordValue == null || string.IsNullOrWhiteSpace(recordValue.Value))
                {
                    throw new Exception($"Field {field.Name} is required");
                }
            }

            //Check if all fields are valid
            foreach (var recordValue in newRecordValues)
            {
                var field = await _context.EntityFields
                    .FirstOrDefaultAsync(x => x.Id == recordValue.EntityFieldId && x.EntityTypeId == record.EntityTypeId);
                if (field == null)
                {
                    throw new Exception($"Field with ID {recordValue.EntityFieldId} does not exist in Entity Type {record.EntityTypeId}");
                }
            }



            //Get the old RecordFieldValues
            var oldRecordValues = await _context.RecordFieldValues
                .Where(x => x.RecordId == id)
                .ToListAsync();

            var oldRecordRelations = await _context.RecordRelations
                .Where(x => x.LeftRecordId == id)
                .ToListAsync();

            //Compare oldRecordValues with the new values in recordValues param and update the ones that are different, delete the ones that are not in the new recordValues and add the new ones
            foreach (var recordValue in newRecordValues)
            {
                var oldRecordValue = oldRecordValues.FirstOrDefault(x => x.EntityFieldId == recordValue.EntityFieldId);
                if (oldRecordValue != null)
                {
                    //Update the old value with the new one
                    oldRecordValue.FieldValue = recordValue.Value;
                }
                else
                {
                    //Add the new value
                    RecordFieldValue newFieldValue = new()
                    {
                        RecordId = id,
                        EntityFieldId = recordValue.EntityFieldId,
                        FieldValue = recordValue.Value
                    };
                    await _context.RecordFieldValues.AddAsync(newFieldValue);
                }
            }
            //Delete the old values that are not in the new recordValues
            foreach (var oldRecordValue in oldRecordValues)
            {
                var newRecordValue = newRecordValues.FirstOrDefault(x => x.EntityFieldId == oldRecordValue.EntityFieldId);
                if (newRecordValue == null)
                {
                    //Delete the old value
                    _context.RecordFieldValues.Remove(oldRecordValue);
                }
            }



            //Validate if the recordRelations param is not null and if it contains any values
            if (recordRelations is not null)
            {
                if (recordRelations.Count > 0)
                {

                    //Validate if the recordRelations are valid by checking if the entity relation exists and Record.Relation.EntityTypeId is the same as the record.EntityTypeId
                    foreach (var recordRelation in recordRelations)
                    {
                        var entityRelation = await _context.EntityRelations
                            .FirstOrDefaultAsync(x => x.Id == recordRelation.EntityRelationId);
                        if (entityRelation == null)
                        {
                            throw new Exception($"Entity Relation with ID {recordRelation.EntityRelationId} does not exist");
                        }
                        if ((entityRelation.EntityTypeId_Right != record.EntityTypeId) && (entityRelation.EntityTypeId_Left != record.EntityTypeId))
                        {
                            throw new Exception($"Entity Relation with ID {recordRelation.EntityRelationId} is not valid for the record with ID {id}");
                        }
                    }

                    //Validate if the recordRelations are valid by checking if the recordRelation.RecordIds exist in the database, don't user Foreach loop for this, use a single query to check if all records exist
                    var recordIds = recordRelations.SelectMany(x => x.RecordIds).Distinct().ToList();
                    var records = await _context.Records
                        .Where(x => recordIds.Contains(x.Id))
                        .ToListAsync();
                    if (records.Count != recordIds.Count)
                        {
                        throw new Exception("One or more records do not exist");
                    }



                    //Compare oldRecordRelations with the new values in recordRelations param and update the ones that are different, delete the ones that are not in the new recordRelations and add the new ones
                    foreach (var recordRelation in recordRelations)
                    {
                        var oldRecordRelation = oldRecordRelations.FirstOrDefault(x => x.EntityRelationId == recordRelation.EntityRelationId);
                        
                        if (oldRecordRelation != null)
                        {
                            //Update the old value with the new one
                            oldRecordRelation.RightRecordId = recordRelation.RecordIds.FirstOrDefault();
                        }
                        else
                        {
                            //Add the new value
                            RecordRelation newRecordRelation = new()
                            {
                                EntityRelationId = recordRelation.EntityRelationId,
                                LeftRecordId = id,
                                RightRecordId = recordRelation.RecordIds.FirstOrDefault(),
                                CreateUserId = userId,
                                CreateDateTime = DateTime.UtcNow
                            };
                            await _context.RecordRelations.AddAsync(newRecordRelation);
                        }
                    }
                    //Delete the old values that are not in the new recordRelations
                    foreach (var oldRecordRelation in oldRecordRelations)
                    {
                        var newRecordRelation = recordRelations.FirstOrDefault(x => x.EntityRelationId == oldRecordRelation.EntityRelationId);
                        if (newRecordRelation == null)
                        {
                            //Delete the old value
                            _context.RecordRelations.Remove(oldRecordRelation);
                        }
                    }
                }
                
            }

            


            //Update the record with the new values
            record.Name = name;
            record.OwnerUserId = ownerUserId;
            record.LastUpdateUserId = userId;
            record.LastUpdate = DateTime.UtcNow;
            record.IterationId += 1;
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Record> UpdateOwnerAsync(int id, string newOwnerId, string updatingUserId)
        {
            var record = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.OwnerUserId = newOwnerId;
            record.LastUpdateUserId = updatingUserId;
            record.LastUpdate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.IsDeleted = true;

            await _context.SaveChangesAsync();
            return true;
        }

        // Methods for managing RecordFieldValues

        public async Task<RecordFieldValue> AddFieldValueAsync(int recordId, int entityFieldId, string fieldValue)
        {
            // Check if record exists
            var record = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == recordId);
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            // Check if entity field exists
            var entityField = await _context.EntityFields
                .FirstOrDefaultAsync(x => x.Id == entityFieldId);
            if (entityField == null)
            {
                throw new Exception("Entity Field not found");
            }

            // Check if field value already exists
            var existingFieldValue = await _context.RecordFieldValues
                .FirstOrDefaultAsync(x => x.RecordId == recordId && x.EntityFieldId == entityFieldId);

            if (existingFieldValue != null)
            {
                // Update existing field value
                existingFieldValue.FieldValue = fieldValue;
                await _context.SaveChangesAsync();
                return existingFieldValue;
            }
            else
            {
                // Create new field value
                RecordFieldValue newFieldValue = new()
                {
                    RecordId = recordId,
                    EntityFieldId = entityFieldId,
                    FieldValue = fieldValue
                };

                await _context.RecordFieldValues.AddAsync(newFieldValue);
                await _context.SaveChangesAsync();
                return newFieldValue;
            }
        }

        public async Task<bool> DeleteFieldValueAsync(int recordId, int entityFieldId)
        {
            var fieldValue = await _context.RecordFieldValues
                .FirstOrDefaultAsync(x => x.RecordId == recordId && x.EntityFieldId == entityFieldId);
            if (fieldValue == null)
            {
                throw new Exception("Record Field Value not found");
            }

            _context.RecordFieldValues.Remove(fieldValue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RecordFieldValue>> GetFieldValuesAsync(int recordId)
        {
            var fieldValues = await _context.RecordFieldValues
                .Include(rfv => rfv.EntityField)
                .Where(x => x.RecordId == recordId)
                .ToListAsync();
            return fieldValues;
        }

        // Methods for managing RecordRelations

        public async Task<RecordRelation> AddRelationAsync(int entityRelationId, int LeftRecordId, int RightRecordId, string userId)
        {
            // Check if entity relation exists
            var entityRelation = await _context.EntityRelations
                .FirstOrDefaultAsync(x => x.Id == entityRelationId);
            if (entityRelation == null)
            {
                throw new Exception("Entity Relation not found");
            }

            // Check if source record exists
            var sourceRecord = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == LeftRecordId);
            if (sourceRecord == null)
            {
                throw new Exception("Source Record not found");
            }

            // Check if destination record exists
            var destinationRecord = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == RightRecordId);
            if (destinationRecord == null)
            {
                throw new Exception("Destination Record not found");
            }

            // Check if relation already exists
            var existingRelation = await _context.RecordRelations
                .FirstOrDefaultAsync(x => x.EntityRelationId == entityRelationId &&
                                         x.LeftRecordId == LeftRecordId &&
                                         x.RightRecordId == RightRecordId);
            if (existingRelation != null)
            {
                throw new Exception("Record Relation already exists");
            }

            RecordRelation newRelation = new()
            {
                EntityRelationId = entityRelationId,
                LeftRecordId = LeftRecordId,
                RightRecordId = RightRecordId,
                CreateUserId = userId,
                CreateDateTime = DateTime.UtcNow
            };

            await _context.RecordRelations.AddAsync(newRelation);
            await _context.SaveChangesAsync();
            return newRelation;
        }

        public async Task<bool> DeleteRelationAsync(int id)
        {
            var relation = await _context.RecordRelations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (relation == null)
            {
                throw new Exception("Record Relation not found");
            }

            _context.RecordRelations.Remove(relation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RecordRelation>> GetSourceRelationsAsync(int recordId)
        {
            var relations = await _context.RecordRelations
                .Include(x => x.EntityRelation)
                .Include(x => x.RightRecord)
                    .ThenInclude(x => x.EntityType)
                .Where(x => x.LeftRecordId == recordId)
                .ToListAsync();
            return relations;
        }

        public async Task<List<RecordRelation>> GetDestinationRelationsAsync(int recordId)
        {
            var relations = await _context.RecordRelations
                .Include(x => x.EntityRelation)
                .Include(x => x.LeftRecord)
                    .ThenInclude(x => x.EntityType)
                .Where(x => x.RightRecordId == recordId)
                .ToListAsync();
            return relations;
        }







        private bool IsPrimaryRecordField(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName)) return false;

            return typeof(Record)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Any(prop => prop.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }



    }
}
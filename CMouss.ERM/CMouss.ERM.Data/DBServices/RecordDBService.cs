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

        public async Task<List<Record>> GetByEntityTypeIdAsync(int entityTypeId, string searchFor, string orderBy, int page, int pageSize, bool reverseOrder)
        {
            string sortOrder = reverseOrder ? "descending" : "ascending";
            bool sortIsByFieldValues = !IsPrimaryRecordField(orderBy);
            string sortByEntityFieldName = sortIsByFieldValues ? orderBy : null;

            // Filter base query
            var query = _context.Records
                .Include(x => x.EntityType)
                .Include(x => x.RecordFieldValues)
                    .ThenInclude(rfv => rfv.EntityField)
                .Where(x => x.EntityTypeId == entityTypeId &&
                            x.RecordFieldValues.Any(rfv => rfv.FieldValue.Contains(searchFor)));

            // Project and sort
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

            // Materialize the result
            var result = await ordered
                .Select(x => x.Record)
                .ToListAsync();

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

        public async Task<Record> AddAsync(int entityTypeId, string userId)
        {
            Record record = new()
            {
                EntityTypeId = entityTypeId,
                CreateUserId = userId,
                CreateDateTime = DateTime.UtcNow,
                LastUpdateUserId = userId,
                LastUpdate = DateTime.UtcNow,
                OwnerUserId = userId
            };

            await _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Record> UpdateAsync(int id, string userId)
        {
            var record = await _context.Records
                .FirstOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.LastUpdateUserId = userId;
            record.LastUpdate = DateTime.UtcNow;

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
                .FirstOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            // Delete related RecordFieldValues
            var recordFieldValues = await _context.RecordFieldValues
                .Where(x => x.RecordId == id)
                .ToListAsync();
            if (recordFieldValues.Any())
            {
                _context.RecordFieldValues.RemoveRange(recordFieldValues);
            }

            // Delete related RecordRelations
            var recordRelations = await _context.RecordRelations
                .Where(x => x.LeftRecordId == id || x.RightRecordId == id)
                .ToListAsync();
            if (recordRelations.Any())
            {
                _context.RecordRelations.RemoveRange(recordRelations);
            }

            _context.Records.Remove(record);
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
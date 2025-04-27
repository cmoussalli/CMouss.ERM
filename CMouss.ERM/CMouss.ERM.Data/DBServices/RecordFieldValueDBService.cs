using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Data.DBServices
{
    public class RecordFieldValueDBService
    {
        private readonly ERMDBContext _context;

        public RecordFieldValueDBService(ERMDBContext context)
        {
            _context = context;
        }

        public async Task<List<RecordFieldValue>> GetAllAsync()
        {
            var recordFieldValues = await _context.RecordFieldValues
                .ToListAsync();
            return recordFieldValues;
        }

        public async Task<RecordFieldValue> GetByIdAsync(int id)
        {
            RecordFieldValue response = new();
            var recordFieldValue = await _context.RecordFieldValues
                .FirstOrDefaultAsync(x => x.Id == id);
            if (recordFieldValue != null)
            {
                response = recordFieldValue;
            }
            return response;
        }

        public async Task<List<RecordFieldValue>> GetByRecordIdAsync(int recordId)
        {
            var recordFieldValues = await _context.RecordFieldValues
                .Where(x => x.RecordId == recordId)
                .ToListAsync();
            return recordFieldValues;
        }

        public async Task<List<RecordFieldValue>> GetByEntityFieldIdAsync(int entityFieldId)
        {
            var recordFieldValues = await _context.RecordFieldValues
                .Where(x => x.EntityFieldId == entityFieldId)
                .ToListAsync();
            return recordFieldValues;
        }

        public async Task<RecordFieldValue> GetByRecordAndFieldAsync(int recordId, int entityFieldId)
        {
            RecordFieldValue response = new();
            var recordFieldValue = await _context.RecordFieldValues
                .FirstOrDefaultAsync(x => x.RecordId == recordId && x.EntityFieldId == entityFieldId);
            if (recordFieldValue != null)
            {
                response = recordFieldValue;
            }
            return response;
        }

        public async Task<RecordFieldValue> AddAsync(int recordId, int entityFieldId, string fieldValue)
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
                throw new Exception("Record Field Value already exists");
            }

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

        public async Task<RecordFieldValue> UpdateAsync(int id, string fieldValue)
        {
            var recordFieldValue = await _context.RecordFieldValues
                .FirstOrDefaultAsync(x => x.Id == id);
            if (recordFieldValue == null)
            {
                throw new Exception("Record Field Value not found");
            }
            
            recordFieldValue.FieldValue = fieldValue;
            await _context.SaveChangesAsync();
            return recordFieldValue;
        }

        public async Task<RecordFieldValue> UpdateByRecordAndFieldAsync(int recordId, int entityFieldId, string fieldValue)
        {
            var recordFieldValue = await _context.RecordFieldValues
                .FirstOrDefaultAsync(x => x.RecordId == recordId && x.EntityFieldId == entityFieldId);
            if (recordFieldValue == null)
            {
                throw new Exception("Record Field Value not found");
            }
            
            recordFieldValue.FieldValue = fieldValue;
            await _context.SaveChangesAsync();
            return recordFieldValue;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recordFieldValue = await _context.RecordFieldValues
                .FirstOrDefaultAsync(x => x.Id == id);
            if (recordFieldValue == null)
            {
                throw new Exception("Record Field Value not found");
            }
            
            _context.RecordFieldValues.Remove(recordFieldValue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByRecordAndFieldAsync(int recordId, int entityFieldId)
        {
            var recordFieldValue = await _context.RecordFieldValues
                .FirstOrDefaultAsync(x => x.RecordId == recordId && x.EntityFieldId == entityFieldId);
            if (recordFieldValue == null)
            {
                throw new Exception("Record Field Value not found");
            }
            
            _context.RecordFieldValues.Remove(recordFieldValue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByRecordIdAsync(int recordId)
        {
            var recordFieldValues = await _context.RecordFieldValues
                .Where(x => x.RecordId == recordId)
                .ToListAsync();
            
            if (!recordFieldValues.Any())
            {
                return false;
            }
            
            _context.RecordFieldValues.RemoveRange(recordFieldValues);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByEntityFieldIdAsync(int entityFieldId)
        {
            var recordFieldValues = await _context.RecordFieldValues
                .Where(x => x.EntityFieldId == entityFieldId)
                .ToListAsync();
            
            if (!recordFieldValues.Any())
            {
                return false;
            }
            
            _context.RecordFieldValues.RemoveRange(recordFieldValues);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

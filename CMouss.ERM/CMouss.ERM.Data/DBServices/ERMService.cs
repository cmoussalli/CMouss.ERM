using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CMouss.ERM.Data.DBModels;
using CMouss.ERM.Data.ViewModels;

namespace CMouss.ERM.Data.DBServices
{
    public class ERMService
    {

        private readonly ERMDBContext _context;

        public ERMService(ERMDBContext context)
        {
            _context = context;
        }



        public async Task<List<Record>> Search(string query)
        {
            List<Record> response = new();
            var fieldTypes = await _context.DataTypes.ToListAsync();
            return response;
        }


        public async Task<int> AddRecord(int entityTypeId,List<RecordValue> recordValues ,string userId)
        {
            int result = 0;
            //Validate if record type is Valid
            var entityType = await _context.EntityTypes.FirstOrDefaultAsync(x => x.Id == entityTypeId);
            if (entityType == null)
            {
                throw new Exception("Entity Type not found");
            }

            //Validate if record values fullfill the required fields
            var requiredFields = await _context.EntityFields
                .Where(x => x.EntityTypeId == entityTypeId && x.IsRequired)
                .ToListAsync();
            //--Validate if all required fields are present
            foreach (var field in requiredFields)
            {
                var recordValue = recordValues.FirstOrDefault(x => x.FieldID == field.Id);
                if (recordValue == null || string.IsNullOrEmpty(recordValue.Value))
                {
                    throw new Exception($"Field {field.Name} is required");
                }
            }

            //Create the record
            Record entityRecord = new()
            {
                EntityTypeId = entityTypeId,
                CreateUserId = userId,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = userId,
                LastUpdate = DateTime.Now,
                OwnerUserId = userId
            };
            _context.Records.Add(entityRecord);
            _context.SaveChanges();

            //Create the field values
            foreach (var recordValue in recordValues)
            {
                var field = await _context.EntityFields.FirstOrDefaultAsync(x => x.Id == recordValue.FieldID);
                if (field == null)
                {
                    throw new Exception($"Field {recordValue.FieldID} not found");
                }
                RecordFieldValue recordFieldValue = new()
                {
                    RecordId = entityRecord.Id,
                    EntityFieldId = field.Id,
                    FieldValue = recordValue.Value,
                };
                _context.RecordFieldValues.Add(recordFieldValue);
            }



            return result;
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Data.DBServices
{
    public class EntityFieldDBService
    {
        private readonly ERMDBContext _context;

        public EntityFieldDBService(ERMDBContext context)
        {
            _context = context;
        }


        public async Task<List<EntityField>> GetAllAsync()
        {
            List<EntityField> response = new();
            var entityFields = await _context.EntityFields.ToListAsync();
            return response;
        }


        public async Task<EntityField> GetByIdAsync(int id)
        {
            EntityField response = new();
            var entityField = await _context.EntityFields
                .Include(x => x.DataType)
                .Include(x => x.EntityType)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityField != null)
            {
                response = entityField;
            }
            return response;
        }

        public async Task<List<EntityField>> GetByEntityTypeIdAsync(int entityTypeId)
        {
            List<EntityField> response = new();
            var entityFields = await _context.EntityFields
                .Include(x => x.DataType)
                .Include(x => x.EntityType)
                .Where(x => x.EntityTypeId == entityTypeId)
                .ToListAsync();
            if (entityFields != null && entityFields.Count > 0)
            {
                response.AddRange(entityFields);
            }
            return response;
        }

        public async Task<List<EntityField>> GetByDataTypeIdAsync(int dataTypeId)
        {
            List<EntityField> response = new();
            var entityFields = await _context.EntityFields
                .Include(x => x.DataType)
                .Include(x => x.EntityType)
                .Where(x => x.DataTypeId == dataTypeId)
                .ToListAsync();
            if (entityFields != null && entityFields.Count > 0)
            {
                response.AddRange(entityFields);
            }
            return response;
        }


        public async Task<EntityField> AddAsync(string name, int fieldTypeId, int entityTypeId, bool isRequired)
        {
            EntityField response = new();
            var entityFieldExist = await _context.EntityFields
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.EntityTypeId == entityTypeId);
            if (entityFieldExist != null)
            {
                throw new Exception("Entity Field already exist");
            }
            response.Name = name;
            response.DataTypeId = fieldTypeId;
            response.EntityTypeId = entityTypeId;
            response.IsRequired = isRequired;
            await _context.EntityFields.AddAsync(response);
            await _context.SaveChangesAsync();
            return response;
        }


        public async Task<EntityField> UpdateAsync(int id, string name, int fieldTypeId, int entityTypeId, bool isRequired)
        {
            EntityField response = new();
            var entityField = await _context.EntityFields
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityField == null)
            {
                throw new Exception("Entity Field not found");
            }
            entityField.Name = name;
            entityField.DataTypeId = fieldTypeId;
            entityField.EntityTypeId = entityTypeId;
            entityField.IsRequired = isRequired;
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityField = await _context.EntityFields
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityField == null)
            {
                throw new Exception("Entity Field not found");
            }
            _context.EntityFields.Remove(entityField);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByEntityTypeIdAsync(int entityTypeId)
        {
            var entityFields = await _context.EntityFields
                .Where(x => x.EntityTypeId == entityTypeId)
                .ToListAsync();
            if (entityFields == null || entityFields.Count == 0)
            {
                throw new Exception("Entity Fields not found");
            }
            _context.EntityFields.RemoveRange(entityFields);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByDataTypeIdAsync(int dataTypeId)
        {
            var entityFields = await _context.EntityFields
                .Where(x => x.DataTypeId == dataTypeId)
                .ToListAsync();
            if (entityFields == null || entityFields.Count == 0)
            {
                throw new Exception("Entity Fields not found");
            }
            _context.EntityFields.RemoveRange(entityFields);
            await _context.SaveChangesAsync();
            return true;

        }






    }
}

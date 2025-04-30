using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Data.DBServices
{
    public class EntityTypeDBService
    {
        private readonly ERMDBContext _context;

        public EntityTypeDBService(ERMDBContext context)
        {
            _context = context;
        }


        public async Task<List<EntityType>> GetAllAsync()
        {
            List<EntityType> response = new();
            var entityTypes = await _context.EntityTypes.Where( x => x.IsDeleted == false).ToListAsync();
            return response;
        }

        public async Task<EntityType> GetByIdAsync(int id)
        {
            EntityType response = new();
            var entityType = await _context.EntityTypes
                .Include(x => x.EntityFields)
                .Include(x => x.DefaultEntityListView)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityType != null)
            {
                response = entityType;
            }
            return response;
        }

        public async Task<EntityType> AddAsync(string name, string pluralName)
        {
            EntityType response = new();
            var entityTypeExist = await _context.EntityTypes
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() || x.PluralName.ToLower() == pluralName.ToLower());
            if (entityTypeExist != null)
            {
                throw new Exception("Entity Type already exist");
            }

            response.Name = name;
            response.PluralName = pluralName;
            response.IsDeleted = false;

            await _context.EntityTypes.AddAsync(response);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<EntityType> UpdateAsync(int id, string name, string pluralName)
        {
            EntityType response = new();
            var entityType = await _context.EntityTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityType == null)
            {
                throw new Exception("Entity Type not found");
            }
            entityType.Name = name;
            entityType.PluralName = pluralName;
            await _context.SaveChangesAsync();
            return entityType;
        }

        public async Task DeleteAsync(int id)
        {
            var entityType = await _context.EntityTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityType == null)
            {
                throw new Exception("Entity Type not found");
            }
            entityType.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task RestoreAsync(int id)
        {
            var entityType = await _context.EntityTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityType == null)
            {
                throw new Exception("Entity Type not found");
            }
            entityType.IsDeleted = false;
            await _context.SaveChangesAsync();
        }

        public async Task<List<EntityType>> GetAllDeletedAsync()
        {
            List<EntityType> response = new();
            var entityTypes = await _context.EntityTypes
                .Where(x => x.IsDeleted)
                .ToListAsync();
            return entityTypes;
        }






    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Data.DBServices
{
    public class EntityRelationDBService
    {
        private readonly ERMDBContext _context;

        public EntityRelationDBService(ERMDBContext context)
        {
            _context = context;
        }

        public async Task<List<EntityRelation>> GetAllAsync()
        {
            var entityRelations = await _context.EntityRelations
                .Include(x => x.EntityType_Left)
                .Include(x => x.EntityType_Right)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            return entityRelations;
        }

        public async Task<EntityRelation> GetByIdAsync(int id)
        {
            EntityRelation response = new();
            var entityRelation = await _context.EntityRelations
                .Include(x => x.EntityType_Left)
                .Include(x => x.EntityType_Right)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityRelation != null)
            {
                response = entityRelation;
            }
            return response;
        }

        public async Task<List<EntityRelation>> GetByEntityTypeId_LeftAsync(int EntityTypeId_Left)
        {
            var entityRelations = await _context.EntityRelations
                .Include(x => x.EntityType_Left)
                .Include(x => x.EntityType_Right)
                .Where(x => x.EntityTypeId_Left == EntityTypeId_Left && !x.IsDeleted)
                .ToListAsync();
            return entityRelations;
        }

        public async Task<List<EntityRelation>> GetByEntityTypeId_RightAsync(int EntityTypeId_Right)
        {
            var entityRelations = await _context.EntityRelations
                .Include(x => x.EntityType_Left)
                .Include(x => x.EntityType_Right)
                .Where(x => x.EntityTypeId_Right == EntityTypeId_Right && !x.IsDeleted)
                .ToListAsync();
            return entityRelations;
        }

      

        public async Task<bool> DeleteAsync(int id)
        {
            var entityRelation = await _context.EntityRelations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityRelation == null)
            {
                throw new Exception("Entity Relation not found");
            }
            entityRelation.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreAsync(int id)
        {
            var entityRelation = await _context.EntityRelations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityRelation == null)
            {
                throw new Exception("Entity Relation not found");
            }
            entityRelation.IsDeleted = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<EntityRelation>> GetAllDeletedAsync()
        {
            var entityRelations = await _context.EntityRelations
                .Include(x => x.EntityType_Left)
                .Include(x => x.EntityType_Right)
                .Where(x => x.IsDeleted)
                .ToListAsync();
            return entityRelations;
        }
    }
}

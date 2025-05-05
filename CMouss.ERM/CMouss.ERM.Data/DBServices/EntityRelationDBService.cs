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

        public async Task<EntityRelation> AddAsync( string relationName, int entityTypeId_Left, int entityTypeId_Right, string title_Left, string title_Right, bool isList_Left, bool isList_Right, bool isRequired_Left, bool isRequired_Right)
        {
            var entityRelation = new EntityRelation
            {
                Name = relationName,

                EntityTypeId_Left = entityTypeId_Left,
                EntityTypeId_Right = entityTypeId_Right,

                Title_Left = title_Left,
                Title_Right = title_Right,
                IsRequired_Left = isRequired_Left,
                IsRequired_Right = isRequired_Right,
                IsList_Left = isList_Left,
                IsList_Right = isList_Right,
                
                IsDeleted = false,

            };
            await _context.EntityRelations.AddAsync(entityRelation);
            await _context.SaveChangesAsync();
            return entityRelation;
        }

        public async Task<EntityRelation> UpdateAsync(int id, string relationName, int entityTypeId_Left, int entityTypeId_Right, string title_Left, string title_Right, bool isList_Left, bool isList_Right, bool isRequired_Left, bool isRequired_Right)
        {
            var entityRelation = await _context.EntityRelations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityRelation == null)
            {
                throw new Exception("Entity Relation not found");
            }
            entityRelation.Name = relationName;
            entityRelation.EntityTypeId_Left = entityTypeId_Left;
            entityRelation.EntityTypeId_Right = entityTypeId_Right;
            entityRelation.Title_Left = title_Left;
            entityRelation.Title_Right = title_Right;
            entityRelation.IsRequired_Left = isRequired_Left;
            entityRelation.IsRequired_Right = isRequired_Right;
            entityRelation.IsList_Left = isList_Left;
            entityRelation.IsList_Right = isList_Right;
            await _context.SaveChangesAsync();
            return entityRelation;
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

        public async Task<bool> DeleteByEntityTypeId(int entityTypeId)
        {
            var entityRelations = await _context.EntityRelations
                .Where(x => x.EntityTypeId_Left == entityTypeId || x.EntityTypeId_Right == entityTypeId)
                .ToListAsync();
            if (entityRelations.Count == 0)
            {
                throw new Exception("Entity Relation not found");
            }
            foreach (var entityRelation in entityRelations)
            {
                entityRelation.IsDeleted = true;
            }
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

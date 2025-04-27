using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Data.DBServices
{
    public  class EntityListViewDBService
    {
        private readonly ERMDBContext _context;

        public EntityListViewDBService(ERMDBContext context)
        {
            _context = context;
        }


        public async Task<List<EntityListView>> GetAllAsync()
        {
            List<EntityListView> response = new();
            var entityViews = await _context.EntityListViews.ToListAsync();
            return response;
        }

        public async Task<List<EntityListView>> GetListByEntityTypeIdAsync(int entityTypeId)
        {
            List<EntityListView> response = new();
            var entityViews = await _context.EntityListViews.Where(x => x.EntityTypeId == entityTypeId).ToListAsync();
            return response;
        }

        public async Task<EntityListView> GetByIdAsync(int id)
        {
            EntityListView response = new();
            var entityView = await _context.EntityListViews
                .Include(x => x.EntityType)
                .Include(x => x.EntityListViewFields)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityView != null)
            {
                response = entityView;
            }
            return response;
        }

        public async Task<EntityListView> AddAsync(string name, int entityTypeId)
        {
            EntityListView response = new();
            var entityViewExist = await _context.EntityListViews
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() );
            if (entityViewExist != null)
            {
                throw new Exception("Entity View already exist");
            }
            response.Name = name;
            response.IsDeleted = false;
            response.IsPublished = false;
            response.EntityTypeId = entityTypeId;
            await _context.EntityListViews.AddAsync(response);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<EntityListView> UpdateAsync(int id, string name, int entityTypeId)
        {
            EntityListView response = new();
            var entityView = await _context.EntityListViews
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityView == null)
            {
                throw new Exception("Entity View not found");
            }
            entityView.Name = name;
            entityView.EntityTypeId = entityTypeId;
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityView = await _context.EntityListViews
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityView == null)
            {
                throw new Exception("Entity View not found");
            }
            entityView.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PublishAsync(int id)
        {
            var entityView = await _context.EntityListViews
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityView == null)
            {
                throw new Exception("Entity View not found");
            }
            entityView.IsPublished = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnPublishAsync(int id)
        {
            var entityView = await _context.EntityListViews
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityView == null)
            {
                throw new Exception("Entity View not found");
            }
            entityView.IsPublished = false;
            await _context.SaveChangesAsync();
            return true;
        }

      

        





    }
}

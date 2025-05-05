using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Data.DBServices
{
    public class EntityListViewFieldDBService
    {
        private readonly ERMDBContext _context;

        public EntityListViewFieldDBService(ERMDBContext context)
        {
            _context = context;
        }

        public async Task<List<EntityListViewField>> GetAllAsync()
        {
            List<EntityListViewField> response = new();
            var entityViewFields = await _context.EntityListViewFields.ToListAsync();
            return response;
        }

        public async Task<List<EntityListView>> GetListByEntityTypeIdAsync(int entityTypeId)
        {
            List<EntityListView> response = new();
            var entityViewFields = await _context.EntityListViewFields
                .Include(x => x.EntityListView)
                .Include(x => x.EntityField)
                .Where(x => x.EntityField.EntityTypeId == entityTypeId)
                .ToListAsync();
            if (entityViewFields != null && entityViewFields.Count > 0)
            {
                response.AddRange(entityViewFields.Select(x => x.EntityListView));
            }
            return response;
        }

        public async Task<EntityListViewField> GetByIdAsync(int id)
        {
            EntityListViewField response = new();
            var entityViewField = await _context.EntityListViewFields
                .Include(x => x.EntityListView)
                .Include(x => x.EntityField)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityViewField != null)
            {
                response = entityViewField;
            }
            return response;
        }

        public async Task<EntityListViewField> AddAsync(int entityViewId, int entityFieldId)
        {
            EntityListViewField response = new();
            var entityViewFieldExist = await _context.EntityListViewFields
                .FirstOrDefaultAsync(x => x.EntityListViewId == entityViewId && x.EntityFieldId == entityFieldId);
            if (entityViewFieldExist != null)
            {
                throw new Exception("Entity View Field already exist");
            }

            response.EntityListViewId = entityViewId;
            response.EntityFieldId = entityFieldId;
            await _context.EntityListViewFields.AddAsync(response);
            await _context.SaveChangesAsync();
            return response;
        }






    }
}

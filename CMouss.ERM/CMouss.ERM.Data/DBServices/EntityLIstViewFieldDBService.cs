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

        public async Task<List<EntityListViewField>> GetListByEntityListViewIdAsync(int entityListViewId)
        {
            List<EntityListViewField> response = new();
            var entityViewFields = await _context.EntityListViewFields
                .Include(x => x.EntityField)
                .Where(x => x.EntityListViewId == entityListViewId)
                .ToListAsync();
            if (entityViewFields != null)
            {
                response.AddRange(entityViewFields);
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

        public async Task<EntityListViewField> UpdateAsync(int id, int entityFieldId)
        {
            EntityListViewField response = new();
            var entityViewField = await _context.EntityListViewFields
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityViewField == null)
            {
                throw new Exception("Entity View Field not found");
            }
            entityViewField.EntityFieldId = entityFieldId;
            _context.EntityListViewFields.Update(entityViewField);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityViewField = await _context.EntityListViewFields
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entityViewField == null)
            {
                throw new Exception("Entity View Field not found");
            }
            _context.EntityListViewFields.Remove(entityViewField);
            await _context.SaveChangesAsync();
            return true;
        }

        //Method: MoveUpAsync(entityListViewFieldId): Load a list of the entityfields that in the same entitylistview of the selected entityListViewField, Validate first if the fieldid is exist and sortid is bigger than 1, update the sortid of the fieldid to sortid - 1, and update the sortid of the fieldid - 1 to sortid + 1
        public async Task<bool> MoveUpAsync(int entityListViewFieldId)
        {
            var entityListViewField = await _context.EntityListViewFields
                .FirstOrDefaultAsync(x => x.Id == entityListViewFieldId);
            if (entityListViewField == null)
            {
                throw new Exception("Entity List View Field not found");
            }
            if (entityListViewField.SortId <= 1)
            {
                throw new Exception("Entity List View Field Sort Id is already at the top");
            }
            var entityListViewFields = await _context.EntityListViewFields
                .Where(x => x.EntityListViewId == entityListViewField.EntityListViewId).OrderBy(o => o.SortId)
                .ToListAsync();
            var entityListViewFieldToMove = entityListViewFields.OrderByDescending(o => o.SortId).FirstOrDefault(x => x.SortId < entityListViewField.SortId);
            if (entityListViewFieldToMove == null)
            {
                throw new Exception("Entity List View Field to move not found");
            }
            entityListViewField.SortId--;
            entityListViewFieldToMove.SortId++;
            _context.EntityListViewFields.Update(entityListViewField);
            _context.EntityListViewFields.Update(entityListViewFieldToMove);
            await _context.SaveChangesAsync();
            return true;
        }

        //Method: MoveDownAsync(entityListViewFieldId): Load a list of the entityfields that in the same entitylistview of the selected entityListViewField, Validate first if the fieldid is exist and sortid is bigger than 1, update the sortid of the fieldid to sortid - 1, and update the sortid of the fieldid - 1 to sortid + 1
        public async Task<bool> MoveDownAsync(int entityListViewFieldId)
        {
            var entityListViewField = await _context.EntityListViewFields
                .FirstOrDefaultAsync(x => x.Id == entityListViewFieldId);
            if (entityListViewField == null)
            {
                throw new Exception("Entity List View Field not found");
            }
            var entityListViewFields = await _context.EntityListViewFields
                .Where(x => x.EntityListViewId == entityListViewField.EntityListViewId).OrderBy(o => o.SortId)
                .ToListAsync();
            var entityListViewFieldToMove = entityListViewFields.OrderBy(o => o.SortId).FirstOrDefault(x => x.SortId > entityListViewField.SortId);
            if (entityListViewFieldToMove == null)
            {
                throw new Exception("Entity List View Field to move not found");
            }
            entityListViewField.SortId++;
            entityListViewFieldToMove.SortId--;
            _context.EntityListViewFields.Update(entityListViewField);
            _context.EntityListViewFields.Update(entityListViewFieldToMove);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}

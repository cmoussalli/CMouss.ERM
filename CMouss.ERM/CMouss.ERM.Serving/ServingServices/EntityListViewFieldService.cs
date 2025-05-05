using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data;

namespace CMouss.ERM.Serving.ServingServices
{
    public class EntityListViewFieldService
    {
        ServingServiceParams _servingServiceParams;

        ERMDBContext db = new();

        DBService dbService = new();

        public EntityListViewFieldService(ServingServiceParams servingServiceParams)
        {
            _servingServiceParams = servingServiceParams;
        }


        public async Task<List<EntityListViewField>> GetAllAsync()
        {
            List<EntityListViewField> result = new();
            List<Data.DBModels.EntityListViewField> elvfs = await dbService.EntityListViewFieldDBService.GetAllAsync();
            if (elvfs.Count > 0)
            {
                result.AddRange(new Adapters.EntityListViewFieldAdapter().Convert(elvfs));
            }
            return result;
        }

        public async Task<EntityListViewField> GetByIdAsync(int id)
        {
            EntityListViewField result = new();
            Data.DBModels.EntityListViewField elvf = await dbService.EntityListViewFieldDBService.GetByIdAsync(id);
            if (elvf is not null)
            {
                result = new Adapters.EntityListViewFieldAdapter().Convert(elvf);
            }
            return result;
        }

        public async Task<List<EntityListViewField>> GetByEntityListViewIdAsync(int entityListViewId)
        {
            List<EntityListViewField> result = new();
            List<Data.DBModels.EntityListViewField> elvfs = await dbService.EntityListViewFieldDBService.GetListByEntityListViewIdAsync(entityListViewId);
            if (elvfs.Count > 0)
            {
                result.AddRange(new Adapters.EntityListViewFieldAdapter().Convert(elvfs));
            }
            return result;
        }

        public async Task<EntityListViewField> AddAsync(int entityListViewId, int entityFieldId)
        {
            EntityListViewField result = new();
            Data.DBModels.EntityListViewField elvf = await dbService.EntityListViewFieldDBService.AddAsync(entityListViewId, entityFieldId);
            if (elvf is not null)
            {
                result = new Adapters.EntityListViewFieldAdapter().Convert(elvf);
            }
            return result;
        }

        public async Task<EntityListViewField> UpdateAsync(int entityTypeViewId, int entityFieldId)
        {
            EntityListViewField result = new();
            Data.DBModels.EntityListViewField elvf = await dbService.EntityListViewFieldDBService.UpdateAsync(entityTypeViewId, entityFieldId);
            if (elvf is not null)
            {
                result = new Adapters.EntityListViewFieldAdapter().Convert(elvf);
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            result = await dbService.EntityListViewFieldDBService.DeleteAsync(id);
            return result;
        }

        public async Task<bool> DeleteByEntityListViewIdAsync(int entityListViewId)
        {
            bool result = false;
            result = await dbService.EntityListViewFieldDBService.DeleteByEntityListViewIdAsync(entityListViewId);
            return result;
        }

        public async Task<bool> MoveUpAsync(int id)
        {
            bool result = false;
            result = await dbService.EntityListViewFieldDBService.MoveUpAsync(id);
            return result;
        }

        public async Task<bool> MoveDownAsync(int id)
        {
            bool result = false;
            result = await dbService.EntityListViewFieldDBService.MoveDownAsync(id);
            return result;
        }



    }
}

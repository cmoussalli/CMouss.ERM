using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data;

namespace CMouss.ERM.Serving.ServingServices
{
    public class EntityListViewService
    {
        ServingServiceParams _servingServiceParams;

        ERMDBContext db = new();

        DBService dbService = new();

        public EntityListViewService(ServingServiceParams servingServiceParams)
        {
            _servingServiceParams = servingServiceParams;
        }

        public async Task<List<EntityListView>> GetAllAsync()
        {
            List<EntityListView> result = new();
            List<Data.DBModels.EntityListView> elvs = await dbService.EntityListViewDBService.GetAllAsync();
            if (elvs.Count > 0)
            {
                result.AddRange(new Adapters.EntityListViewAdapter().Convert(elvs));
            }
            return result;
        }

        public async Task<List<EntityListView>> GetByEntityTypeIdAsync(int entityTypeId)
        {
            List<EntityListView> result = new();
            List<Data.DBModels.EntityListView> elvs = await dbService.EntityListViewDBService.GetListByEntityTypeIdAsync(entityTypeId);
            if (elvs.Count > 0)
            {
                result.AddRange(new Adapters.EntityListViewAdapter().Convert(elvs));
            }
            return result;
        }

        public async Task<EntityListView> GetByIdAsync(int id)
        {
            EntityListView result = new();
            Data.DBModels.EntityListView elv = await dbService.EntityListViewDBService.GetByIdAsync(id);
            if (elv is not null)
            {
                result = new Adapters.EntityListViewAdapter().Convert(elv);
            }
            return result;
        }

        public async Task<EntityListView> AddAsync(string name, int entityTypeId)
        {
            EntityListView result = new();
            Data.DBModels.EntityListView elv = await dbService.EntityListViewDBService.AddAsync(name, entityTypeId);
            if (elv is not null)
            {
                result = new Adapters.EntityListViewAdapter().Convert(elv);
            }
            return result;
        }

        public async Task<EntityListView> UpdateAsync(int id, string name, int entityTypeId)
        {
            EntityListView result = new();
            Data.DBModels.EntityListView elv = await dbService.EntityListViewDBService.UpdateAsync(id, name, entityTypeId);
            if (elv is not null)
            {
                result = new Adapters.EntityListViewAdapter().Convert(elv);
            }
            return result;
        }
            

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            result = await dbService.EntityListViewDBService.DeleteAsync(id);
            return result;
        }

        public async Task<bool> PublishAsync(int id)
        {
            bool result = false;
            result = await dbService.EntityListViewDBService.PublishAsync(id);
            return result;
        }

        public async Task<bool> UnPublishAsync(int id)
        {
            bool result = false;
            result = await dbService.EntityListViewDBService.UnPublishAsync(id);
            return result;
        }








    }
}

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
            List<Data.DBModels.EntityListViewField> elvfs = await dbService.EntityListViewFieldDBService.GetByEntityListViewIdAsync(entityListViewId);
            if (elvfs.Count > 0)
            {
                result.AddRange(new Adapters.EntityListViewFieldAdapter().Convert(elvfs));
            }
            return result;
        }




    }
}

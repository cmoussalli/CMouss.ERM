using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data;

namespace CMouss.ERM.Serving.ServingServices
{
    public class EntityFieldService
    {

        ServingServiceParams _servingServiceParams;

        ERMDBContext db = new();

        DBService dbService = new();

        public EntityFieldService(ServingServiceParams servingServiceParams)
        {
            _servingServiceParams = servingServiceParams;
        }



        public async Task<List<EntityField>> GetAllAsync()
        {
            List<EntityField> result = new();
            List<Data.DBModels.EntityField> efs = await dbService.EntityFieldDBService.GetAllAsync();
            if (efs.Count > 0)
            {
                result.AddRange(new Adapters.EntityFieldAdapter().Convert(efs));
            }
            return result;
        }

        public async Task<EntityField> GetByIdAsync(int id)
        {
            EntityField result = new();
            Data.DBModels.EntityField ef = await dbService.EntityFieldDBService.GetByIdAsync(id);
            if (ef is not null)
            {
                result = new Adapters.EntityFieldAdapter().Convert(ef);
            }
            return result;
        }

        public async Task<List<EntityField>> GetByEntityTypeIdAsync(int entityTypeId)
        {
            List<EntityField> result = new();
            List<Data.DBModels.EntityField> efs = await dbService.EntityFieldDBService.GetByEntityTypeIdAsync(entityTypeId);
            if (efs.Count > 0)
            {
                result.AddRange(new Adapters.EntityFieldAdapter().Convert(efs));
            }
            return result;
        }

        public async Task<List<EntityField>> GetByDataTypeIdAsync(int dataTypeId)
        {
            List<EntityField> result = new();
            List<Data.DBModels.EntityField> efs = await dbService.EntityFieldDBService.GetByDataTypeIdAsync(dataTypeId);
            if (efs.Count > 0)
            {
                result.AddRange(new Adapters.EntityFieldAdapter().Convert(efs));
            }
            return result;
        }

        public async Task<EntityField> AddAsync(string name, int entityTypeId, int dataTypeId, bool isRequired, int id)
        {
            EntityField result = new();
            Data.DBModels.EntityField ef = await dbService.EntityFieldDBService.AddAsync(name, entityTypeId, dataTypeId, isRequired);
            if (ef is not null)
            {
                result = new Adapters.EntityFieldAdapter().Convert(ef);
            }
            return result;
        }

        public async Task<EntityField> UpdateAsync(int id, string name, int entityTypeId, int dataTypeId, bool isRequired)
        {
            EntityField result = new();
            Data.DBModels.EntityField ef = await dbService.EntityFieldDBService.UpdateAsync(id, name, entityTypeId, dataTypeId, isRequired);
            if (ef is not null)
            {
                result = new Adapters.EntityFieldAdapter().Convert(ef);
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = await dbService.EntityFieldDBService.DeleteAsync(id);
            return result;
        }

        public async Task<bool> DeleteByEntityTypeIdAsync(int entityTypeId)
        {
            bool result = await dbService.EntityFieldDBService.DeleteByEntityTypeIdAsync(entityTypeId);
            return result;
        }
        public async Task<bool> DeleteByDataTypeIdAsync(int dataTypeId)
        {
            bool result = await dbService.EntityFieldDBService.DeleteByDataTypeIdAsync(dataTypeId);
            return result;
        }










    }



}

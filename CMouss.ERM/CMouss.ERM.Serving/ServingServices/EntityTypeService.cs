using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data;
using Microsoft.EntityFrameworkCore;
using CMouss.ERM.Serving;

namespace CMouss.ERM.Serving
{
    public class EntityTypeService
    {
        ServingServiceParams _servingServiceParams;

        ERMDBContext db = new();

        DBService dbService = new();

        public EntityTypeService(ServingServiceParams servingServiceParams)
        {
            _servingServiceParams = servingServiceParams;
        }


        public async Task<List<EntityType>> GetAllAsync()
        {
            List<EntityType> result = new();
            List<Data.DBModels.EntityType> ets = await dbService.EntityTypeDBService.GetAllAsync();
            if (ets.Count > 0)
            {
                result.AddRange(new Adapters.EntityTypeAdapter().Convert(ets));
            }
            return result;
        }

        public async Task<EntityType> GetByIdAsync(int id)
        {
            EntityType result = new();
            Data.DBModels.EntityType et = await dbService.EntityTypeDBService.GetByIdAsync(id);
            if (et != null)
            {
                result = new Adapters.EntityTypeAdapter().Convert(et);
            }
            return result;
        }

        public async Task<EntityType> AddAsync(string name, string pluralName)
        {
            EntityType result = new();
            Data.DBModels.EntityType et = await dbService.EntityTypeDBService.AddAsync(name, pluralName);
            if (et != null)
            {
                result = new Adapters.EntityTypeAdapter().Convert(et);
            }
            return result;
        }

        public async Task<EntityType> UpdateAsync(int id, string name, string pluralName)
        {
            EntityType result = new();
            Data.DBModels.EntityType et = await dbService.EntityTypeDBService.UpdateAsync(id, name, pluralName);
            if (et != null)
            {
                result = new Adapters.EntityTypeAdapter().Convert(et);
            }
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await dbService.EntityTypeDBService.DeleteAsync(id);
        }

        public async Task RestoreAsync(int id)
        {
            await dbService.EntityTypeDBService.RestoreAsync(id);
        }

        public async Task<List<EntityType>> GetAllDeletedAsync()
        {
            List<EntityType> result = new();
            List<Data.DBModels.EntityType> ets = await dbService.EntityTypeDBService.GetAllDeletedAsync();
            if (ets.Count > 0)
            {
                result.AddRange(new Adapters.EntityTypeAdapter().Convert(ets));
            }
            return result;


        }

    }
}

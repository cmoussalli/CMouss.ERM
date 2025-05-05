using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data;

namespace CMouss.ERM.Serving.ServingServices
{
    public class EntityRelationService
    {

        ServingServiceParams _servingServiceParams;

        ERMDBContext db = new();

        DBService dbService = new();

        public EntityRelationService(ServingServiceParams servingServiceParams)
        {
            _servingServiceParams = servingServiceParams;
        }

        public async Task<List<EntityRelation>> GetAllAsync()
        {
            List<EntityRelation> result = new();
            List<Data.DBModels.EntityRelation> ers = await dbService.EntityRelationDBService.GetAllAsync();
            if (ers.Count > 0)
            {
                result.AddRange(new Adapters.EntityRelationAdapter().Convert(ers));
            }
            return result;
        }

        public async Task<EntityRelation> getByIdAsync(int id)
        {
            EntityRelation result = new();
            Data.DBModels.EntityRelation er = await dbService.EntityRelationDBService.GetByIdAsync(id);
            if (er is not null)
            {
                result = new Adapters.EntityRelationAdapter().Convert(er);
            }
            return result;
        }

        public async Task<List<EntityRelation>> GetByEntityTypeId_LeftAsync(int entityTypeId)
        {
            List<EntityRelation> result = new();
            List<Data.DBModels.EntityRelation> ers = await dbService.EntityRelationDBService.GetByEntityTypeId_LeftAsync(entityTypeId);
            if (ers.Count > 0)
            {
                result.AddRange(new Adapters.EntityRelationAdapter().Convert(ers));
            }
            return result;
        }

        public async Task<List<EntityRelation>> GetByEntityTypeId_RightAsync(int entityTypeId)
        {
            List<EntityRelation> result = new();
            List<Data.DBModels.EntityRelation> ers = await dbService.EntityRelationDBService.GetByEntityTypeId_RightAsync(entityTypeId);
            if (ers.Count > 0)
            {
                result.AddRange(new Adapters.EntityRelationAdapter().Convert(ers));
            }
            return result;
        }

        public async Task<EntityRelation> AddAsync(string relationName, int entityTypeId_Left, int entityTypeId_Right, string title_Left, string title_Right, bool isList_Left, bool isList_Right, bool isRequired_Left, bool isRequired_Right)
        {
            EntityRelation result = new();
            Data.DBModels.EntityRelation er = await dbService.EntityRelationDBService.AddAsync(relationName, entityTypeId_Left, entityTypeId_Right, title_Left, title_Right, isList_Left, isList_Right, isRequired_Left, isRequired_Right);
            if (er is not null)
            {
                result = new Adapters.EntityRelationAdapter().Convert(er);
            }
            return result;
        }

        public async Task<EntityRelation> UpdateAsync(int id, string relationName, int entityTypeId_Left, int entityTypeId_Right, string title_Left, string title_Right, bool isList_Left, bool isList_Right, bool isRequired_Left, bool isRequired_Right)
        {
            EntityRelation result = new();
            Data.DBModels.EntityRelation er = await dbService.EntityRelationDBService.UpdateAsync(id, relationName, entityTypeId_Left, entityTypeId_Right, title_Left, title_Right, isList_Left, isList_Right, isRequired_Left, isRequired_Right);
            if (er is not null)
            {
                result = new Adapters.EntityRelationAdapter().Convert(er);
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            result = await dbService.EntityRelationDBService.DeleteAsync(id);
            return result;
        }

        public async Task<bool> DeleteByEntityTypeId(int entityTypeId)
        {
            bool result = false;
            result = await dbService.EntityRelationDBService.DeleteByEntityTypeId(entityTypeId);
            return result;
        }


        public async Task<bool> RestoreAsync(int id)
        {
            bool result = false;
            result = await dbService.EntityRelationDBService.RestoreAsync(id);
            return result;
        }

        public async Task<List<EntityRelation>> GetAllDeletedAsync()
        {
            List<EntityRelation> result = new();
            List<Data.DBModels.EntityRelation> ers = await dbService.EntityRelationDBService.GetAllDeletedAsync();
            if (ers.Count > 0)
            {
                result.AddRange(new Adapters.EntityRelationAdapter().Convert(ers));
            }
            return result;
        }



    }



}

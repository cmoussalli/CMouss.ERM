using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class EntityRelationAdapter
    {

        public EntityRelation Convert(Data.DBModels.EntityRelation model)
        {
            EntityRelation result = new()
            {
                Id = model.Id,
                Name = model.Name,
                Title_Left = model.Title_Left,
                EntityTypeId_Left = model.EntityTypeId_Left,
                IsRequired_Left = model.IsRequired_Left,
                IsList_Left = model.IsList_Left,

                Title_Right = model.Title_Right,
                EntityTypeId_Right = model.EntityTypeId_Right,
                IsRequired_Right = model.IsRequired_Right,
                IsList_Right = model.IsList_Right,
                IsDeleted = model.IsDeleted,


                EntityType_Left = model.EntityType_Left is not null ? new EntityTypeAdapter().Convert(model.EntityType_Left) : null,
                EntityType_Right = model.EntityType_Right is not null ? new EntityTypeAdapter().Convert(model.EntityType_Right) : null,

            };




            return result;
        }
        public List<EntityRelation> Convert(List<Data.DBModels.EntityRelation> models)
        {
            List<EntityRelation> result = new();
            foreach (var model in models)
            {
                result.Add(Convert(model));
            }
            return result;
        }



    }
}

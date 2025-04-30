using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;

namespace CMouss.ERM.Serving.Adapters
{
    public class EntityFieldAdapter
    {
        public EntityField Convert(Data.DBModels.EntityField model)
        {
            EntityField result = new EntityField()
            {
                Id = model.Id,
                Name = model.Name,
                EntityTypeId = model.EntityTypeId,
                DataTypeId = model.DataTypeId,
                IsRequired = model.IsRequired,
                DefaultValue = model.DefaultValue
            };

            if (model.EntityType is not null) { result.EntityType = new EntityTypeAdapter().Convert(model.EntityType); }
            if (model.DataType is not null) { result.DataType = new DataTypeAdapter().Convert(model.DataType); }

            return result;
        }


        public List<EntityField> Convert(List<Data.DBModels.EntityField> models)
        {
            List<EntityField> result = new();
            foreach (var model in models)
            {
                result.Add(Convert(model));
            }
            
            return result;
        }


    }
}

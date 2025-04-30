using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class RecordAdapter
    {

        public Record Convert(Data.DBModels.Record model)
        {
            Record result = new()
            {
                Id = model.Id,
                EntityTypeId = model.EntityTypeId,
                Name = model.Name,
                CreateDateTime = model.CreateDateTime,
                LastUpdateUserId = model.LastUpdateUserId,
                LastUpdate = model.LastUpdate,
                OwnerUserId = model.OwnerUserId,

            };

            if (model.EntityType is not null) { result.EntityType = new EntityTypeAdapter().Convert(model.EntityType); }
            if (model.RecordFieldValues is not null) { result.RecordFieldValues = new RecordFieldValueAdapter().Convert(model.RecordFieldValues); }
            if (model.RecordRelations is not null) { result.Relations = new RelationAdapter().Convert(model.RecordRelations); }

            return result;
        }



    }


}

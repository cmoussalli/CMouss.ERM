using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class RecordAdapter
    {

        public Record Convert(Data.DBModels.Record model, bool loadParents = true, bool loadRecordFieldValues = false, bool loadRelations = false)
        {
            Record result = new()
            {
                Id = model.Id,
                EntityTypeId = model.EntityTypeId,
                Name = model.Name,
                CreateUserId = model.CreateUserId,
                CreateDateTime = model.CreateDateTime,
                LastUpdateUserId = model.LastUpdateUserId,
                LastUpdate = model.LastUpdate,
                OwnerUserId = model.OwnerUserId,

            };


            //Load Parents
            if (loadParents)
            {
                if (model.EntityType is not null) { result.EntityType = new EntityTypeAdapter().Convert(model.EntityType); }
            }

            //load Record Field Values
            if (loadRecordFieldValues)
            {
                if (model.RecordFieldValues is not null) { result.RecordFieldValues = new RecordFieldValueAdapter().Convert(model.RecordFieldValues); }
            }


            //Load Relations
            if (loadRelations)
            {
                result.Relations = new RelationAdapter().Convert(model.Relations, true, true);
            }

            return result;
        }

        public List<Record> Convert(List<Data.DBModels.Record> models)
        {
            List<Record> result = new();
            foreach (var model in models)
            {
                result.Add(Convert(model));
            }
            return result;
        }

    }


}

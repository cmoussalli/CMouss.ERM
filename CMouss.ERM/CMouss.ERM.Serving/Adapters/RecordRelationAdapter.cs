using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class RecordRelationAdapter
    {

        public RecordRelation Convert(Data.DBModels.RecordRelation model)
        {
            RecordRelation result = new()
            {
                Id = model.Id,
                EntityRelationId = model.EntityRelationId,
                LeftRecordId = model.LeftRecordId,
                RightRecordId = model.RightRecordId,
                CreateUserId = model.CreateUserId,
                CreateDateTime = model.CreateDateTime,

            };

            if (model.EntityRelation is not null) { result.EntityRelation = new EntityRelationAdapter().Convert(model.EntityRelation); }
            if (model.LeftRecord is not null) { result.LeftRecord = new RecordAdapter().Convert(model.LeftRecord); }
            if (model.RightRecord is not null) { result.RightRecord = new RecordAdapter().Convert(model.RightRecord); }

            return result;
        }



    }
}

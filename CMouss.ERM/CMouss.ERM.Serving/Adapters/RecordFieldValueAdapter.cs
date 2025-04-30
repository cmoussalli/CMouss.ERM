using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class RecordFieldValueAdapter
    {

        public RecordFieldValue Convert(Data.DBModels.RecordFieldValue model)
        {
            RecordFieldValue result = new()
            {
                Id = model.Id,
                RecordId = model.RecordId,
                EntityFieldId = model.EntityFieldId,
                FieldValue = model.FieldValue,
            
            };
            
            if (model.Record is not null) { result.Record = new RecordAdapter().Convert(model.Record); }
            if (model.EntityField is not null) { result.EntityField = new EntityFieldAdapter().Convert(model.EntityField); }

            return result;
        }


        public List<RecordFieldValue> Convert(List<Data.DBModels.RecordFieldValue> models)
        {
            List<RecordFieldValue> result = new();
            foreach (var model in models)
            {
                result.Add(Convert(model));
            }
            return result;
        }

    }
}

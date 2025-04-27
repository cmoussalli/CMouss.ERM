using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;

namespace CMouss.ERM.Serving.Adapters
{
    public class FieldTypeAdapter
    {
        public APIModels.FieldType Convert(FieldType model)
        {
            APIModels.FieldType result = new();
            result.Id = model.Id;
            result.Name = model.Name;
            return result;
        }

        public List<APIModels.FieldType> Convert(List<FieldType> models)
        {
            List<APIModels.FieldType> result = new();
            foreach (var model in models)
            {
                result.Add(Convert(model));
            }
            return result;
        }
    }


}

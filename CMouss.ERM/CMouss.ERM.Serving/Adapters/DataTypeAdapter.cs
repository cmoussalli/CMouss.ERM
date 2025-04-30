using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class DataTypeAdapter
    {
        public DataType Convert(Data.DBModels.DataType model)
        {
            DataType result = new();
            result.Id = model.Id;
            result.Name = model.Name;
            return result;
        }

        public List<DataType> Convert(List<Data.DBModels.DataType> models)
        {
            List<DataType> result = new();
            foreach (var model in models)
            {
                result.Add(Convert(model));
            }
            return result;
        }
    }


}

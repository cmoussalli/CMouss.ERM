using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data;

namespace CMouss.ERM.Serving.ServingServices
{
    public class DataTypeService
    {
        ServingServiceParams _servingServiceParams;

        ERMDBContext db = new();

        DBService dbService = new();

        public DataTypeService(ServingServiceParams servingServiceParams)
        {
            _servingServiceParams = servingServiceParams;
        }

        public async Task<List<DataType>> GetAllAsync()
        {
            List<DataType> result = new();
            List<Data.DBModels.DataType> fts = await dbService.DataTypeDBService.GetAllAsync();
            if (fts.Count > 0)
            {
                result.AddRange(new Adapters.DataTypeAdapter().Convert(fts));
            }
            return result;
        }









    }
}

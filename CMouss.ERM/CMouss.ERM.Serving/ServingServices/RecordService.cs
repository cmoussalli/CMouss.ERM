using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Serving.ServingServices
{
    public class RecordService
    {

        ServingServiceParams _servingServiceParams;

        ERMDBContext db = new();

        DBService dbService = new();

        public RecordService(ServingServiceParams servingServiceParams)
        {
            _servingServiceParams = servingServiceParams;
        }


        public async Task<List<Record>> GetByEntityTypeIdAsync(int entityTypeId)
        {
            return new Adapters.RecordAdapter().Convert(await dbService.RecordDBService.GetByEntityTypeIdAsync(entityTypeId));
        }


        public async Task<List<Record>> GetByEntityTypeIdAsync(int entityTypeId, string searchFor)
        {
            return new Adapters.RecordAdapter().Convert(await dbService.RecordDBService.GetByEntityTypeIdAsync(entityTypeId, searchFor));
        }

         




    }
}

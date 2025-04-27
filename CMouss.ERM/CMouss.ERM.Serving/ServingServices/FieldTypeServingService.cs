//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CMouss.ERM.Data;
//using CMouss.ERM.Data.DBModels;
//using CMouss.ERM.Data.DBServices;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;

//namespace CMouss.ERM.Serving.ServingServices
//{
//    public class FieldTypeServingService
//    {
//        private ERMDBContext db;
//        public FieldTypeServingService(ERMDBContext _db)
//        {
//            _db = db;
//        }


//        public async Task<List<FieldType>> GetList()
//        {
//            List<FieldType> response = new();
//            var fieldTypes = await FieldTypeDBService.GetAll();

//            return fieldTypes;
//        }


//    }






//}

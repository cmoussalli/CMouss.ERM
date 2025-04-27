//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CMouss.ERM.Data;
//using CMouss.ERM.Serving.ServingServices;

//namespace CMouss.ERM.Serving
//{
//    public class ServingHubConfig
//    {
//        public string ConnectionString { get; set; }
    


//    }




//    public class ServingHub
//    {
//        ERMDBContext _db = new ERMDBContext();

//        string connectionString ="";
//        public string ConnectionString { get => connectionString;}


//        public enum ServingHubType
//        {
//            SQLite,
//            SQLServer
//        }




//        public ServingHub(ServingHubConfig config)
//        {
//            FieldTypeServingService = new FieldTypeServingService(_db);
//        }




//        FieldTypeServingService FieldTypeServingService;


//    }


//}

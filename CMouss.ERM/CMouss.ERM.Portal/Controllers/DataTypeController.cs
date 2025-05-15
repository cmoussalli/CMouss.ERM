//using Microsoft.AspNetCore.Mvc;
//using CMouss.ERM.Portal;
//using CMouss.ERM.Data.DBServices;
//using CMouss.ERM.Data;
//using CMouss.ERM.APIModels.ResponseModels;
//using CMouss.ERM.Data.DBModels;
//using Microsoft.EntityFrameworkCore.Internal;
//using CMouss.ERM.Serving;
//using CMouss.ERM.Serving.Adapters;

//namespace CMouss.ERM.Portal.Controllers
//{
//    public class DataTypeController : Controller
//    {
//        ERMDBContext db = new ERMDBContext();


//        //[HttpPost]
//        //[Route(APIRoutes.DataType.GetList)]
//        //public async Task<ActionResult<DataTypeResponseModels.GetAll>> GetList()
//        //{
//        //    DataTypeResponseModels.GetAll result = new();
//        //    DataTypeDBService svc = new DataTypeDBService(db);
//        //    List<DataType> dbResult = await svc.GetAllAsync();
//        //    result.DataTypes = new DataTypeAdapter().Convert(dbResult);

//        //    return Ok(result);
//        //}
//    }
//}

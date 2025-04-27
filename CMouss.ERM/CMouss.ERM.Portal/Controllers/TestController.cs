using Microsoft.AspNetCore.Mvc;
using CMouss.ERM.Portal;
using CMouss.ERM.Data.DBServices;
using CMouss.ERM.Data;
using CMouss.ERM.APIModels.ResponseModels;
using CMouss.ERM.Data.DBModels;
using Microsoft.EntityFrameworkCore.Internal;
using CMouss.ERM.Serving;
using CMouss.ERM.Serving.Adapters;

namespace CMouss.ERM.Portal.Controllers
{
    public class TestController : Controller
    {
        ERMDBContext db = new ERMDBContext();


        [HttpPost]
        [Route(APIRoutes.Test.TestData)]
        public async Task<ActionResult<GenericResponseModel>> TestData()
        {
            GenericResponseModel result = new();
            try
            {
                

                //db.EntityTypes.Add()
            }
            catch(Exception ex)
            {
                
            }

            return Ok(result);
        }
    }
}

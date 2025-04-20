using Microsoft.AspNetCore.Mvc;
using CMouss.ERM.Portal;


namespace CMouss.ERM.Portal.Controllers
{
    public class FieldTypeController : Controller
    {

        [HttpPost]
        [Route(APIRoutes.FieldType.GetList)]
        public IActionResult GetList()
        {
            return Ok();
        }
    }
}

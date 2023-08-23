using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portal.APIs.Controllers
{
    //[Route("api/[controller]/[action]")] --> not recommended to put action
    //[Route("api/[controller]")]   --> standard
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        [Route("~/api/GetData")]
        public string[] GetData()
        {
            string[] Names = { "Ahmed", "Ali", "Sara" };
            return Names;
        }

        [HttpGet]
        [Route("~/api/GetAllData")]
        public string[] GetAllData()
        {
            string[] Names = { "A", "M", "S" };
            return Names;
        }
    }
}

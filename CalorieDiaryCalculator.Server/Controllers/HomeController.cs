using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieDiaryCalculator.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase {

        [Authorize]
        public IActionResult Get() {
            return this.Ok("Works!");
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieDiaryCalculator.Server.Features {
    public class HomeController : ApiController {

        [Authorize]
        public IActionResult Get() {
            return this.Ok("Works!");
        }
    }
}
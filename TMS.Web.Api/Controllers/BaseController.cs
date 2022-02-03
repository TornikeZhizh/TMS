using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TMS.Commons;

namespace TMS.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly AppSettings _appSettings;
        protected string UserName => Utils.GetUserName(Request.Headers["Authorization"].ToString());

        protected bool IsAdmin => Utils.IsAdmin(Request.Headers["Authorization"].ToString());

        public BaseController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

    }
}

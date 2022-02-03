using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMS.Commons;

namespace TMS.Web.Api.Controllers
{
   
    public class AssetsController : BaseController
    {
        public AssetsController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(string uid)
        {
            return Ok(new FileStream(Path.Combine(_appSettings.AttachmentsLocation, uid), FileMode.Open, FileAccess.Read));
            
        }
    }
}

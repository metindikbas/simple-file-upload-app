using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleFileUpload.Application.UploadSettings.Queries.ListUploadSettings;

namespace SimpleFileUpload.Api.Controllers
{
    public class UploadSettingsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<UploadSettingDto>>> GetAllUploadSettings()
        {
            return await Mediator.Send(new ListUploadSettingsQuery());
        }
    }
}
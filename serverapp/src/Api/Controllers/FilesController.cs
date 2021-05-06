using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleFileUpload.Application.Common.Models;
using SimpleFileUpload.Application.SimpleFiles.Commands.UploadFiles;
using SimpleFileUpload.Application.SimpleFiles.Queries.GetFile;
using SimpleFileUpload.Application.SimpleFiles.Queries.ListFiles;

namespace SimpleFileUpload.Api.Controllers
{
    public class FilesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedViewModel<SimpleFileDto>>> GetAllFiles(
            [FromQuery] ListFilesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SimpleFileViewDto>> GetAllFiles(
            [FromRoute] Guid id)
        {
            return await Mediator.Send(new GetFileQuery{Id = id});
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadFiles()
        {
            var command = new UploadFilesCommand();
            if (Request.HasFormContentType)
                foreach (var file in Request.Form.Files)
                {
                    byte[] bytes;
                    var fileName = file.FileName;

                    using (var reader = new BinaryReader(file.OpenReadStream()))
                        bytes = reader.ReadBytes((int) file.OpenReadStream().Length);

                    command.Files.Add(new UploadFileDto(
                        fileName,
                        file.ContentType,
                        bytes
                    ));
                }

            await Mediator.Send(command);
            return StatusCode((int) HttpStatusCode.Created);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkriderAPI.Controllers
{
    public class FileManagerController : BaseAPiController
    {
        [HttpPost("Single"), DisableRequestSizeLimit]
        public async Task<IActionResult> Single(List<IFormFile> files)
        {
            var response = new Result();
            try
            {
                if (!Request.Form.Files.Any())
                {
                    response = new Result
                    {
                        IsSuccessful = true,
                        Message = "No Files Selected!",
                        ReturnedCode = StatusCodes.Status400BadRequest.ToString(),
                        ReturnedObject = null
                    };

                    return BadRequest(response);
                }

                var httpPostedFile = Request.Form.Files[0];

                var fileName = FileHelper.GenerateFileName(httpPostedFile.FileName);
                var folderPath = FileHelper.GetTempFolderPath();

                using (var bits = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
                {
                    await httpPostedFile.CopyToAsync(bits);
                }

                response = new Result
                {
                    IsSuccessful = true,
                    Message = "Operation Successful!",
                    ReturnedCode = StatusCodes.Status200OK.ToString(),
                    ReturnedObject = new { fileName }
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Message = e.Message;
                response.ReturnedCode = StatusCodes.Status500InternalServerError.ToString();

                return BadRequest(response);
            }
        }
    }
}
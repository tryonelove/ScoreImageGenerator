using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoreImageGenerator.Generator.Core;

namespace ScoreImageGenerator.Web.API.Controllers
{
    [ApiController]
    [Route("score")]
    public class ImageGeneratorController: ControllerBase
    {
        private readonly ILogger<ImageGeneratorController> _logger;

        public ImageGeneratorController(ILogger<ImageGeneratorController> logger)
        {
            _logger = logger;
        }        
        
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Get(string username, int mode, int limit, int type)
        {
            _logger.LogInformation($"Username: {username} | Mode: {mode} | Limit: {limit} | Type: {type}");
            ImageHandler handler = new ImageHandler(username, limit, mode, type);
            var image = await handler.GetByteImageAsync();
            return File(image, "image/png");
        }
    }
}
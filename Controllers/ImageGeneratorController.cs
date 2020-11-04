using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoreImageGenerator.Helpers;
using SixLabors.ImageSharp;

namespace ScoreImageGenerator.Controllers
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
        public IActionResult Get(string username, int limit, int type)
        {
            _logger.LogInformation($"Username: {username} | Limit: {limit} | Type: {type}");
            ImageHandler handler = new ImageHandler(username, limit, type);
            handler.GetImage();
            string pwd = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(pwd, "template.png");
            return PhysicalFile(filePath, "image/png");
        }
    }
}
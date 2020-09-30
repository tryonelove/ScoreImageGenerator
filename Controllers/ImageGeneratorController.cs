using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScoreImageGenerator.Controllers
{
    [ApiController]
    [Route("score")]
    public class ImageGeneratorController: ControllerBase
    {
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Get(string username, int limit, int type)
        {
            Console.WriteLine($"Username: {username}");
            Console.WriteLine($"Limit: {limit}");
            Console.WriteLine($"Type: {type}");
            ImageHandler handler = new ImageHandler(username, limit, type);
            handler.GetImage();
            return PhysicalFile("/home/tryonelove/Documents/ScoreImageGenerator/nigger.png", "image/png");
        }
    }
}
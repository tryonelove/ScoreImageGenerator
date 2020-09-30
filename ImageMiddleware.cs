using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ScoreImageGenerator
{
    public class ImageMiddleware
    {
        private readonly RequestDelegate _next;
        public ImageMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var username = context.Request.Query["username"];
            var scoreType = int.Parse(context.Request.Query["type"]);
            var limit = int.Parse(context.Request.Query["limit"]);
            if(scoreType != (int)ScoreType.Best || scoreType != (int)ScoreType.Last)
            {
                context.Response.StatusCode = 404;
            }
            await _next.Invoke(context);
        }
    }
}
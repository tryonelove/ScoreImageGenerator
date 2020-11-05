using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.HttpSys;
using Newtonsoft.Json;
using ScoreImageGenerator.Helpers.API.Responses;

namespace ScoreImageGenerator.Helpers.API
{
    public interface IRequest
    {
        public Task PerformAsync();
    }
}
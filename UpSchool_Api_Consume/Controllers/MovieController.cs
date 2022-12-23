using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UpSchool_Api_Consume.Models;
using Newtonsoft.Json;

namespace UpSchool_Api_Consume.Controllers
{
    public class MovieController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<MovieListViewModel> movies = new List<MovieListViewModel>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
    {
        { "X-RapidAPI-Key", "de4f9922d2msh4b0e087ebed5975p1aba80jsn10aeb0527b81" },
        { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<List<MovieListViewModel>>(body);
                return View(movies);
            }
      
        }
    }
}

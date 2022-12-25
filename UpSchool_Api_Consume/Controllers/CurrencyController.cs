﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UpSchool_Api_Consume.Models;
using Newtonsoft.Json;

namespace UpSchool_Api_Consume.Controllers
{
    public class CurrencyController : Controller
    {
        public async Task<IActionResult> Index()
        {
   

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=AED&locale=en-gb"),
                Headers =
    {
        { "X-RapidAPI-Key", "de4f9922d2msh4b0e087ebed5975p1aba80jsn10aeb0527b81" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var currencies = JsonConvert.DeserializeObject<CurrencyListViewModel>(body);
                return View(currencies.exchange_rates);

            }
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConsumodeApi.Data;
using ConsumodeApi.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace ConsumodeApi.Controllers
{
 
    public class GruposController : Controller
    {
        private readonly HttpClient _httpClient;

        public GruposController(IHttpClientFactory httpClientFactory)
        { 
           _httpClient = httpClientFactory.CreateClient();
           _httpClient.BaseAddress = new Uri("http://localhost:44306/api");
        
        }


        // GET: Grupos
   
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Grupos");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var grupos = JsonConvert.DeserializeObject<IEnumerable<Grupos>>(content);
                return View("Index",grupos);
            }
            return View(new List<Grupos>());
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Grupos grupos)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(grupos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Grupos", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el lote");
                }
            }
            return View(grupos);
        }





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConsumodeApi.Data;
using ConsumodeApi.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace ConsumodeApi.Controllers
{
    public class LotesController : Controller
    {
        private readonly HttpClient _httpClient;

        public LotesController(IHttpClientFactory httpClientFactory)
        {

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44306/api");

        }

        // GET: Lotes
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Lotes");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var lotes = JsonConvert.DeserializeObject<IEnumerable<Lotes>>(content);
                return View("Index", lotes);
            }
            return View(new List<Lotes>());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lotes lotes)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(lotes);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Lotes", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el lote");
                }
            }
            return View(lotes);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var response = await _httpClient.GetAsync($"/api/Lotes?id={Id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var lote = JsonConvert.DeserializeObject<Lotes>(content);

                return View(lote);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Lotes lotes)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(lotes);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Lotes/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al Actualizar finca");
                }
            }
            return View(lotes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Lotes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var lotes = JsonConvert.DeserializeObject<Lotes>(content);

                return View(lotes);

            }
            else
            {
                return RedirectToAction("Details");
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Lotes/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar Lotes";
                return RedirectToAction("Index");
            }
        }



    }
}

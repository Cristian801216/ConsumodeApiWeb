using ConsumodeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ConsumodeApi.Controllers
{
    public class FincasController : Controller
    {
        private readonly HttpClient _httpClient;


        public FincasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44306/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Fincas/lista");

            if (response.IsSuccessStatusCode)
            {
               var content = await response.Content.ReadAsStringAsync();
                var fincas = JsonConvert.DeserializeObject<IEnumerable<Fincas>>(content);
                return View("Index", fincas);
            }
            return View(new List<Fincas>());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Fincas fincas)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(fincas);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Fincas/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear Finca");
                }
            }
            return View(fincas);
        }

        public async Task<IActionResult> Edit (int Id)
        {
            var response = await _httpClient.GetAsync($"/api/Fincas/Modificar?id={Id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var finca = JsonConvert.DeserializeObject<Fincas>(content);

                return View(finca);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Fincas fincas)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(fincas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Fincas/Modificar?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al Actualizar finca");
                }
            }
            return View(fincas);
        }


        public async Task<IActionResult> Details (int id)
        {
            var response = await _httpClient.GetAsync($"/api/Fincas/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var finca = JsonConvert.DeserializeObject<Fincas>(content);

                return View(finca);

            }
            else {
                return RedirectToAction("Details");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Fincas/eliminar?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar Finca";
                return RedirectToAction("Index");
            }
        }


    }
}

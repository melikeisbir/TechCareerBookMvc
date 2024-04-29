using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TechCareerBookMvc.Models;

namespace TechCareerBookMvc.Controllers
{
    public class KitapController : Controller
    {   //rest api
        public async Task<IActionResult> Index()
        {
            List<Kitap> kitapList = new List<Kitap>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44333/api/Kitap"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    kitapList = JsonConvert.DeserializeObject<List<Kitap>>(apiResponse);
                }
            }
            return View(kitapList);
        }
        public async Task<IActionResult> EditKitap(int id)
        {
            Kitap duzenlenecekKitap = new Kitap();
            using (var httpClient = new HttpClient())
            {
                using (var gelenYanit = await httpClient.GetAsync("https://localhost:44333/api/Kitap/" + id))
                {
                    string gelenKitapDetayString = await gelenYanit.Content.ReadAsStringAsync();
                    duzenlenecekKitap = JsonConvert.DeserializeObject<Kitap>(gelenKitapDetayString);
                }
            }
            return View(duzenlenecekKitap);
        }

        [HttpPost]
        public async Task<IActionResult> EditKitap(Kitap kitap)
        {
            using (var httpClient = new HttpClient())
            {
                Kitap guncellenecekKitap = new Kitap()
                {
                    Id = kitap.Id,
                    KitapAdi = kitap.KitapAdi,
                    Fiyati = kitap.Fiyati,
                    SayfaSayisi = kitap.SayfaSayisi
                };
                httpClient.BaseAddress = new Uri("https://localhost:44333/");
                var response = httpClient.PutAsJsonAsync("api/Kitap", guncellenecekKitap).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Kitap");
                }
                else
                {
                    return NotFound();
                }
            }
        }
        public ViewResult KitapEkle() => View(); //kitapekle metodu view oluşturacak
        [HttpPost]
        public async Task<IActionResult> KitapEkle(Kitap kitap)
        {
            Kitap eklenecekKitap = new Kitap();
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent serializeEdilecekKitap = new StringContent(JsonConvert.SerializeObject(kitap), Encoding.UTF8, "application/json");
                //{"KitapAdi", kitap.KitapAdi, "Fiyati": kitap.Fiyati}
                using (var response = await httpClient.PostAsync("https://localhost:44333/api/Kitap", serializeEdilecekKitap))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Kitap");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetKitap(int id)
        {
            Kitap detayKitap = new Kitap();
            using (var httpClient = new HttpClient())
            {
                using (var gelenYanit = await httpClient.GetAsync("https://localhost:44333/api/Kitap/" + id))
                {
                    string gelenKitapDetapString = await gelenYanit.Content.ReadAsStringAsync();
                    detayKitap = JsonConvert.DeserializeObject<Kitap>(gelenKitapDetapString);
                }
            }
            return View(detayKitap);
        }
        public async Task<IActionResult> DeleteKitap(int id)
        {
            using(var httpClient = new HttpClient())
            {
                using (var gelenYanit = await httpClient.DeleteAsync("https://localhost:44333/api/Kitap/" + id))
                {
                    string abc = await gelenYanit.Content.ReadAsStringAsync();
                }
                return RedirectToAction("Index");
            }
        }
    }
}

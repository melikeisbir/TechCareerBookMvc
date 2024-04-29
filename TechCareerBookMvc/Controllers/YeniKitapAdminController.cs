using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCareerBookMvc.Data;
using TechCareerBookMvc.Models;
using TechCareerBookMvc.ViewModels;

namespace TechCareerBookMvc.Controllers
{
    public class YeniKitapAdminController : Controller
    {
        public ApplicationDbContext _context;
        public IWebHostEnvironment _environment;

        public YeniKitapAdminController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            List<YeniKitap> kitapListesi = new List<YeniKitap>();
            kitapListesi = await _context.YeniKitap.ToListAsync(); //select * from Kitap 
            return View(kitapListesi);
        }

        //bu metod ekranı oluşturan metod 
        public IActionResult Create()
        {
            return View();
        }

        //bu metodda ekrandan girilen değerleri alıp işleyecek metodumuz
        [HttpPost]
        public async Task<IActionResult> Cre6ate(YeniKitapViewModel model)
        {
            try
            {
                //if(ModelState.IsValid)
                //{
                string yuklenenResimAdi = ResimYukle(model);
                YeniKitap kitap = new YeniKitap
                {
                    kitapAdi = model.kitapAdi,
                    ISBN = model.ISBN,
                    fiyat = model.fiyat,
                    kitapResim = yuklenenResimAdi,
                    yayinlanmaTarihi = model.yayinlanmaTarihi
                };

                _context.Add(kitap); //insert into
                await _context.SaveChangesAsync(); // oluşturulan insert kodunu sqlserver execute edecek
                return RedirectToAction(nameof(Index));
                // }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction(nameof(Index));
        }
        private string ResimYukle(YeniKitapViewModel model)
        {
            string dosyaAdi = "";
            string dosyaninYuklenecegiKlasorYolu = Path.Combine(_environment.WebRootPath, "Uploads");

            if (!Directory.Exists(dosyaninYuklenecegiKlasorYolu))
            {
                Directory.CreateDirectory(dosyaninYuklenecegiKlasorYolu);
            }

            if (model.KitapPicture.FileName != null)
            {
                dosyaAdi = model.KitapPicture.FileName;
                string filePath = Path.Combine(dosyaninYuklenecegiKlasorYolu, dosyaAdi);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    //seçilen resim ilgili klasörü ilgili ismi ile birlikte oluşturulur
                    model.KitapPicture.CopyTo(fileStream);
                }

            }
            return dosyaAdi;
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitapDetay = await _context.YeniKitap.FindAsync(id);

            YeniKitapViewModel kitapViewModel = new()
            {
                kitapAdi = kitapDetay.kitapAdi,
                ISBN = kitapDetay.ISBN,
                fiyat = kitapDetay.fiyat,
                yayinlanmaTarihi = kitapDetay.yayinlanmaTarihi,
                kitapResim = kitapDetay.kitapResim


            };
            return View(kitapViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, YeniKitapViewModel model)
        {
            var kitap = await _context.YeniKitap.FindAsync(model.Id);
            kitap.kitapAdi = model.kitapAdi;
            kitap.ISBN = model.ISBN;
            kitap.fiyat = model.fiyat;
            kitap.yayinlanmaTarihi = model.yayinlanmaTarihi;
            //düzenleme sayfasında bir başka resim seçtiysem kontrolünü yapmam gerekiyro
            if (model.KitapPicture != null)
            {
                //resmini değiştirmek istediğim ürünün database deki kitapResim kolonundaki adına göre
                // git wwwroot klasörü altındaki Uploads klasöründeki ilgili resmi bul ve sil
                string filePath = Path.Combine(_environment.WebRootPath, "Uploads", kitap.kitapResim);
                System.IO.File.Delete(filePath);


            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //select * from Kitap where Id=id
            var kitap = await _context.YeniKitap
                .FirstOrDefaultAsync(m => m.Id == id);

            string filePath = Path.Combine(_environment.WebRootPath, "Uploads", kitap.kitapResim);
            System.IO.File.Delete(filePath);
            _context.YeniKitap.Remove(kitap);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
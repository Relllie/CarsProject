using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Repositories;
using WebProject.ViewModels;

namespace WebProject.Controllers
{
    public class BrandController : Controller
    {
        private ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;
        BrandRepository brandRepository;
        List<Brand> brands = new List<Brand>();
        public BrandController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            this.brandRepository = new BrandRepository(_context);
            brands = _context.Brands.ToList();
        }
        public ActionResult BrandsPage() 
        {
            return View(brands);
        }
        [HttpGet]
        public IActionResult AddBrand()
        {
            ViewData["Title"] = "Новый бренд";
            NewBrand NB = new NewBrand();
            return View(NB);
        }
        [HttpPost]
        public async Task<IActionResult> AddBrand(NewBrand model)
        {
            if (ModelState.IsValid)
            {
                Brand NB = await _context.Brands.FirstOrDefaultAsync(b => b.Name == model.Name);
                if (NB == null && model.Name != "")
                {
                    NB = new Brand() { Active = model.Active, Name = model.Name };

                    await brandRepository.UpdateAsync(NB);
                    await brandRepository.SaveAsync();
                    return RedirectToAction("BrandsPage", "Brand");
                }
                else
                    ModelState.AddModelError("", "Такой бренд уже существует");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ChangeBrand(int id)
        {
            ViewData["Title"] = "Изменение бренда";
            Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand != null)
            {
                ChangeBrand CB = new ChangeBrand() { Id = brand.Id, Name = brand.Name, Active = brand.Active };
                return View(CB);
            }
            else
                return RedirectToAction("BrandsPage", "Brand");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeBrand(ChangeBrand model)
        {
            if (ModelState.IsValid)
            {
                Brand CB = await _context.Brands.FirstOrDefaultAsync(b => b.Id == model.Id);
                if (model.Name != null)
                {
                    CB.Name = model.Name;
                    CB.Active = model.Active;
                    Console.WriteLine(model.Name + " " + " " + model.Active);
                    await brandRepository.UpdateAsync(CB);
                    await brandRepository.SaveAsync();
                    return RedirectToAction("BrandsPage", "Brand");
                }
                else
                    ModelState.AddModelError("", "Имя не может быть пустым");
            }
            return View(model);
        }
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            _context.Brands.Remove(brand);
            await brandRepository.SaveAsync();
            return RedirectToAction("BrandsPage", "Brand");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

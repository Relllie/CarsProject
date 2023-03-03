using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Repositories;
using WebProject.ViewModels;

namespace WebProject.Controllers
{
    public class ModelController: Controller
    {
        private ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;
        BrandRepository brandRepository;
        ModelRepository modelRepository;
        List<Brand> brands = new List<Brand>();
        List<Model> models = new List<Model>();
        public bool sortType;
        public ModelController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            brandRepository = new BrandRepository(_context);
            modelRepository = new ModelRepository(_context);
            this.brands = _context.Brands.ToList();
            this.models = _context.Models.ToList();
            sortType = false;
        }
        public ActionResult ModelsPage(SortState? sortOrder)
        {
            IEnumerable<Model> models = _context.Models.ToList();

            ViewBag.brands = _context.Brands.ToList();
            switch(sortOrder)
            {
                case SortState.BrandUnsorted:
                    sortOrder = SortState.BrandAsc;
                    break;
                case SortState.BrandAsc:
                    sortOrder = SortState.BrandDesc;
                    break;
                default:
                    sortOrder = SortState.BrandUnsorted;
                    break;

            }
            ViewData["BrandSort"] = sortOrder;
            models = sortOrder switch
            {
                SortState.BrandDesc => models.OrderByDescending(b => b.BrandId),
                SortState.BrandUnsorted => models,
                _ => models.OrderBy(b => b.BrandId),
            };
            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> NewModel()
        {
            ViewData["Title"] = "Новая модель";
            NewModel NM = new NewModel() { Name = "Новая модель"};
            ViewBag.BrandList = brands;
            return View(NM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewModel(NewModel model)
        {
            if (ModelState.IsValid)
            {
                Model NM = await _context.Models.FirstOrDefaultAsync(m => m.Name == model.Name);
                if (NM == null)
                {
                    Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == model.BrandName);
                    NM = new Model() { Active = model.Active, Name = model.Name, BrandId = brand.Id };

                    await modelRepository.CreateAsync(NM);
                    await modelRepository.SaveAsync();
                    return RedirectToAction("ModelsPage", "Model");
                }
                else
                    ModelState.AddModelError("", "Такой бренд уже существует");
            }
            ViewBag.BrandList = brands;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeModel(int id) 
        {
            ViewData["Title"] = "Изменение модели";
            Model model = await _context.Models.FirstOrDefaultAsync(m => m.Id == id);
            if(model != null) 
            {
                Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == model.BrandId);
                ChangeModel CM = new ChangeModel() { Id = model.Id, Name = model.Name, BrandName = brand.Name, Active = model.Active };
                ViewBag.Brands = brands;
                return View(CM);
            }
            return RedirectToAction("ModelsPage","Model");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeModel(ChangeModel model)
        {
            if (ModelState.IsValid)
            {
                Model ChangedModel = await _context.Models.FirstOrDefaultAsync(m=>m.Id == model.Id);
                if(model.Name != null && ChangedModel!=null) 
                {
                    ChangedModel.Name = model.Name;
                    ChangedModel.Active = model.Active;
                    Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == model.BrandName);
                    ChangedModel.BrandId = brand.Id;

                    await modelRepository.UpdateAsync(ChangedModel);
                    await modelRepository.SaveAsync();
                    return RedirectToAction("ModelsPage", "Model");
                }
                else
                    ModelState.AddModelError("", "Имя не может быть пустым");
            }
            ViewBag.Brands = brands;
            return View(model);
        }
        public async Task<ActionResult> DeleteModel(int id)
        {
            var model = await _context.Models.FirstOrDefaultAsync(b => b.Id == id);
            _context.Models.Remove(model);
            await modelRepository.SaveAsync();
            return RedirectToAction("ModelsPage", "Model");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;
using System.Diagnostics;
using WebProject.Models;
using WebProject.Repositories;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            if (!context.Brands.Any())
            {
                var brands = new List<Brand>()
                {
                    new Brand() { Name = "BMW", Active = true },
                    new Brand() { Name = "Cadillac",Active = false },
                    new Brand() { Name = "Hyundai", Active = true },
                    new Brand() { Name = "Kia",Active = true },
                    new Brand() { Name = "Lexus", Active = false },
                    new Brand() { Name = "Mazda",Active = true },
                    new Brand() { Name = "Renault", Active = true },
                    new Brand() { Name = "Volvo",Active = false }
                };
                foreach (var item in brands)
                {
                    context.Brands.Add(item);
                }
                context.SaveChanges();
                var models = new List<Model>()
                {
                    new Model() { BrandId = brands.Where(i => i.Name=="BMW").First().Id, Name = "x3", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="BMW").First().Id, Name = "501", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="BMW").First().Id, Name = "E3", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="Cadillac").First().Id, Name = "CT6",Active = false },
                    new Model() { BrandId = brands.Where(i => i.Name=="Cadillac").First().Id, Name = "De Ville",Active = false },
                    new Model() { BrandId = brands.Where(i => i.Name=="Cadillac").First().Id, Name = "Fleetwood",Active = false },
                    new Model() { BrandId = brands.Where(i => i.Name=="Hyundai").First().Id, Name = "Accent", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="Hyundai").First().Id, Name = "Grace", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="Hyundai").First().Id, Name = "Satellite", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="Kia").First().Id, Name = "Bongo",Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="Kia").First().Id, Name = "Ceed",Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="Kia").First().Id, Name = "Rio",Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name=="Lexus").First().Id, Name = "CT", Active = false },
                    new Model() { BrandId = brands.Where(i => i.Name == "Lexus").First().Id, Name = "GS", Active = false },
                    new Model() { BrandId = brands.Where(i => i.Name == "Lexus").First().Id, Name = "ES", Active = false },
                    new Model() { BrandId = brands.Where(i => i.Name == "Mazda").First().Id, Name = "Biante",Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name == "Mazda").First().Id, Name = "Cosmo",Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name == "Mazda").First().Id, Name = "Flair",Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name == "Renault").First().Id, Name = "Alaskan", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name == "Renault").First().Id, Name = "Captur", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name == "Renault").First().Id, Name = "Scenic", Active = true },
                    new Model() { BrandId = brands.Where(i => i.Name == "Volvo").First().Id, Name = "S40",Active = false },
                    new Model() { BrandId = brands.Where(i => i.Name == "Volvo").First().Id, Name = "C70",Active = false },
                    new Model() { BrandId = brands.Where(i => i.Name == "Volvo").First().Id, Name = "FH-Series",Active = false }
                };

                foreach (var model in models) { context.Models.Add(model); };
                context.SaveChanges();
            }
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
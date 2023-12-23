using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TTMDotNetCore.WebMVCApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TTMDotNetCore.WebMVCApp.Controllers
{
    public class HighChartsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
        public IActionResult BasicColumnChart()
        {
            HighChartBasicColumnChartModel model = new HighChartBasicColumnChartModel
            {
                Categories = new List<string> { "USA", "China", "Brazil", "EU", "India", "Russia" },
                Name = new List<string> { "Corn", "Wheat" },
                Data = new List<List<int>>
                {
                    new List<int> { 406292, 260000, 107000, 68300, 27500, 14500 },
                    new List<int> { 51086, 136000, 5500, 141000, 107180, 77000 }
                }
            };

            return View(model);
        }
    }
}

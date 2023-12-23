using Microsoft.AspNetCore.Mvc;
using TTMDotNetCore.WebMVCApp.Models;

namespace TTMDotNetCore.WebMVCApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
        public IActionResult PyramidChart()
        {
			CanvasChartPyramidChartModel model = new CanvasChartPyramidChartModel
			{
				Categories = new List<string> { "Day 1", "Day 2", "Day 3", "Day 4", "Day 5", "Day 6", "Day 7" },
				Data = new List<int> { 300, 50, 100, 300, 50, 100, 20 },

			};
			return View(model);
        }
    }
}

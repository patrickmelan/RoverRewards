using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using RoverCore.BreadCrumbs.Models;
using RoverHello.Domain.Entities.Identity;
using RoverHello.Web.Areas.Dashboard.Models.StudentViewModels;
using System.Threading.Tasks;

namespace RoverHello.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin,Student")]
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new StudentViewModel
            {
                User = await _userManager.GetUserAsync(User)
            };

            return View(viewModel);
        }
    }
}

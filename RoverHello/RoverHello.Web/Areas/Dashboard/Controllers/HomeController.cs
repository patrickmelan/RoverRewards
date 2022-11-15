﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoverHello.Domain.Entities.Identity;
using RoverHello.Web.Areas.Dashboard.Models.HomeViewModels;
using RoverHello.Web.Controllers;
using RoverCore.BreadCrumbs.Services;
using System.Threading.Tasks;

namespace RoverHello.Web.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize]
[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController : BaseController<HomeController>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
            .Then("Home");

        var viewModel = new HomeViewModel
        {
            User = await _userManager.GetUserAsync(User)
        };

        _toast.Success($"Welcome back {viewModel.User.FirstName}!");

        return View(viewModel);
    }
}
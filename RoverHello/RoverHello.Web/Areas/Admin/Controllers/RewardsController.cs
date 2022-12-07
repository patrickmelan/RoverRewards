using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoverCore.BreadCrumbs.Services;
using RoverCore.Datatables.DTOs;
using RoverCore.Datatables.Extensions;
using RoverHello.Web.Controllers;
using RoverHello.Infrastructure.Common.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RoverHello.Domain.Entities;
using RoverHello.Infrastructure.Persistence.DbContexts;

namespace RoverHello.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RewardsController : BaseController<RewardsController>
{
	public class RewardIndexViewModel 
	{
		[Key]            
	    public int Id { get; set; }
	    public string Name { get; set; }
	    public int PointCost { get; set; }
	    public string Description { get; set; }
	    public bool Available { get; set; }
	    public int Count { get; set; }
	}

	private const string createBindingFields = "Id,Name,PointCost,Description,Available,Count";
    private const string editBindingFields = "Id,Name,PointCost,Description,Available,Count";
    private const string areaTitle = "Admin";

    private readonly ApplicationDbContext _context;

    public RewardsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Rewards
    public IActionResult Index()
    {
        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
			.Then("Manage Reward");       
		
		// Fetch descriptive data from the index dto to build the datatables index
		var metadata = DatatableExtensions.GetDtMetadata<RewardIndexViewModel>();
		
		return View(metadata);
   }

    // GET: Admin/Rewards/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        ViewData["AreaTitle"] = areaTitle;
        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
            .ThenAction("Manage Reward", "Index", "Rewards", new { Area = "Admin" })
            .Then("Reward Details");            

        if (id == null)
        {
            return NotFound();
        }

        var reward = await _context.Reward
            .FirstOrDefaultAsync(m => m.Id == id);
        if (reward == null)
        {
            return NotFound();
        }

        return View(reward);
    }

    // GET: Admin/Rewards/Create
    public IActionResult Create()
    {
        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
            .ThenAction("Manage Reward", "Index", "Rewards", new { Area = "Admin" })
            .Then("Create Reward");     

       return View();
	}

    // POST: Admin/Rewards/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind(createBindingFields)] Reward reward)
    {
        ViewData["AreaTitle"] = areaTitle;

        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
        .ThenAction("Manage Reward", "Index", "RewardsController", new { Area = "Admin" })
        .Then("Create Reward");     
        
        // Remove validation errors from fields that aren't in the binding field list
        ModelState.Scrub(createBindingFields);           

        if (ModelState.IsValid)
        {
            _context.Add(reward);
            await _context.SaveChangesAsync();
            
            _toast.Success("Created successfully.");
            
                return RedirectToAction(nameof(Index));
            }
        return View(reward);
    }

    // GET: Admin/Rewards/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        ViewData["AreaTitle"] = areaTitle;

        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
        .ThenAction("Manage Reward", "Index", "Rewards", new { Area = "Admin" })
        .Then("Edit Reward");     

        if (id == null)
        {
            return NotFound();
        }

        var reward = await _context.Reward.FindAsync(id);
        if (reward == null)
        {
            return NotFound();
        }
        

        return View(reward);
    }

    // POST: Admin/Rewards/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind(editBindingFields)] Reward reward)
    {
        ViewData["AreaTitle"] = areaTitle;

        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
        .ThenAction("Manage Reward", "Index", "Rewards", new { Area = "Admin" })
        .Then("Edit Reward");  
    
        if (id != reward.Id)
        {
            return NotFound();
        }
        
        Reward model = await _context.Reward.FindAsync(id);

        model.Name = reward.Name;
        model.PointCost = reward.PointCost;
        model.Description = reward.Description;
        model.Available = reward.Available;
        model.Count = reward.Count;
        // Remove validation errors from fields that aren't in the binding field list
        ModelState.Scrub(editBindingFields);           

        if (ModelState.IsValid)
        {
            try
            {
                await _context.SaveChangesAsync();
                _toast.Success("Updated successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RewardExists(reward.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(reward);
    }

    // GET: Admin/Rewards/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        ViewData["AreaTitle"] = areaTitle;

        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
        .ThenAction("Manage Reward", "Index", "Rewards", new { Area = "Admin" })
        .Then("Delete Reward");  

        if (id == null)
        {
            return NotFound();
        }

        var reward = await _context.Reward
            .FirstOrDefaultAsync(m => m.Id == id);
        if (reward == null)
        {
            return NotFound();
        }

        return View(reward);
    }

    // POST: Admin/Rewards/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reward = await _context.Reward.FindAsync(id);
        _context.Reward.Remove(reward);
        await _context.SaveChangesAsync();
        
        _toast.Success("Reward deleted successfully");

        return RedirectToAction(nameof(Index));
    }

    private bool RewardExists(int id)
    {
        return _context.Reward.Any(e => e.Id == id);
    }


	[HttpPost]
	[ValidateAntiForgeryToken]
    public async Task<IActionResult> GetReward(DtRequest request)
    {
        try
		{
			var query = _context.Reward;
			var jsonData = await query.GetDatatableResponseAsync<Reward, RewardIndexViewModel>(request);

            return Ok(jsonData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating Reward index json");
        }
        
        return StatusCode(500);
    }

}


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
using System.Collections.Generic;
using RoverHello.Web.Areas.Identity.Models.AccountViewModels;

namespace RoverHello.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class EventsController : BaseController<EventsController>
{
	public class EventIndexViewModel 
	{
		[Key]            
	    public int Id { get; set; }

        public DateTime Date { get; set; }
	    public string Name { get; set; }
	    public int Points { get; set; }
	    public string Description { get; set; }
        public List<UserViewModel> Attendees { get; set; } 
	}

	private const string createBindingFields = "Id,Date,Name,Points,Description,Attendees";
    private const string editBindingFields = "Id,Date,Name,Points,Description,Attendees";
    private const string areaTitle = "Admin";

    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Events
    public IActionResult Index()
    {
        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
			.Then("Manage Event");       
		
		// Fetch descriptive data from the index dto to build the datatables index
		var metadata = DatatableExtensions.GetDtMetadata<EventIndexViewModel>();
		
		return View(metadata);
   }

    // GET: Admin/Events/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        ViewData["AreaTitle"] = areaTitle;
        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
            .ThenAction("Manage Event", "Index", "Events", new { Area = "Admin" })
            .Then("Event Details");            

        if (id == null)
        {
            return NotFound();
        }

        var @event = await _context.Event
            .FirstOrDefaultAsync(m => m.Id == id);
        if (@event == null)
        {
            return NotFound();
        }

        return View(@event);
    }

    // GET: Admin/Events/Create
    public IActionResult Create()
    {
        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
            .ThenAction("Manage Event", "Index", "Events", new { Area = "Admin" })
            .Then("Create Event");     

       return View();
	}

    // POST: Admin/Events/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind(createBindingFields)] Event @event)
    {
        ViewData["AreaTitle"] = areaTitle;

        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
        .ThenAction("Manage Event", "Index", "EventsController", new { Area = "Admin" })
        .Then("Create Event");     
        
        // Remove validation errors from fields that aren't in the binding field list
        ModelState.Scrub(createBindingFields);           

        if (ModelState.IsValid)
        {
            _context.Add(@event);
            await _context.SaveChangesAsync();
            
            _toast.Success("Created successfully.");
            
                return RedirectToAction(nameof(Index));
            }
        return View(@event);
    }

    // GET: Admin/Events/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        ViewData["AreaTitle"] = areaTitle;

        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
        .ThenAction("Manage Event", "Index", "Events", new { Area = "Admin" })
        .Then("Edit Event");     

        if (id == null)
        {
            return NotFound();
        }

        var @event = await _context.Event.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }
        

        return View(@event);
    }

    // POST: Admin/Events/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind(editBindingFields)] Event @event)
    {
        ViewData["AreaTitle"] = areaTitle;

        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
        .ThenAction("Manage Event", "Index", "Events", new { Area = "Admin" })
        .Then("Edit Event");  
    
        if (id != @event.Id)
        {
            return NotFound();
        }
        
        Event model = await _context.Event.FindAsync(id);

        model.Name = @event.Name;
        model.Date = @event.Date;
        model.Points = @event.Points;
        model.Description = @event.Description;
        model.Attendees = @event.Attendees;
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
                if (!EventExists(@event.Id))
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
        return View(@event);
    }

    // GET: Admin/Events/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        ViewData["AreaTitle"] = areaTitle;

        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
        .ThenAction("Manage Event", "Index", "Events", new { Area = "Admin" })
        .Then("Delete Event");  

        if (id == null)
        {
            return NotFound();
        }

        var @event = await _context.Event
            .FirstOrDefaultAsync(m => m.Id == id);
        if (@event == null)
        {
            return NotFound();
        }

        return View(@event);
    }

    // POST: Admin/Events/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var @event = await _context.Event.FindAsync(id);
        _context.Event.Remove(@event);
        await _context.SaveChangesAsync();
        
        _toast.Success("Event deleted successfully");

        return RedirectToAction(nameof(Index));
    }

    private bool EventExists(int id)
    {
        return _context.Event.Any(e => e.Id == id);
    }


	[HttpPost]
	[ValidateAntiForgeryToken]
    public async Task<IActionResult> GetEvent(DtRequest request)
    {
        try
		{
			var query = _context.Event;
			var jsonData = await query.GetDatatableResponseAsync<Event, EventIndexViewModel>(request);

            return Ok(jsonData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating Event index json");
        }
        
        return StatusCode(500);
    }

}


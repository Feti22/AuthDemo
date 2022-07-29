using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthDemo.Data;
using AuthDemo.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuthDemo.Controllers
{
    public class VehiculesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager <ApplicationUser> _userManager;

        private string userName;
        private string userManager;

        public VehiculesController(ApplicationDbContext context)
        {
            _context = context;
            _userManager = _userManager;
        }

        // GET: Vehicules
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicule.Include(v => v.User);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Index(int VehiculeId)
        {
            if (VehiculeId != null)

                try
                {
                    Vehicule selected = _context.Vehicule.First(v => v.Id == VehiculeId);
                    string userName = User.Identity.Name;

                    ApplicationUser user = _context.Users.First(u => u.UserName == userName);
                    user.Vehicules.Add(selected);
                    selected.User = user;
                    selected.UserId = user.Id;

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");
                }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> MyVehicules()
        {
            string userName = User.Identity.Name;
            ApplicationUser user = await _context.Users.Include(u => u.Vehicules)
                .FirstAsync(u => u.UserName == userName);
            
            return View(user);
        }
        // GET: Vehicules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicule == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicule
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // GET: Vehicules/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Vehicules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Make,Model,Year,UserId")] Vehicule vehicule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vehicule.UserId);
            return View(vehicule);
        }

        // GET: Vehicules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicule == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicule.FindAsync(id);
            if (vehicule == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vehicule.UserId);
            return View(vehicule);
        }

        // POST: Vehicules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Make,Model,Year,UserId")] Vehicule vehicule)
        {
            if (id != vehicule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculeExists(vehicule.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vehicule.UserId);
            return View(vehicule);
        }

        // GET: Vehicules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicule == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicule
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // POST: Vehicules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicule == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicule'  is null.");
            }
            var vehicule = await _context.Vehicule.FindAsync(id);
            if (vehicule != null)
            {
                _context.Vehicule.Remove(vehicule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculeExists(int id)
        {
          return (_context.Vehicule?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

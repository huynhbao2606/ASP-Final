﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_Final.Data;
using ASP_Final.Models;

namespace ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VaccineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaccineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Vaccine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vaccine.Include(v => v.Type);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Vaccine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine
                .Include(v => v.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // GET: Admin/Vaccine/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.Type, "Id", "Id");
            return View();
        }

        // POST: Admin/Vaccine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,ExpirationData,Price,TypeId,CreatedAt")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.Type, "Id", "Id", vaccine.TypeId);
            return View(vaccine);
        }

        // GET: Admin/Vaccine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.Type, "Id", "Id", vaccine.TypeId);
            return View(vaccine);
        }

        // POST: Admin/Vaccine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,ExpirationData,Price,TypeId,CreatedAt")] Vaccine vaccine)
        {
            if (id != vaccine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineExists(vaccine.Id))
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
            ViewData["TypeId"] = new SelectList(_context.Type, "Id", "Id", vaccine.TypeId);
            return View(vaccine);
        }

        // GET: Admin/Vaccine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine
                .Include(v => v.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // POST: Admin/Vaccine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccine == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vaccine'  is null.");
            }
            var vaccine = await _context.Vaccine.FindAsync(id);
            if (vaccine != null)
            {
                _context.Vaccine.Remove(vaccine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineExists(int id)
        {
            return (_context.Vaccine?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

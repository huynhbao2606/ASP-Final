using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_Final.Data;
using ASP_Final.Models;
using ASP_Final.Dao.IRepository;
using X.PagedList;
using ASP_MVC.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VaccineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VaccineController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Vaccine
        public IActionResult Index(int? page)
        {
            if (page == null) page = 1;

            int pageSize = 7;


            int pageNumber = (page ?? 1);

            IEnumerable<Vaccine> vaccineList = _unitOfWork.Vaccine.GetEntities(
                filter: null,
                orderBy: null,
                includeProperties: "Type"
            );


            return View(vaccineList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Vaccine/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _unitOfWork.Vaccine.GetEntityById == null)
            {
                return NotFound();
            }

            var vaccine = _unitOfWork.Vaccine.GetEntityById(id);

            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }


        // GET: Admin/Vaccine/Create
        public IActionResult Upsert(int? id)
        {

            VaccineDTO vaccineDTO = new VaccineDTO();

            vaccineDTO.TypeList = _unitOfWork.Type.GetAll().Select(
                i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

            Vaccine vaccine;
            if (id == null || id == 0)
            {
                vaccine = new Vaccine();
            }
            else
            {
                vaccine = _unitOfWork.Vaccine.GetEntityById((int)id);

                if (vaccine == null)
                {
                    return NotFound();
                }
            }

            vaccineDTO.Vaccine = vaccine;
            return View(vaccineDTO);
        }

        // POST: Admin/Vaccine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(
         [Bind("Vaccine")] VaccineDTO vaccineDto)
        {

            bool isCreate = vaccineDto.Vaccine.Id == 0;

            /// validate
            bool checkVaccineNameExist = _unitOfWork.Vaccine
                .GetEntities(
                    filter: isCreate
                        ? i => i.Name == vaccineDto.Vaccine.Name
                        : i => i.Name == vaccineDto.Vaccine.Name && i.Id != vaccineDto.Vaccine.Id,
                    orderBy: null,
                    includeProperties: null)
                .Any();

            if (checkVaccineNameExist)
            {
                ModelState.AddModelError("Name", "The vaccine name already exist");
                TempData["vaccnieNameError"] = "The vaccine name already exist";
            }

            vaccineDto.TypeList = _unitOfWork.Type.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });


            /// save data
            if (ModelState.IsValid)
            {


                if (isCreate)
                {
                    _unitOfWork.Vaccine.Add(vaccineDto.Vaccine);
                }
                else
                {
                    _unitOfWork.Vaccine.Update(vaccineDto.Vaccine);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(vaccineDto);
        }

        // POST: Admin/Vaccine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

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

    }
}

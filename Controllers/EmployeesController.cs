#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConsidTestApplication.Data;
using ConsidTestApplication.Models;

namespace ConsidTestApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly LibraryContext _context;

        public EmployeesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {   
            var managers = _context.Employees.Where(m => (m.IsManager == true) || (m.IsCEO == true)).ToList();       
            ViewBag.ManagerList = managers;
            return View();
        }

        // POST: Employees/Create
        // Before creating checks if there is a CEO.
        // Depending on what role is chosen different salaries are calculated.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Salary,IsCEO,IsManager,ManagerId")] Employees employees)
        {   
            var CEOExist = _context.Employees.Where(m => m.IsCEO == employees.IsCEO);
            var CEOId = _context.Employees.Where(x => x.IsCEO == true).Select(x => x.Id).SingleOrDefault();
            var ManagerID = _context.Employees.Where(x => x.IsManager == true).Select(x => x.Id).ToList();
            
            if (employees.IsCEO && CEOExist.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (ModelState.IsValid)
                {
                    
                    if (employees.IsCEO && employees.ManagerId != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else if (employees.IsManager && !(ManagerID.Contains((int)employees.ManagerId) || employees.ManagerId == CEOId))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else if (employees.IsManager==false && (!ManagerID.Contains((int)employees.ManagerId) || employees.ManagerId == CEOId))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else { 
                        if (employees.IsCEO)
                        {
                            employees.Salary = employees.Salary * (decimal)2.725;
                            _context.Add(employees);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else if(employees.IsManager) {

                            employees.Salary = employees.Salary * (decimal)1.725;
                            _context.Add(employees);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            employees.Salary = employees.Salary * (decimal)1.125; 
                            _context.Add(employees);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));    
                        }
                    }
                }
            }
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            var managers = _context.Employees.Where(m => (m.IsManager == true) || (m.IsCEO == true)).ToList();
            ViewBag.ManagerList = managers;
            return View(employees);
        }

        // POST: Employees/Edit/5
        // 
        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Salary,IsCEO,IsManager,ManagerId")] Employees employees)
        {
            var CEOId = _context.Employees.Where(x => x.IsCEO == true).Select(x => x.Id).SingleOrDefault();
            var ManagerID = _context.Employees.Where(x => x.IsManager == true).Select(x => x.Id).ToList();

            if (employees.Id == id && employees.IsCEO)
            {
                return RedirectToAction(nameof(Index));
            }
            else { 
                if (id != employees.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (employees.IsCEO && employees.ManagerId != null)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else if (employees.IsManager && !(ManagerID.Contains((int)employees.ManagerId) || employees.ManagerId == CEOId))
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else if ((employees.IsManager == false) && (!ManagerID.Contains((int)employees.ManagerId) || employees.ManagerId == CEOId))
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else { 
                            if (employees.IsCEO)
                            {
                                employees.Salary = employees.Salary * (decimal)2.725;
                                _context.Update(employees);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                            else if (employees.IsManager)
                            {

                                employees.Salary = employees.Salary * (decimal)1.725;
                                _context.Update(employees);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));

                            }
                            else
                            {
                                employees.Salary = employees.Salary * (decimal)1.125;
                                _context.Update(employees);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeesExists(employees.Id))
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
                return View(employees);
            }
            
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var connectedToEmployee = _context.Employees.Where(a => a.ManagerId == id);
            if (!connectedToEmployee.Any())
            {
                var employees = await _context.Employees.FindAsync(id);
                _context.Employees.Remove(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinCareForUniversity.Data;
using SkinCareForUniversity.Models;

namespace SkinCareForUniversity.Controllers;

public class AppointmentsController : Controller
{
    private readonly SkinCareContext _context;

    public AppointmentsController(SkinCareContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var appointments = await _context.Appointments
            .OrderByDescending(a => a.AppointmentDate)
            .ThenByDescending(a => a.AppointmentTime)
            .ToListAsync();
        return View(appointments);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (appointment is null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,StudentId,PhoneNumber,ServiceType,AppointmentDate,AppointmentTime,Notes,AcceptTerms")] Appointment appointment)
    {
        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

        _context.Add(appointment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment is null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,StudentId,PhoneNumber,ServiceType,AppointmentDate,AppointmentTime,Notes")] Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

        try
        {
            _context.Update(appointment);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AppointmentExists(appointment.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (appointment is null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment is not null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool AppointmentExists(int id)
    {
        return _context.Appointments.Any(e => e.Id == id);
    }
}

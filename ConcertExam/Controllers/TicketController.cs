using Concert.Data;
using ConcertExam.Data.Entities;
using ConcertExam.Helpers;
using ConcertExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.Controllers
{
    public class TicketController : Controller
    {
        private readonly ICombosHelper _combosHelper;
        private readonly DataContext _context;

        public TicketController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        public IActionResult CheckTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            Ticket ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == id);

            if (id <= 0 || id >= 5002)
            {
                TempData["Message"] = "Error de boleta, no existe";

                return RedirectToAction(nameof(CheckTicket));

            }

            if (ticket.WasUsed != false)
            {
                TempData["Message"] = "Ticket ya es usado.";
                TempData["Name"] = ticket.Name;
                TempData["Document"] = ticket.Document;
                TempData["Date"] = ticket.DateTime;
                //TODO : time and entrance
                return RedirectToAction(nameof(CheckTicket), new { Id = ticket.Id });
            }
            else
            {
                TempData["Message"] = "Ticket no ha sido usada.";
                return RedirectToAction(nameof(EditCheckTicket), new { Id = ticket.Id });
            }
        }


        //public async Task<IActionResult> TicketDetails(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Ticket ticket = await _context.Tickets
        //        .Include(t => t.Entrance).
        //        FirstOrDefault(t => t.Id ==);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ticket);
        //}

        public async Task<IActionResult> EditCheckTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        public async Task<IActionResult> EditTicket(int? id)
        {
            Ticket ticket = await _context.Tickets.FindAsync(id);
            TicketViewModel model = new()
            {
                Document = ticket.Document,
                Id = ticket.Id,
                Name = ticket.Name,
                DateTime = (DateTime)ticket.DateTime,
                WasUsed = true,
                Entrances = await _combosHelper.GetComboEntrancesAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTicket(int id, TicketViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = await _context.Tickets.FindAsync(model.Id);
                    ticket.Document = model.Document;
                    ticket.Name = model.Name;
                    ticket.DateTime = model.DateTime;
                    ticket.WasUsed = true;
                    ticket.Entrance = await _context.Entrances.FindAsync(model.EntranceId);
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Entrances = await _combosHelper.GetComboEntrancesAsync();
            return View(model);
        }


    }

}


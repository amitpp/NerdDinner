using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NerdDinner.Data;
using NerdDinner.Models;
using System.Linq;





namespace NerdDinner.Controllers
{
    public class DinnerController : Controller
    {
        private readonly DinnerContext _context;
        public DinnerController(DinnerContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dinners = from dinner in _context.Dinner
                          select dinner;
            var rsvps = from rsvp in _context.Rsvp
                       group rsvp by rsvp.DinnerID into acceptance
                       select new
                       {
                           DinnerID = acceptance.Key,
                           rsvps = acceptance.Count()
                       };
            var rsvplist = new Dictionary<int, int>();
            foreach (var num in rsvps)
            {
                rsvplist.Add(num.DinnerID, num.rsvps);
            }
            ViewData["rsvp"] = rsvplist;
            return View(await dinners.ToListAsync());
        }

        public async Task<IActionResult> Logged()
        {
            ViewData["session"] = HttpContext.Session.GetString("userId");
            var dinners = from dinner in _context.Dinner
                          select dinner;
            var rsvps = from rsvp in _context.Rsvp
                       group rsvp by rsvp.DinnerID into acceptance
                       select new
                       {
                           DinnerID = acceptance.Key,
                           rsvps = acceptance.Count()
                       };
            var rsvplist = new Dictionary<int,int>();
            foreach (var num in rsvps)
            {
                rsvplist.Add(num.DinnerID, num.rsvps);
            }
            ViewData["rsvp"] = rsvplist;
            return View(await dinners.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewData["session"] = HttpContext.Session.GetString("userId");
            var uid = 0;
            if (ViewData["session"] != null)
            {
                uid = int.Parse(HttpContext.Session.GetString("Uid"));
            }
            if (id == null)
            {
                return View("NotFound");
            }

            var dinner = await _context.Dinner
                .FirstOrDefaultAsync(m => m.DinnerID == id);
            var flag = await _context.Dinner.Where(m => m.DinnerID == id &&
                m.LoginID == uid).ToListAsync();
            ViewData["flag"] = flag.Capacity;
            if (dinner == null)
            {
                return View("NotFound");
            }
            return View(dinner);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["session"] = HttpContext.Session.GetString("userId");
            if (id == null)
            {
                return View("NotFound");
            }

            var dinner = await _context.Dinner.FindAsync(id);
            if (dinner == null)
            {
                return View("NotFound");
            }
            return View(dinner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DinnerID,Title,EventDate,Description,HostedBy,ContactPhone,Address,Country,Latitude,Longitude")] Dinner dinner)
        {
            ViewData["session"] = HttpContext.Session.GetString("userId");
            if (id != dinner.DinnerID)
            {
                return View("NotFound");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dinner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DinnerExists(dinner.DinnerID))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Logged));
            }
            return View(dinner);
        }

        public IActionResult Create()
        {
            ViewData["session"] = HttpContext.Session.GetString("userId");
            return View();
        }

        // POST: Dinner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DinnerID,Title,EventDate,Description,HostedBy,ContactPhone,Address,Country,Latitude,Longitude")] Dinner dinner)
        {
            ViewData["session"] = HttpContext.Session.GetString("userId");
            var uid = int.Parse(HttpContext.Session.GetString("Uid"));
            if (ModelState.IsValid)
            {
                dinner.LoginID = uid;
                _context.Add(dinner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Logged));
            }
            return View(dinner);
        }

         private bool DinnerExists(int id)
        {
            return _context.Dinner.Any(e => e.DinnerID == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["session"] = HttpContext.Session.GetString("userId");
            if (id == null)
            {
                return NotFound();
            }

            var dinner = await _context.Dinner
                .FirstOrDefaultAsync(m => m.DinnerID == id);
            if (dinner == null)
            {
                return NotFound();
            }

            return View(dinner);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["session"] = HttpContext.Session.GetString("userId");
            var dinner = await _context.Dinner.FindAsync(id);
            _context.Dinner.Remove(dinner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Logged));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rsvp([Bind("DinnerID")]Rsvp rsvp)
        {
            var session = HttpContext.Session.GetString("userId");
            rsvp.AttendeeName = session;
            ViewData["session"] = session;
            if (RsvpExists(session, rsvp.DinnerID))
            {
                return View("RsvpDone");
            }
            _context.Add(rsvp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = rsvp.DinnerID });
        }
        private bool RsvpExists(string uname, int rid)
        {
            return _context.Rsvp.Any(e => e.AttendeeName == uname && e.DinnerID == rid);
        }
    }
}

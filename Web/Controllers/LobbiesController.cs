using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers {
    public class LobbiesController : Controller {
        private readonly ApplicationDbContext _context;

        public LobbiesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Lobbies
        [Authorize(Roles="User, Moderator, Admin")]
        public async Task<IActionResult> Index() {
            return View(await _context.Lobby.ToListAsync());
        }

        // GET: Lobbies/Details/5
        [Authorize(Roles="User, Moderator, Admin")]
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var lobby = await _context.Lobby
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lobby == null) {
                return NotFound();
            }

            return View(lobby);
        }

        // GET: Lobbies/Create
        [Authorize(Roles="User,Moderator,Admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: Lobbies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Lobby lobby) {
            if (ModelState.IsValid) {
                _context.Add(lobby);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lobby);
        }

        // GET: Lobbies/Edit/5
        [Authorize(Roles="User,Moderator,Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var lobby = await _context.Lobby.FindAsync(id);
            if (lobby == null) {
                return NotFound();
            }
            return View(lobby);
        }

        // POST: Lobbies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="User,Moderator,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Lobby lobby) {
            if (id != lobby.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(lobby);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!LobbyExists(lobby.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lobby);
        }

        // GET: Lobbies/Delete/5
        [Authorize(Roles="User,Moderator,Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var lobby = await _context.Lobby
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lobby == null) {
                return NotFound();
            }

            return View(lobby);
        }

        // POST: Lobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="User,Moderator,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var lobby = await _context.Lobby.FindAsync(id);
            if (lobby != null) {
                _context.Lobby.Remove(lobby);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LobbyExists(int id) {
            return _context.Lobby.Any(e => e.Id == id);
        }
    }
}

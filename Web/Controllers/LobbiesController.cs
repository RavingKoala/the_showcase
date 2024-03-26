using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.ViewModels;
using Web.Data;
using Web.Services;

namespace Web.Controllers
{
    [Authorize]
    public class LobbiesController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LobbiesController(ApplicationDbContext context, UserManager<IdentityUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // GET: Lobbies
        public async Task<IActionResult> Index() {
            List<LobbyListItem> listItems = await _context.Lobby.Select(lobby =>
                new LobbyListItem {
                    Id = lobby.Id,
                    Name = lobby.Name
                }
            ).ToListAsync();
            return View(listItems);
        }
        
        // GET: Lobbies/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(string id) {
            Lobby? lobby = await _context.Lobby.FindAsync(id);
            if (lobby is null)
                return RedirectToAction(nameof(Index));

            string? userId = _userManager.GetUserId(User);
            if (userId is null)
                return RedirectToAction(nameof(Index));

            IdentityUser? user1 = await _userManager.FindByIdAsync(lobby.Player1Id);
            if (user1 is null)
                return RedirectToAction(nameof(Index));
            string? user1Name = await _userManager.GetUserNameAsync(user1);
            if (user1Name is null)
                return RedirectToAction(nameof(Index));

            IdentityUser? user2 = null;
            string? user2Name = null;
            if (lobby.Player2Id is not null) {
                user2 = await _userManager.FindByIdAsync(lobby.Player2Id);
                if (user2 is null)
                    return RedirectToAction(nameof(Index));
                user2Name = await _userManager.GetUserNameAsync(user2);
                if (user2Name is null)
                    return RedirectToAction(nameof(Index));
            }

            LobbyDetails viewModel = new LobbyDetails {
                Id = lobby.Id,
                Name = lobby.Name,
                isHost = userId == lobby.Player1Id,
                Player1Id = lobby.Player1Id,
                Player1Name = user1Name,
                Player2Id = lobby.Player2Id,
                Player2Name = user2Name,
            };

            if (userId == lobby.Player1Id || userId == lobby.Player2Id)
                return View(viewModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Lobbies/Create
        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        // POST: Lobbies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LobbyCreate input) {
            if (!ModelState.IsValid)
                return View(input);

            string? userId = _userManager.GetUserId(User);
            if (userId is null)
                return RedirectToAction(nameof(Index));

            string newId = IdManager.GenerateGUID();

            Lobby lobby = new Lobby() {
                Id = newId,
                Name = input.Name,
                Player1Id = userId
            };

            _context.Add(lobby);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = newId });
        }

        // Lobbies/Leave/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Leave(string id) {
            if (id is null)
                return NotFound();

            string? userId = _userManager.GetUserId(User);
            if (userId is null)
                return RedirectToAction(nameof(Index));

            var lobby = await _context.Lobby.FindAsync(id);
            if (lobby is null)
                return NotFound();

            if (lobby.Player1Id == userId) //host
                _context.Remove(lobby);

            if (lobby.Player2Id == userId) { //host
                lobby.Player2Id = null;
                _context.Update(lobby);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Lobbies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(string id) {
            var lobby = await _context.Lobby.FindAsync(id);

            if (lobby is not null) {
                _context.Lobby.Remove(lobby);
            }
            
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }
        
        // Post: Lobbies/Join/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(string id) {
            var lobby = await _context.Lobby.FindAsync(id);
            
            if (lobby is null)
                return RedirectToAction(nameof(Index));

            string? userId = _userManager.GetUserId(User);
            if (userId is null)
                return RedirectToAction(nameof(Index));
            
            if (userId == lobby.Player1Id // rejoin
                || userId == lobby.Player2Id)
                return RedirectToAction(nameof(Details), new { id = id });

            if (lobby.Player2Id is null) {
                lobby.Player2Id = userId;
                _context.Update(lobby);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LobbyExists(string id) {
            return _context.Lobby.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REVALB.Data;
using REVALB.Models;
using REVALB.Models.ViewModels;

namespace REVALB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;


        public AdminController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // prikaz korisnika
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // prikaz svih albuma
        public async Task<IActionResult> Albums()
        {
            var albums = await _context.Albums
                .Include(a => a.Artist)
                .ToListAsync();
            return View(albums);
        }

        // brisanje albuma (sa recenzijama i komentarima)
        [HttpPost]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.Comments)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (album == null)
                return NotFound();

            // prvo brisi komentare, zatim recenzije
            if (album.Reviews != null)
            {
                foreach (var review in album.Reviews)
                {
                    _context.Comments.RemoveRange(review.Comments);
                }

                _context.Reviews.RemoveRange(album.Reviews);
            }

            // na kraju album
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return RedirectToAction("Albums");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // ne dozvoli da admin sam sebe obrise
            var currentUser = await _userManager.GetUserAsync(User);
            if (user.Id == currentUser.Id)
                return Forbid();

            await _userManager.DeleteAsync(user);

            return RedirectToAction("Users");
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var model = new EditRoleViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                CurrentRole = currentRoles.FirstOrDefault(),
                AvailableRoles = allRoles
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);

            // ukloni stare role
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // dodaj novu rolu
            if (!string.IsNullOrEmpty(model.SelectedRole))
            {
                await _userManager.AddToRoleAsync(user, model.SelectedRole);
            }

            return RedirectToAction("Users");
        }
        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _context.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Categories");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Albums)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            if (category.Albums != null && category.Albums.Any())
            {
                TempData["Error"] = "Cannot delete category because it is in use.";
                return RedirectToAction("Categories");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Categories");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Panel()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Public()
        {
            return View();
        }
    }
}
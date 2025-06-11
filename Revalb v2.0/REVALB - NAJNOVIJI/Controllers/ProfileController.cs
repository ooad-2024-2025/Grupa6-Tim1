using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REVALB.Data;
using REVALB.HelperClass;
using REVALB.Models;
using REVALB.Models.ViewModels;

namespace REVALB.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Me()
        {
            var user = await _userManager.Users
                .Include(u => u.FavoriteAlbums)
                .Include(u => u.Reviews)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var isArtist = userRoles.Contains("Artist");

            List<Album>? userAlbums = null;

            if (isArtist)
            {
                userAlbums = await _context.Albums
                    .Where(a => a.ArtistId == user.Id)
                    .ToListAsync();
            }

            ViewBag.IsArtist = isArtist;
            ViewBag.UserAlbums = userAlbums;

            return View(user);
        }
        [HttpGet("Profile/User/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PublicProfile(int id)
        {
            var user = await _context.Users
                .Include(u => u.Reviews)
                    .ThenInclude(r => r.Album)
                .Include(u => u.FavoriteAlbums)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.IsArtist = roles.Contains("Artist");

            var userAlbums = await _context.Albums
                .Where(a => a.ArtistId == user.Id)
                .ToListAsync();

            ViewBag.UserAlbums = userAlbums;

            return View("Public", user);


        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User model, IFormFile profileImage)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            try
            {
                if (profileImage != null && profileImage.Length > 0)
                {
                    var result = UploadHandler.Upload(profileImage, "profile");
                    if (!result.Success)
                    {
                        ModelState.AddModelError("", result.MessageOrPath);
                        return View(model);
                    }

                    user.ProfilePictureURL = result.MessageOrPath; // valid path like /uploads/profile/xyz.jpg
                }

                await _userManager.UpdateAsync(user);
                return RedirectToAction("Me");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while uploading the file: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Password changed successfully.";
                return RedirectToAction("Me");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


    }
}
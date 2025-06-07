using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Revalb.Models;

namespace Revalb.Controllers
{
    [Authorize]
    public class KorisniciController : Controller
    {
        private readonly UserManager<Korisnik> _userManager;

        public KorisniciController(UserManager<Korisnik> userManager)
        {
            _userManager = userManager;
        }

        // GET: Korisnici/Profile
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            return View(user);
        }

        // GET: Korisnici/Edit
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Korisnici/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Korisnik model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // Update only allowed fields
            user.Ime = model.Ime;
            user.Prezime = model.Prezime;
            user.Nadimak = model.Nadimak;
            user.Slika = model.Slika;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Profile));
        }
    }
}
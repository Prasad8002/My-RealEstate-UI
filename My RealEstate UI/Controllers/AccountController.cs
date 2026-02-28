using System.Web.Mvc;
using My_RealEstate_UI.Models;

namespace My_RealEstate_UI.Controllers
{
    public class AccountController : Controller
    {
        // ── GET: /Account/Login ───────────────────────────────
        [HttpGet]
        public ActionResult Login()
        {
            // Redirect if already logged in
            if (Session["UserId"] != null)
                return RedirectToAction("Index", "Home");

            return View(new LoginViewModel());
        }

        // ── POST: /Account/Login ──────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: Replace with real DB authentication
            // Example: var user = db.Users.FirstOrDefault(u => u.Email == model.Email);
            // Then verify hashed password with BCrypt or Identity

            // Dummy check for demonstration
            if (model.Email == "admin@dwello.com" && model.Password == "Admin@1234")
            {
                Session["UserId"] = 1;
                Session["UserName"] = "Admin";
                Session["UserEmail"] = model.Email;

                if (model.RememberMe)
                {
                    var cookie = new System.Web.HttpCookie("DwelloUser", model.Email)
                    {
                        Expires = System.DateTime.Now.AddDays(30)
                    };
                    Response.Cookies.Add(cookie);
                }

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Invalid email or password. Please try again.";
            return View(model);
        }

        // ── GET: /Account/Register ────────────────────────────
        [HttpGet]
        public ActionResult Register()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("Index", "Home");

            return View(new RegisterViewModel());
        }

        // ── POST: /Account/Register ───────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: Replace with real DB user creation
            // Example:
            // var user = new User {
            //     FirstName = model.FirstName,
            //     LastName  = model.LastName,
            //     Email     = model.Email,
            //     Phone     = model.Phone,
            //     Password  = BCrypt.HashPassword(model.Password),
            //     CreatedAt = DateTime.Now
            // };
            // db.Users.Add(user);
            // db.SaveChanges();

            TempData["Success"] = "Account created successfully! Please sign in.";
            return RedirectToAction("Login");
        }

        // ── GET: /Account/ForgotPassword ─────────────────────
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        // ── POST: /Account/ForgotPassword ────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: Check if email exists in DB, generate token, send email
            // Example:
            // var user = db.Users.FirstOrDefault(u => u.Email == model.Email);
            // if (user != null) { SendResetEmail(user); }

            // Always show success (security best practice - don't reveal if email exists)
            TempData["Success"] = "If that email is registered, you'll receive a reset link shortly.";
            return RedirectToAction("ForgotPassword");
        }

        // ── GET: /Account/Logout ──────────────────────────────
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            // Clear remember-me cookie
            if (Request.Cookies["DwelloUser"] != null)
            {
                var cookie = new System.Web.HttpCookie("DwelloUser")
                {
                    Expires = System.DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
            }

            TempData["Success"] = "You have been signed out successfully.";
            return RedirectToAction("Login");
        }
    }
}
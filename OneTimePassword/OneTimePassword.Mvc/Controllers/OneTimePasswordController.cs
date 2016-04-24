using OneTimePassword.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OneTimePassword.Mvc.Controllers
{
    public class OneTimePasswordController : Controller
    {
        private IPasswordService _service;

        public OneTimePasswordController(IPasswordService service)
        {
            _service = service;
        }

        // GET: OneTimePassword
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GeneratePasword(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return Json(new { status = System.Net.HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);

            var result = _service.GeneratePassword(userId);

            return Json(
                new
                {
                    status = System.Net.HttpStatusCode.OK,
                    data = result
                },
                JsonRequestBehavior.AllowGet
            );
        }

        public JsonResult IsPasswordValid(string userId, string password)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
                return Json(new { status = System.Net.HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);

            var result = _service.IsPasswordValid(userId, password);

            return Json(
                new
                {
                    status = System.Net.HttpStatusCode.OK,
                    data = result.ToString()
                },
                JsonRequestBehavior.AllowGet
            );

        }
    }
}
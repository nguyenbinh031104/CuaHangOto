using CuaHangOto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangOto.Controllers
{
    public class HomeController : Controller
    {
        // Co so du lieu
        CSDLDataContext csdl = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True");
        public ActionResult Index()
        {
            var xes = from x in csdl.Xes select x;
            return View(xes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
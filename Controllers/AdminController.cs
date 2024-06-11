using CuaHangOto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangOto.Controllers
{
    public class AdminController : Controller
    {
        // Co so du lieu
        CSDLDataContext csdl = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True");

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Trang thống kê 
        [Authorize(Roles = "Admin")] 
        public ActionResult ThongKe()
        {
            var tk = from t in csdl.HoaDons select t;
            return View(tk);
        }

        // GET: Trang khách hàng 
        [Authorize(Roles = ("Admin"))]
        public ActionResult KhachHang()
        {
            var khs = from kh in csdl.KhachHangs select kh;
            return View(khs);
        }
    }
}
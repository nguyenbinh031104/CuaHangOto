using CuaHangOto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CuaHangOto.Controllers
{
    public class AccountsController : Controller
    {
        // Co so du lieu
        CSDLDataContext csdl = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True");

        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Login(KhachHang khachHang, FormCollection collection)
        {
            var tendn = collection["Email"];
            var matkhau = collection["MatKhau"];

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Khong duoc de trong";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Khong duoc de trong";
            }
            else
            {
                var result = csdl.KhachHangs.SingleOrDefault(n => n.Email == tendn && n.MatKhau == matkhau);
                if (result != null)
                {
                    ViewBag.Thongbao = "Dang nhap thanh cong";
                    Session["Taikhoan"] = result;
                    FormsAuthentication.SetAuthCookie(khachHang.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Thongbao = "Sai tai khoan hoac mat khau";
                }
            }
            return View();
        }

        // GET: Trang dang ky 
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(KhachHang khachHang)
        {
            csdl.KhachHangs.InsertOnSubmit(khachHang);
            csdl.SubmitChanges();
            return RedirectToAction("Index", "Home");
        }

        // dang xuat
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
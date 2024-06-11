using CuaHangOto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangOto.Controllers
{
    public class GioHangController : Controller
    {
        // Co so du lieu
        CSDLDataContext csdl = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True");

        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        public List<GioHang> LayGiohang()
        {
            List<GioHang> ls = Session["Giohang"] as List<GioHang>;
            if(ls == null)
            {
                ls = new List<GioHang>();
                Session["Giohang"] = ls;
            }
            return ls;
        }

        // Them vao gio hang 
        public ActionResult ThemGiohang(int iXe, string strURL)
        {
            List<GioHang> ls = LayGiohang(); 

            GioHang sanpham = ls.Find(n => n.iXe == iXe); 
            if(sanpham == null)
            {
                sanpham = new GioHang(iXe); 
                ls.Add(sanpham); 
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        // Tong so luong 
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> ls = Session["Giohang"] as List<GioHang>; 

            if(ls != null)
            {
                iTongSoLuong = ls.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }

        // Tinh tong tien 
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> ls = Session["Giohang"] as List<GioHang>; 
            if(ls != null)
            {
                iTongTien = ls.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }

        // Trang gio hang 
        [Authorize]
        public ActionResult GioHang()
        {
            List<GioHang> ls = LayGiohang(); 

            if(ls.Count == 0)
            {
                return RedirectToAction("SanPham", "Xe"); 
            }

            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(ls);
        }

        // Xoa gio hang 
        public ActionResult XoaGiohang(int iXe)
        {
            List<GioHang> ls = LayGiohang(); 

            GioHang sanpham = ls.SingleOrDefault(n => n.iXe  == iXe); 

            if(sanpham != null)
            {
                ls.RemoveAll(n => n.iXe == iXe);
                return RedirectToAction(nameof(GioHang));
            }

            if(ls.Count == 0)
            {
                return RedirectToAction("SanPham", "Xe");
            }

            return RedirectToAction("GioHang");
        }


        // Cap nhat gio hang 
        //public ActionResult CapNhapGioHang()
        //{
        //    return View();
        //}


        // Xoa tat ca gio hang 
        public ActionResult XoaTatGioHang()
        {
            List<GioHang> ls = LayGiohang();
            ls.Clear();

            return RedirectToAction("SanPham", "Xe");
        }

        // Chuc nang dat hang 
        //public ActionResult DatHang()
        //{
        //    if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == null)
        //    {
        //        return RedirectToAction("Login", "Accounts");
        //    }

        //    if (Session["Giohang"] == null)
        //    {
        //        return RedirectToAction("SanPham", "Xe");
        //    }

        //    List<GioHang> ls = LayGiohang();
        //    return View(ls);
        //}

    }
}
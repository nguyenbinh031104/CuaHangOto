using CuaHangOto.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

using PagedList.Mvc;
using System.Diagnostics;

namespace CuaHangOto.Controllers
{
    public class XeController : Controller
    {
        // Cơ sở dữ liệu
        CSDLDataContext csdl = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True");

        // GET: Xe
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Xe()
        {
            var xes = from x in csdl.Xes select x;
            return View(xes);
        }

        // GET: Trang chi tiết xe 
        [Authorize(Roles = "Admin")]
        public ActionResult Detail(int id)
        {
            var xeId = from x in csdl.Xes where x.XeId == id select x;
            return View(xeId);
        }

        // GET: Trang chi tiết xe cho người dùng
        [Authorize(Roles = "Admin, User")]
        public ActionResult DetailSP(int id)
        {
            var xeId = from x in csdl.Xes where x.XeId == id select x;
            return View(xeId);
        }

        // GET: Trang chỉnh sửa xe
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var xeId = csdl.Xes.SingleOrDefault(n => n.XeId == id);
            // Tao dropdown list 
            ViewBag.HXId = new SelectList(csdl.HangXes.ToList().OrderBy(n => n.Ten), "HXId", "Ten");
            ViewBag.DXId = new SelectList(csdl.DongXes.ToList().OrderBy(n => n.Ten), "DXId", "Ten");

            if (xeId == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(xeId);
        }

        [HttpPost] 
        public ActionResult Edit(Xe xe, int id)
        {
            // Tao dropdown list 
            ViewBag.HXId = new SelectList(csdl.HangXes.ToList().OrderBy(n => n.Ten), "HXId", "Ten");
            ViewBag.DXId = new SelectList(csdl.DongXes.ToList().OrderBy(n => n.Ten), "DXId", "Ten");

            var xeId = csdl.Xes.SingleOrDefault(n => n.XeId == id);
            xeId.XeId = id;

            if (ModelState.IsValid)
            {
                // Kiểm tra hình ảnh hoa có được upload hay không
                if (Request.Files.Count > 0 && Request.Files[0] != null)
                {
                    var file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        // Lưu tên file hình ảnh vào đối tượng hoa
                        xe.HinhAnh = Path.GetFileName(file.FileName);

                        // Lưu file hình ảnh vào thư mục ảnh trên server
                        var path = Path.Combine(Server.MapPath("~/HinhAnh/"), xe.HinhAnh);
                        file.SaveAs(path);
                    }
                }
                
                // Gán các giá trị cho các cột và lưu lại
                // Không dùng upadtemodel vì nó chỉ dùng cho formcollection
                xeId.Ten = xe.Ten;
                xeId.Gia = xe.Gia;
                xeId.HinhAnh = xe.HinhAnh;
                xeId.NhienLieu = xe.NhienLieu;
                xeId.Mau = xe.Mau;
                xeId.Mota = xe.Mota;
                xeId.NamSX = xe.NamSX;
                xeId.SoKM = xe.SoKM;
                xeId.HXId = xe.HXId;
                xeId.DXId = xe.DXId;

                csdl.SubmitChanges();
                return RedirectToAction(nameof(Xe));
            } 
            return View(xe);
        }

        // GET: Trang sản phẩm xe dùng cho người dùng
        // Hàm lấy xe mưới cho phân trang
        private List<Xe> LayXeMoi(int count)
        {
            return csdl.Xes.OrderByDescending(a => a.XeId).Take(count).ToList();
        }
        public ActionResult SanPham(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var xemoi = LayXeMoi(6); 

            return View(xemoi.ToPagedList(pageNum, pageSize));
        }

        // GET: Trang thêm xe 
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Create()
        {
            // Tạo dropdown list 
            ViewBag.HXId = new SelectList(csdl.HangXes.ToList().OrderBy(n => n.Ten), "HXId", "Ten");
            ViewBag.DXId = new SelectList(csdl.DongXes.ToList().OrderBy(n => n.Ten), "DXId", "Ten");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Xe xe)
        {
            // Tao dropdown list 
            ViewBag.HXId = new SelectList(csdl.HangXes.ToList().OrderBy(n => n.Ten), "HXId", "Ten");
            ViewBag.DXId = new SelectList(csdl.DongXes.ToList().OrderBy(n => n.Ten), "DXId", "Ten");


            if (ModelState.IsValid)
            {
                // Kiểm tra hình ảnh hoa có được upload hay không
                if (Request.Files.Count > 0 && Request.Files[0] != null)
                {
                    var file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        // Lưu tên file hình ảnh vào đối tượng hoa
                        xe.HinhAnh = Path.GetFileName(file.FileName);

                        // Lưu file hình ảnh vào thư mục ảnh trên server
                        var path = Path.Combine(Server.MapPath("~/HinhAnh/"), xe.HinhAnh);
                        file.SaveAs(path);
                    }
                }

                // Thêm xe mới
                csdl.Xes.InsertOnSubmit(xe);
                csdl.SubmitChanges();
                return RedirectToAction(nameof(Xe));
            }
            return View(xe);
        }

        // GET: Trang xóa xe 
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var xeId = csdl.Xes.SingleOrDefault(n => n.XeId == id); 
            if (xeId == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(xeId);
        }

        [HttpPost, ActionName("Delete")] 
        public ActionResult DeleteConfirm(int? id)
        {
            var xeId = csdl.Xes.SingleOrDefault(n => n.XeId == id);

            if (xeId == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            csdl.Xes.DeleteOnSubmit(xeId);
            csdl.SubmitChanges();

            return RedirectToAction(nameof(Xe));
        }

        // GET: Trang xe theo dòng xe 
        public ActionResult XeDongXe()
        {
            var dongXe = from dx in csdl.DongXes select dx; 
            return View(dongXe);
        }

        // Get: Trang xe theo dòng xe
        public ActionResult XeTheoDongXe(int? id)
        {
            var dongXe = from dx in csdl.Xes where dx.DXId == id select dx;
            return View(dongXe);
        }

        // GET: Trang xe theo hãng xe 
        public ActionResult XeHangXe()
        {
            var hangXe = from hx in csdl.HangXes select hx; 
            return View(hangXe);
        }

        // Get: Trang xe theo hãng xe
        public ActionResult XeTheoHangXe(int? id)
        {
            var hangXe = from dx in csdl.Xes where dx.HXId == id select dx;
            return View(hangXe);
        }
    }
}
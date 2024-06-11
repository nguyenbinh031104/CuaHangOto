using CuaHangOto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangOto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DongXeController : Controller
    {
        // Co so du lieu
        CSDLDataContext csdl = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True");

        // GET: DongXe
        public ActionResult DongXe()
        {
            var dongXes = from dx in csdl.DongXes select dx;
            return View(dongXes);
        }

        // GET: Trang chi tiet dong xe 
        public ActionResult Detail(int id)
        {
            var dongXeId = from dx in csdl.DongXes where dx.DXId == id select dx;
            return View(dongXeId);
        }

        // GET: Trang them dong xe 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, DongXe dongXe)
        {
            // Tạo biến CB_Loaitin và gán giá trị của người dùng nhập vào từ
            //form trong trang Create.aspx
            var ten = collection["Ten"];
            var mota = collection["Mota"];
            //Nếu CB_Loaitin có giá trị == null ( để trống )
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Loi"] = "Không được để trống ";
            }
            else if (string.IsNullOrEmpty(mota))
            {
                ViewData["Loi"] = "Không được để trống ";
            }
            else
            {
                dongXe.Ten = ten;
                dongXe.Mota = mota;
                csdl.DongXes.InsertOnSubmit(dongXe);
                //Thực hiện tạo mới
                csdl.SubmitChanges();
                return RedirectToAction(nameof(DongXe));
            }
            return this.Create();
        }

        // GET: Trang chinh sua dong xe 
        public ActionResult Edit(int id)
        {
            var dongXeId = csdl.DongXes.First(m => m.DXId == id);
            return View(dongXeId);
        }

        // POST : Trang chinh sua dong xe 
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var dongXeId = csdl.DongXes.First(m => m.DXId == id);

            var ten = collection["Ten"];
            var mota = collection["Mota"];

            dongXeId.DXId = id;

            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Loi"] = "Không được để trống ";
            }
            else if (string.IsNullOrEmpty(mota))
            {
                ViewData["Loi"] = "Không được để trống ";
            }
            else
            {
                dongXeId.Ten = ten;
                dongXeId.Mota = mota;

                // Thực hiện update
                UpdateModel(dongXeId);
                csdl.SubmitChanges();
                return RedirectToAction(nameof(DongXe));
            }
            return this.Edit(id);
        }

        // GET : Trang xoa dong xe 
        public ActionResult Delete(int id)
        {
            var dongXeId = csdl.DongXes.First(m => m.DXId == id);
            return View(dongXeId);
        }

        // POST : Trang xoa dong xe 
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Lay ID
            var dongXeId = csdl.DongXes.Where(m => m.DXId == id).First();

            // Xóa
            csdl.DongXes.DeleteOnSubmit(dongXeId);
            csdl.SubmitChanges();
            return RedirectToAction(nameof(DongXe));
        }
    }
}
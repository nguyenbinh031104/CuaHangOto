using CuaHangOto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangOto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HangXeController : Controller
    {
        CSDLDataContext CSDL = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True");
        
        // GET: HangXe
        public ActionResult HangXe()
        {
            var hangXes = from hx in CSDL.HangXes select hx;
            return View(hangXes);
        }

        // GET: Trang chi tiet hang xe 
        public ActionResult Detail(int id)
        {
            var hangXeId = from hx in CSDL.HangXes where hx.HXId == id select hx;
            return View(hangXeId);
        }

        // GET: Trang them hang xe 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, HangXe hangXe)
        {
            // Tạo biến CB_Loaitin và gán giá trị của người dùng nhập vào từ
            //form trong trang Create.aspx
            var CB_Hangxe = collection["Ten"];
            var mota = collection["Mota"];
            //Nếu CB_Loaitin có giá trị == null ( để trống )
            if (string.IsNullOrEmpty(CB_Hangxe))
            {
                ViewData["Loi"] = "Không được để trống ";
            }
            else
            {
                hangXe.Ten = CB_Hangxe; 
                hangXe.Mota = mota;
                CSDL.HangXes.InsertOnSubmit(hangXe);
                //Thực hiện tạo mới
                CSDL.SubmitChanges();
                return RedirectToAction("HangXe");
            }
            return this.Create();
        }

        // GET: Trang chinh sua hang xe 
        public ActionResult Edit(int id)
        {
            var hangXeId = CSDL.HangXes.First(m => m.HXId == id);
            return View(hangXeId);
        }

        // POST : Trang chinh sua hang xe 
        [HttpPost]       
        public ActionResult Edit(int id, FormCollection collection)
        {
            var hangXeId = CSDL.HangXes.First(m => m.HXId == id);
            var ten = collection["Ten"];
            var mota = collection["Mota"];

            hangXeId.HXId = id;

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
                hangXeId.Ten = ten;
                hangXeId.Mota = mota;
                // Thực hiện updat
                UpdateModel(hangXeId);
                CSDL.SubmitChanges();
                return RedirectToAction("HangXe");
            }

            return this.Edit(id);
        }

        // GET : Trang xoa hang xe 
        public ActionResult Delete(int id)
        {
            var hangXeId = CSDL.HangXes.First(m => m.HXId == id);
            return View(hangXeId);
        }

        // POST : Trang xoa hang xe 
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Tạo biến D_Tin gán với dối dượng có ID bằng với ID tham số
            var hangXeId = CSDL.HangXes.Where(m => m.HXId == id).First();
            //xóa
            CSDL.HangXes.DeleteOnSubmit(hangXeId);
            CSDL.SubmitChanges();
            return RedirectToAction("HangXe");
        }
    }
}
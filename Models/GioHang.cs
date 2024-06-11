using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangOto.Models
{
    public class GioHang
    {
        // Co so du lieu
        CSDLDataContext csdl = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True"); 

        public int iXe { get; set; }     
        public string xTen { get; set; } 
        public string xHinhAnh { get; set; } 
        public Double dPhuPhi { get; set; } 
        public Double dThue {  set; get; } 
        public Double xGia {  get; set; } 
        public int iSoLuong { get; set; }

        public Double dThanhTien
        {
            get { return dPhuPhi + dThue + xGia; }
        }

        public GioHang(int XEId)
        {
            iXe = XEId;
            Xe xe = csdl.Xes.Single(n => n.XeId == iXe);
            xTen = xe.Ten;
            xHinhAnh = xe.HinhAnh;
            xGia = double.Parse(xe.Gia.ToString());
            iSoLuong = 1;
        }
    }
}
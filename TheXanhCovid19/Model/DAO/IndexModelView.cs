using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheXanhCovid19.Common
{
    public class IndexModelView
    {
        public tblNguoiDung ngModel { get; set; }

        public tblQRCode QRModel { get; set; }
    }
}
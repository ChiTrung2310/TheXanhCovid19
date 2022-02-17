using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheXanhCovid19.Common
{
    [Serializable]
    public class AdminLogin
    {
        public int MaQT { set; get; }
        public string UserName { set; get; }
    }
}
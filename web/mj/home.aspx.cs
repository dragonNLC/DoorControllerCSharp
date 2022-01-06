using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class home : userpagebase
    {
        public Dictionary<int, string> lclist;
        protected void Page_Load(object sender, EventArgs e)
        {
            lclist = Bll.devicemanger.lcpzz;

        }
    }
}
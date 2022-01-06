using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class xzmsj : userpagebase
    {
        public string doorjsons = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Bll.devicemanger dvmg = new Bll.devicemanger();


            doorjsons=Bll.helper.tojson(dvmg.DoorDetail_query());


        }
    }
}
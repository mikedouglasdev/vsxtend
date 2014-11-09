using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Vsxtend.Samples.Mvc.Controllers
{
    public class BuildController : Controller
    {
        //
        // GET: /Build/

        public async Task<ActionResult> Index()
        {
            //TfsService client = new TfsService();
            //string response = await client.GetBuilds();
            return View();
        }

    }
}

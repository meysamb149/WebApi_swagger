using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi_swagger.Controllers
{
    public class zarinpalController : Controller
    {
        // GET: zarinpal
        public ActionResult Index()
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();
            string Authority;

            int Status = zp.PaymentRequest("6576b501-0b36-430d-83af-9761ba88fb95", int.Parse("100"), "خشکشویی آنلاین هلکو", "you@yoursite.com", "09190608912", "http://onlinurlspelatform.setapi.ir/Verify/Index", out Authority);

            if (Status == 100)
            {
                Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
            }
            else
            {
                return View("error: " + Status);
                //Response.Write("error: " + Status);
            }
            return View();
        }
    }

}

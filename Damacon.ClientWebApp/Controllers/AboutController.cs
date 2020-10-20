using Damacon.ClientWebApp.Helpers;
using Damacon.ClientWebApp.Models;
using Damacon.DAL;
using Damacon.DAL.Operations.Contracts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MarkupConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Damacon.ClientWebApp.Controllers
{
    [AllowAnonymous]
    public class AboutController : BaseController
    {
        public ActionResult StoreList()
        {
           
            return View();
        }

        

        public ActionResult PrivacyPolicy()
        {
            string cultureName = RouteData.Values["culture"] as string;
            // Attempt to read the culture cookie from Request
            if (cultureName == null)
            {
                return View();
            }
            string path = null;

            if (cultureName.IndexOf("it", StringComparison.InvariantCultureIgnoreCase) > 0)
            {
                path = Server.MapPath("~\\AboutFiles\\Privacy_IT.rtf"); ;
            }
            else
            {
                path = Server.MapPath("~\\AboutFiles\\Privacy_EN.rtf"); ;
            }

            LoadRtfData(path);
            return View();
        }

        public ActionResult Regulations()
        {
            string cultureName = RouteData.Values["culture"] as string;
            // Attempt to read the culture cookie from Request
            if (cultureName == null)
            {
                return View();
            }
            string path = null;

            if (cultureName.IndexOf("it", StringComparison.InvariantCultureIgnoreCase) > 0)
            {
                path = Server.MapPath("~\\AboutFiles\\REGOLAMENTO_IT.rtf"); ;
            }
            else
            {
                path = Server.MapPath("~\\AboutFiles\\REGOLAMENTO_EN.rtf"); ;
            }

            LoadRtfData(path);
            return View();
        }

        private void LoadRtfData(string path)
        {
            var thread = new Thread(ConvertRtfInSTAThread);
            var threadData = new ConvertRtfThreadData { RtfText = System.IO.File.ReadAllText(path) };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(threadData);
            thread.Join();
            string html = threadData.HtmlText;
            ViewBag.EncodedHtml = MvcHtmlString.Create(html);
        }

        private void ConvertRtfInSTAThread(object rtf)
        {
            var threadData = rtf as ConvertRtfThreadData;
            threadData.HtmlText = RtfToHtmlConverter.ConvertRtfToHtml(threadData.RtfText);
        }


        private class ConvertRtfThreadData
        {
            public string RtfText { get; set; }
            public string HtmlText { get; set; }
        }
    }
}
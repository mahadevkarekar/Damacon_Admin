using MarkupConverter;
using System.Threading;
using System.Web.Mvc;
using System;

namespace Damacon.WebApp.Controllers
{
    public class AboutController : BaseController
    {
        // GET: About
        public ActionResult Version()
        {
            string cultureName = RouteData.Values["culture"] as string;
            // Attempt to read the culture cookie from Request
            if (cultureName ==null)
            {
                return View();
            }
            string path = null;

            if (cultureName.IndexOf("it", StringComparison.InvariantCultureIgnoreCase) > 0)
            {
                path = Server.MapPath("~\\AboutFiles\\Note legali_IT.rtf"); ;
            }
            else
            {
                path = Server.MapPath("~\\AboutFiles\\Note legali_EN.rtf"); ;
            }
            
            var thread = new Thread(ConvertRtfInSTAThread);
            var threadData = new ConvertRtfThreadData { RtfText = System.IO.File.ReadAllText(path) };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(threadData);
            thread.Join();
            string html = threadData.HtmlText;
            ViewBag.EncodedHtml = MvcHtmlString.Create(html);
            return View();
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
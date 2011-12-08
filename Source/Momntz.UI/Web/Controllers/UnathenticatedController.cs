using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Momntz.UI.Web.ActionResults;

namespace Momntz.UI.Web.Controllers
{
    public class UnAuthenticatedController : Controller
    {
        //protected AutoMapViewResult AutoMapView<TDestination>(ViewResult viewResult)
        //{
        //    return new AutoMapViewResult(viewResult.ViewData.Model.GetType(), typeof(TDestination), viewResult);
        //}

        protected FormActionResult<TForm> Form<TForm>(TForm form, ActionResult success)
        {
            var failure = View(form);
            return new FormActionResult<TForm>(form, success, failure);
        }

        //protected XmlResult<T> Xml<T>(T toSerialize)
        //{
        //    return new XmlResult<T>
        //    {
        //        ModelToSerialize = toSerialize
        //    };
        //}

        protected ActionResult Query<TDestination>(ViewResult viewResult)
        {
            return viewResult;
        }
    }
}
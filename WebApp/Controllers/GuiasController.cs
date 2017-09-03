﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GuiasController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Guias()
        {
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

        public ActionResult GUIA_LOGISTIC_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<vis_GUIAS> guia_logistic = db.vis_GUIAS;
            DataSourceResult result = guia_logistic.ToDataSourceResult(request, gUIA_LOGISTIC => new {
                guia_id = gUIA_LOGISTIC.guia_id,
                guia_origen = gUIA_LOGISTIC.guia_origen,
                origen = gUIA_LOGISTIC.origen,
                guia_destino = gUIA_LOGISTIC.guia_destino,
                destino = gUIA_LOGISTIC.destino,
                guia_code = gUIA_LOGISTIC.guia_code,
                guia_date = gUIA_LOGISTIC.guia_date,
                tran_id = gUIA_LOGISTIC.tran_id,
                tran_name = gUIA_LOGISTIC.tran_name
            });

            return Json(result);
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

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
    public class GridController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GUIA_LOGISTIC_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<GUIA_LOGISTIC> guia_logistic = db.GUIA_LOGISTIC;
            DataSourceResult result = guia_logistic.ToDataSourceResult(request, gUIA_LOGISTIC => new {
                guia_id = gUIA_LOGISTIC.guia_id,
                guia_origen = gUIA_LOGISTIC.guia_origen,
                guia_destino = gUIA_LOGISTIC.guia_destino,
                guia_code = gUIA_LOGISTIC.guia_code,
                guia_date = gUIA_LOGISTIC.guia_date,
                tras_id = gUIA_LOGISTIC.tras_id,
                guia_status = gUIA_LOGISTIC.guia_status,
                guia_lat = gUIA_LOGISTIC.guia_lat,
                guia_lon = gUIA_LOGISTIC.guia_lon,
                guia_latend = gUIA_LOGISTIC.guia_latend,
                guia_lonend = gUIA_LOGISTIC.guia_lonend
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

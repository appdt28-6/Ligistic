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
    public class DestinosController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Index()
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

        public ActionResult DESTINO_LOGISTIC_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<DESTINO_LOGISTIC> destino_logistic = db.DESTINO_LOGISTIC;
            DataSourceResult result = destino_logistic.ToDataSourceResult(request, dESTINO_LOGISTIC => new {
                dest_id = dESTINO_LOGISTIC.dest_id,
                dest_name = dESTINO_LOGISTIC.dest_name,
                dest_addres = dESTINO_LOGISTIC.dest_addres
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DESTINO_LOGISTIC_Create([DataSourceRequest]DataSourceRequest request, DESTINO_LOGISTIC dESTINO_LOGISTIC)
        {
            if (ModelState.IsValid)
            {
                var entity = new DESTINO_LOGISTIC
                {
                    dest_name = dESTINO_LOGISTIC.dest_name,
                    dest_addres = dESTINO_LOGISTIC.dest_addres
                };

                db.DESTINO_LOGISTIC.Add(entity);
                db.SaveChanges();
                dESTINO_LOGISTIC.dest_id = entity.dest_id;
            }

            return Json(new[] { dESTINO_LOGISTIC }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DESTINO_LOGISTIC_Update([DataSourceRequest]DataSourceRequest request, DESTINO_LOGISTIC dESTINO_LOGISTIC)
        {
            if (ModelState.IsValid)
            {
                var entity = new DESTINO_LOGISTIC
                {
                    dest_id = dESTINO_LOGISTIC.dest_id,
                    dest_name = dESTINO_LOGISTIC.dest_name,
                    dest_addres = dESTINO_LOGISTIC.dest_addres
                };

                db.DESTINO_LOGISTIC.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { dESTINO_LOGISTIC }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DESTINO_LOGISTIC_Destroy([DataSourceRequest]DataSourceRequest request, DESTINO_LOGISTIC dESTINO_LOGISTIC)
        {
            if (ModelState.IsValid)
            {
                var entity = new DESTINO_LOGISTIC
                {
                    dest_id = dESTINO_LOGISTIC.dest_id,
                    dest_name = dESTINO_LOGISTIC.dest_name,
                    dest_addres = dESTINO_LOGISTIC.dest_addres
                };

                db.DESTINO_LOGISTIC.Attach(entity);
                db.DESTINO_LOGISTIC.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { dESTINO_LOGISTIC }.ToDataSourceResult(request, ModelState));
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

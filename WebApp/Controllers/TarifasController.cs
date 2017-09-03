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
    public class TarifasController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Tarifas()
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

        public ActionResult DESTINO_LOGISTIC_COSTO_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<vis_Tarifas> destino_logistic_costo = db.vis_Tarifas;
            DataSourceResult result = destino_logistic_costo.ToDataSourceResult(request, dESTINO_LOGISTIC_COSTO => new {
                cost_id = dESTINO_LOGISTIC_COSTO.cost_id,
                dest_ini = dESTINO_LOGISTIC_COSTO.dest_ini,
                origen = dESTINO_LOGISTIC_COSTO.origen,
                dest_end = dESTINO_LOGISTIC_COSTO.dest_end,
                destino = dESTINO_LOGISTIC_COSTO.destino,
                dest_cost = dESTINO_LOGISTIC_COSTO.dest_cost,
                dest_cost_redondo = dESTINO_LOGISTIC_COSTO.dest_cost_redondo
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DESTINO_LOGISTIC_COSTO_Create([DataSourceRequest]DataSourceRequest request, vis_Tarifas dESTINO_LOGISTIC_COSTO)
        {
            if (ModelState.IsValid)
            {
                var entity = new DESTINO_LOGISTIC_COSTO
                {
                    dest_ini = dESTINO_LOGISTIC_COSTO.dest_ini,
                    dest_end = dESTINO_LOGISTIC_COSTO.dest_end,
                    dest_cost = dESTINO_LOGISTIC_COSTO.dest_cost,
                    dest_cost_redondo = dESTINO_LOGISTIC_COSTO.dest_cost_redondo
                };

                db.DESTINO_LOGISTIC_COSTO.Add(entity);
                db.SaveChanges();
                dESTINO_LOGISTIC_COSTO.cost_id = entity.cost_id;
            }

            return Json(new[] { dESTINO_LOGISTIC_COSTO }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DESTINO_LOGISTIC_COSTO_Update([DataSourceRequest]DataSourceRequest request, vis_Tarifas dESTINO_LOGISTIC_COSTO)
        {
            if (ModelState.IsValid)
            {
                var entity = new DESTINO_LOGISTIC_COSTO
                {
                    cost_id = dESTINO_LOGISTIC_COSTO.cost_id,
                    dest_ini = dESTINO_LOGISTIC_COSTO.dest_ini,
                    dest_end = dESTINO_LOGISTIC_COSTO.dest_end,
                    dest_cost = dESTINO_LOGISTIC_COSTO.dest_cost,
                    dest_cost_redondo = dESTINO_LOGISTIC_COSTO.dest_cost_redondo
                };

                db.DESTINO_LOGISTIC_COSTO.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { dESTINO_LOGISTIC_COSTO }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DESTINO_LOGISTIC_COSTO_Destroy([DataSourceRequest]DataSourceRequest request, vis_Tarifas dESTINO_LOGISTIC_COSTO)
        {
            if (ModelState.IsValid)
            {
                var entity = new DESTINO_LOGISTIC_COSTO
                {
                    cost_id = dESTINO_LOGISTIC_COSTO.cost_id,
                    dest_ini = dESTINO_LOGISTIC_COSTO.dest_ini,
                    dest_end = dESTINO_LOGISTIC_COSTO.dest_end,
                    dest_cost = dESTINO_LOGISTIC_COSTO.dest_cost,
                    dest_cost_redondo = dESTINO_LOGISTIC_COSTO.dest_cost_redondo
                };

                db.DESTINO_LOGISTIC_COSTO.Attach(entity);
                db.DESTINO_LOGISTIC_COSTO.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { dESTINO_LOGISTIC_COSTO }.ToDataSourceResult(request, ModelState));
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

        public ActionResult Get_Origen()
        {

            var inst = db.DESTINO_LOGISTIC.ToList();

            return Json(inst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Destino()
        {

            var inst = db.DESTINO_LOGISTIC.ToList();

            return Json(inst, JsonRequestBehavior.AllowGet);
        }
    }
}

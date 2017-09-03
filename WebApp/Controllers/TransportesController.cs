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
    public class TransportesController : Controller
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

        public ActionResult TRANS_LOGISTIC_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<TRANS_LOGISTIC> trans_logistic = db.TRANS_LOGISTIC;
            DataSourceResult result = trans_logistic.ToDataSourceResult(request, tRANS_LOGISTIC => new {
                tran_id = tRANS_LOGISTIC.tran_id,
                tran_name = tRANS_LOGISTIC.tran_name,
                tran_phone = tRANS_LOGISTIC.tran_phone,
                tran_user = tRANS_LOGISTIC.tran_user,
                tran_password = tRANS_LOGISTIC.tran_password
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TRANS_LOGISTIC_Create([DataSourceRequest]DataSourceRequest request, TRANS_LOGISTIC tRANS_LOGISTIC)
        {
            if (ModelState.IsValid)
            {
                var entity = new TRANS_LOGISTIC
                {
                    tran_name = tRANS_LOGISTIC.tran_name,
                    tran_phone = tRANS_LOGISTIC.tran_phone,
                    tran_user = tRANS_LOGISTIC.tran_user,
                    tran_password = tRANS_LOGISTIC.tran_password
                };

                db.TRANS_LOGISTIC.Add(entity);
                db.SaveChanges();
                tRANS_LOGISTIC.tran_id = entity.tran_id;
            }

            return Json(new[] { tRANS_LOGISTIC }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TRANS_LOGISTIC_Update([DataSourceRequest]DataSourceRequest request, TRANS_LOGISTIC tRANS_LOGISTIC)
        {
            if (ModelState.IsValid)
            {
                var entity = new TRANS_LOGISTIC
                {
                    tran_id = tRANS_LOGISTIC.tran_id,
                    tran_name = tRANS_LOGISTIC.tran_name,
                    tran_phone = tRANS_LOGISTIC.tran_phone,
                    tran_user = tRANS_LOGISTIC.tran_user,
                    tran_password = tRANS_LOGISTIC.tran_password
                };

                db.TRANS_LOGISTIC.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { tRANS_LOGISTIC }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TRANS_LOGISTIC_Destroy([DataSourceRequest]DataSourceRequest request, TRANS_LOGISTIC tRANS_LOGISTIC)
        {
            if (ModelState.IsValid)
            {
                var entity = new TRANS_LOGISTIC
                {
                    tran_id = tRANS_LOGISTIC.tran_id,
                    tran_name = tRANS_LOGISTIC.tran_name,
                    tran_phone = tRANS_LOGISTIC.tran_phone,
                    tran_user = tRANS_LOGISTIC.tran_user,
                    tran_password = tRANS_LOGISTIC.tran_password
                };

                db.TRANS_LOGISTIC.Attach(entity);
                db.TRANS_LOGISTIC.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { tRANS_LOGISTIC }.ToDataSourceResult(request, ModelState));
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

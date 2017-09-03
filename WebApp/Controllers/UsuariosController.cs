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
    public class UsuariosController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Usuarios()
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

        public ActionResult USUARIOS_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<USUARIOS_LOGISTIC> usuarios = db.USUARIOS_LOGISTIC;
            DataSourceResult result = usuarios.ToDataSourceResult(request, uSUARIOS => new {
                user_id = uSUARIOS.user_id,
                user_name = uSUARIOS.user_name,
                user_phone = uSUARIOS.user_phone,
                user_mail = uSUARIOS.user_mail,
                user_user = uSUARIOS.user_user,
                user_password = uSUARIOS.user_password,
                user_role = uSUARIOS.user_role
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIOS_Create([DataSourceRequest]DataSourceRequest request, USUARIOS_LOGISTIC uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new USUARIOS_LOGISTIC
                {
                    user_name = uSUARIOS.user_name,
                    user_phone = uSUARIOS.user_phone,
                    user_mail = uSUARIOS.user_mail,
                    user_user = uSUARIOS.user_user,
                    user_password = uSUARIOS.user_password,
                    user_role = uSUARIOS.user_role
                };

                db.USUARIOS_LOGISTIC.Add(entity);
                db.SaveChanges();
                uSUARIOS.user_id = entity.user_id;
            }

            return Json(new[] { uSUARIOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIOS_Update([DataSourceRequest]DataSourceRequest request, USUARIOS_LOGISTIC uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new USUARIOS_LOGISTIC
                {
                    user_id = uSUARIOS.user_id,
                    user_name = uSUARIOS.user_name,
                    user_phone = uSUARIOS.user_phone,
                    user_mail = uSUARIOS.user_mail,
                    user_user = uSUARIOS.user_user,
                    user_password = uSUARIOS.user_password,
                    user_role = uSUARIOS.user_role
                };

                db.USUARIOS_LOGISTIC.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { uSUARIOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIOS_Destroy([DataSourceRequest]DataSourceRequest request, USUARIOS_LOGISTIC uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new USUARIOS_LOGISTIC
                {
                    user_id = uSUARIOS.user_id,
                    user_name = uSUARIOS.user_name,
                    user_phone = uSUARIOS.user_phone,
                    user_mail = uSUARIOS.user_mail,
                    user_user = uSUARIOS.user_user,
                    user_password = uSUARIOS.user_password,
                    user_role = uSUARIOS.user_role
                };

                db.USUARIOS_LOGISTIC.Attach(entity);
                db.USUARIOS_LOGISTIC.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { uSUARIOS }.ToDataSourceResult(request, ModelState));
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

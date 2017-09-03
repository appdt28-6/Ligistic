using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
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

        public ActionResult GUIA_LOGISTIC_Read([DataSourceRequest]DataSourceRequest request)
        {

            DateTime enteredDate = DateTime.Today;
            IQueryable<vis_GUIAS> guia_logistic = db.vis_GUIAS.Where(q=>q.guia_date== enteredDate && q.guia_status == 0);
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

        public ActionResult GUIA_LOGISTIC_End_Read([DataSourceRequest]DataSourceRequest request)
        {

            DateTime enteredDate = DateTime.Today;
            IQueryable<vis_GUIAS> guia_logistic = db.vis_GUIAS.Where(q => q.guia_date == enteredDate && q.guia_status==1);
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
        public ActionResult Login()
        {
            ViewBag.Message = "Login";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model, string returnUrl)
        {

            var role_id = IsValidUser(model.UserName, model.Password);
            var user_id = model.UserName;
            int comp = Convert.ToInt32(Session["comp_id"].ToString());

            if (ModelState.IsValid && Session["role_id"] != null)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                //return RedirectToLocal(returnUrl);
               
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
              
            }
            else
            {
                // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
                //ModelState.AddModelError(key: "", errorMessage: "El nombre de usuario o la contraseña especificados son incorrectos o la conexión no se pudo establecer.\n Verifique conexón/Usario y password");
                // ModelState.AddModelError(key: "", errorMessage: "Usuario o contraseña son incorrectos o la conexión no se pudo establecer.");
                ViewData["Error"] = "Usuario o contraseña son incorrectos o la conexión no se pudo establecer.";
            }

            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))            

            return View(model);
        }
        protected string IsValidUser(string username, string password)
        {
            var role_id = "";

            try
            {

                //Creamos la conexion con la cadena especificada en el Context
                using (db)
                {
                    //Recuperamos los datos del SP
                    var user = db.USUARIOS_LOGISTIC.Where(u => u.user_user == username && u.user_password == password);
                    //Recorremos el resultado para validar la informacion
                    foreach (var result in user)
                    {
                        if (result.user_role != "")
                        {
                            Session["user_id"] = username;
                            Session["role_id"] = result.user_role;
                            Session["comp_id"] = result.comp_id;
                            //Session["comp_identifier"] = result.Usua_Id.ToString();
                            role_id = result.user_role;
                        }
                    }
                }

            }
            catch (Exception q)
            {
                role_id = null;

            }
            return role_id;
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

    }
}

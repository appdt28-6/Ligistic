using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ResumenGuiasController : Controller
    {
        private AppDTEntities db = new AppDTEntities();
        // GET: ResumenGuias
        public ActionResult Resumen()
        {
            return View();
        }

        public ActionResult GUIA_LOGISTIC_Read([DataSourceRequest]DataSourceRequest request,string Dateini,string Dataend)
        {
           // ViewData["ResumenGuias"] = db.ps_GUIAS_RESUMEN(Dateini, Dataend).ToList();

            IQueryable<ps_GUIAS_RESUMEN_Result> guia_logistic = db.ps_GUIAS_RESUMEN(Dateini, Dataend).ToList().AsQueryable();
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

            //return PartialView("_Resumen");
        }
    }
}
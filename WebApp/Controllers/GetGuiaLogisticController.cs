using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GetGuiaLogisticController : ApiController
    {
        public HttpResponseMessage Index(int transid, int origenid, int destinoid,string code,string lat,string lon)
        {
            string message = "";
            using (AppDTEntities db = new AppDTEntities())
            {
                try
                {
                   // var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd HH:mm");
                    var entity = new GUIA_LOGISTIC
                    {
                        tras_id = transid,
                        guia_origen = origenid,
                        guia_destino = destinoid,
                        guia_code = code,
                        guia_date = DateTime.Now,
                        guia_status = 0,
                        guia_lat=lat,
                        guia_lon=lon
                        
                    };

                    db.GUIA_LOGISTIC.Add(entity);
                    db.SaveChanges();

                    message = "ok";
                }
                catch (Exception e)
                {
                    message = e.ToString();
                }

            };

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("[{\"Succes\":\"" + message + "\"}]")
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }
    }
}

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
    public class GetGuiaLogisticEndController : ApiController
    {
        public HttpResponseMessage Index(int transid, string latend, string lonend)
        {
            string message = "";
            var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
            using (AppDTEntities db = new AppDTEntities())
            {
                try
                {
                    // var id = db.GUIA_LOGISTIC.Where(q => q.tras_id == transid && q.guia_date==fecha && q.guia_status == 0).ToList();
                    var id = db.GUIA_LOGISTIC.Where(q=>q.tras_id==transid && q.guia_status==0).Max(q=>q.guia_id);
                    var stud = (from s in db.GUIA_LOGISTIC
                                    where s.guia_id == id
                                    select s).FirstOrDefault();

                        stud.guia_latend = latend;
                        stud.guia_lonend = lonend;
                        stud.guia_status = 1;

                    int num = db.SaveChanges();
                    

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

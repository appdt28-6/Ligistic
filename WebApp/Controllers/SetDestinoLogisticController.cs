using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SetDestinoLogisticController : ApiController
    {
        AppDTEntities db = new AppDTEntities();
       
        public IEnumerable<DESTINO_LOGISTIC_Return> Index()
        {
            var result = (from g in db.DESTINO_LOGISTIC.OrderBy(q=>q.dest_name)
                          select new DESTINO_LOGISTIC_Return
                          {
                              dest_id = g.dest_id,
                              dest_name = g.dest_name,
                              dest_addres = g.dest_addres
                          }).ToList();
            return result;
        }
    }
}

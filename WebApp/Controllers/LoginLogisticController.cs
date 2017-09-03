using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class LoginLogisticController : ApiController
    {
        public List<Login_Logistic_Return> Index(string user, string password)
        {
            using (AppDTEntities db = new AppDTEntities())
            {
                return db.TRANS_LOGISTIC.Where(q => q.tran_user == user && q.tran_password == password).Select(barber => new Login_Logistic_Return()
                {
                    tran_id = barber.tran_id,
                    tran_name = barber.tran_name,
                    tran_phone = barber.tran_phone
                }).ToList();
            };
        }
    }
}

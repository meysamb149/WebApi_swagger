using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using WebApi_swagger.Models;


namespace WebApi_swagger.Controllers
{
    public class TLaw_MoshtariController : ApiController
    {
        [HttpGet]
        [Route("api/G1/GetListTLaw_Moshtari/{Unique_code}")]
        public IHttpActionResult GetListTLaw_Moshtari(string Unique_code)
        {
            if (Unique_code != "PlatformV199325694")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            int maxOrder = session.Query<TLaw>().Max(x => x.IdLaw);
            var ListApplicationVersion = session.Query<TLaw>()
                                         .Where(p =>  p.LawtypeStr== "moshtari")
                                         .Select(x =>
           new {
                    LawId = x.IdLaw,
                    LawText = x.LawTextStr
            }).OrderByDescending(a => a.LawId).Take(1); ;
                                         
            var ListApplicationVersion2 = ListApplicationVersion.ToList();
            var json = JsonConvert.SerializeObject(new
            {
                MyLaw = ListApplicationVersion2
            });

            JObject json5 = JObject.Parse(json);
            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetListTLaw_Services/{Unique_code}")]
        public IHttpActionResult GetListTLaw_Services(string Unique_code)
        {
            if (Unique_code != "PlatformV199325694")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            int maxOrder = session.Query<TLaw>().Max(x => x.IdLaw);
            var ListApplicationVersion = session.Query<TLaw>()
                                         .Where(p =>  p.LawtypeStr == "Serviser")
                                         .Select(x =>
           new {
               LawId = x.IdLaw,
               LawText = x.LawTextStr
           }).OrderByDescending(a=>a.LawId).Take(1);
            var ListApplicationVersion2 = ListApplicationVersion.ToList();
            var json = JsonConvert.SerializeObject(new
            {
                MyLaw = ListApplicationVersion2
            });

            JObject json5 = JObject.Parse(json);
            session.Close();
            return Ok(json5);
        }


        [HttpPost]
        [Route("api/G1/AddContactUs_Moshtari/{Unique_code},{UsersId},{Phone},{User_Text}/")]
        public IHttpActionResult AddContactUs_Moshtari(string Unique_code, string UsersId, string Phone, string User_Text)
        {
            if (Unique_code == "{Unique_code}" || UsersId == "UsersId" || Unique_code == "" || UsersId == "")
            {
                return Ok("مقادیر خالی");
            }
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            long UserID = Convert.ToInt64(UsersId);
            long _UserID = 0;
            _UserID = session.Query<TUsers>().Where(p => p.IdUsrer== UserID).Select(a=>a.IdUsrer).SingleOrDefault<long>();
            if (_UserID==0)
            {
                return Ok("کاربر یافت نشد");
            }

            TUsers UserId = new TUsers
            {
                IdUsrer = _UserID
            };

            TContactus tContactus = new TContactus
            {
                UserId = _UserID,///تو جاهایی که داریم جوین میکنیم باید یک ایست از او جدول وجود داشته باشد
                UserPhone = Phone,
                UserText = User_Text
            };
            //try
            //{

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(tContactus);
                    transaction.Commit();
                }
                session.Close();
                return Ok("مورد ثبت شد");
            //}
            //catch (Exception e) { return Ok("خطا در ثبت اطلاعات"); ; }
            //System.Environment.Exit(0);
            //session.Close();


            //return Ok("");

        }

        [HttpGet]
        [Route("api/G1/GetListAllQuestions/{Unique_code}")]
        public IHttpActionResult GetListAllQuestions(string Unique_code)
        {
            if (Unique_code != "PlatformV199325694")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            var ListQuestions = session.Query<TAllQuestions>()
                                         .Select(x =>
           new {
               x.IdQuestions,
               x.TitrQuestions,
               x.TextQuestions
           });
            var ListQuestions2 = ListQuestions.ToList();

            int C_Questions = 0;
            C_Questions = ListQuestions2.Count();
            if (C_Questions == 0)
            {
                return Ok("با پشتیبانی تماس حاصل نمایید");
            }

            else
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Questions = ListQuestions2
                });

                JObject json5 = JObject.Parse(json);

                session.Close();
                return Ok(json5);
            }
        }

    }
}

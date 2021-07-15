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

namespace WebApi_swagger.Controllers.Home
{
    public class MenuUserController : ApiController
    {
        [HttpPost]
        [Route("api/G1/AddNazarForServiser/{Unique_code},{UsersId},{ServiserId},{TextNazar_},{NumRank},{OrderId}")]
        public IHttpActionResult AddNazarForServiser(string Unique_code, string UsersId, string ServiserId, string TextNazar_,string NumRank,string OrderId)
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
            long _UserId = Convert.ToInt64(UsersId);
            long _OrderId = Convert.ToInt64(OrderId);
            Int32 _ServiserId = Convert.ToInt32(ServiserId);
            Int32 _NumRank = Convert.ToInt32(NumRank);

            TUsers TUserId = new TUsers
            {
                IdUsrer = _UserId
            };

            TServicer TServicerId = new TServicer
            {
                IdServicer = _ServiserId
            };

            TLActive TLActiveId = new TLActive
            {
                IdActive = 2
            };


            TNazaratForServicer tNazaratForServicer = new TNazaratForServicer
            {
                TUsers = TUserId,///تو جاهایی که داریم جوین میکنیم باید یک ایست از او جدول وجود داشته باشد
                DateCreate = DateTime.Now,
                OrderId=_OrderId,
                TServicer= TServicerId,
                NumRank=_NumRank,
                TextNazar= TextNazar_

            };
            try
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(tNazaratForServicer);
                    transaction.Commit();
                }
                #region افزودن تعداد نظرات به تعداد نظرات سرویس دهنده
                var emp = session.QueryOver<TServicer>().Where(p => p.IdServicer == _ServiserId).SingleOrDefault();
                if (emp.CNazarat == null)
                {
                    emp.CNazarat = 1;
                }
                else
                {
                    emp.CNazarat = emp.CNazarat + 1;
                }

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(emp);
                    transaction.Commit();
                }
                #endregion
                session.Close();
                return Ok("مورد ثبت شد");
            }
            catch (Exception e) { return Ok("خطا در ثبت اطلاعات"); ; }
            System.Environment.Exit(0);
            session.Close();


            //return Ok("");

        }

        [HttpGet]
        [Route("api/G1/GetListAllNazarat/{Unique_code}")]
        public IHttpActionResult GetListAllNazarat(string Unique_code)
        {
            //var result = Regex.Replace("{\"MyCustomName\":[{\"LawId\":2,\"LawText\":\"58\"}]}", @"{\\", @"");
            if (Unique_code != "PlatformV199325694")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            var ListAllNazarat = session.Query<TNazaratForServicer>()
                                         .Where(p => p.TLActive.IdActive==1)
                                         .Select(x =>
           new {
               ID_Nazarat=x.IdNazaratForServicer,
               Date_Create = x.DateCreate,
               Servicer = x.TServicer.NameServicer,
               Text_Nazar=x.TextNazar
           });
            var ListAllNazarat2 = ListAllNazarat.Take(30).OrderByDescending(s=>s.ID_Nazarat).ToList();
            var json = JsonConvert.SerializeObject(new
            {
                AllNazarat = ListAllNazarat2
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }


        [HttpGet]
        [Route("api/G1/GetUsers_Logout/{Unique_code},{Phone},{Unique_device_code},{Id}")]
        public IHttpActionResult GetUsers_Logout(string Unique_code, string Phone, string Unique_device_code, string Id)
        {
            if (Phone == "{Phone}" || Unique_device_code == "{Unique_device_code}" || Id == "Id")
            {
                return Ok("مقادیر خالی");
            }
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }

            ISession session = OpenNHibertnateSession.OpenSession();
            string IsPhoneRegistered = Phone;
            int IsPhoneRegistered_Count = IsPhoneRegistered.Length;
            if (IsPhoneRegistered_Count != 11)
            {
                session.Close();
                return Ok("شماره تلفن اشتباه می باشد");
            }
            string IsDevice_ID = Unique_device_code;
            long UserId = Convert.ToInt64(Id);
            long _UserId = 0;
            long _UserId2 = Convert.ToInt64(Id);
            int _Activation = 00;
            _UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered && p.DeviceIdLogin == IsDevice_ID).Select(p => p.IdUsrer).SingleOrDefault<long>();

            if (_UserId != 0)
            {
                _Activation = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId && p.Activation == 1).Select(p => p.Activation).SingleOrDefault<int>();
                if (_Activation == 1)
                {
                    var emp = session.Get<TUsers>(_UserId);
                    emp.Activation = 2;
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(emp);
                        transaction.Commit();
                        return Ok("کاربر خارج شد");
                    }
                }
                else
                {
                    return Ok("خروج غیر مجاز");
                }
            }
            else
            {
                return Ok("کاربر یافت نشد");
            }
            return Ok("Error");
        }

    }
}

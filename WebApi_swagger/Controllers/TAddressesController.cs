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
    public class TAddressesController : ApiController
    {
        [HttpGet]
        [Route("api/G1/GetListTOstan/{Unique_code}")]
        public IHttpActionResult GetListTOstan(string Unique_code)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            
            var ListOstan = session.Query<TLOstan>()
                                         .Where(p => p.TLActive.IdActive == 1 )
                                         .Select(x =>
           new {
               ID_Ostan = x.IdOstan,
               Titels_Ostan = x.TitelsOstan
           });
            var ListOstan2 = ListOstan.ToList();
            var json = JsonConvert.SerializeObject(new
            {
                Ostans = ListOstan2
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetListTCity/{Unique_code},{Ostan_ID}")]
        public IHttpActionResult GetListTCity(string Unique_code ,string Ostan_ID)
        {
            if (Ostan_ID == "{Ostan_ID}" || Ostan_ID == "" )
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Ostan_ID = Convert.ToInt32(Ostan_ID);

            var ListCity = session.Query<TLCity>()
                                         .Where(p => p.TLOstan.IdOstan == _Ostan_ID && p.TLActive.IdActive == 1)
                                         .Select(x =>
           new {
               ID_City = x.IdCity,
               Titels_City = x.TitelsCity
           });
            var ListCity2 = ListCity.ToList();

            int C_City = 0;
            C_City = ListCity2.Count();
            if (C_City==0)
            {
                return Ok("با پشتیبانی تماس حاصل نمایید");
            }

            else
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Citys = ListCity2
                });

                JObject json5 = JObject.Parse(json);

                session.Close();
                return Ok(json5);
            }
            
        }


        [HttpGet]
        [Route("api/G1/GetListTMahaleh/{Unique_code},{City_ID}")]
        public IHttpActionResult GetListTMahaleh(string Unique_code, string City_ID)
        {
            if (City_ID == "{City_ID}" || City_ID == "")
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _City_ID = Convert.ToInt32(City_ID);

            var ListMahaleh = session.Query<TLMahaleh>()
                                         .Where(p => p.TLCity.IdCity == _City_ID && p.TLActive.IdActive==1)
                                         .Select(x =>
           new {
               ID_Mahaleh = x.IdMahaleh,
               Titels_Mahaleh = x.TitelsMahaleh
           });
            var ListMahaleh2 = ListMahaleh.ToList();

            int C_Mahaleh = 0;
            C_Mahaleh = ListMahaleh2.Count();
            if (C_Mahaleh == 0)
            {
                return Ok("با پشتیبانی تماس حاصل نمایید");
            }

            else
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Mahalehs = ListMahaleh2
                });

                JObject json5 = JObject.Parse(json);

                session.Close();
                return Ok(json5);
            }

        }


        [HttpGet]
        [Route("api/G1/GetListTAddress_Moshtari/{Unique_code},{UsersId}")]
        public IHttpActionResult GetListTAddress_Moshtari(string Unique_code , string UsersId)
        {
            var List_Address = new List<AddressList>();

                var List_Address0 = new AddressList();
            //List_Address0.ID_Addresses = 0;
            //List_Address0.Address_Name = "";
            //List_Address0.Titels_Address = "";
            //List_Address0.Latitude =0;
            //List_Address0.Longitude=0;
            //List_Address0.City_ID = 0;
            //List_Address0.Mahaleh_ID = 0;
            //List_Address0.Ostan_ID = 0;
            //List_Address0.Type_Erore = "";
            //List_Address.Add(List_Address0);


            if (Unique_code == "{Unique_code}"  || UsersId == "UsersId" || Unique_code == "" || UsersId == "")
            {
                //return Ok("مقادیر خالی");
                List_Address0.ID_Addresses = 0;
                List_Address0.Address_Name = "";
                List_Address0.Titels_Address = "";
                List_Address0.Latitude = 0;
                List_Address0.Longitude = 0;
                List_Address0.City_ID = 0;
                List_Address0.Mahaleh_ID = 0;
                List_Address0.Ostan_ID = 0;
                List_Address0.Type_Erore = "مقادیر خالی";
                List_Address.Add(List_Address0);
            }
            if (Unique_code != "PlatformV199325694")
            {
                //return Ok("کلید نادرست");
                List_Address0.ID_Addresses = 0;
                List_Address0.Address_Name = "";
                List_Address0.Titels_Address = "";
                List_Address0.Latitude = 0;
                List_Address0.Longitude = 0;
                List_Address0.City_ID = 0;
                List_Address0.Mahaleh_ID = 0;
                List_Address0.Ostan_ID = 0;
                List_Address0.Type_Erore = "کلید نادرست";
                List_Address.Add(List_Address0);
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            long _UserId = Convert.ToInt64(UsersId);

            var ListAddress = session.Query<TAddresses>()
                                         .Where(p => p.TUsers.IdUsrer == _UserId && p.TLActive.IdActive==1 )
                                         .Select(x =>
           new {
               ID_Addresses=x.IdAddresses,
               Address_Name = x.AddressName,
               Titels_Address = x.Titels_Address,
               Latitude=x.Latitude,
               Longitude=x.Longitude,
               Phone_Addresss=x.Phone_Addresss,
               City_ID=x.TLCity.IdCity,
               Mahaleh_ID=x.TLMahaleh.IdMahaleh,
               Ostan_ID=x.TLOstan.IdOstan
           });

       

        var ListAddress2 = ListAddress.ToList();
            int C_List = 0;
            C_List=ListAddress2.Count();

            if (C_List==0)
            {
                //session.Close();
                //return Ok("درحال حاظر آدرسی ثبت نشده است");
                List_Address0.ID_Addresses = 0;
                List_Address0.Address_Name = "";
                List_Address0.Titels_Address = "";
                List_Address0.Latitude = 0;
                List_Address0.Longitude = 0;
                List_Address0.City_ID = 0;
                List_Address0.Mahaleh_ID = 0;
                List_Address0.Ostan_ID = 0;
                List_Address0.Type_Erore = "درحال حاظر آدرسی ثبت نشده است";
                List_Address.Add(List_Address0);
            }

            else
            {
                for (int i = 0; i < C_List; i++)
                {
                    var List_Address01 = new AddressList();
                    List_Address01.ID_Addresses = ListAddress2[i].ID_Addresses;
                    List_Address01.Address_Name = ListAddress2[i].Address_Name;
                    List_Address01.Titels_Address = ListAddress2[i].Titels_Address;
                    List_Address01.Latitude = ListAddress2[i].Latitude;
                    List_Address01.Longitude = ListAddress2[i].Longitude;
                    List_Address01.City_ID = ListAddress2[i].City_ID;
                    List_Address01.Mahaleh_ID = ListAddress2[i].Mahaleh_ID;
                    List_Address01.Ostan_ID = ListAddress2[i].Ostan_ID;
                    List_Address01.Phone_Addresss= ListAddress2[i].Phone_Addresss;
                    List_Address01.Type_Erore = "";
                    List_Address.Add(List_Address01);

                }
              
            }
            var json = JsonConvert.SerializeObject(new
            {
                MyAddresses = List_Address
            });

            JObject json5 = JObject.Parse(json);
            session.Close();
            return Ok(json5);

        }

        [HttpGet]
        [Route("api/G1/GetCheckMahaleID/{Unique_code},{Address_Id},{Mahaleh_Id_Serveser}")]
        public IHttpActionResult GetCheckMahaleID(string Unique_code, long Address_Id , Int32 Mahaleh_Id_Serveser)//جهت چک کردن یکسان بودن محله ای دی آدرس مورد نظر با سرویس دهنده انتخابی
        {
            if (Unique_code == "{Unique_code}" || Address_Id == 0 || Unique_code == "" || Mahaleh_Id_Serveser == 0)
            {
                return Ok("مقادیر خالی");
            }
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            var ListAddress = session.Query<TAddresses>()
                                         .Where(p => p.IdAddresses== Mahaleh_Id_Serveser)
                                         .Select(x =>
           new {
               ID_Addresses = x.IdAddresses,
               Address_Name = x.AddressName,
               Titels_Address = x.Titels_Address,
               Latitude = x.Latitude,
               Longitude = x.Longitude,
               Phone_Addresss = x.Phone_Addresss,
               City_ID = x.TLCity.IdCity,
               Mahaleh_ID = x.TLMahaleh.IdMahaleh
           });

            var ListAddress2 = ListAddress.ToList();
            int C_List = 0;

            C_List = ListAddress2.Count();

            var ListMasseges = new List<Massege>();
            if (C_List != 0)
            {
                var Masseges = new Massege();
                Masseges.id = 0;
                Masseges.Mahaleh_ID = ListAddress2[0].Mahaleh_ID;
                Masseges.Type_Erore = "";
                ListMasseges.Add(Masseges);
            }
            else
            {
                var Masseges = new Massege();
                Masseges.id = 0;
                Masseges.Mahaleh_ID = 0;
                Masseges.Type_Erore = "محله سرویس دهنه با محله آدرس یکی نمی باشد";
                ListMasseges.Add(Masseges);
            }


            //var json = JsonConvert.SerializeObject(new
            //{
            //    MyAddresses = ListAddress
            //});
            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                Mahaleh = ListMasseges
            });

            JObject json5 = JObject.Parse(json);
                session.Close();
                return Ok(json5);
            

        }

    


        [HttpPost]
        [Route("api/G1/AddAddress_Moshtari/{Unique_code},{UsersId},{Address_Name},{MahaleId},{CityId},{OstanId},{TitelsAddress},{Latitude_},{Longitude_},{PhoneAddresss}/")]
        public IHttpActionResult AddAddress_Moshtari(string Unique_code, string UsersId , string Address_Name ,string MahaleId , string CityId ,string OstanId , string TitelsAddress , decimal? Latitude_ , decimal? Longitude_ , string PhoneAddresss)
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
            Int32 _MahaleId= Convert.ToInt32(MahaleId);
            Int32 _CityId = Convert.ToInt32(CityId);
            Int32 _OstanId = Convert.ToInt32(OstanId);

            TUsers TUserId = new TUsers
            {
                IdUsrer = _UserId
            };

            TLMahaleh TLMahalehId = new TLMahaleh
            {
                IdMahaleh = _MahaleId
            };

            TLCity TLCityId = new TLCity
            {
                IdCity = _CityId
            };

            TLOstan TLOstanId = new TLOstan
            {
                IdOstan = _OstanId
            };

            TLActive TLActiveId = new TLActive
            {
                IdActive = 1
            };

            TAddresses tAddresses = new TAddresses
            {
                TUsers = TUserId,///تو جاهایی که داریم جوین میکنیم باید یک ایست از او جدول وجود داشته باشد
                AddressName= Address_Name,
                TLMahaleh = TLMahalehId,
                TLCity = TLCityId,
                TLOstan= TLOstanId,
                TLActive = TLActiveId,
                Titels_Address =TitelsAddress,
                Latitude = Latitude_,
                Longitude = Longitude_,
                Phone_Addresss =PhoneAddresss
            };
            try
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(tAddresses);
                    transaction.Commit();
                }
                session.Close();
                return Ok("مورد ثبت شد");
            }
            catch (Exception e) { return Ok("خطا در ثبت اطلاعات"); ; }
            System.Environment.Exit(0);
            session.Close();


            //return Ok("");

        }


        [HttpPost]
        [Route("api/G1/EditAddress_Moshtari/{Unique_code},{UsersId},{Address_Name},{MahaleId},{CityId},{OstanId},{TitelsAddress},{Latitude_},{Longitude_},{PhoneAddresss},{AddresessId}")]
        public IHttpActionResult EditAddress_Moshtari(string Unique_code, string UsersId, string Address_Name, string MahaleId, string CityId, string OstanId, string TitelsAddress, decimal? Latitude_, decimal? Longitude_, string PhoneAddresss , string AddresessId)
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
            Int32 _MahaleId = Convert.ToInt32(MahaleId);
            Int32 _CityId = Convert.ToInt32(CityId);
            Int32 _OstanId = Convert.ToInt32(OstanId);
            long _AddresessId = Convert.ToInt64(AddresessId);

            #region جستجو و آپدیت آدرس
            long Addresess_ = 0;
            Addresess_=session.QueryOver<TAddresses>().Where(p => p.IdAddresses == _AddresessId).Select(p => p.IdAddresses).SingleOrDefault<long>();
            if (Addresess_==0)
            {
                return Ok("آدرس یافت نشد");

            }

            else
            {
                var emp = session.QueryOver<TAddresses>().Where(p => p.IdAddresses == Addresess_).SingleOrDefault();
                emp.TUsers = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId).SingleOrDefault();
                emp.TLMahaleh = session.QueryOver<TLMahaleh>().Where(p => p.IdMahaleh == _MahaleId).SingleOrDefault();
                emp.TLCity = session.QueryOver<TLCity>().Where(p => p.IdCity == _CityId).SingleOrDefault();
                emp.TLOstan = session.QueryOver<TLOstan>().Where(p => p.IdOstan == _OstanId).SingleOrDefault();
                emp.TLActive = session.QueryOver<TLActive>().Where(p => p.IdActive == 1).SingleOrDefault();
                emp.AddressName = Address_Name;
                emp.Titels_Address = TitelsAddress;
                emp.Latitude = Convert.ToDecimal(Latitude_);
                emp.Longitude = Convert.ToDecimal(Longitude_);
                emp.Phone_Addresss = PhoneAddresss;
               
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(emp);
                    transaction.Commit();
                }
            }
            #endregion

            session.Close();
            return Ok("مورد ثبت شد");

        }

        [HttpPost]
        [Route("api/G1/DeleteAddress_Moshtari/{Unique_code},{UsersId},{AddresessId}")]
        public IHttpActionResult DeleteAddress_Moshtari(string Unique_code, string UsersId,  string AddresessId)
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
            long _AddresessId = Convert.ToInt64(AddresessId);

            #region جستجو و آپدیت آدرس
            long Addresess_ = 0;
            Addresess_ = session.QueryOver<TAddresses>().Where(p => p.IdAddresses == _AddresessId).Select(p => p.IdAddresses).SingleOrDefault<long>();
            if (Addresess_ == 0)
            {
                return Ok("آدرس یافت نشد");

            }

            else
            {
                var emp = session.QueryOver<TAddresses>().Where(p => p.IdAddresses == Addresess_).SingleOrDefault();
                emp.TLActive = session.QueryOver<TLActive>().Where(p => p.IdActive == 2).SingleOrDefault();
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(emp);
                    transaction.Commit();
                }
            }
            #endregion

            session.Close();
            return Ok("مورد حذف شد");

        }


        internal class Massege
        {
            public Int32 id { get; set; }
            public Int32 Mahaleh_ID { get; set; }
            public string Type_Erore { get; set; }
        }

        internal class AddressList
        {
            public long ID_Addresses { get; set; }
            public string Address_Name { get; set; }
            public string Titels_Address { get; set; }
            public decimal? Latitude { get; set; }
            public decimal? Longitude { get; set; }
            public string Phone_Addresss { get; set; }
            public int City_ID { get; set; }
            public int Mahaleh_ID { get; set; }
            public int Ostan_ID { get; set; }
            public string Type_Erore { get; set; }
        }
    }

}

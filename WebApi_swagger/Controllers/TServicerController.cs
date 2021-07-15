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
    public class TServicerController : ApiController
    {

        [HttpGet]
        [Route("api/G1/GetListTAllServeser/{Unique_code}")]
        public IHttpActionResult GetListTAllServeser(string Unique_code)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            var ListServiserZarfiyat_Mahale = from s in session.Query<TZarfiyatShiftServicer>()
                                                  //where (p.TMahale.MahaleId == _Mahale_ID) && (p.TNanvaei.TLActive.Id == 1)
                                              join l2 in session.Query<TServicerForMahaleh>()
                                                               on s.TServicer.IdServicer equals l2.TServicer.IdServicer
                                              where (l2.TLActive.IdActive == 1
                                                     //p.TShift.ShiftId == l2.TShift.ShiftId &&
                                                     //p.TNan.NanId == l2.TNan.NanId
                                                     )
                                              select new
                                              {
                                                  IdServicer = s.TServicer.IdServicer,
                                                  NameServicer = s.TServicer.NameServicer,
                                                  AvgRank = s.TServicer.AvgRank,
                                                  Address = s.TServicer.Address,
                                                  Hazine = l2.TLNoeHPeyk.TitelsNoeHPeyk,
                                                  //HazinehPeyk = s.TLNoeHPeyk.TitelsNoeHPeyk,
                                                  MatnNamayeshHezinePeyk = l2.TLNoeHPeyk.MatnNamayesh,
                                                  HazinehKolSefareshBishtarAz = l2.TLNoeHPeyk.HazinehKolSefareshBishtarAz,
                                                  HazinehKolSefareshKamtarAz = l2.TLNoeHPeyk.HazinehKolSefareshKamtarAz,
                                                  TitelsNoeHPeykElse=l2.TLNoeHPeyk.TitelsNoeHPeykElse,
                                                  Img = s.TServicer.Img,
                                                  CNazarat = s.TServicer.CNazarat,
                                                  IdOstan = l2.TLOstan.IdOstan,
                                                  IdCity = l2.TLCity.IdCity,
                                                  IdMahaleh = l2.TLMahaleh.IdMahaleh,
                                                  TitelsMahaleh = l2.TLMahaleh.TitelsMahaleh,
                                                  CountService = s.CountService,
                                                  IdShift = s.TShift.IdShift,
                                                  TozihatKhedmat = s.TServicer.TozihatKhedmat,
                                              };
            var ListServiserZarfiyat_Mahale_ = ListServiserZarfiyat_Mahale.ToList();

            #region چک کردن ظرفیت  باز برای سفارش گیری حتی برای یک شیفت خاص
            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day);
            DateTime now1 = DateTime.Now.AddDays(1);
            DateTime Time_Now1 = new DateTime(now.Year, now.Month, now.Day);
            DateTime now2 = DateTime.Now.AddDays(2);
            DateTime Time_Now2 = new DateTime(now.Year, now.Month, now.Day);

            int _IdServiser1 = 0;
            int _IdServiser2 = 0;
            int _IdServiser3 = 0;
            int count_Item = 0;
            int C = ListServiserZarfiyat_Mahale_.Count;
            for (int i = 0; i < C; i++)
            {
                _IdServiser1 = 0;
                _IdServiser1 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                &&
                p.Datenowgo == Time_Now
                //&& p.Datenowgo == Time_Now1 && p.Datenowgo == Time_Now2 
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                _IdServiser2 = 0;
                _IdServiser2 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                //&& p.Datenowgo == Time_Now
                && p.Datenowgo == Time_Now1
                //&& p.Datenowgo == Time_Now2 
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                _IdServiser3 = 0;
                _IdServiser3 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                //&& p.Datenowgo == Time_Now
                //&& p.Datenowgo == Time_Now1
                && p.Datenowgo == Time_Now2
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                if (_IdServiser1 == _IdServiser2 && _IdServiser2 == _IdServiser3 && _IdServiser1 == _IdServiser3)
                {
                    count_Item = ListServiserZarfiyat_Mahale_.Where(x => x.IdServicer == _IdServiser1 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift).Count();
                    if (count_Item != 0)
                    {
                        ListServiserZarfiyat_Mahale_.RemoveAll(x => x.IdServicer == _IdServiser1 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift);
                        C = ListServiserZarfiyat_Mahale_.Count();
                    }

                    //C = C - count_Item;
                    if (count_Item != 0)
                    {
                        i = i - 1;
                    }

                    count_Item = 0;
                }

            }
            #endregion


            //#region حذف سرویس دهنده های تکراری
            //var ListServiserZarfiyat_Mahale_G = ListServiserZarfiyat_Mahale_
            //      .GroupBy(a => a.IdServicer )
            //      .Select(
            //    gg => new
            //    {
            //        _GIdServicer = gg.Key,
            //        counts = gg.Count()
            //    }
            //    ).ToList();
            //#endregion


            C = ListServiserZarfiyat_Mahale_.Count();

            for (int i = 0; i < C; i++)
            {
                count_Item = ListServiserZarfiyat_Mahale_.Where(x => x.IdServicer == ListServiserZarfiyat_Mahale_[i].IdServicer && x.IdMahaleh == ListServiserZarfiyat_Mahale_[i].IdMahaleh).Count();
                if (count_Item >1)
                {
                    ListServiserZarfiyat_Mahale_.RemoveAt(i);
                    C = ListServiserZarfiyat_Mahale_.Count();
                    i--;
                }
                
            }

            C = ListServiserZarfiyat_Mahale_.OrderByDescending(a=>a.AvgRank).Count();
            var List_Shifts = new List<ListMassege>();
            if (C == 0)
            {
                var Fasl_Nomreh = new ListMassege();
                Fasl_Nomreh.ServicesName = "";
                Fasl_Nomreh.IdServicer = 0;
                Fasl_Nomreh.AvgRank = 0;
                Fasl_Nomreh.Address = "";
                Fasl_Nomreh.Hazine = "";
                Fasl_Nomreh.MatnNamayeshHezinePeyk = "";
                Fasl_Nomreh.HazinehKolSefareshBishtarAz = "";
                Fasl_Nomreh.HazinehKolSefareshKamtarAz = "";
                Fasl_Nomreh.TitelsNoeHPeykElse = "";
                Fasl_Nomreh.Img = "";
                Fasl_Nomreh.CNazarat = 0;
                Fasl_Nomreh.IdOstan = 0;
                Fasl_Nomreh.IdCity = 0;
                Fasl_Nomreh.IdMahaleh = 0;
                Fasl_Nomreh.TitelsMahaleh = "";
                Fasl_Nomreh.CountService = 0;
                Fasl_Nomreh.IdShift = 0;
                Fasl_Nomreh.Type_Erore = "تمامی شیفت های سرویس دهندگان پر است ";
                Fasl_Nomreh.Date_Shift = "";
                Fasl_Nomreh.TozihatKhedmat = "";
                List_Shifts.Add(Fasl_Nomreh);
            }
            else
            {

                for (int i = 0; i < C; i++)
                {
                    //Int32 id = ListServiserZarfiyat_Mahale_[i].id;
                    //var list1 = ListServiserZarfiyat_Mahale_.Where(a => a.IdServicer ==id ).Take(1).SingleOrDefault();
                    var Fasl_Nomreh = new ListMassege();
                    int i2 = 0;
                    i2 = i + 1;
                    Fasl_Nomreh.ID = i2;
                    Fasl_Nomreh.IdServicer = ListServiserZarfiyat_Mahale_[i].IdServicer;
                    Fasl_Nomreh.ServicesName = ListServiserZarfiyat_Mahale_[i].NameServicer;
                    Fasl_Nomreh.AvgRank = ListServiserZarfiyat_Mahale_[i].AvgRank;
                    Fasl_Nomreh.Address = ListServiserZarfiyat_Mahale_[i].Address;
                    Fasl_Nomreh.Hazine = ListServiserZarfiyat_Mahale_[i].Hazine.ToString();
                    Fasl_Nomreh.MatnNamayeshHezinePeyk = ListServiserZarfiyat_Mahale_[i].MatnNamayeshHezinePeyk;
                    Fasl_Nomreh.HazinehKolSefareshBishtarAz = ListServiserZarfiyat_Mahale_[i].HazinehKolSefareshBishtarAz.ToString();
                    Fasl_Nomreh.HazinehKolSefareshKamtarAz = ListServiserZarfiyat_Mahale_[i].HazinehKolSefareshKamtarAz.ToString();
                    Fasl_Nomreh.TitelsNoeHPeykElse = ListServiserZarfiyat_Mahale_[i].TitelsNoeHPeykElse.ToString();
                    Fasl_Nomreh.Hazine = ListServiserZarfiyat_Mahale_[i].Hazine.ToString();
                    Fasl_Nomreh.Img = ListServiserZarfiyat_Mahale_[i].Img;
                    Fasl_Nomreh.CNazarat = ListServiserZarfiyat_Mahale_[i].CNazarat;
                    Fasl_Nomreh.IdOstan = ListServiserZarfiyat_Mahale_[i].IdOstan;
                    Fasl_Nomreh.IdCity = ListServiserZarfiyat_Mahale_[i].IdCity;
                    Fasl_Nomreh.IdMahaleh = ListServiserZarfiyat_Mahale_[i].IdMahaleh;
                    Fasl_Nomreh.TitelsMahaleh = ListServiserZarfiyat_Mahale_[i].TitelsMahaleh;
                    Fasl_Nomreh.CountService = ListServiserZarfiyat_Mahale_[i].CountService;
                    Fasl_Nomreh.IdShift = ListServiserZarfiyat_Mahale_[i].IdShift;
                    Fasl_Nomreh.Type_Erore = "";
                    Fasl_Nomreh.Date_Shift = "";
                    Fasl_Nomreh.TozihatKhedmat = ListServiserZarfiyat_Mahale_[i].TozihatKhedmat;
                    List_Shifts.Add(Fasl_Nomreh);


                }
            }


            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                Allserviser = List_Shifts
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetListTAllGroup/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetListTAllGroup(string Unique_code, string Serviser_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var ListGroup = from s in session.Query<TNoeProduct>()
                            where (s.TLActive.IdActive == 1
                                   &&
                                   s.TServicer.IdServicer == _Serviser_Id
                                   )
                            select new
                            {
                                IdNoeProduct = s.IdNoeProduct,
                                Group_Id = s.TGroup.IdGroup,
                                NameGroup = s.TGroup.NameGroup,
                                Img = "http://api.monasebsazan.ir/images.jpg"
                            };
            var ListGroups = ListGroup.ToList();




            #region حذف سرویس دهنده های تکراری
            var ListGroups_G = ListGroups
                  .GroupBy(a => a.Group_Id)
                  .Select(
                gg => new
                {
                    _GroupID = gg.Key,
                    counts = gg.Count()
                }
                ).ToList();
            #endregion
            int C = 0;
            C = ListGroups_G.Count;

            var ListGroupss = new List<ListGroups>();
            if (C == 0)
            {
                var Group = new ListGroups();
                Group.Group_Id = 0;
                Group.Group_Name = "";
                Group.Img = "";
                Group.Type_Erore = "موردی یافت نشد";
                ListGroupss.Add(Group);
            }
            else
            {

                for (int i = 0; i < C; i++)
                {
                    Int32 id = ListGroups_G[i]._GroupID;
                    var list1 = ListGroups.Where(a => a.Group_Id == id).Take(1).SingleOrDefault();
                    var Group = new ListGroups();
                    Group.Group_Id = list1.Group_Id;
                    Group.Group_Name = list1.NameGroup;
                    Group.Img = list1.Img;
                    Group.Type_Erore = "";
                    ListGroupss.Add(Group);


                }
            }


            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                AllGroup = ListGroupss
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetListTAllServeser_product/{Unique_code},{Serviser_Id},{Group_Id}")]
        public IHttpActionResult GetListTAllServeser_product(string Unique_code, string Serviser_Id, Int32 Group_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var ListServiserProduct =
                from s in session.Query<TNoeProduct>()
                where (s.TServicer.IdServicer == _Serviser_Id && s.TLActive.IdActive == 1 && s.TGroup.IdGroup == Group_Id)
                join l2 in session.Query<TGroupProduct>()
                                 on s.TGroupProduct.IdGroupProduct equals l2.IdGroupProduct

                select
           new {
               ID_Noe_product = s.IdNoeProduct,
               Group_product_ID = s.TGroupProduct.IdGroupProduct,
               Name_product = l2.GroupProductTitels,
                Img= s.Img,
               //Img = "http://api.monasebsazan.ir/images.jpg",
               Noe_Khedmat_ID = s.TNoeKhedmat.IdNoeKhedmat,
               Noe_Khedmat_Text = s.TNoeKhedmat.TitelsNoeKhedmat,
               Price = s.Price,
               Group_ID = s.TGroup.IdGroup,
               Group_Name = s.TGroup.NameGroup

           };


            var ListServiserProduct_ = ListServiserProduct.ToList();

            var myStoreList = ListServiserProduct_
                                                 .GroupBy(s => s.Group_product_ID)
                                                 .Select(grp => grp.FirstOrDefault())
                                                 .OrderBy(s => s.Group_product_ID)
                                                 .ToList();


            var json = JsonConvert.SerializeObject(new
            {
                Allserviser_product = myStoreList
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetListTNoeKhedmat/{Unique_code},{Serviser_Id},{Group_product_ID}")]
        public IHttpActionResult GetListTNoeKhedmat(string Unique_code, string Serviser_Id, Int32 Group_product_ID)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var ListServiserProduct = session.Query<TNoeProduct>().Where(s => s.TServicer.IdServicer == _Serviser_Id && s.TLActive.IdActive == 1 && s.TGroupProduct.IdGroupProduct == Group_product_ID)
               .Select(x =>
           new
           {
               ID_Noe_product = x.IdNoeProduct,
               Group_product_ID = 0,
               Name_product = "",
               //Img=x.Img,
               Img = "",
               Noe_Khedmat_ID = x.TNoeKhedmat.IdNoeKhedmat,
               Noe_Khedmat_Text = x.TNoeKhedmat.TitelsNoeKhedmat,
               Price = x.Price,
               Group_ID = 0,
               Group_Name = 0

           });
            var ListServiserProduct_ = ListServiserProduct.ToList();
            var myStoreList = ListServiserProduct_
                                                .GroupBy(s => s.Noe_Khedmat_ID)
                                                .Select(grp => grp.FirstOrDefault())
                                                .OrderBy(s => s.Noe_Khedmat_ID)
                                                .ToList();


            var json = JsonConvert.SerializeObject(new
            {
                Allserviser_Khedmat = myStoreList
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetListTAllShiftGetProduct/{Unique_code},{Serviser_Id},{Mahaleh_Id}")]
        public IHttpActionResult GetListTAllShiftGetProduct(string Unique_code, string Serviser_Id, string Mahaleh_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            int _Serviser_Id = Convert.ToInt32(Serviser_Id);
            int _Mahaleh_Id = Convert.ToInt32(Mahaleh_Id);
            ISession session = OpenNHibertnateSession.OpenSession();

            var ListServiserZarfiyat_Mahale = (from s in session.Query<TZarfiyatShiftServicer>()
                                               where (s.TServicer.IdServicer == _Serviser_Id)
                                               join l2 in session.Query<TServicerForMahaleh>()
                                                                on s.TServicer.IdServicer equals l2.TServicer.IdServicer
                                               where (l2.TLActive.IdActive == 1 &&
                                                      l2.TLMahaleh.IdMahaleh == _Mahaleh_Id &&
                                                      l2.TServicer.IdServicer == _Serviser_Id
                                                      //p.TNan.NanId == l2.TNan.NanId
                                                      )
                                               select new
                                               {
                                                   IdServicer = s.TServicer.IdServicer,
                                                   NameServicer = s.TServicer.NameServicer,
                                                   AvgRank = s.TServicer.AvgRank,
                                                   Address = s.TServicer.Address,
                                                   Hazine = l2.TLNoeHPeyk.TitelsNoeHPeyk,
                                                   Img = s.TServicer.Img,
                                                   CNazarat = s.TServicer.CNazarat,
                                                   IdOstan = l2.TLOstan.IdOstan,
                                                   IdCity = l2.TLCity.IdCity,
                                                   IdMahaleh = l2.TLMahaleh.IdMahaleh,
                                                   CountService = s.CountService,
                                                   IdShift = s.TShift.IdShift,
                                                   TypeShift = s.TShift.ShiftText,
                                                   NoeHPeyk = l2.TLNoeHPeyk.TitelsNoeHPeyk,
                                                   start_HH=s.TShift.ShiftHhShowStartTime,
                                                   start_MM=s.TShift.ShiftMmShowStartTime,
                                                   End_HH=s.TShift.ShiftHhEndStartTime,
                                                   End_MM=s.TShift.ShiftMmEndStartTime
                                               });
            var ListServiserZarfiyat_Mahale_ = ListServiserZarfiyat_Mahale.ToList();

            #region چک کردن ظرفیت  باز برای سفارش گیری حتی برای یک شیفت خاص
            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day);
            int _DayId = Convert.ToInt32(DayNumber(Time_Now.DayOfWeek.ToString()));
            DateTime now1 = DateTime.Now.AddDays(1);
            DateTime Time_Now1 = new DateTime(now1.Year, now1.Month, now1.Day);
            int _DayId1 = Convert.ToInt32(DayNumber(Time_Now1.DayOfWeek.ToString()));
            DateTime now2 = DateTime.Now.AddDays(2);
            DateTime Time_Now2 = new DateTime(now2.Year, now2.Month, now2.Day);
            int _DayId2 = Convert.ToInt32(DayNumber(Time_Now2.DayOfWeek.ToString()));

            int _IdServiser1 = 0;
            int _IdServiser2 = 0;
            int _IdServiser3 = 0;
            int count_Item = 0;
            int C = ListServiserZarfiyat_Mahale_.Count;
            for (int i = 0; i < C; i++)
            {
                _IdServiser1 = 0;
                _IdServiser1 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                &&
                p.Datenowgo == Time_Now
                //&& p.Datenowgo == Time_Now1 && p.Datenowgo == Time_Now2 
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                _IdServiser2 = 0;
                _IdServiser2 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                //&& p.Datenowgo == Time_Now
                && p.Datenowgo == Time_Now1
                //&& p.Datenowgo == Time_Now2 
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                _IdServiser3 = 0;
                _IdServiser3 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                //&& p.Datenowgo == Time_Now
                //&& p.Datenowgo == Time_Now1
                && p.Datenowgo == Time_Now2
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                if (_IdServiser1 == _IdServiser2 && _IdServiser2 == _IdServiser3 && _IdServiser1 == _IdServiser3)
                {
                    count_Item = ListServiserZarfiyat_Mahale_.Where(x => x.IdServicer == _IdServiser1 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift).Count();
                    if (count_Item != 0)
                    {
                        ListServiserZarfiyat_Mahale_.RemoveAll(x => x.IdServicer == _IdServiser1 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift);
                        C = ListServiserZarfiyat_Mahale_.Count();
                    }

                    //C = C - count_Item;
                    if (count_Item != 0)
                    {
                        i = i - 1;
                    }

                    count_Item = 0;
                }

            }
            #endregion
            C = ListServiserZarfiyat_Mahale_.Count;
            //var Fasl_Nomreh = new ListMassege();
            var List_Shifts = new List<ListShift>();
            if (C == 0)
            {
                var Fasl_Nomreh = new ListShift();
                Fasl_Nomreh.IdServicer = 0;
                Fasl_Nomreh.Hazine = "";
                Fasl_Nomreh.IdShift = 0;
                Fasl_Nomreh.Type_Erore = "تمامی شیفت های سرویس دهنده پر است لطفا از صفحه اصلی سرویس دهنده دیگری را انتخاب نمایید";
                Fasl_Nomreh.Date_Shift = "";
                Fasl_Nomreh.Day_Name = "";
                List_Shifts.Add(Fasl_Nomreh);
            }
            else
            {
                for (int i = 0; i < C; i++)
                {
                    int _HH = now.Hour;
                    int _MM = now.Minute;

                    #region امروز
                    _IdServiser1 = 0;
                    _IdServiser1 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                    p.CountGoService <= ListServiserZarfiyat_Mahale_[i].CountService
                    &&
                    p.Datenowgo == Time_Now
                    //&& p.Datenowgo == Time_Now1 && p.Datenowgo == Time_Now2 
                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();
                    if (_IdServiser1 == 0) //یعنی برای این تاریخ و این شیفت وجوددارد
                    {
                        if ((ListServiserZarfiyat_Mahale_[i].End_HH > _HH) || (ListServiserZarfiyat_Mahale_[i].End_HH == _HH && ListServiserZarfiyat_Mahale_[i].End_MM >= _MM))
                           //(ListServiserZarfiyat_Mahale_[i].start_HH > _HH) || (ListServiserZarfiyat_Mahale_[i].start_HH == _HH && ListServiserZarfiyat_Mahale_[i].start_MM >= _MM)
                        {
                            var Fasl_Nomreh = new ListShift();
                            Fasl_Nomreh.ListID = 0;
                            Fasl_Nomreh.DayID = _DayId;
                            Fasl_Nomreh.IdServicer = 0;
                            Fasl_Nomreh.Hazine = ListServiserZarfiyat_Mahale_[i].NoeHPeyk.ToString();
                            Fasl_Nomreh.Shifttype = ListServiserZarfiyat_Mahale_[i].TypeShift;
                            Fasl_Nomreh.IdShift = ListServiserZarfiyat_Mahale_[i].IdShift;
                            Fasl_Nomreh.Type_Erore = "";
                            Fasl_Nomreh.Date_Shift = Time_Now.ToString("yyyy/MM/dd");
                            Fasl_Nomreh.Day_Name = " امروز ";
                            List_Shifts.Add(Fasl_Nomreh);
                        }
                        //var Fasl_Nomreh = new ListShift();
                        //Fasl_Nomreh.ListID = 0;
                        //Fasl_Nomreh.DayID = _DayId;
                        //Fasl_Nomreh.IdServicer = 0;
                        //Fasl_Nomreh.Hazine = ListServiserZarfiyat_Mahale_[i].NoeHPeyk;
                        //Fasl_Nomreh.Shifttype = ListServiserZarfiyat_Mahale_[i].TypeShift;
                        //Fasl_Nomreh.IdShift = ListServiserZarfiyat_Mahale_[i].IdShift;
                        //Fasl_Nomreh.Type_Erore = "";
                        //Fasl_Nomreh.Date_Shift = Time_Now.ToString("yyyy/MM/dd");
                        //Fasl_Nomreh.Day_Name = " امروز ";
                        //List_Shifts.Add(Fasl_Nomreh);
                    }
                    #endregion

                    //#region حذف موارد امروز که شیفتشون گذشته
                    //List_Shifts.RemoveAll(x => x.ShiftId == ListHoliday2[i].ShiftId && x.NanvaeiId == ListHoliday2[i].NanvaeiId);
                    //#endregion
                    //List_Shifts.Remove(a=>a.)
                    #region فردا
                    _IdServiser2 = 0;
                    _IdServiser2 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                    p.CountGoService <= ListServiserZarfiyat_Mahale_[i].CountService
                    //&& p.Datenowgo == Time_Now
                    && p.Datenowgo == Time_Now1
                    //&& p.Datenowgo == Time_Now2 
                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                    if (_IdServiser2 == 0) //یعنی برای این تاریخ و این شیفت وجوددارد
                    {
                        var Fasl_Nomreh2 = new ListShift();
                        Fasl_Nomreh2.ListID = 1;
                        Fasl_Nomreh2.DayID = _DayId1;
                        Fasl_Nomreh2.IdServicer = 0;
                        Fasl_Nomreh2.Hazine = ListServiserZarfiyat_Mahale_[i].NoeHPeyk.ToString();
                        Fasl_Nomreh2.Shifttype = ListServiserZarfiyat_Mahale_[i].TypeShift;
                        Fasl_Nomreh2.IdShift = ListServiserZarfiyat_Mahale_[i].IdShift;
                        //Fasl_Nomreh.Type_Erore = ListServiserZarfiyat_Mahale_[i].AvgRank;
                        Fasl_Nomreh2.Date_Shift = Time_Now1.ToString("yyyy/MM/dd");
                        Fasl_Nomreh2.Day_Name = " فردا ";
                        List_Shifts.Add(Fasl_Nomreh2);
                    }
                    #endregion

                    #region پس فردا
                    _IdServiser3 = 0;
                    _IdServiser3 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                    p.CountGoService <= ListServiserZarfiyat_Mahale_[i].CountService
                    //&& p.Datenowgo == Time_Now
                    //&& p.Datenowgo == Time_Now1
                    && p.Datenowgo == Time_Now2
                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                    if (_IdServiser3 == 0) //یعنی برای این تاریخ و این شیفت وجوددارد
                    {
                        var Fasl_Nomreh3 = new ListShift();
                        Fasl_Nomreh3.ListID = 2;
                        Fasl_Nomreh3.DayID = _DayId2;
                        Fasl_Nomreh3.IdServicer = 0;
                        Fasl_Nomreh3.Hazine = ListServiserZarfiyat_Mahale_[i].NoeHPeyk.ToString() ;
                        Fasl_Nomreh3.Shifttype = ListServiserZarfiyat_Mahale_[i].TypeShift;
                        Fasl_Nomreh3.IdShift = ListServiserZarfiyat_Mahale_[i].IdShift;
                        //Fasl_Nomreh.Type_Erore = ListServiserZarfiyat_Mahale_[i].AvgRank;
                        Fasl_Nomreh3.Date_Shift = Time_Now2.ToString("yyyy/MM/dd");
                        Fasl_Nomreh3.Day_Name = " پس فردا ";
                        List_Shifts.Add(Fasl_Nomreh3);
                    }
                    #endregion
                }
            }


            var json = JsonConvert.SerializeObject(new
            {
                AllserviserShift =
                List_Shifts.OrderBy(a => a.DayID)
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);

        }

        [HttpGet]
        [Route("api/G1/GetListTAllShiftDeliveryProduct/{Unique_code},{Serviser_Id},{Mahaleh_Id},{DateShiftDaryaft}")]
        public IHttpActionResult GetListTAllShiftDeliveryProduct(string Unique_code, string Serviser_Id, string Mahaleh_Id, string DateShiftDaryaft)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            int _Serviser_Id = Convert.ToInt32(Serviser_Id);
            int _Mahaleh_Id = Convert.ToInt32(Mahaleh_Id);
            DateTime _Date = Convert.ToDateTime(DateShiftDaryaft);
            ISession session = OpenNHibertnateSession.OpenSession();

            var ListServiserZarfiyat_Mahale = (from s in session.Query<TZarfiyatShiftServicer>()
                                               where (s.TServicer.IdServicer == _Serviser_Id)
                                               join l2 in session.Query<TServicerForMahaleh>()
                                                                on s.TServicer.IdServicer equals l2.TServicer.IdServicer
                                               where (l2.TLActive.IdActive == 1 &&
                                                      l2.TLMahaleh.IdMahaleh == _Mahaleh_Id &&
                                                      l2.TServicer.IdServicer == _Serviser_Id
                                                      //p.TNan.NanId == l2.TNan.NanId
                                                      )
                                               select new
                                               {
                                                   IdServicer = s.TServicer.IdServicer,
                                                   NameServicer = s.TServicer.NameServicer,
                                                   AvgRank = s.TServicer.AvgRank,
                                                   Address = s.TServicer.Address,
                                                   Hazine = l2.TLNoeHPeyk.TitelsNoeHPeyk,
                                                   Img = s.TServicer.Img,
                                                   CNazarat = s.TServicer.CNazarat,
                                                   IdOstan = l2.TLOstan.IdOstan,
                                                   IdCity = l2.TLCity.IdCity,
                                                   IdMahaleh = l2.TLMahaleh.IdMahaleh,
                                                   CountService = s.CountService,
                                                   IdShift = s.TShift.IdShift,
                                                   TypeShift = s.TShift.ShiftText,
                                                   NoeHPeyk = l2.TLNoeHPeyk.TitelsNoeHPeyk
                                               });
            var ListServiserZarfiyat_Mahale_ = ListServiserZarfiyat_Mahale.ToList();

            #region چک کردن ظرفیت  باز برای سفارش گیری حتی برای یک شیفت خاص
            DateTime now = _Date.AddDays(3);
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day);
            int _DayId = Convert.ToInt32(DayNumber(Time_Now.DayOfWeek.ToString()));
            DateTime now1 = _Date.AddDays(4);
            DateTime Time_Now1 = new DateTime(now1.Year, now1.Month, now1.Day);
            int _DayId1 = Convert.ToInt32(DayNumber(Time_Now1.DayOfWeek.ToString()));
            DateTime now2 = _Date.AddDays(5);
            DateTime Time_Now2 = new DateTime(now2.Year, now2.Month, now2.Day);
            int _DayId2 = Convert.ToInt32(DayNumber(Time_Now2.DayOfWeek.ToString()));

            string Time_Now_Day = DayName(Time_Now.DayOfWeek.ToString());
            string Time_Now1_Day = DayName(Time_Now1.DayOfWeek.ToString());
            string Time_Now2_Day = DayName(Time_Now2.DayOfWeek.ToString());

            int _IdServiser1 = 0;
            int _IdServiser2 = 0;
            int _IdServiser3 = 0;
            int count_Item = 0;
            int C = ListServiserZarfiyat_Mahale_.Count;
            for (int i = 0; i < C; i++)
            {
                _IdServiser1 = 0;
                _IdServiser1 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountDeliveryService >= ListServiserZarfiyat_Mahale_[i].CountService
                &&
                p.Datenowgo == Time_Now
                //&& p.Datenowgo == Time_Now1 && p.Datenowgo == Time_Now2 
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                _IdServiser2 = 0;
                _IdServiser2 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountDeliveryService >= ListServiserZarfiyat_Mahale_[i].CountService
                //&& p.Datenowgo == Time_Now
                && p.Datenowgo == Time_Now1
                //&& p.Datenowgo == Time_Now2 
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                _IdServiser3 = 0;
                _IdServiser3 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                p.CountDeliveryService >= ListServiserZarfiyat_Mahale_[i].CountService
                //&& p.Datenowgo == Time_Now
                //&& p.Datenowgo == Time_Now1
                && p.Datenowgo == Time_Now2
                && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                if (_IdServiser1 == _IdServiser2 && _IdServiser2 == _IdServiser3 && _IdServiser1 == _IdServiser3)
                {
                    count_Item = ListServiserZarfiyat_Mahale_.Where(x => x.IdServicer == _IdServiser1 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift).Count();
                    if (count_Item != 0)
                    {
                        ListServiserZarfiyat_Mahale_.RemoveAll(x => x.IdServicer == _IdServiser1 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift);
                        C = ListServiserZarfiyat_Mahale_.Count();
                    }

                    //C = C - count_Item;
                    if (count_Item != 0)
                    {
                        i = i - 1;
                    }

                    count_Item = 0;
                }

            }
            #endregion
            C = ListServiserZarfiyat_Mahale_.Count;
            //var Fasl_Nomreh = new ListMassege();
            var List_Shifts = new List<ListShift>();
            if (C == 0)
            {
                var Fasl_Nomreh = new ListShift();
                Fasl_Nomreh.IdServicer = 0;
                Fasl_Nomreh.Hazine = "";
                Fasl_Nomreh.IdShift = 0;
                Fasl_Nomreh.Type_Erore = "تمامی شیفت های سرویس دهنده پر است لطفا از صفحه اصلی سرویس دهنده دیگری را انتخاب نمایید";
                Fasl_Nomreh.Date_Shift = "";
                Fasl_Nomreh.Day_Name = "";
                List_Shifts.Add(Fasl_Nomreh);
            }
            else
            {
                for (int i = 0; i < C; i++)
                {

                    #region امروز
                    _IdServiser1 = 0;
                    _IdServiser1 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                    p.CountGoService <= ListServiserZarfiyat_Mahale_[i].CountService
                    &&
                    p.Datenowgo == Time_Now
                    //&& p.Datenowgo == Time_Now1 && p.Datenowgo == Time_Now2 
                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();
                    if (_IdServiser1 == 0) //یعنی برای این تاریخ و این شیفت وجوددارد
                    {
                        var Fasl_Nomreh = new ListShift();
                        Fasl_Nomreh.ListID = 0;
                        Fasl_Nomreh.DayID = _DayId;
                        Fasl_Nomreh.IdServicer = 0;
                        Fasl_Nomreh.Hazine = ListServiserZarfiyat_Mahale_[i].NoeHPeyk.ToString();
                        Fasl_Nomreh.Shifttype = ListServiserZarfiyat_Mahale_[i].TypeShift;
                        Fasl_Nomreh.IdShift = ListServiserZarfiyat_Mahale_[i].IdShift;
                        Fasl_Nomreh.Type_Erore = "";


                        Fasl_Nomreh.Date_Shift = Time_Now.ToString("yyyy/MM/dd");
                        Fasl_Nomreh.Day_Name = Time_Now_Day;
                        List_Shifts.Add(Fasl_Nomreh);
                    }
                    #endregion

                    #region فردا
                    _IdServiser2 = 0;
                    _IdServiser2 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                    p.CountGoService <= ListServiserZarfiyat_Mahale_[i].CountService
                    //&& p.Datenowgo == Time_Now
                    && p.Datenowgo == Time_Now1
                    //&& p.Datenowgo == Time_Now2 
                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                    if (_IdServiser2 == 0) //یعنی برای این تاریخ و این شیفت وجوددارد
                    {
                        var Fasl_Nomreh2 = new ListShift();
                        Fasl_Nomreh2.ListID = 1;
                        Fasl_Nomreh2.DayID = _DayId1;
                        Fasl_Nomreh2.IdServicer = 0;
                        Fasl_Nomreh2.Hazine = ListServiserZarfiyat_Mahale_[i].NoeHPeyk.ToString() ;
                        Fasl_Nomreh2.Shifttype = ListServiserZarfiyat_Mahale_[i].TypeShift;
                        Fasl_Nomreh2.IdShift = ListServiserZarfiyat_Mahale_[i].IdShift;
                        //Fasl_Nomreh.Type_Erore = ListServiserZarfiyat_Mahale_[i].AvgRank;
                        Fasl_Nomreh2.Date_Shift = Time_Now1.ToString("yyyy/MM/dd");
                        Fasl_Nomreh2.Day_Name = Time_Now1_Day;
                        List_Shifts.Add(Fasl_Nomreh2);
                    }
                    #endregion

                    #region پس فردا
                    _IdServiser3 = 0;
                    _IdServiser3 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                    p.CountGoService <= ListServiserZarfiyat_Mahale_[i].CountService
                    //&& p.Datenowgo == Time_Now
                    //&& p.Datenowgo == Time_Now1
                    && p.Datenowgo == Time_Now2
                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                    if (_IdServiser3 == 0) //یعنی برای این تاریخ و این شیفت وجوددارد
                    {
                        var Fasl_Nomreh3 = new ListShift();
                        Fasl_Nomreh3.ListID = 2;
                        Fasl_Nomreh3.DayID = _DayId2;
                        Fasl_Nomreh3.IdServicer = 0;
                        Fasl_Nomreh3.Hazine = ListServiserZarfiyat_Mahale_[i].NoeHPeyk.ToString();
                        Fasl_Nomreh3.Shifttype = ListServiserZarfiyat_Mahale_[i].TypeShift;
                        Fasl_Nomreh3.IdShift = ListServiserZarfiyat_Mahale_[i].IdShift;
                        //Fasl_Nomreh.Type_Erore = ListServiserZarfiyat_Mahale_[i].AvgRank;
                        Fasl_Nomreh3.Date_Shift = Time_Now2.ToString("yyyy/MM/dd");
                        Fasl_Nomreh3.Day_Name = Time_Now2_Day;
                        List_Shifts.Add(Fasl_Nomreh3);
                    }
                    #endregion



                }
            }


            var json = JsonConvert.SerializeObject(new
            {
                AllserviserShift =
                List_Shifts.OrderBy(a => a.DayID)
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);

        }

        [HttpGet]
        [Route("api/G1/GetServerSpecifications/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetServerSpecifications(string Unique_code, string Serviser_Id)
        {
            var ListAll = new List<ServerSpecifications>();
            if (Unique_code != "PlatformV199325694")
            {
                //return Ok("کلید نادرست");
                var Group = new ServerSpecifications();
                Group.NameServicer = "";
                Group.Address = "";
                Group.Img = "";
                Group.Latitude = "";
                Group.Longitude = "";
                Group.AvgRank = "";
                Group.Phone1 = "";
                Group.Active = "";
                Group.Type_Erore = "کلید نادرست";
                ListAll.Add(Group);
            }
          

            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var Servicer = from s in session.Query<TServicer>()
                           where (
                                  s.IdServicer == _Serviser_Id
                                  )
                           select new
                           {
                               NameServicer = s.NameServicer,
                               Address = s.Address,
                               s.Img,
                               s.Latitude,
                               s.Longitude,
                               s.AvgRank,
                               s.Phone1,
                               TitelsActive= s.TLActive.TitelsActive,
                               Telegram= s.Telegram,
                               Instagram=s.Instagram,
                               WhatsApp=s.Whatsapp,
                               Other_Page=s.OtherPage

                           };
            var _Servicer = Servicer.ToList();
            int C = 0;
            C = _Servicer.Count;
            if (C==0)
            {
                var Group = new ServerSpecifications();
                Group.NameServicer = "";
                Group.Address = "";
                Group.Img = "";
                Group.Latitude = "";
                Group.Longitude = "";
                Group.AvgRank = "";
                Group.Phone1 = "";
                Group.Active = "";
                Group.Type_Erore = "مورد یافت نشد";
                Group.Telegram = "";
                Group.Instagram = "";
                Group.WhatsApp = "";
                Group.Other_Page = "";
                ListAll.Add(Group);
            }
            else
            {
                for (int i = 0; i < C; i++)
                {
                    var Group = new ServerSpecifications();
                    Group.NameServicer = _Servicer[i].NameServicer;
                    Group.Address = _Servicer[i].Address;
                    Group.Img = _Servicer[i].Img;
                    Group.Latitude = _Servicer[i].Latitude.ToString();
                    Group.Longitude = _Servicer[i].Longitude.ToString();
                    Group.AvgRank = _Servicer[i].AvgRank.ToString();
                    Group.Phone1 = _Servicer[i].Phone1;
                    Group.Active = _Servicer[i].TitelsActive;
                    Group.Type_Erore = "";
                    Group.Telegram = _Servicer[i].Telegram;
                    Group.Instagram = _Servicer[i].Instagram;
                    Group.WhatsApp = _Servicer[i].WhatsApp;
                    Group.Other_Page = _Servicer[i].Other_Page;
                    ListAll.Add(Group);
                }
            }


            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                Servicer = ListAll
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetServerAllShift/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetServerAllShift(string Unique_code, string Serviser_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var Servicer = from s in session.Query<TZarfiyatShiftServicer>()
                           where (
                                  s.TServicer.IdServicer == _Serviser_Id
                                  )
                           select new
                           {
                               TextShift = s.TShift.ShiftText,
                           };
            var _Servicer = Servicer.ToList();

            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                ServicerShift = _Servicer
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetServerAllMahaleh/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetServerAllMahaleh(string Unique_code, string Serviser_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var Servicer = from s in session.Query<TServicerForMahaleh>()
                           where (
                                  s.TServicer.IdServicer == _Serviser_Id
                                  )
                           select new
                           {
                               Mahaleh = s.TLMahaleh.TitelsMahaleh,
                               City=s.TLCity.TitelsCity,
                               Ostan=s.TLOstan.TitelsOstan,
                               Titels=s.TitelsForLookups,
                               HazinehPeyk=s.TLNoeHPeyk.TitelsNoeHPeyk,
                               Vaziyat=s.TLActive.TitelsActive
                           };
            var _Servicer = Servicer.ToList();

            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                ServicerMahaleh = _Servicer
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/ChekCommentUser/{Unique_code},{User_Id}")]
        public IHttpActionResult ChekCommentUser(string Unique_code, long User_Id)
        {
            var ListComments = new List<ListChekCommentUser>();
            if (Unique_code != "PlatformV199325694")
            {
                //var List_ = new List<ListChekCommentUser>();
                var Group = new ListChekCommentUser();
                //ListChekCommentUser massege = new ListChekCommentUser
                {
                    Group.IdServicer = "";
                    Group.NameServicer = "";
                    Group.ShenaseSefaresh = "";
                    Group.IdOrder = "";
                    Group.Datetahvil = "";
                    Group.Img = "";
                    Group.Type_Erore = "کلید نادرست";
                    ListComments.Add(Group);
                };
                //var carss = massege;
                var json2 = JsonConvert.SerializeObject(new
                {
                    ChekComment = ListComments
                });
                JObject json21 = JObject.Parse(json2);
                return Ok(json21);
                //return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = 0;
            Int64 _Order_Id = 0;
            var List = session.Query<TOrder>()
                                        .Where(p => p.TUsers.IdUsrer == User_Id && (p.TLVaziyatTahvil.IdVaziyatTahvil==8 || p.TLVaziyatTahvil.IdVaziyatTahvil == 9 || p.TLVaziyatTahvil.IdVaziyatTahvil == 10))
                                        .Select(x =>
          new {
              IdServicer = x.TServicer.IdServicer,
              NameServicer = x.TServicer.NameServicer,
              x.ShenaseSefaresh,
              x.IdOrder,
              Datetahvil=x.Datetahvil.ToString("yyyy/MM/dd"),
              Img=x.TServicer.Img
          }).OrderByDescending(a => a.IdOrder).Take(1);
            var List2 = List.ToList();
            if (List2.Count==0)
            {
                //ListChekCommentUser massege = new ListChekCommentUser
                 var Group = new ListChekCommentUser();
                {
                    Group.IdServicer = "";
                    Group.NameServicer = "";
                    Group.ShenaseSefaresh = "";
                    Group.IdOrder = "";
                    Group.Datetahvil = "";
                    Group.Img = "";
                    Group.Type_Erore = "مورد یافت نشد";
                    ListComments.Add(Group);
                };
                //var carss = massege;
                var json2 = JsonConvert.SerializeObject(new
                {
                    ChekComment = ListComments
                });
                JObject json21 = JObject.Parse(json2);
                return Ok(json21);
                //return Ok("مورد یافت نشد");
            }

            long _IdNazarat = 0;
            long _idOreer0 = 0;
            _idOreer0 = List2[0].IdOrder;
            _IdNazarat = session.QueryOver<TNazaratForServicer>().Where(p => p.TUsers.IdUsrer == User_Id && p.OrderId== _idOreer0).Select(p => p.IdNazaratForServicer).SingleOrDefault<long>();
            #region  و جود دارد ثبت نظر


            if (_IdNazarat==0)
            {
                //var massege = new List<ListChekCommentUser>();
                var Group = new ListChekCommentUser();
                //ListChekCommentUser massege = new ListChekCommentUser
                {
                    Group.IdServicer = List2[0].IdServicer.ToString();
                    Group.NameServicer = List2[0].NameServicer;
                    Group.ShenaseSefaresh = List2[0].ShenaseSefaresh.ToString();
                    Group.IdOrder = List2[0].IdOrder.ToString();
                    Group.Datetahvil = List2[0].Datetahvil;
                    Group.Img = List2[0].Img;
                    Group.Type_Erore = "";
                    ListComments.Add(Group);
                };
                //var carss = massege;
                var json2 = JsonConvert.SerializeObject(new
                {
                    ChekComment = ListComments
                });
                JObject json21 = JObject.Parse(json2);
                return Ok(json21);
                //var json = JsonConvert.SerializeObject(new
                //{
                //    ChekSabteNazar = List2
                //});
                //JObject json5 = JObject.Parse(json);
                //session.Close();
                //return Ok(json5);
            }

            else
            {
                var Group = new ListChekCommentUser();
                //ListChekCommentUser massege = new ListChekCommentUser
                {
                    Group.IdServicer = "";
                    Group.NameServicer = "";
                    Group.ShenaseSefaresh = "";
                    Group.IdOrder = "";
                    Group.Datetahvil = "";
                    Group.Img = "";
                    Group.Type_Erore = "مورد یافت نشد";
                    ListComments.Add(Group);
                };
                //var carss = massege;
                var json2 = JsonConvert.SerializeObject(new
                {
                    ChekComment = ListComments
                });
                JObject json21 = JObject.Parse(json2);
                return Ok(json21);
                //return Ok("مورد یافت نشد");
                //return Ok("مورد یافت نشد");
            }

            return Ok("Error");

            #endregion


        }

        [HttpGet]
        [Route("api/G1/SetCommentUser/{Unique_code},{Order_Id},{User_Id},{Comment},{Rank}")]
        public IHttpActionResult SetCommentUser(string Unique_code, long Order_Id, long User_Id, string Comment, int Rank)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = 0;
            _Serviser_Id = session.QueryOver<TOrder>().Where(p => p.IdOrder == Order_Id && p.TUsers.IdUsrer == User_Id).Select(p => p.TServicer.IdServicer).SingleOrDefault<Int32>();
            #region ثبت نظر

            TUsers TUserstId = new TUsers
            {
                IdUsrer = User_Id
            };

            TServicer TServicerId = new TServicer
            {
                IdServicer = _Serviser_Id
            };

            TLActive TLActiveId = new TLActive
            {
                IdActive = 2
            };

            TNazaratForServicer Nazarat = new TNazaratForServicer
            {
                TUsers = TUserstId,
                DateCreate = DateTime.Now,
                OrderId = Order_Id,
                TServicer = TServicerId,
                NumRank = Rank,
                TextNazar = Comment,
                TLActive = TLActiveId
            };

            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    session.Save(Nazarat);
                    transaction.Commit();
                    session.Close();
                    return Ok("مورد ثبت شد");
                }
            }
            catch (Exception)
            {

                return Ok("خطا در ثبت اطلاعات");
            }


        //return Ok("ادامه مراحل ارسال پیامک");

        #endregion


    }

        [HttpGet]
        [Route("api/G1/GetAllCommentServises/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetAllCommentServises(string Unique_code, long Serviser_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            #region واکشی نظرات
            var ListAll = session.Query<TNazaratForServicer>().Where(s => s.TServicer.IdServicer == Serviser_Id && s.TLActive.IdActive == 1 )
              .Select(x =>
          new
          {
             x.TUsers.NameFamily,
              DateCreate= x.DateCreate.ToString("yyyy/MM/dd"),
              x.NumRank,
              x.TextNazar
        }).ToList();
            Int32 C_List = 0;
            C_List = ListAll.Count;
            var ListComments = new List<ListComment>();
            if (C_List == 0)
            {
                var Group = new ListComment();
                Group.User_Name = "";
                Group.DateCreate = "";
                Group.NumRank = 0;
                Group.TextNazar = "";
                Group.Type_Erore = "موردی یافت نشد";
                ListComments.Add(Group);
            }
            else
            {

                for (int i = 0; i < C_List; i++)
                {
                    var Group = new ListComment();
                    Group.User_Name = ListAll[i].NameFamily;
                    Group.DateCreate = ListAll[i].DateCreate;
                    Group.NumRank = ListAll[i].NumRank;
                    Group.TextNazar = ListAll[i].TextNazar;
                    Group.Type_Erore = "";
                    ListComments.Add(Group);


                }
            }

            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                Comments = ListComments
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);

            #endregion


        }

        [HttpGet]
        [Route("api/G1/GetDescriptionServeser/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetDescriptionServeser(string Unique_code, int Serviser_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            var Servicer = from s in session.Query<TServicer>()
                           where (
                                  s.IdServicer == Serviser_Id
                                  )
                           select new
                           {
                               s.IdServicer,
                               s.NameFamilyAdminServicer,
                               s.NameServicer,
                               s.Address,
                               s.Img,
                               s.PhoneAdmin,
                               s.Phone1,
                               s.NumberGharardad,
                               s.ShabaCartBank,
                               s.NameBank,
                               s.OstanId,
                               s.CityId,
                               s.Telegram,
                               s.Instagram,
                               s.Whatsapp,
                               s.TozihatKhedmat,
                               s.AvgRank


                           };
            var _Servicer = Servicer.ToList();
            int C = 0;
            C = _Servicer.OrderByDescending(a => a.IdServicer).Count();
            var List_ListServiser = new List<ListServiser>();
            if (C == 0)
            {
                var ListServiser1 = new ListServiser();
                ListServiser1.IdServicer = 0;
                ListServiser1.ServicesName = "";
                ListServiser1.NameFamily = "";
                ListServiser1.Img = "";
                ListServiser1.Mobail = "";
                ListServiser1.Telphon = "";
                ListServiser1.NumberGhardad = "";
                ListServiser1.NumberShaba = "";
                ListServiser1.NumberBank = "";
                ListServiser1.AvgRank = 0;
                ListServiser1.Address = "";
                ListServiser1.Telegram = "";
                ListServiser1.Instageram = "";
                ListServiser1.Watsaap = "";
                ListServiser1.TozihatKhedmat = "";
                ListServiser1.Type_Erore = "مورد یافت نشد ";

                List_ListServiser.Add(ListServiser1);
            }
            else
            {

                for (int i = 0; i < C; i++)
                {
                    //Int32 id = ListServiserZarfiyat_Mahale_[i].id;
                    //var list1 = ListServiserZarfiyat_Mahale_.Where(a => a.IdServicer ==id ).Take(1).SingleOrDefault();
                    var ListServiser1 = new ListServiser();
                    int i2 = 0;
                    i2 = i + 1;
                    ListServiser1.ID = i2;
                    ListServiser1.IdServicer = _Servicer[i].IdServicer;
                    ListServiser1.ServicesName = _Servicer[i].NameServicer;
                    ListServiser1.NameFamily = _Servicer[i].NameFamilyAdminServicer;
                    ListServiser1.Img = _Servicer[i].Img;
                    ListServiser1.Mobail = _Servicer[i].PhoneAdmin;
                    ListServiser1.Telphon = _Servicer[i].Phone1;
                    ListServiser1.NumberGhardad = _Servicer[i].NumberGharardad;
                    ListServiser1.NumberShaba = _Servicer[i].ShabaCartBank;
                    ListServiser1.NumberBank = _Servicer[i].NameBank;
                    ListServiser1.AvgRank = _Servicer[i].AvgRank;
                    ListServiser1.Address = _Servicer[i].Address;
                    ListServiser1.Telegram = _Servicer[i].Telegram;
                    ListServiser1.Instageram = _Servicer[i].Instagram;
                    ListServiser1.Watsaap = _Servicer[i].Whatsapp;
                    ListServiser1.TozihatKhedmat = _Servicer[i].TozihatKhedmat;
                    ListServiser1.Type_Erore = " ";
                    List_ListServiser.Add(ListServiser1);


                }
            }


            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                serviser = List_ListServiser
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetServerAllMahaleh2/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetServerAllMahaleh2(string Unique_code, string Serviser_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var Servicer = from s in session.Query<TServicerForMahaleh>()
                           where (
                                  s.TServicer.IdServicer == _Serviser_Id
                                  )
                           select new
                           {
                               Mahaleh = s.TLMahaleh.TitelsMahaleh,
                               City = s.TLCity.TitelsCity,
                               Ostan = s.TLOstan.TitelsOstan,
                               Titels = s.TitelsForLookups,
                               HazinehPeyk = s.TLNoeHPeyk.TitelsNoeHPeyk,
                               Vaziyat = s.TLActive.TitelsActive
                           };
            var _Servicer = Servicer.ToList();

            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                ServicerMahaleh = _Servicer
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetServerAllPeyks/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetServerAllPeyks(string Unique_code, string Serviser_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var Servicer = from s in session.Query<TPeyks>()
                           where (
                                  s.ServicerId == _Serviser_Id
                                  )
                           select new
                           {

                               s.NameFamily,
                               TitelsNoeVasileh=s.TLNoeVasileh.TitelsNoeVasileh,
                               s.NumPelakVasileh,
                               s.Phone,
                               TitelsActive= s.TLActive.TitelsActive,
                               Pass=s.Pass
                           };
            var _Servicer = Servicer.ToList();

            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                ServicerPeyks = _Servicer
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }



        public static string DayName(string DaySE)
        {
            switch (DaySE)
            {
                case "Saturday":
                    DaySE = "شنبه";
                    break;
                case "Sunday":
                    DaySE = "یکشنبه";
                    break;

                case "Monday":
                    DaySE = "دوشنبه";
                    break;
                case "Tuesday":
                    DaySE = "سه شنبه";
                    break;
                case "Wednesday":
                    DaySE = "چهار شنبه";
                    break;
                case "Thursday":
                    DaySE = "پنج شنبه";
                    break;
                case "Friday":
                    DaySE = "جمعه";
                    break;

                default:
                    // code block
                    break;
            }

            return (DaySE);

        }

        public static string DayNumber(string DaySE)
        {
            switch (DaySE)
            {
                case "Saturday":
                    DaySE = "1";
                    break;
                case "Sunday":
                    DaySE = "2";
                    break;

                case "Monday":
                    DaySE = "3";
                    break;
                case "Tuesday":
                    DaySE = "4";
                    break;
                case "Wednesday":
                    DaySE = "5";
                    break;
                case "Thursday":
                    DaySE = "6";
                    break;
                case "Friday":
                    DaySE = "7";
                    break;

                default:
                    // code block
                    break;
            }

            return (DaySE);

        }

        internal class ListMassege
        {
            public Int32 ID { get; set; }
            public Int32 IdServicer { get; set; }
            public string ServicesName { get; set; }
            public int? AvgRank { get; set; }
            public string Address { get; set; }
            public string Hazine { get; set; }
            //public string Hadenesabe_Peyk_Raygan { get; set; }
            public string MatnNamayeshHezinePeyk { get; set; }
            public string HazinehKolSefareshBishtarAz { get; set; }
            public string HazinehKolSefareshKamtarAz { get; set; }
            public string TitelsNoeHPeykElse { get; set; }
            public string Img { get; set; }
            public int? CNazarat { get; set; }
            public Int32 IdOstan { get; set; }
            public Int32 IdCity { get; set; }
            public Int32 IdMahaleh { get; set; }
            public string TitelsMahaleh { get; set; }
            public int? CountService { get; set; }
            public Int32 IdShift { get; set; }
            public string Type_Erore { get; set; }
            public string Date_Shift { get; set; }
            public string TozihatKhedmat { get; set; }

        }

        internal class ListServiser
        {
            public Int32 ID { get; set; }
            public Int32 IdServicer { get; set; }
            public string ServicesName { get; set; }
            public string NameFamily { get; set; }
            public string Img { get; set; }
            public string Mobail { get; set; }
            public string Telphon { get; set; }
            public string NumberGhardad { get; set; }
            public string NumberShaba { get; set; }
            public string NumberBank { get; set; }

            public int? AvgRank { get; set; }
            public string Address { get; set; }
            public string Telegram { get; set; }
            public string Instageram { get; set; }
            public string Watsaap { get; set; }
            public string TozihatKhedmat { get; set; }
            public string Type_Erore { get; set; }

        }

        internal class ListShift
        {
            public Int32 ListID { get; set; }
            public Int32 DayID { get; set; }
            public Int32 IdServicer { get; set; }
            public string Hazine { get; set; }
            public Int32 IdShift { get; set; }
            public string Shifttype { get; set; }
            public string Type_Erore { get; set; }
            public string Date_Shift { get; set; }
            public string Day_Name { get; set; }

        }

        internal class ListGroups
        {
            public Int32 Group_Id { get; set; }
            public string Group_Name { get; set; }
            public string Img { get; set; }
            public string Type_Erore { get; set; }
        }

        internal class ListComment
        {
            public string User_Name { get; set; }
            public string DateCreate { get; set; }
            public Int32? NumRank { get; set; }
            public string TextNazar { get; set; }
            public string Type_Erore { get; set; }
        }

        internal class ServerSpecifications
        {
            public string NameServicer { get; set; }
            public string Address { get; set; }
            public string Img { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string AvgRank { get; set; }
            public string Phone1 { get; set; }
            public string Active { get; set; }
            public string Type_Erore { get; set; }
            public string Telegram { get; set; }
            public string Instagram { get; set; }
            public string WhatsApp { get; set; }
            public string Other_Page { get; set; }
        }

        

            internal class ListChekCommentUser
        {
            public string IdServicer { get; set; }
            public string NameServicer { get; set; }
            public string ShenaseSefaresh { get; set; }
            public string IdOrder { get; set; }
            public string Datetahvil { get; set; }
            public string Img { get; set; }
            public string Type_Erore { get; set; }
        }

        


    }
}

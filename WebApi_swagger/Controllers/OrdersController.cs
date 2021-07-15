using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Linq;
using SmsIrRestful;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using WebApi_swagger.Models;

namespace WebApi_swagger.Controllers
{
    public class OrdersController : ApiController
    {

        [HttpGet]
        [Route("api/G1/GetListOrderUser/{Unique_code},{User_ID},{IsPhoneRegistered}")]
        public IHttpActionResult GetListOrderUser(string Unique_code, string User_ID, string IsPhoneRegistered)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            long UserId = 0;
            UserId = Convert.ToInt64(User_ID);
            long _UserId = 0;
            ISession session = OpenNHibertnateSession.OpenSession();

            _UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered && p.IdUsrer == UserId).Select(p => p.IdUsrer).SingleOrDefault<long>();
            var List_Order = (session.Query<TOrder>().Where(a => a.TUsers.IdUsrer == _UserId).Select(a => new
            {
                IdOrder = a.IdOrder,
                OrderDate = a.OrderNow,
                ShiftDaryaft = a.TShift.ShiftText,
                ShiftTahvil = a.TShift2.ShiftText,
                DateDaryaft = a.OrderDate,
                Datetahvil = a.Datetahvil,
                Address = a.TAddresses.Titels_Address,
                NameAddress = a.TAddresses.AddressName,
                PhoneAddress = a.TAddresses.Phone_Addresss,
                NameServicer = a.TServicer.NameServicer,
                AddressServicer = a.TServicer.Address,
                VaziyatTahvil = a.TLVaziyatTahvil.TextForUser,
                VaziyatTahvilID = a.TLVaziyatTahvil.IdVaziyatTahvil,
                NoePay = a.TLNoePay.Title,
                ShenaseSefaresh = a.ShenaseSefaresh,
                OrderPriceBof = a.OrderPriceBof,
                PeykPrice = a.PeykPrice,
                OrderPrice = a.OrderPrice

            }
            ));
            Int32 C_List = 0;
            C_List = List_Order.Count();
            var List_Orders = new List<ListOrders>();
            if (C_List == 0)
            {
                var List_Orders0 = new ListOrders();
                List_Orders0.IdOrder = 0;
                List_Orders0.OrderDate = null;
                List_Orders0.ShiftDaryaft = "";
                List_Orders0.ShiftTahvil = "";
                List_Orders0.DateDaryaft = null;
                List_Orders0.Datetahvil = null;
                List_Orders0.AddressUser = "";
                List_Orders0.NameAddress = "";
                List_Orders0.PhoneAddress = "";
                List_Orders0.NameServicer = "";
                List_Orders0.AddressServicer = "";
                List_Orders0.VaziyatTahvil = "";
                List_Orders0.VaziyatTahvil_ID = 0;
                List_Orders0.NoePay = "";
                List_Orders0.ShenaseSefaresh = 0;
                List_Orders0.OrderPriceBof = 0;
                List_Orders0.PeykPrice = 0;
                List_Orders0.OrderPrice = 0;
                List_Orders0.Type_Erore = "موردی یافت نشد";
                List_Orders.Add(List_Orders0);
            }

            else
            {
                var _List_Order = List_Order.ToList();
                for (int i = 0; i < C_List; i++)
                {
                    var List_Orders02 = new ListOrders();
                    List_Orders02.IdOrder = _List_Order[i].IdOrder;
                    List_Orders02.OrderDate = (_List_Order[i].OrderDate).ToString("yyyy/MM/dd");
                    List_Orders02.ShiftDaryaft = _List_Order[i].ShiftDaryaft;
                    List_Orders02.ShiftTahvil = _List_Order[i].ShiftTahvil;
                    if (_List_Order[i].Datetahvil != null)
                    {
                        List_Orders02.Datetahvil = (_List_Order[i].Datetahvil).ToString("yyyy/MM/dd");
                        string _DayName = "";
                        _DayName = DayName(_List_Order[i].Datetahvil.DayOfWeek.ToString());
                        List_Orders02.Daytahvil = _DayName;
                    }
                    else
                    {
                        List_Orders02.Datetahvil = "";
                        List_Orders02.Daytahvil = "";

                    }

                    if (_List_Order[i].DateDaryaft != null)
                    {
                        List_Orders02.DateDaryaft = (_List_Order[i].DateDaryaft).ToString("yyyy/MM/dd");
                        string _DayName = "";
                        _DayName = DayName(_List_Order[i].DateDaryaft.DayOfWeek.ToString());
                        List_Orders02.DayDaryaft = _DayName;
                    }
                    else
                    {
                        List_Orders02.DateDaryaft = "";
                        List_Orders02.DayDaryaft = "";

                    }

                    List_Orders02.AddressUser = _List_Order[i].Address;
                    List_Orders02.NameAddress = _List_Order[i].NameAddress;
                    List_Orders02.PhoneAddress = _List_Order[i].PhoneAddress;
                    List_Orders02.NameServicer = _List_Order[i].NameServicer;
                    List_Orders02.AddressServicer = _List_Order[i].AddressServicer;
                    List_Orders02.VaziyatTahvil = _List_Order[i].VaziyatTahvil;
                    List_Orders02.VaziyatTahvil_ID = _List_Order[i].VaziyatTahvilID;
                    List_Orders02.NoePay = _List_Order[i].NoePay;
                    List_Orders02.ShenaseSefaresh = _List_Order[i].ShenaseSefaresh;
                    List_Orders02.OrderPriceBof = _List_Order[i].OrderPriceBof;
                    List_Orders02.PeykPrice = _List_Order[i].PeykPrice;
                    List_Orders02.OrderPrice = _List_Order[i].OrderPrice;
                    List_Orders02.Type_Erore = "";
                    List_Orders.Add(List_Orders02);
                }
            }


            var json = JsonConvert.SerializeObject(new
            {
                MyListOrder = List_Orders.OrderByDescending(a => a.IdOrder)
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetListOrderDetailUser/{Unique_code},{User_ID},{Order_ID}")]
        public IHttpActionResult GetListOrderDetailUser(string Unique_code, string User_ID, string Order_ID)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            long UserId = 0;
            UserId = Convert.ToInt64(User_ID);
            long OrderId = 0;
            OrderId = Convert.ToInt64(Order_ID);
            long _UserId = 0;
            long _OrderId = 0;

            var Lists = new List<ListSabad>();
            ISession session = OpenNHibertnateSession.OpenSession();

            _UserId = session.QueryOver<TUsers>().Where(p => p.IdUsrer == UserId).Select(p => p.IdUsrer).SingleOrDefault<long>();
            _OrderId = session.QueryOver<TOrder>().Where(p => p.IdOrder == OrderId).Select(p => p.IdOrder).SingleOrDefault<long>();
            if (_UserId == 0 || _OrderId == 0)
            {

                //return Ok("موارد نادرست");
                var Group = new ListSabad();
                Group.IdSabad = 0;
                Group.NameGroup = "";
                Group.GroupProductTitels = "";
                Group.TitelsNoeKhedmat = "";
                Group.TozihatMoshtari = "";
                Group.PriceBof = 0;
                Group.Tedad = 0;
                Group.Washing = 0;
                Group.WashingIroning = 0;
                Group.Titels = ("موارد نادرست");
                Lists.Add(Group);
            }

            #region نمایش خروجی
            else
            {
                #region واکشی مقادیر مربوط به سفارشات
                var OrderSabad = session.Query<TSabad>()
                                         .Where(p => p.TOrder.IdOrder == _OrderId)
                                         .Select(x =>
                                                  new {
                                                      x.IdSabad,
                                                      x.TNoeProduct.TGroup.NameGroup,
                                                      x.TNoeProduct.TGroupProduct.GroupProductTitels,
                                                      x.TNoeProduct.TNoeKhedmat.TitelsNoeKhedmat,
                                                      x.TozihatMoshtari,
                                                      x.PriceBof,
                                                      x.Tedad,
                                                      x.TNoeProduct.TNoeKhedmat.IdNoeKhedmat,

                                                  });

                var ListOrderSabad = OrderSabad.ToList();


                int C_OrderSabad = 0;
                C_OrderSabad = ListOrderSabad.Count;

                if (C_OrderSabad != 0)
                {
                    int _WashingIroning = 0;
                    _WashingIroning = ListOrderSabad.Where(m => m.IdNoeKhedmat == 1).Count();
                    int _Washing = 0;
                    _Washing = ListOrderSabad.Where(m => m.IdNoeKhedmat == 2).Count();
                    #region قرار دادن در خروجی
                    int C_List = 0;
                    C_List = ListOrderSabad.Count;
                    for (int i = 0; i < C_List; i++)
                    {
                        var Group = new ListSabad();
                        Group.IdSabad = ListOrderSabad[i].IdSabad;
                        Group.NameGroup = ListOrderSabad[i].NameGroup;
                        Group.GroupProductTitels = ListOrderSabad[i].GroupProductTitels;
                        Group.TitelsNoeKhedmat = ListOrderSabad[i].TitelsNoeKhedmat;
                        Group.TozihatMoshtari = ListOrderSabad[i].TozihatMoshtari;
                        Group.PriceBof = ListOrderSabad[i].PriceBof;
                        Group.Tedad = ListOrderSabad[i].Tedad;
                        Group.Washing = _Washing;
                        Group.WashingIroning = _WashingIroning;
                        Group.Titels = "";
                        Lists.Add(Group);
                    }
                    #endregion


                }



                #endregion

            }


            //int ww= ListGroups_G[0]._GroupID.IdShift

            var json = JsonConvert.SerializeObject(new
            {
                ListDay = Lists
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
            #endregion
        }


        [HttpGet]
        [Route("api/G1/GetListSetVaziyatOrder/{Unique_code},{User_ID},{Order_ID}")]
        public IHttpActionResult GetListSetVaziyatOrder(string Unique_code, string User_ID, long Order_ID)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }

            ISession session = OpenNHibertnateSession.OpenSession();

            long UserId = 0;
            UserId = Convert.ToInt64(User_ID);
            long OrderId = 0;
            OrderId = Convert.ToInt64(Order_ID);
            long _UserId = 0;

            var Lists = new List<ListSabad>();

            _UserId = session.QueryOver<TUsers>().Where(p => p.IdUsrer == UserId).Select(p => p.IdUsrer).SingleOrDefault<long>();

            long _Order_ID = 0;
            _Order_ID = session.QueryOver<TOrder>().Where(p => p.IdOrder == Order_ID && p.TUsers.IdUsrer == _UserId).Select(p => p.IdOrder).SingleOrDefault<long>();

            if (_Order_ID != 0 && _UserId != 0)
            {
                int _IdVaziyatSabad = 0;
                _IdVaziyatSabad = session.QueryOver<TOrder>().Where(p => p.IdOrder == Order_ID)
                             .Select(p => p.TLVaziyatSabad.IdVaziyatSabad).SingleOrDefault<int>();

                int _Vaziyat_ID = 0;
                _Vaziyat_ID = session.QueryOver<TOrder>().Where(p => p.IdOrder == Order_ID)
                             .Select(p => p.TLVaziyatTahvil.IdVaziyatTahvil).SingleOrDefault<int>();

                if ((_IdVaziyatSabad != 1) && (_Vaziyat_ID != 1008 || _Vaziyat_ID != 6))
                {
                    return Ok("شما نمی توانید مورد را تغییر بدهید");
                }
                else
                {
                    #region آپدیث سبد خرید
                    var emp = session.QueryOver<TOrder>().Where(p => p.IdOrder == Order_ID).SingleOrDefault();
                    emp.TLVaziyatSabad = session.QueryOver<TLVaziyatSabad>().Where(p => p.IdVaziyatSabad == 1).SingleOrDefault();
                    emp.TLVaziyatTahvil = session.QueryOver<TLVaziyatTahvil>().Where(p => p.IdVaziyatTahvil == 5).SingleOrDefault();

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(emp);
                        transaction.Commit();
                        return Ok("مورد ثبت شد");
                    }
                    #endregion
                }
            }

            #region نمایش خروجی


            else if (_Order_ID == 0 || _UserId == 0)
            {
                return Ok("مورد یافت نشد");
            }
            return Ok("مورد یافت نشد");

            #endregion

        }



        [HttpGet]
        [Route("api/G1/GetPeykId/{Unique_code},{Serviser_Id},{Address_ID},{ShiftId},{Day},{ShiftIdTahvil},{DateTahvil}")]
        public IHttpActionResult GetPeykId(string Unique_code, string Serviser_Id, string Address_ID, string ShiftId, int Day, int ShiftIdTahvil, string DateTahvil)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            long IsPeykId = 0;
            int _Serviser_Id = Convert.ToInt32(Serviser_Id);
            long IsAddress_ID = Convert.ToInt64(Address_ID);
            int _Mahale_ID = 0;
            _Mahale_ID = session.QueryOver<TAddresses>().Where(p => p.IdAddresses == IsAddress_ID).Select(S => S.TLMahaleh.IdMahaleh).SingleOrDefault<int>();
            int IsShiftId = Convert.ToInt16(ShiftId);

            int IsDay = Convert.ToInt16(Day);
            DateTime dayDate = System.DateTime.Now.AddDays(IsDay);
            DateTime _DateTahvil = Convert.ToDateTime(DateTahvil);





            #region واکشی پیک مورد نظر که ظرفیت خالی دارد
            var listAllPeyks = session.Query<TServicerForMahalehByPeyk>()
                                    .Where(p => p.TServicerForMahaleh.TServicer.IdServicer == _Serviser_Id && p.TShift.IdShift == IsShiftId && p.TLActive.IdActive == 1 && p.TServicerForMahaleh.TLMahaleh.IdMahaleh == _Mahale_ID)
                                    .Select(x =>
                                             new {
                                                 x.IdServicerForMahalehByPeyk,
                                                 IdPeyks = x.TPeyks.IdPeyks,
                                                 OlaviatEntekhab = x.OlaviatEntekhab,
                                                 x.Countservice
                                             }).ToList();

            DateTime? timepeyk = DateTime.Now.AddDays(IsDay);
            //var listAllPeyksGive = session.Query<TZarfiyatShiftPeyks>()
            //                              .Where(p => p.Datenowgiv== time && p.TShift.IdShift==IsShiftId)
            //                              .Select(x =>
            //                                       new {
            //                                           x.TPeyks.IdPeyks,
            //                                           CountServiceGiv= x.CountService
            //                                       }).ToList();
            #region جداسازی لیست پیک هایی که برای دریافت کالاظرفیت دارند
            var listAllPeyksGive = from s in session.Query<TZarfiyatShiftPeyks>()
                                   where (s.Datenowgiv == timepeyk && s.TShift.IdShift == IsShiftId)
                                   join l2 in session.Query<TServicerForMahalehByPeyk>()
                                                               on s.TPeyks.IdPeyks equals l2.TPeyks.IdPeyks
                                   where (l2.TServicerForMahaleh.TServicer.IdServicer == _Serviser_Id && l2.TShift.IdShift == IsShiftId && l2.TLActive.IdActive == 1 && l2.TServicerForMahaleh.TLMahaleh.IdMahaleh == _Mahale_ID)
                                   select new
                                   {
                                       IdPeyks = s.TPeyks.IdPeyks,
                                       CountserviceDefult = s.CountService,
                                       CountServiceGiv = l2.Countservice,
                                       OlaviatEntekhab = l2.OlaviatEntekhab
                                   };
            var listAllPeyksGive_ = listAllPeyksGive.ToList();


            int C_Peykgive = 0;
            C_Peykgive = listAllPeyksGive.Count();
            if (C_Peykgive != 0) /// پیک ها سرویسی برای دریافت کالا داشتن 
            {
                #region حذف پیک هایی که ظرفیت دریافت آنه پر شده است
                for (int i = 0; i < C_Peykgive; i++)
                {
                    long _peykID = listAllPeyksGive_[i].IdPeyks;
                    int? s1_Giv = 0;
                    long s1_PeykID = 0;
                    s1_Giv = listAllPeyksGive_[i].CountServiceGiv;
                    s1_PeykID = listAllPeyksGive_[i].IdPeyks;
                    listAllPeyks.RemoveAll(x => x.Countservice < listAllPeyksGive_[i].CountServiceGiv && x.IdPeyks == s1_PeykID);

                }
                #endregion
            }

            if (listAllPeyks.Count == 0) /// ظرفیت همه پیک ها پر است
            {
                return Ok("ظرفیت پیک ها پر است");
            }
            #endregion

            #region جداسازی لیست پیک هایی که برای ارسال کالاظرفیت دارند
            var listAllPeyksGo = from s in session.Query<TCGoZarfiyatShPeyks>()
                                 where (s.Datenowgo == _DateTahvil && s.TShift.IdShift == IsShiftId)
                                 join l2 in session.Query<TServicerForMahalehByPeyk>()
                                                             on s.TPeyks.IdPeyks equals l2.TPeyks.IdPeyks
                                 where (l2.TServicerForMahaleh.TServicer.IdServicer == _Serviser_Id && l2.TShift.IdShift == IsShiftId && l2.TLActive.IdActive == 1 && l2.TServicerForMahaleh.TLMahaleh.IdMahaleh == _Mahale_ID)
                                 select new
                                 {
                                     IdPeyks = s.TPeyks.IdPeyks,
                                     CountServiceGo = s.CountGoPeyks,
                                     Countservice = l2.Countservice,
                                     OlaviatEntekhab = l2.OlaviatEntekhab
                                 };
            var listAllPeyksGo_ = listAllPeyksGo.ToList();


            int C_Peykgive2 = 0;
            C_Peykgive2 = listAllPeyksGo_.Count();
            if (C_Peykgive2 != 0) /// پیک ها سرویسی برای دریافت کالا داشتن 
            {
                #region حذف پیک هایی که ظرفیت دریافت آنه پر شده است
                for (int i = 0; i < C_Peykgive2; i++)
                {
                    long _peykID = listAllPeyksGo_[i].IdPeyks;
                    int? s1_Giv = 0;
                    long s1_PeykID = 0;
                    s1_Giv = listAllPeyksGo_[i].CountServiceGo;
                    s1_PeykID = listAllPeyksGo_[i].IdPeyks;
                    listAllPeyks.RemoveAll(x => x.Countservice < listAllPeyksGo_[i].CountServiceGo && x.IdPeyks == s1_PeykID);

                }
                #endregion
            }

            if (listAllPeyks.Count == 0) /// ظرفیت همه پیک ها پر است
            {
                return Ok("ظرفیت پیک ها پر است");
            }
            #endregion

            #region جوین کردن هر دو ظرفیت پیک های دریافت و ارسال و انتخاب یک پیک با اولویت بالا
            var listAllPeyks0 = from s in listAllPeyksGo_
                                join l2 in listAllPeyksGive_
                                                            on s.IdPeyks equals l2.IdPeyks
                                select new
                                {
                                    IdPeyks = s.IdPeyks,
                                    OlaviatEntekhab = l2.OlaviatEntekhab
                                };
            var listAllPeyks0_ = listAllPeyks0.ToList();
            //if (listAllPeyks0_.Count == 0)
            //{
            //    return Ok("پیکی وجود ندارد");
            //}
            //else
            //{
            //    IsPeykId = listAllPeyks.OrderByDescending(i => i.OlaviatEntekhab).Select(y => y.IdPeyks).Take(1).SingleOrDefault<long>();
            //}
            #endregion

            //else 
            if (listAllPeyks.Count != 0)
            {
                IsPeykId = listAllPeyks.OrderByDescending(i => i.OlaviatEntekhab).Select(y => y.IdPeyks).Take(1).SingleOrDefault<long>();
            }

            return Ok(IsPeykId.ToString());


            #endregion

        }


        [HttpGet]
        [Route("api/G1/GetFinalizatioOofPurchase/{Phone},{Id},{Serviser_Id},{Address_ID},{Product_Tedad},{NoeProduct_ID},{ShiftId},{PeykId},{Tozihat},{Day},{TemporderId_},{End_Record_},{ShiftIdTahvil},{DateTahvil},{DateDaryaft},{tCodeTakhfif_ID}")]
        public IHttpActionResult GetFinalizatioOofPurchase(string Phone, string Id, string Serviser_Id, string Address_ID, string Product_Tedad, string NoeProduct_ID, string ShiftId, string PeykId, string Tozihat, int Day, long TemporderId_, int End_Record_, int ShiftIdTahvil, DateTime DateTahvil, DateTime DateDaryaft, int tCodeTakhfif_ID)
        {
            if (Phone == "{Phone}" || Id == "{Id}" || Address_ID == "{Address_ID}" || NoeProduct_ID == "{NoeProduct_ID}" || Product_Tedad == "{Product_Tedad}" || ShiftId == "{ShiftId}" || PeykId == "{PeykId}" || Day == null)
            {
                return Ok("مقادیر خالی");
            }
            if (Phone == "0" || Id == "0" || Address_ID == "0" || NoeProduct_ID == "0" || Product_Tedad == "0" || ShiftId == "0" || PeykId == "0" || Day == null)
            {
                return Ok("مقادیر خالی");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            int _Serviser_Id = Convert.ToInt32(Serviser_Id);
            string IsPhoneRegistered = Phone;
            long IsAddress_ID = Convert.ToInt64(Address_ID);
            int _Mahale_ID = 0;
            int IsProduct_Tedad = Convert.ToInt16(Product_Tedad);
            long IsNoeProduct_ID = Convert.ToInt64(NoeProduct_ID);

            int IsShiftId = Convert.ToInt16(ShiftId);

            int IsPeykId = Convert.ToInt16(PeykId);

            int IsDay = Convert.ToInt16(Day);
            //DateTime now = DateTime.Now;
            //DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day + IsDay);
            string day = System.DateTime.Now.AddDays(IsDay).DayOfWeek.ToString();
            int _Peyk_Gherymat = 0;
            int _Nan_Gherymat = 0;
            int Peyk_Gherymat = 0;
            int Nan_Gherymat = 0;
            string _noeNan;
            long IsId = Convert.ToInt64(Id);
            long _UserId = 0;
            int _Mahale_Active = 0;
            DateTime _DateTahvil = Convert.ToDateTime(DateTahvil);
            DateTime _DateDaryaft = Convert.ToDateTime(DateDaryaft);
            //try
            //{
            var ListMasseges = new List<Massege_Temp>();
            var Masseges = new Massege_Temp();

            _UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered && p.IdUsrer == IsId).Select(p => p.IdUsrer).SingleOrDefault<long>();
            _Mahale_ID = session.QueryOver<TAddresses>().Where(p => p.IdAddresses == IsAddress_ID && p.TUsers.IdUsrer == IsId).Select(S => S.TLMahaleh.IdMahaleh).SingleOrDefault<int>();
            if (_UserId != 0)
            {
                if (_Mahale_ID != 0)
                {
                    _Mahale_Active = session.QueryOver<TLMahaleh>().Where(p => p.TLActive.IdActive == 1 && p.IdMahaleh == _Mahale_ID).Select(S => S.TLActive.IdActive).SingleOrDefault<int>();
                    if (_Mahale_Active == 0)
                    {
                        //return Ok("محله مورد نظر غیر فعال می باشد");
                        Masseges.IDTemp = 0;
                        Masseges.Shenaseh = "";
                        Masseges.Types = "محله مورد نظر غیر فعال می باشد";
                        ListMasseges.Add(Masseges);

                        var json1 = JsonConvert.SerializeObject(new
                        {
                            //Allserviser = ListServiserZarfiyat_Mahale_
                            Mahaleh = ListMasseges
                        });

                        JObject json51 = JObject.Parse(json1);
                        session.Close();
                        return Ok(json51);
                    }
                }
                if (_Mahale_ID == 0)
                {
                    //return Ok("آدرس مورد نظر یافت نشد");
                    Masseges.IDTemp = 0;
                    Masseges.Shenaseh = "";
                    Masseges.Types = "آدرس مورد نظر یافت نشد";
                    ListMasseges.Add(Masseges);


                    var json2 = JsonConvert.SerializeObject(new
                    {
                        //Allserviser = ListServiserZarfiyat_Mahale_
                        Mahaleh = ListMasseges
                    });

                    JObject json52 = JObject.Parse(json2);
                    session.Close();
                    return Ok(json52);
                }

                //string day = System.DateTime.Now.DayOfWeek.ToString();

                //if (day == "Friday")
                //{
                //    //return Ok("امروز جمعست");
                //    Masseges.IDTemp = 0;
                //    Masseges.Shenaseh = "";
                //    Masseges.Types = "امروز جمعست";
                //    ListMasseges.Add(Masseges);

                //    var json3 = JsonConvert.SerializeObject(new
                //    {
                //        //Allserviser = ListServiserZarfiyat_Mahale_
                //        Mahaleh = ListMasseges
                //    });

                //    JObject json53 = JObject.Parse(json3);
                //    session.Close();
                //    return Ok(json53);
                //}


                else ///اگر هر روز غیر از جمعه بود
                {
                    #region اگر اولین محصول از اولین ثبت سفرش بود یعنی از قبل سفارش برای این کاربر ثبت نشده بود
                    if (TemporderId_ == 0)
                    {
                        long _IdNoeProduct = 0;
                        var List_Product = session.Query<TNoeProduct>().Where(p => p.IdNoeProduct == IsNoeProduct_ID).Select(x =>
                                new {
                                    x.IdNoeProduct,
                                    x.Maxcount,
                                    x.Mincount
                                });

                        _IdNoeProduct = List_Product.Select(a => a.IdNoeProduct).SingleOrDefault<long>();
                        if (_IdNoeProduct != IsNoeProduct_ID)
                        {
                            //return Ok("کد محصول نامشخص");
                            Masseges.IDTemp = 0;
                            Masseges.Shenaseh = "";
                            Masseges.Types = "کد محصول نامشخص";
                            ListMasseges.Add(Masseges);

                            var json4 = JsonConvert.SerializeObject(new
                            {
                                //Allserviser = ListServiserZarfiyat_Mahale_
                                Mahaleh = ListMasseges
                            });

                            JObject json54 = JObject.Parse(json4);
                            session.Close();
                            return Ok(json54);
                        }
                        int? _max_Tedad = List_Product.Select(a => a.Maxcount).SingleOrDefault<int?>();
                        int? _min_Tedad = List_Product.Select(a => a.Mincount).SingleOrDefault<int?>();
                        if ((IsProduct_Tedad > _max_Tedad) || (_min_Tedad > IsProduct_Tedad))
                        {
                            //return Ok("تعداد محصول درخواستی بیش از حد مجاز");
                            Masseges.IDTemp = 0;
                            Masseges.Shenaseh = "";
                            Masseges.Types = "تعداد محصول درخواستی بیش از حد مجاز";
                            ListMasseges.Add(Masseges);

                            var json56 = JsonConvert.SerializeObject(new
                            {
                                //Allserviser = ListServiserZarfiyat_Mahale_
                                Mahaleh = ListMasseges
                            });

                            JObject json556 = JObject.Parse(json56);
                            session.Close();
                            return Ok(json556);
                        }

                        #region استخراج شیفتای با قابلیت انتخاب از 2 ساعت دیگه
                        var time = DateTime.Now.AddDays(IsDay).TimeOfDay;
                        TimeSpan span = DateTime.Now.TimeOfDay;
                        int hours = time.Hours;
                        int minutes = time.Minutes;

                        //23:35

                        var ListShift = from p in session.Query<TShift>()
                                        where
                                        (
                                        (
                                        IsDay == 0 &&
                                        (
                                        ((p.ShiftHhShowStartTime > hours) || (p.ShiftHhShowStartTime == hours && p.ShiftMmShowStartTime + 15 >= minutes))
                                        ||
                                        ((p.ShiftHhEndStartTime > hours) || (p.ShiftHhEndStartTime == hours && p.ShiftMmEndStartTime + 15 >= minutes))
                                        )
                                         && p.TLActive.IdActive == 1
                                        && p.IdShift == IsShiftId
                                        ) ||

                                        (
                                        IsDay != 0
                                         && p.TLActive.IdActive == 1
                                        && p.IdShift == IsShiftId
                                        )


                                        )///افزوده شد apiبرای این 
                                        //||
                                        //(IsDay == 1 && p.TLActive.Id == 1 &&
                                        //p.ShiftId == IsShiftId)
                                        select new
                                        {
                                            ShiftId = p.IdShift,
                                            ShiftText = p.ShiftText
                                        };
                        #endregion
                        int C_ListShift = 0;
                        C_ListShift = ListShift.Count();
                        if (C_ListShift == 0 && IsDay == 0)
                        {
                            //return Ok("متاسفانه زمان سفارش گیری شیفت مورد نظر به اتمام رسیده ");
                            Masseges.IDTemp = 0;
                            Masseges.Shenaseh = "";
                            Masseges.Types = "متاسفانه زمان سفارش گیری شیفت مورد نظر به اتمام رسیده ";
                            ListMasseges.Add(Masseges);

                            var json7 = JsonConvert.SerializeObject(new
                            {
                                //Allserviser = ListServiserZarfiyat_Mahale_
                                Mahaleh = ListMasseges
                            });

                            JObject json57 = JObject.Parse(json7);
                            session.Close();
                            return Ok(json57);
                        }
                        var all_shift = ListShift.ToList();// لیستی از همه شیفت های تعریف شده از 2 ساعت به بعد
                                                           //List<int> reserved_shift = new List<int>();
                        if ((all_shift != null) && (all_shift.Count() != 0))
                        {
                            int C_Nanvaei_mojud = 0;
                            #region استخراج سرویس دهنده که با محله و ادرس ارسالی و شیفت های قابل انتخاب می توانن انتخاب بشن
                            var ListServiserZarfiyat_Mahale = from s in session.Query<TZarfiyatShiftServicer>()
                                                              where (s.TServicer.IdServicer == _Serviser_Id)
                                                              join l2 in session.Query<TServicerForMahaleh>()
                                                                               on s.TServicer.IdServicer equals l2.TServicer.IdServicer
                                                              where (l2.TLActive.IdActive == 1 &&
                                                                     l2.TLMahaleh.IdMahaleh == _Mahale_ID &&
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
                                                                  IdShift = s.TShift.IdShift
                                                              };
                            var ListServiserZarfiyat_Mahale_ = ListServiserZarfiyat_Mahale.ToList();

                            #region چک کردن ظرفیت  باز برای سفارش گیری حتی برای در شیفت  و روز خاص
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
                            int count_Item2 = 0;
                            int count_Item3 = 0;
                            int C = ListServiserZarfiyat_Mahale_.Count();
                            for (int i = 0; i < C; i++)
                            {
                                if (Day == 0)
                                {
                                    _IdServiser1 = 0;
                                    _IdServiser1 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                                    p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                                    &&
                                    p.Datenowgo == Time_Now
                                    //&& p.Datenowgo == Time_Now1 && p.Datenowgo == Time_Now2 
                                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                                    if (_IdServiser1 != 0)
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

                                if (Day == 1)
                                {

                                    _IdServiser2 = 0;
                                    _IdServiser2 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                                    p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                                    //&& p.Datenowgo == Time_Now
                                    && p.Datenowgo == Time_Now1
                                    //&& p.Datenowgo == Time_Now2 
                                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();
                                    if (_IdServiser2 != 0)
                                    {

                                        count_Item2 = ListServiserZarfiyat_Mahale_.Where(x => x.IdServicer == _IdServiser2 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift).Count();
                                        if (count_Item2 != 0)
                                        {
                                            ListServiserZarfiyat_Mahale_.RemoveAll(x => x.IdServicer == _IdServiser2 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift);
                                            C = ListServiserZarfiyat_Mahale_.Count();
                                        }

                                        if (count_Item2 != 0)
                                        {
                                            i = i - 1;
                                        }

                                        count_Item2 = 0;
                                    }
                                }

                                if (Day == 2)
                                {
                                    _IdServiser3 = 0;
                                    _IdServiser3 = session.QueryOver<TCGoZarfiyatShServiser>().Where(p =>
                                    p.CountGoService >= ListServiserZarfiyat_Mahale_[i].CountService
                                    //&& p.Datenowgo == Time_Now
                                    //&& p.Datenowgo == Time_Now1
                                    && p.Datenowgo == Time_Now2
                                    && p.ShiftId == ListServiserZarfiyat_Mahale_[i].IdShift
                                    ).Select(p => p.TServicer.IdServicer).SingleOrDefault<int>();

                                    if (_IdServiser1 != 0)
                                    {

                                        count_Item3 = ListServiserZarfiyat_Mahale_.Where(x => x.IdServicer == _IdServiser3 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift).Count();
                                        if (count_Item3 != 0)
                                        {
                                            ListServiserZarfiyat_Mahale_.RemoveAll(x => x.IdServicer == _IdServiser3 && x.IdShift == ListServiserZarfiyat_Mahale_[i].IdShift);
                                            C = ListServiserZarfiyat_Mahale_.Count();
                                        }

                                        if (count_Item3 != 0)
                                        {
                                            i = i - 1;
                                        }

                                        count_Item3 = 0;
                                    }
                                }




                            }
                            #endregion
                            #endregion
                            #region استخراج کلیه شیفت ها و سرویس دهندگان تعطیل از جدولTHoliday 
                            //DateTime now2 = DateTime.Parse("2018/02/24");
                            DateTime now_Holiday = DateTime.Now.AddDays(IsDay);
                            DateTime Time_NowHoliday = new DateTime(now_Holiday.Year, now_Holiday.Month, now_Holiday.Day);

                            var ListHoliday = from p in session.Query<THoliday>()
                                              where (p.HolidayDate == Time_NowHoliday)
                                                 && (p.TShift.IdShift == IsShiftId)
                                                && (p.TServicer.IdServicer == _Serviser_Id)
                                              select new
                                              {
                                                  IdServicer = p.TServicer.IdServicer,
                                                  IdShift = p.TShift.IdShift
                                              };
                            var ListHoliday2 = ListHoliday.ToList();
                            #endregion
                            #region استخراج  سرویس دهنده  این محله بر اساس شیفت هایی که تعطیل نیستن
                            for (int i = 0; i < ListHoliday2.Count; i++)
                            {
                                ListServiserZarfiyat_Mahale_.RemoveAll(x => x.IdShift == ListHoliday2[i].IdShift && x.IdServicer == ListHoliday2[i].IdServicer);
                            }
                            #endregion
                            int C_ListServiserZarfiyat_Mahale = ListServiserZarfiyat_Mahale_.Count;
                            #region انتخاب پیک متناسب با این سرویس دهنده
                            var List_Peyk = from p in session.Query<TServicerForMahalehByPeyk>()
                                            where (p.TServicerForMahaleh.TServicer.IdServicer == _Serviser_Id
                                                                                           && p.TShift.IdShift == IsShiftId)
                                            select new
                                            {
                                                IdPeyks = p.TPeyks.IdPeyks,
                                                IdServicer = p.TServicerForMahaleh.TServicer.IdServicer,
                                                IdShift = p.TShift.IdShift,
                                                OlaviatEntekhab = p.OlaviatEntekhab,
                                                Countservice = p.Countservice
                                            };
                            //var List_Peyk2 =  session.Query<TServicerForMahalehByPeyk>()
                            //                .Where (p=>p.TServicerForMahaleh.TServicer.IdServicer == _Serviser_Id
                            //                                                               && p.TShift.IdShift == IsShiftId)
                            //                .Select( p=> new
                            //                {
                            //                    IdPeyks = p.TPeyks.IdPeyks,
                            //                    IdServicer = p.TServicerForMahaleh.TServicer.IdServicer,
                            //                    IdShift = p.TShift.IdShift,
                            //                    OlaviatEntekhab = p.OlaviatEntekhab,
                            //                    Countservice = p.Countservice
                            //                });
                            int ff = List_Peyk.Count();
                            var List_PeykOk = List_Peyk.ToList();

                            int _IdPeyk1 = 0;
                            int _IdPeyk2 = 0;
                            int _IdPeyk3 = 0;
                            int count_Item_Peyk = 0;
                            int CPeyk = List_PeykOk.Count;
                            for (int i = 0; i < CPeyk; i++)
                            {
                                _IdPeyk1 = 0;
                                _IdPeyk1 = session.QueryOver<TCGoZarfiyatShPeyks>().Where(p =>
                                p.CountGoPeyks >= List_PeykOk[i].Countservice
                                &&
                                p.Datenowgo == Time_Now
                                //&& p.Datenowgo == Time_Now1 && p.Datenowgo == Time_Now2 
                                && p.TShift.IdShift == List_PeykOk[i].IdShift
                                ).Select(p => p.TPeyks.IdPeyks).SingleOrDefault<int>();

                                _IdPeyk2 = 0;
                                _IdPeyk2 = session.QueryOver<TCGoZarfiyatShPeyks>().Where(p =>
                               p.CountGoPeyks >= List_PeykOk[i].Countservice
                                //&& p.Datenowgo == Time_Now
                                && p.Datenowgo == Time_Now1
                                //&& p.Datenowgo == Time_Now2 
                                && p.TShift.IdShift == List_PeykOk[i].IdShift
                                ).Select(p => p.TPeyks.IdPeyks).SingleOrDefault<int>();

                                _IdPeyk3 = 0;
                                _IdPeyk3 = session.QueryOver<TCGoZarfiyatShPeyks>().Where(p =>
                               p.CountGoPeyks >= List_PeykOk[i].Countservice
                                //&& p.Datenowgo == Time_Now
                                //&& p.Datenowgo == Time_Now1
                                && p.Datenowgo == Time_Now2
                                 && p.TShift.IdShift == List_PeykOk[i].IdShift
                                ).Select(p => p.TPeyks.IdPeyks).SingleOrDefault<int>();

                                if (_IdPeyk1 == _IdPeyk2 && _IdPeyk2 == _IdPeyk3 && _IdPeyk1 == _IdPeyk3)
                                {
                                    count_Item_Peyk = List_PeykOk.Where(x => x.IdPeyks == _IdPeyk1 && x.IdShift == List_PeykOk[i].IdShift).Count();
                                    if (count_Item_Peyk != 0)
                                    {
                                        List_PeykOk.RemoveAll(x => x.IdPeyks == _IdPeyk1 && x.IdShift == List_PeykOk[i].IdShift);
                                        C = List_PeykOk.Count();
                                    }

                                    //C = C - count_Item;
                                    if (count_Item_Peyk != 0)
                                    {
                                        i = i - 1;
                                    }

                                    count_Item_Peyk = 0;
                                }

                            }

                            #endregion

                            #region جوین کردن سرویس دهنده با پیک و انتخاب پیک با اولویت بالا
                            var Entekhab_Peyks = (from l1 in ListServiserZarfiyat_Mahale_
                                                  join l2 in List_PeykOk
                                                   on l1.IdServicer equals l2.IdServicer
                                                  select new
                                                  {
                                                      IdServicer = l1.IdServicer,
                                                      IdPeyks = l2.IdPeyks,
                                                      OlaviatEntekhab = l2.OlaviatEntekhab

                                                  }).OrderBy(a => a.OlaviatEntekhab);

                            var List_Entekhab_Peyks = Entekhab_Peyks.ToList();
                            long _Peyks_ID_Entekabi = List_Entekhab_Peyks[0].IdPeyks;
                            #endregion
                            //return Ok(List_Shift_add_Nanvaei_mojud);

                            if (C_ListServiserZarfiyat_Mahale != null && C_ListServiserZarfiyat_Mahale != 0)
                            {
                                #region تشخیص اینکه نوع پرداخت در جدول T_L_noe_pay وجود دارد
                                /// باید تکمیل شود
                                #endregion
                                //return Ok(List_Ok_list3);
                                DateTime TempOrder_Date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                                //UInt16 TempOrder_Price = 100;
                                long tNoeProduct_ID = IsNoeProduct_ID;
                                int tProduct_Tedad = IsProduct_Tedad;
                                //int tDay_ID = List_Ok_list3.Select(x => x.DayId).SingleOrDefault<int>();
                                int tShift_ID = IsShiftId;
                                int tShiftIdTahvil = ShiftIdTahvil;
                                int tMahale_ID = _Mahale_ID;
                                long tUser_ID = _UserId;
                                long tAddress_ID = IsAddress_ID;
                                long tPeyk_ID = _Peyks_ID_Entekabi;
                                int tServiser_ID = _Serviser_Id;
                                int tVaziat_Pardakht_ID = 0;
                                int tCodeTakhfif_ID_ = tCodeTakhfif_ID;
                                int tNoeKhedmat_ID = session.QueryOver<TNoeProduct>().Where(p => p.IdNoeProduct == tNoeProduct_ID).Select(p => p.TNoeKhedmat.IdNoeKhedmat).SingleOrDefault<int>();
                                #region ثبت در Temp
                                int TempOrder_Price = Price(tServiser_ID, tPeyk_ID, IsNoeProduct_ID, IsProduct_Tedad);
                                TempOrder_Date = Convert.ToDateTime(DateDaryaft);
                                //DateTime DateTahvil_ = Convert.ToDateTime(DateTahvil);
                                var _Temp_order = Temp_order(Phone, TempOrder_Date, TempOrder_Price, tNoeProduct_ID, tNoeKhedmat_ID, tProduct_Tedad,
                                    tShift_ID, tMahale_ID, tUser_ID, tAddress_ID, tPeyk_ID, Tozihat, tServiser_ID, tVaziat_Pardakht_ID, TemporderId_, End_Record_, tShiftIdTahvil, DateTahvil, tCodeTakhfif_ID_);
                                if (_Temp_order.Contains("error"))
                                {
                                    //return Ok(_Temp_order);
                                    Masseges.IDTemp = 0;
                                    Masseges.Shenaseh = "";
                                    Masseges.Types = _Temp_order;
                                    ListMasseges.Add(Masseges);

                                    var json8 = JsonConvert.SerializeObject(new
                                    {
                                        //Allserviser = ListServiserZarfiyat_Mahale_
                                        Mahaleh = ListMasseges
                                    });

                                    JObject json58 = JObject.Parse(json8);
                                    session.Close();
                                    return Ok(json58);
                                }
                                else
                                {
                                    //return Ok(_Temp_order);
                                    long _TempID_Fun = Convert.ToInt64(_Temp_order);
                                    long _Shenaseh_Fun = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == _TempID_Fun).Select(p => p.ShenaseSefaresh).SingleOrDefault<long>();
                                    Masseges.IDTemp = _TempID_Fun;
                                    Masseges.Shenaseh = _Shenaseh_Fun.ToString();
                                    Masseges.Types = _Temp_order;
                                    ListMasseges.Add(Masseges);

                                    var json9 = JsonConvert.SerializeObject(new
                                    {
                                        //Allserviser = ListServiserZarfiyat_Mahale_
                                        Mahaleh = ListMasseges
                                    });

                                    JObject json59 = JObject.Parse(json9);
                                    session.Close();
                                    return Ok(json59);
                                }
                                #endregion
                            }
                            else
                            {
                                session.Close();
                                //return Ok("متاسفانه ظرفیت این شیف پر است لطفا شیفت دیگری را انتخاب کنید");
                                Masseges.IDTemp = 0;
                                Masseges.Shenaseh = "";
                                Masseges.Types = "متاسفانه ظرفیت این شیف پر است لطفا شیفت دیگری را انتخاب کنید";
                                ListMasseges.Add(Masseges);

                                var json10 = JsonConvert.SerializeObject(new
                                {
                                    //Allserviser = ListServiserZarfiyat_Mahale_
                                    Mahaleh = ListMasseges
                                });

                                JObject json510 = JObject.Parse(json10);
                                session.Close();
                                return Ok(json510);
                            }
                            //return Ok(List_Ok_list3);
                        }
                    }
                }
                #endregion

                #region محصولات بعدی جهت ثبت در ثبت سفارشات هستن و نیازی به درج مجدد در Temp نیست
                if (TemporderId_ != 0)
                {

                    DateTime now = DateTime.Now;
                    DateTime TempOrder_Date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                    //UInt16 TempOrder_Price = 100;
                    long tNoeProduct_ID = IsNoeProduct_ID;
                    int tProduct_Tedad = IsProduct_Tedad;
                    //int tDay_ID = List_Ok_list3.Select(x => x.DayId).SingleOrDefault<int>();
                    int tShift_ID = IsShiftId;
                    int tShiftIdTahvil = ShiftIdTahvil;
                    int tMahale_ID = _Mahale_ID;
                    long tUser_ID = _UserId;
                    long tAddress_ID = IsAddress_ID;
                    long tPeyk_ID = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == TemporderId_).Select(p => p.TPeyks.IdPeyks).SingleOrDefault<long>();
                    int tServiser_ID = _Serviser_Id;
                    int tVaziat_Pardakht_ID = 0;
                    int tCodeTakhfif_ID_ = tCodeTakhfif_ID;
                    int tNoeKhedmat_ID = session.QueryOver<TNoeProduct>().Where(p => p.IdNoeProduct == tNoeProduct_ID).Select(p => p.TNoeKhedmat.IdNoeKhedmat).SingleOrDefault<int>();
                    #region ثبت در Temp
                    int TempOrder_Price = Price(tServiser_ID, tPeyk_ID, IsNoeProduct_ID, IsProduct_Tedad);
                    TempOrder_Date = Convert.ToDateTime(DateDaryaft);
                    //DateTime DateTahvil_ = Convert.ToDateTime(DateTahvil);
                    var _Temp_order = Temp_order(Phone, TempOrder_Date, TempOrder_Price, tNoeProduct_ID, tNoeKhedmat_ID, tProduct_Tedad,
                        tShift_ID, tMahale_ID, tUser_ID, tAddress_ID, tPeyk_ID, Tozihat, tServiser_ID, tVaziat_Pardakht_ID, TemporderId_, End_Record_, tShiftIdTahvil, DateTahvil, tCodeTakhfif_ID_);
                    if (_Temp_order.Contains("error"))
                    {
                        //return Ok(_Temp_order);
                        Masseges.IDTemp = 0;
                        Masseges.Shenaseh = "";
                        Masseges.Types = _Temp_order;
                        ListMasseges.Add(Masseges);

                        var json11 = JsonConvert.SerializeObject(new
                        {
                            //Allserviser = ListServiserZarfiyat_Mahale_
                            Mahaleh = ListMasseges
                        });

                        JObject json511 = JObject.Parse(json11);
                        session.Close();
                        return Ok(json511);
                    }
                    else
                    {
                        //return Ok(_Temp_order);
                        //long _TempID_Fun =Convert.ToInt64(_Temp_order);
                        //long _Shenaseh_Fun = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == _TempID_Fun).Select(p => p.ShenaseSefaresh).SingleOrDefault<long>();
                        Masseges.IDTemp = 0;
                        Masseges.Shenaseh = "";
                        Masseges.Types = _Temp_order;
                        ListMasseges.Add(Masseges);

                        var json12 = JsonConvert.SerializeObject(new
                        {
                            //Allserviser = ListServiserZarfiyat_Mahale_
                            Mahaleh = ListMasseges
                        });

                        JObject json512 = JObject.Parse(json12);
                        session.Close();
                        return Ok(json512);
                    }
                    #endregion
                }
                #endregion

                //Nanvaei_mojud.RemoveAll(x => x.FirstName == "Bob");

                //    if ((Nanvaei_mojud == null) && (Nanvaei_mojud.Count() == 0))
                //    {
                //        C_Nanvaei_mojud = 0;
                //    }
                //    else if ((Nanvaei_mojud != null) && (Nanvaei_mojud.Count() != 0))
                //    {
                //        return Ok(Nanvaei_mojud);
                //    }
                //    if (C_Nanvaei_mojud == 0)
                //    {
                //        session.Close();
                //        return Ok("لیست خالی");
                //    }
                //}
                //else ///شیفتی نیست
                //{
                //    session.Close();
                //    return Ok("لیست خالی");
                //}



            }
            else
            {
                session.Close();
                //return Ok("کاربر یافت نشد");
                Masseges.IDTemp = 0;
                Masseges.Shenaseh = "";
                Masseges.Types = "کاربر یافت نشد";
                ListMasseges.Add(Masseges);

                var json13 = JsonConvert.SerializeObject(new
                {
                    //Allserviser = ListServiserZarfiyat_Mahale_
                    Mahaleh = ListMasseges
                });

                JObject json513 = JObject.Parse(json13);
                session.Close();
                return Ok(json513);
            }
            //}
            //catch
            //{
            //    session.Close();
            //    return Ok("error");
            //}
            session.Close();
            //return Ok("error");
            Masseges.IDTemp = 0;
            Masseges.Shenaseh = "";
            Masseges.Types = "error";
            ListMasseges.Add(Masseges);

            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                Mahaleh = ListMasseges
            });

            JObject json5 = JObject.Parse(json);
            session.Close();
            return Ok(json5);
        }



        [HttpGet]
        [Route("api/G1/Gettest/{DateTahvil},{DateDaryaft}")]
        public IHttpActionResult Gettest(string DateTahvil, string DateDaryaft)
        {
            DateTime o = Convert.ToDateTime(DateTahvil);
            DateTime o2 = Convert.ToDateTime(DateDaryaft);
            return Ok();
        }

        public static int Price(int? _IsServiserId, long _PeykId, long IsNoeProduct_ID, int IsProduct_Tedad)
        {
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _QeimatVahed = Convert.ToInt32(session.QueryOver<TNoeProduct>().Where(p => p.IdNoeProduct == IsNoeProduct_ID && p.TServicer.IdServicer == _IsServiserId).Select(p => p.Price).SingleOrDefault<string>());
            int _All_pric = IsProduct_Tedad * _QeimatVahed;
            //int _Peyk_QeimatVahed = session.QueryOver<TSendPrice>().Where(p => p.TPeyk.PeykId == PeykId).Select(p => p.OjrateHarSefaresh).SingleOrDefault<int>();
            //int sum = _Nan_pric + _Peyk_QeimatVahed;
            int sum = _All_pric;

            return (Convert.ToInt32(sum));

        }

        [HttpGet]
        [Route("api/G1/Noe_Pay/{Unique_code},{Phone},{Unique_device_code},{Id},{TemporderId_},{noe_pay},{Shenaseh_Sefaresh}")]
        public IHttpActionResult Noe_Pay(string Unique_code, string Phone, string Unique_device_code, string Id, long TemporderId_, int noe_pay, long Shenaseh_Sefaresh)
        {

            if (Phone == "{Phone}" || Unique_device_code == "{Unique_device_code}" || Id == "Id" || noe_pay == 0 || noe_pay > 2 || noe_pay < 0 || Shenaseh_Sefaresh == 0)
            {
                return Ok("مقادیر خالی");
            }

            string IsPhoneRegistered = Phone;
            int IsPhoneRegistered_Count = IsPhoneRegistered.Length;
            if (IsPhoneRegistered_Count != 11)
            {
                return Ok("شماره تلفن اشتباه می باشد");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }

            string IsDevice_ID = Unique_device_code;
            long UserId = Convert.ToInt64(Id);
            string _Device_ID = Unique_device_code;
            long _UserId = 0;
            long _UserId2 = Convert.ToInt64(Id);
            ISession session = OpenNHibertnateSession.OpenSession();

            _UserId = session.QueryOver<TUsers>().Where(p => p.IdUsrer == UserId && p.Tell == IsPhoneRegistered && p.DeviceIdLogin == _Device_ID).Select(p => p.IdUsrer).SingleOrDefault<long>();
            long _TempId = 0;
            _TempId = session.QueryOver<TTemporder>().Where(p => p.TUsers.IdUsrer == UserId && p.IDTemporder == TemporderId_ && p.ShenaseSefaresh == Shenaseh_Sefaresh).Select(p => p.IDTemporder).SingleOrDefault<long>();
            if (_UserId != 0 && _TempId != 0)
            {

                //#region ارسال پیامک
                //string _Servis_Phon = "0";
                //_Servis_Phon = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == TemporderId_).Select(p => p.TServicer.Phone1).SingleOrDefault<string>();
                //var token2 = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                //var ultraFastSend2 = new UltraFastSend()
                //{
                //    Mobile = Convert.ToInt64("09190608912"),
                //    TemplateId = 44257,
                //    ParameterArray = new List<UltraFastParameters>()
                //           {
                //                    new UltraFastParameters()
                //                       {
                //                             Parameter = "Name" , ParameterValue = ""
                //                       }
                //              }.ToArray()

                //};
                //UltraFastSendRespone ultraFastSendRespone2 = new UltraFast().Send(token2, ultraFastSend2);

                //if (ultraFastSendRespone2.IsSuccessful)
                //{

                //}
                //#endregion
                int _TempOrder_Price = 0;
                _TempOrder_Price = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == TemporderId_).Select(p => p.TemporderPrice).SingleOrDefault<int>();
                #region ثبت پرداخت آنلاین
                if (noe_pay == 2)
                {
                    long _TemporderId = 0;
                    _TemporderId = TemporderId_;
                    //دریافت زمان سیستم
                    DateTime now = DateTime.Now;
                    DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                    string _Authority = "0";
                    string Authority = "0";
                    System.Net.ServicePointManager.Expect100Continue = false;
                    Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();

                    //int Status = zp.PaymentRequest("f3704c55-7b41-47c5-b2ae-da24b883eca0", Convert.ToInt32(_TempOrder_Price), "هلکو | خشگشویی آنلاین", "you@yoursite.com", Phone, "http://onlinurlspelatform.monasebsazan.ir/Verify/Index", out Authority);
                    int Status = zp.PaymentRequest("6576b501-0b36-430d-83af-9761ba88fb95", Convert.ToInt32(_TempOrder_Price), "خشکشویی آنلاین هلکو", "you@yoursite.com", "09190608912", "http://onlinurlspelatform.setapi.ir/Verify/Index", out Authority);

                    if (Status == 100)
                    {

                        _Authority = session.QueryOver<TTemporder>().Where(p => p.Authority == Authority).Select(p => p.Authority).SingleOrDefault<string>();
                        var emp1 = session.Get<TTemporder>(_TempId);
                        emp1.Authority = Authority;
                        emp1.TLNoePay = session.QueryOver<TLNoePay>().Where(n => n.IdNoePay == 2).SingleOrDefault();
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(emp1);
                            transaction.Commit();
                        }
                        session.Close();
                        #region ارسال پیامک
                        var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                        var ultraFastSend = new UltraFastSend()
                        {
                            Mobile = Convert.ToInt64("09190608912"),
                            TemplateId = 44257,
                            ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "Name" , ParameterValue = ""
                                       }
                              }.ToArray()

                        };
                        UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                        if (ultraFastSendRespone.IsSuccessful)
                        {

                        }
                        #endregion
                        return Ok("https://www.zarinpal.com/pg/StartPay/" + Authority);
                    }
                    else if (_Authority == "0")
                    {
                        return Ok("error_Zarinpal: " + Status);
                    }
                    else
                    {
                        return Ok("error_Zarinpal: " + Status);
                        //Response.Write("error: " + Status);
                    }
                }
                #endregion

                #region ثبت پرداخت نقدی
                if (noe_pay == 1)
                {

                    #region آپدیت در جدول Temp
                    var emp1 = session.Get<TTemporder>(_TempId);
                    emp1.TLNoePay = session.QueryOver<TLNoePay>().Where(n => n.IdNoePay == 1).SingleOrDefault();
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(emp1);
                        transaction.Commit();
                    }
                    #endregion


                    var ListTemporder = session.Query<TTemporder>().Where(p => p.IDTemporder == _TempId)
                        .Select(x => new
                        {
                            x.IDTemporder,
                            DateDaryaft = x.TemporderDate,
                            x.TemporderPrice,
                            x.TShift.IdShift,
                            x.DayId,
                            DateTahvil = x.Datetahvil,
                            IdShiftTahvil = x.TShift2.IdShift,
                            x.TLMahaleh.IdMahaleh,
                            x.TUsers.IdUsrer,
                            x.TAddresses.IdAddresses,
                            x.TPeyks.IdPeyks,
                            x.TServicer.IdServicer,
                            x.Authority,
                            x.TLNoePay.IdNoePay,
                            x.TakhfifPardakhti,
                            //x.TBazaryab.BazaryabId,
                            x.TemporderPriceBof,
                            x.ShenaseSefaresh,
                            x.PeykPrice,
                            _MizanTakhfif = x.MizanTakhfif
                            //TCodeTakhfifId = x.TCodeTakhfif.IdCodeTakhfif 

                        }).Take(1).ToList();
                    long _BazaryabId = 0;
                    //_BazaryabId = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == _TempId).Select(p => p.TBazaryab.BazaryabId).SingleOrDefault<long>();
                    int _TCodeTakhfifId = 0;
                    _TCodeTakhfifId = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == ListTemporder[0].IDTemporder).Select(p => p.TCodeTakhfif.IdCodeTakhfif).SingleOrDefault<int>();
                    long _OrderId = 0;
                    _OrderId = session.QueryOver<TOrder>().Where(p => p.TUsers.IdUsrer == UserId && p.ShenaseSefaresh == ListTemporder[0].ShenaseSefaresh).Select(p => p.IdOrder).SingleOrDefault<long>();
                    if (_OrderId == 0)/// اگر از قبل درج نشده باشد
                    {
                        #region ثبت در Order
                        DateTime now = DateTime.Now;
                        DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                        #region رند کردن قیمت به 100 تومان
                        int RoundPrice, remainder;
                        RoundPrice = Math.DivRem(Convert.ToInt32(ListTemporder[0].TemporderPrice), 100, out remainder);
                        RoundPrice = RoundPrice * 100;
                        #endregion

                        if (_BazaryabId != 0)
                        {

                            if (_TCodeTakhfifId!=0)
                            {
                                TOrder Order = new TOrder
                                {
                                    OrderDate = ListTemporder[0].DateDaryaft,
                                    OrderPrice = RoundPrice,
                                    TShift = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShift).SingleOrDefault(),
                                    //TDay = session.QueryOver<TDay>().Where(n => n.DayId == ListTemporder[0].DayId).SingleOrDefault(),
                                    TLMahaleh = session.QueryOver<TLMahaleh>().Where(n => n.IdMahaleh == ListTemporder[0].IdMahaleh).SingleOrDefault(),
                                    TUsers = session.QueryOver<TUsers>().Where(n => n.IdUsrer == _UserId).SingleOrDefault(),
                                    TAddresses = session.QueryOver<TAddresses>().Where(n => n.IdAddresses == ListTemporder[0].IdAddresses).SingleOrDefault(),
                                    TPeyks = session.QueryOver<TPeyks>().Where(n => n.IdPeyks == ListTemporder[0].IdPeyks).SingleOrDefault(),
                                    TServicer = session.QueryOver<TServicer>().Where(n => n.IdServicer == ListTemporder[0].IdServicer).SingleOrDefault(),
                                    TLNoePay = session.QueryOver<TLNoePay>().Where(n => n.IdNoePay == ListTemporder[0].IdNoePay).SingleOrDefault(),
                                    TakhfifPardakhti = ListTemporder[0].TakhfifPardakhti,
                                    //TBazaryab = session.QueryOver<TBazaryab>().Where(n => n.BazaryabId == _BazaryabId).SingleOrDefault(),
                                    OrderPriceBof = ListTemporder[0].TemporderPriceBof,
                                    TLVaziyatVarizi = session.QueryOver<TLVaziyatVarizi>().Where(n => n.IdVaziyatVarizi == 5).SingleOrDefault(),
                                    OrderNow = Time_Now,
                                    ShenaseSefaresh = ListTemporder[0].ShenaseSefaresh,
                                    TLVaziyatSabad = session.QueryOver<TLVaziyatSabad>().Where(n => n.IdVaziyatSabad == 1).SingleOrDefault(),
                                    PeykPrice = ListTemporder[0].PeykPrice,
                                    TLVaziyatTahvil = session.QueryOver<TLVaziyatTahvil>().Where(n => n.IdVaziyatTahvil == 6).SingleOrDefault(),
                                    TShift2 = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShiftTahvil).SingleOrDefault(),
                                    Datetahvil = ListTemporder[0].DateTahvil,
                                    MizanTakhfif = ListTemporder[0]._MizanTakhfif,
                                    TCodeTakhfif = session.QueryOver<TCodeTakhfif>().Where(n => n.IdCodeTakhfif == _TCodeTakhfifId).SingleOrDefault(),

                                };
                                try
                                {
                                    using (ITransaction transaction = session.BeginTransaction())
                                    {
                                        session.Save(Order);
                                        transaction.Commit();
                                    }
                                }
                                catch (Exception)
                                {

                                    return Ok("خطا مشکل در اطلاعات دریافتی");
                                }
                            }

                            else if (_TCodeTakhfifId == 0)
                            {
                                TOrder Order = new TOrder
                                {
                                    OrderDate = ListTemporder[0].DateDaryaft,
                                    OrderPrice = RoundPrice,
                                    TShift = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShift).SingleOrDefault(),
                                    //TDay = session.QueryOver<TDay>().Where(n => n.DayId == ListTemporder[0].DayId).SingleOrDefault(),
                                    TLMahaleh = session.QueryOver<TLMahaleh>().Where(n => n.IdMahaleh == ListTemporder[0].IdMahaleh).SingleOrDefault(),
                                    TUsers = session.QueryOver<TUsers>().Where(n => n.IdUsrer == _UserId).SingleOrDefault(),
                                    TAddresses = session.QueryOver<TAddresses>().Where(n => n.IdAddresses == ListTemporder[0].IdAddresses).SingleOrDefault(),
                                    TPeyks = session.QueryOver<TPeyks>().Where(n => n.IdPeyks == ListTemporder[0].IdPeyks).SingleOrDefault(),
                                    TServicer = session.QueryOver<TServicer>().Where(n => n.IdServicer == ListTemporder[0].IdServicer).SingleOrDefault(),
                                    TLNoePay = session.QueryOver<TLNoePay>().Where(n => n.IdNoePay == ListTemporder[0].IdNoePay).SingleOrDefault(),
                                    TakhfifPardakhti = ListTemporder[0].TakhfifPardakhti,
                                    //TBazaryab = session.QueryOver<TBazaryab>().Where(n => n.BazaryabId == _BazaryabId).SingleOrDefault(),
                                    OrderPriceBof = ListTemporder[0].TemporderPriceBof,
                                    TLVaziyatVarizi = session.QueryOver<TLVaziyatVarizi>().Where(n => n.IdVaziyatVarizi == 5).SingleOrDefault(),
                                    OrderNow = Time_Now,
                                    ShenaseSefaresh = ListTemporder[0].ShenaseSefaresh,
                                    TLVaziyatSabad = session.QueryOver<TLVaziyatSabad>().Where(n => n.IdVaziyatSabad == 1).SingleOrDefault(),
                                    PeykPrice = ListTemporder[0].PeykPrice,
                                    TLVaziyatTahvil = session.QueryOver<TLVaziyatTahvil>().Where(n => n.IdVaziyatTahvil == 6).SingleOrDefault(),
                                    TShift2 = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShiftTahvil).SingleOrDefault(),
                                    Datetahvil = ListTemporder[0].DateTahvil,
                                    MizanTakhfif = ListTemporder[0]._MizanTakhfif

                                };
                                try
                                {
                                    using (ITransaction transaction = session.BeginTransaction())
                                    {
                                        session.Save(Order);
                                        transaction.Commit();
                                    }
                                }
                                catch (Exception)
                                {

                                    return Ok("خطا مشکل در اطلاعات دریافتی");
                                }
                            }
                        }

                        else if (_BazaryabId == 0)
                        {
                            if (_TCodeTakhfifId!=0)
                            {
                                TOrder Order = new TOrder
                                {
                                    OrderDate = ListTemporder[0].DateDaryaft,
                                    OrderPrice = RoundPrice,
                                    TShift = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShift).SingleOrDefault(),
                                    //TDay = session.QueryOver<TDay>().Where(n => n.DayId == ListTemporder[0].DayId).SingleOrDefault(),
                                    TLMahaleh = session.QueryOver<TLMahaleh>().Where(n => n.IdMahaleh == ListTemporder[0].IdMahaleh).SingleOrDefault(),
                                    TUsers = session.QueryOver<TUsers>().Where(n => n.IdUsrer == _UserId).SingleOrDefault(),
                                    TAddresses = session.QueryOver<TAddresses>().Where(n => n.IdAddresses == ListTemporder[0].IdAddresses).SingleOrDefault(),
                                    TPeyks = session.QueryOver<TPeyks>().Where(n => n.IdPeyks == ListTemporder[0].IdPeyks).SingleOrDefault(),
                                    TServicer = session.QueryOver<TServicer>().Where(n => n.IdServicer == ListTemporder[0].IdServicer).SingleOrDefault(),
                                    TLNoePay = session.QueryOver<TLNoePay>().Where(n => n.IdNoePay == ListTemporder[0].IdNoePay).SingleOrDefault(),
                                    TakhfifPardakhti = ListTemporder[0].TakhfifPardakhti,
                                    //TBazaryab = session.QueryOver<TBazaryab>().Where(n => n.BazaryabId == _BazaryabId).SingleOrDefault(),
                                    OrderPriceBof = ListTemporder[0].TemporderPriceBof,
                                    TLVaziyatVarizi = session.QueryOver<TLVaziyatVarizi>().Where(n => n.IdVaziyatVarizi == 5).SingleOrDefault(),
                                    OrderNow = Time_Now,
                                    ShenaseSefaresh = ListTemporder[0].ShenaseSefaresh,
                                    TLVaziyatSabad = session.QueryOver<TLVaziyatSabad>().Where(n => n.IdVaziyatSabad == 1).SingleOrDefault(),
                                    PeykPrice = ListTemporder[0].PeykPrice,
                                    //TLVaziyatTahvil = session.QueryOver<TLVaziyatTahvil>().Where(n => n.IdVaziyatTahvil == 1008).SingleOrDefault(),
                                    TLVaziyatTahvil = session.QueryOver<TLVaziyatTahvil>().Where(n => n.IdVaziyatTahvil == 6).SingleOrDefault(),
                                    TShift2 = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShiftTahvil).SingleOrDefault(),
                                    Datetahvil = ListTemporder[0].DateTahvil,
                                    MizanTakhfif = ListTemporder[0]._MizanTakhfif,
                                    TCodeTakhfif = session.QueryOver<TCodeTakhfif>().Where(n => n.IdCodeTakhfif == _TCodeTakhfifId).SingleOrDefault(),
                                };
                                try
                                {
                                    using (ITransaction transaction = session.BeginTransaction())
                                    {
                                        session.Save(Order);
                                        transaction.Commit();
                                    }
                                }
                                catch (Exception)
                                {

                                    return Ok("خطا مشکل در اطلاعات دریافتی");
                                }
                            }

                            else if (_TCodeTakhfifId == 0)
                            {
                                TOrder Order = new TOrder
                                {
                                    OrderDate = ListTemporder[0].DateDaryaft,
                                    OrderPrice = RoundPrice,
                                    TShift = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShift).SingleOrDefault(),
                                    //TDay = session.QueryOver<TDay>().Where(n => n.DayId == ListTemporder[0].DayId).SingleOrDefault(),
                                    TLMahaleh = session.QueryOver<TLMahaleh>().Where(n => n.IdMahaleh == ListTemporder[0].IdMahaleh).SingleOrDefault(),
                                    TUsers = session.QueryOver<TUsers>().Where(n => n.IdUsrer == _UserId).SingleOrDefault(),
                                    TAddresses = session.QueryOver<TAddresses>().Where(n => n.IdAddresses == ListTemporder[0].IdAddresses).SingleOrDefault(),
                                    TPeyks = session.QueryOver<TPeyks>().Where(n => n.IdPeyks == ListTemporder[0].IdPeyks).SingleOrDefault(),
                                    TServicer = session.QueryOver<TServicer>().Where(n => n.IdServicer == ListTemporder[0].IdServicer).SingleOrDefault(),
                                    TLNoePay = session.QueryOver<TLNoePay>().Where(n => n.IdNoePay == ListTemporder[0].IdNoePay).SingleOrDefault(),
                                    TakhfifPardakhti = ListTemporder[0].TakhfifPardakhti,
                                    //TBazaryab = session.QueryOver<TBazaryab>().Where(n => n.BazaryabId == _BazaryabId).SingleOrDefault(),
                                    OrderPriceBof = ListTemporder[0].TemporderPriceBof,
                                    TLVaziyatVarizi = session.QueryOver<TLVaziyatVarizi>().Where(n => n.IdVaziyatVarizi == 5).SingleOrDefault(),
                                    OrderNow = Time_Now,
                                    ShenaseSefaresh = ListTemporder[0].ShenaseSefaresh,
                                    TLVaziyatSabad = session.QueryOver<TLVaziyatSabad>().Where(n => n.IdVaziyatSabad == 1).SingleOrDefault(),
                                    PeykPrice = ListTemporder[0].PeykPrice,
                                    //TLVaziyatTahvil = session.QueryOver<TLVaziyatTahvil>().Where(n => n.IdVaziyatTahvil == 1008).SingleOrDefault(),
                                    TLVaziyatTahvil = session.QueryOver<TLVaziyatTahvil>().Where(n => n.IdVaziyatTahvil == 6).SingleOrDefault(),
                                    TShift2 = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShiftTahvil).SingleOrDefault(),
                                    Datetahvil = ListTemporder[0].DateTahvil,
                                    MizanTakhfif = ListTemporder[0]._MizanTakhfif
                                   
                                };
                                try
                                {
                                    using (ITransaction transaction = session.BeginTransaction())
                                    {
                                        session.Save(Order);
                                        transaction.Commit();
                                    }
                                }
                                catch (Exception)
                                {

                                    return Ok("خطا مشکل در اطلاعات دریافتی");
                                }
                            }

                        }

                    }

                    #endregion

                    var LatOrderId = session.Query<TOrder>()
                           .Where(p => p.ShenaseSefaresh == ListTemporder[0].ShenaseSefaresh && p.TUsers.IdUsrer == ListTemporder[0].IdUsrer && p.OrderDate == ListTemporder[0].DateDaryaft)
                                              .Select(x => new
                                              {
                                                  x.IdOrder
                                                  //,
                                                  //x.TBazaryab.BazaryabId
                                              }).OrderByDescending(p => p.IdOrder).Take(1).ToList();
                    if (_BazaryabId != 0)
                    {
                        //#region ثبت اطلاعات در جدول درامدهای بازاریاب


                        //#region مبلغ سهم بازاریاب از این فروش
                        //long _BazaryabDaramadId = 0;
                        //_BazaryabDaramadId = session.QueryOver<TBazaryabDaramad>().Where(p => p.TOrder.OrderId == LatOrderId[0].OrderId).Select(s => s.IdBazaryabDaramad).SingleOrDefault<long>();

                        //if (_BazaryabDaramadId == 0) ///اگر از قبل درج نشده باشد
                        //{
                        //    decimal _Darsad_Bazaryab = 0;
                        //    _Darsad_Bazaryab = session.QueryOver<TBazaryab>().Where(p => p.BazaryabId == _BazaryabId).Select(p => p.DarsadAzForosh).SingleOrDefault<decimal>();
                        //    int Mablag_Bazaryab = 0;
                        //    Mablag_Bazaryab = Convert.ToInt32(((ListTemporder[0].TemporderPriceBof) / 100) * _Darsad_Bazaryab);
                        //    #endregion

                        //    string format = "dd/MM/yyyy HH:mm:ss.ff";
                        //    string str2 = DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
                        //    DateTime date2 = DateTime.ParseExact(str2, format, CultureInfo.InvariantCulture);

                        //    TBazaryabDaramad BazaryabDaramad = new TBazaryabDaramad
                        //    {
                        //        TBazaryab = session.QueryOver<TBazaryab>().Where(n => n.BazaryabId == _BazaryabId).SingleOrDefault(),
                        //        MablaghPey = ListTemporder[0].TemporderPriceBof,
                        //        Date = date2,
                        //        TOrder = session.QueryOver<TOrder>().Where(n => n.OrderId == LatOrderId[0].OrderId).SingleOrDefault(),
                        //        TLBazaryabVtasviyeh = session.QueryOver<TLBazaryabVtasviyeh>().Where(n => n.IdVaziyatTasviyeh == 3).SingleOrDefault(),
                        //        MablaghSahmBazarab = Mablag_Bazaryab
                        //    };
                        //    try
                        //    {
                        //        using (ITransaction transaction = session.BeginTransaction())
                        //        {
                        //            session.Save(BazaryabDaramad);
                        //            transaction.Commit();
                        //        }
                        //    }
                        //    catch (Exception)
                        //    {
                        //        return Ok("error_Naghdi");
                        //    }
                        //}

                        //#endregion
                    }



                    #region آپدیت orderId در سبد خرید

                    var Sabad = from p in session.Query<TSabad>()
                                where (p.TTemporder.IDTemporder == ListTemporder[0].IDTemporder)
                                select new
                                {
                                    p.IdSabad,
                                    p.TNoeProduct.IdNoeProduct,
                                    p.TNoeKhedmat.IdNoeKhedmat,
                                    p.Tedad,
                                    p.TTemporder.IDTemporder,
                                    p.ShenasehSefaresh,
                                    //p.TBazaryab.BazaryabId,
                                    p.PriceAll,
                                    //p.TakhfifBazaryab,
                                    //p.PriceAll,
                                    p.PriceBof,
                                    p.DateCreate,
                                    p.TLVaziyatSabad.IdVaziyatSabad,
                                    p.TozihatMoshtari,
                                    p.TozihatSevviser

                                };
                    var Sabad_mojud = Sabad.ToList();

                    //Sabad_mojud
                    //long _OrderId = session.QueryOver<TOrder>().Where(p => p.TUser.UserId == LatOrderId[0].UserId && p.Authority == _Authority0 && p.RahgiriCode == RefID2).Select(p => p.OrderId).SingleOrDefault<long>();
                    int c_sabad = 0;
                    c_sabad = Sabad_mojud.Count();
                    for (int i = 0; i < c_sabad; i++)
                    {
                        var emp22 = session.Get<TSabad>(Sabad_mojud[i].IdSabad);
                        emp22.TOrder = session.QueryOver<TOrder>().Where(n => n.IdOrder == LatOrderId[0].IdOrder).SingleOrDefault();
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(emp22);
                            transaction.Commit();
                        }
                    }

                    #endregion

                    #region    آپدیت اطلاعات جدول تجمعی شیفتی پیک

                    //#region افزودن ظرفیت دریافت
                    //Int64 _TZarfiyatShiftPeyksid = 0;
                    //_TZarfiyatShiftPeyksid = session.QueryOver<TZarfiyatShiftPeyks>().Where(p => p.TPeyks.IdPeyks == (ListTemporder[0].IdPeyks) && p.TShift.IdShift == ListTemporder[0].IdShift && p.TServicer.IdServicer == ListTemporder[0].IdServicer && p.Datenowgiv == ListTemporder[0].DateDaryaft).Select(p => p.IdZarfiyatShiftPeyks).SingleOrDefault<Int64>();

                    //if (_TZarfiyatShiftPeyksid != 0)
                    //{
                    //    var emp3 = session.Get<TZarfiyatShiftPeyks>(_TZarfiyatShiftPeyksid);
                    //    emp3.CountService++;
                    //    using (ITransaction transaction = session.BeginTransaction())
                    //    {
                    //        session.Save(emp3);
                    //        transaction.Commit();
                    //    }
                    //}
                    //else//درج اطلاعات جدید تو جدول shs
                    //{
                    //    //TNan TNanId = new TNan
                    //    //{
                    //    //    NanId = Sabad_mojud[0].NanId
                    //    //};
                    //    TZarfiyatShiftPeyks TSh = new TZarfiyatShiftPeyks
                    //    {
                    //        TServicer = session.QueryOver<TServicer>().Where(n => n.IdServicer == ListTemporder[0].IdServicer).SingleOrDefault(),
                    //        TPeyks = session.QueryOver<TPeyks>().Where(n => n.IdPeyks == ListTemporder[0].IdPeyks).SingleOrDefault(),
                    //        TShift = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShift).SingleOrDefault(),
                    //        //TMahale = session.QueryOver<TMahale>().Where(n => n.MahaleId == ListTemporder[0].MahaleId).SingleOrDefault(),

                    //        CountService = 1,
                    //        Datenowgiv = ListTemporder[0].DateDaryaft
                    //    };
                    //    using (ITransaction transaction = session.BeginTransaction())
                    //    {
                    //        session.Save(TSh);
                    //        transaction.Commit();
                    //    }
                    //}
                    //#endregion

                    #region افزودن ظرفیت ارسال
                    Int64 _TCGoZarfiyatShPeyksid = 0;
                    _TCGoZarfiyatShPeyksid = session.QueryOver<TCGoZarfiyatShPeyks>().Where(p => p.TPeyks.IdPeyks == (ListTemporder[0].IdPeyks) && p.TShift.IdShift == ListTemporder[0].IdShift && p.Datenowgo == ListTemporder[0].DateTahvil).Select(p => p.IdCGoZarfiyatShPeyk).SingleOrDefault<Int64>();

                    if (_TCGoZarfiyatShPeyksid != 0)
                    {
                        var emp3 = session.Get<TCGoZarfiyatShPeyks>(_TCGoZarfiyatShPeyksid);
                        emp3.CountGoPeyks++;
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(emp3);
                            transaction.Commit();
                        }
                    }
                    else//درج اطلاعات جدید تو جدول shs
                    {
                        //TNan TNanId = new TNan
                        //{
                        //    NanId = Sabad_mojud[0].NanId
                        //};
                        TCGoZarfiyatShPeyks TSh = new TCGoZarfiyatShPeyks
                        {
                            //TServicer = session.QueryOver<TServicer>().Where(n => n.IdServicer == ListTemporder[0].IdServicer).SingleOrDefault(),
                            TPeyks = session.QueryOver<TPeyks>().Where(n => n.IdPeyks == ListTemporder[0].IdPeyks).SingleOrDefault(),
                            TShift = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShift).SingleOrDefault(),
                            //TMahale = session.QueryOver<TMahale>().Where(n => n.MahaleId == ListTemporder[0].MahaleId).SingleOrDefault(),

                            CountGoPeyks = 1,
                            Datenowgo = ListTemporder[0].DateTahvil
                        };
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(TSh);
                            transaction.Commit();
                        }
                    }
                    #endregion

                    #endregion

                    #region    آپدیت اطلاعات جدول تجمعی شیفتی سرویس دهنده

                    //#region افزودن ظرفیت دریافت
                    //Int64 _TZarfiyatShiftServicerid = 0;
                    //_TZarfiyatShiftServicerid = session.QueryOver<TZarfiyatShiftServicer>().Where(p =>  p.TShift.IdShift == ListTemporder[0].IdShift && p.TServicer.IdServicer == ListTemporder[0].IdServicer && p.Datenowgiv == ListTemporder[0].DateDaryaft).Select(p => p.IdZarfiyatShiftServicer).SingleOrDefault<Int64>();

                    //if (_TZarfiyatShiftServicerid != 0)
                    //{
                    //    var emp3 = session.Get<TZarfiyatShiftPeyks>(_TZarfiyatShiftServicerid);
                    //    emp3.CountService++;
                    //    using (ITransaction transaction = session.BeginTransaction())
                    //    {
                    //        session.Save(emp3);
                    //        transaction.Commit();
                    //    }
                    //}
                    //else//درج اطلاعات جدید تو جدول shs
                    //{
                    //    //TNan TNanId = new TNan
                    //    //{
                    //    //    NanId = Sabad_mojud[0].NanId
                    //    //};
                    //    TZarfiyatShiftServicer TSh = new TZarfiyatShiftServicer
                    //    {
                    //        TServicer = session.QueryOver<TServicer>().Where(n => n.IdServicer == ListTemporder[0].IdServicer).SingleOrDefault(),
                    //        TShift = session.QueryOver<TShift>().Where(n => n.IdShift == ListTemporder[0].IdShift).SingleOrDefault(),
                    //        //TMahale = session.QueryOver<TMahale>().Where(n => n.MahaleId == ListTemporder[0].MahaleId).SingleOrDefault(),

                    //        CountService = 1,
                    //        Datenowgiv = ListTemporder[0].DateDaryaft
                    //    };
                    //    using (ITransaction transaction = session.BeginTransaction())
                    //    {
                    //        session.Save(TSh);
                    //        transaction.Commit();
                    //    }
                    //}
                    //#endregion

                    #region افزودن ظرفیت ارسال
                    Int64 _TCGoZarfiyatShServiserid = 0;
                    _TCGoZarfiyatShServiserid = session.QueryOver<TCGoZarfiyatShServiser>().Where(p => (p.TServicer.IdServicer == (ListTemporder[0].IdServicer)) && p.ShiftId == ListTemporder[0].IdShift && p.Datenowgo == ListTemporder[0].DateTahvil).Select(p => p.IdCGoZarfiyatShServiser).SingleOrDefault<Int64>();

                    if (_TCGoZarfiyatShServiserid != 0)
                    {
                        var emp3 = session.Get<TCGoZarfiyatShServiser>(_TCGoZarfiyatShServiserid);
                        emp3.CountGoService++;
                        emp3.CountDeliveryService++;
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(emp3);
                            transaction.Commit();
                        }
                    }
                    else//درج اطلاعات جدید تو جدول shs
                    {
                        //TNan TNanId = new TNan
                        //{
                        //    NanId = Sabad_mojud[0].NanId
                        //};
                        TCGoZarfiyatShServiser TSh = new TCGoZarfiyatShServiser
                        {
                            TServicer = session.QueryOver<TServicer>().Where(n => n.IdServicer == ListTemporder[0].IdServicer).SingleOrDefault(),
                            //TPeyks = session.QueryOver<TPeyks>().Where(n => n.IdPeyks == ListTemporder[0].IdPeyks).SingleOrDefault(),
                            ShiftId = ListTemporder[0].IdShift,
                            //TMahale = session.QueryOver<TMahale>().Where(n => n.MahaleId == ListTemporder[0].MahaleId).SingleOrDefault(),

                            CountDeliveryService = 1,
                            CountGoService = 1,
                            Datenowgo = ListTemporder[0].DateTahvil
                        };
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(TSh);
                            transaction.Commit();
                        }
                    }
                    #endregion

                    #endregion

                    session.Close();
                    #region ارسال پیامک
                    var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                    var ultraFastSend = new UltraFastSend()
                    {
                        Mobile = Convert.ToInt64("09190608912"),
                        TemplateId = 44257,
                        ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "Name" , ParameterValue = ""
                                       }
                              }.ToArray()

                    };
                    UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                    if (ultraFastSendRespone.IsSuccessful)
                    {

                    }
                    #endregion
                    return Ok("پرداخت نقدی ثبت شد");
                }
                #endregion
            }
            else
            {
                return Ok("اطلاعات دریافتی اشتباه می باشد");
            }
            return Ok("error");
        }
        //[HttpGet]
        //[Route("api/G1/NaGHditest/")]
        //public IHttpActionResult NaGHditest()
        //{
        //    string IsPhoneRegistered = "0913";
        //    DateTime TempOrder_Date = DateTime.Now;
        //    uint TempOrder_Price = 1000;
        //    int tNan_ID = 1;
        //    int tNan_Tedad = 1;
        //    int tDay_ID = 1;
        //    int tShift_ID = 1;
        //    int tMahale_ID = 1;
        //    int tUser_ID = 1;
        //    int tAddress_ID = 2;
        //    int tPeyk_ID = 1;
        //    int tNanvaei_ID = 2;
        //    int tVaziat_Pardakht_ID = 0;

        //    int BazaryabId_ = 356864;
        //    int OfForUser = 0;
        //    int ShenashSabd_ = 0;
        //    #region محاسبه درصد تخفیف  بازاریاب از مبلغ نهایی برای مشتری
        //    decimal _DarsadTakhfifBazaryab = session.QueryOver<TBazaryab>().Where(p => p.BazaryabId == BazaryabId_).Select(p => p.DarsadTakhfifBazaryab).SingleOrDefault<decimal>();
        //    OfForUser = Convert.ToInt32((TempOrder_Price / 100) * _DarsadTakhfifBazaryab);

        //    #endregion

        //    var _Naghdipay_Pey = Naghdipay_Pey_Temp(IsPhoneRegistered, TempOrder_Date, TempOrder_Price, tNan_ID, tNan_Tedad, tDay_ID,
        //                                tShift_ID, tMahale_ID, tUser_ID, tAddress_ID, tPeyk_ID, tNanvaei_ID, tVaziat_Pardakht_ID, BazaryabId_, OfForUser, ShenashSabd_);
        //    if (_Naghdipay_Pey.Contains("error") || _Naghdipay_Pey.Contains("error_Naghdi") || _Naghdipay_Pey.Contains("error_Nagdi_insert"))
        //    {
        //        return Ok(_Naghdipay_Pey);
        //    }
        //    else
        //    {
        //        return Ok(_Naghdipay_Pey);
        //    }

        //}
        //public static string Naghdipay_Pey_Temp(string Phone, DateTime TempOrder_Date, uint TempOrder_Price, Int32 tNan_ID, int tNan_Tedad, int tDay_ID, int tShift_ID, int tMahale_ID, long tUser_ID, long tAddress_ID, int tPeyk_ID, int tNanvaei_ID, int tVaziat_Pardakht_ID, int BazaryabId_, int OfForUser, long ShenashSabd_)
        //{
        //    long ShenashSabd = 0;
        //    if (ShenashSabd_ == 0)
        //    {
        //        #region ایجاد شناسه سبد خرید
        //        string format = "dd/MM/yyyy HH:mm:ss.ff";
        //        string str = DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
        //        DateTime date = DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
        //        //int msec = date.Millisecond;
        //        string dd1 = Convert.ToString(date.Year) + Convert.ToString(date.Month) + Convert.ToString(date.Day);
        //        string dd2 = Convert.ToString(date.Hour) + Convert.ToString(date.Minute) + Convert.ToString(date.Second) + Convert.ToString(date.Millisecond);

        //        int Lat = dd1.Length;
        //        int caseSwitch = 9 - Lat;

        //        switch (caseSwitch)
        //        {
        //            case 0:
        //                break;
        //            case 1:
        //                dd1 = "1" + dd1;
        //                break;
        //            case 2:
        //                dd1 = "11" + dd1;
        //                break;
        //            case 3:
        //                dd1 = "111" + dd1;
        //                break;
        //            case 4:
        //                dd1 = "1111" + dd1;
        //                break;
        //            case 5:
        //                dd1 = "11111" + dd1;
        //                break;
        //            case 6:
        //                dd1 = "111111" + dd1;
        //                break;
        //            default:
        //                break;
        //        }

        //        int Lat2 = dd2.Length;
        //        int caseSwitch2 = 9 - Lat2;

        //        switch (caseSwitch2)
        //        {
        //            case 0:
        //                break;
        //            case 1:
        //                dd2 = "1" + dd2;
        //                break;
        //            case 2:
        //                dd2 = "11" + dd2;
        //                break;
        //            case 3:
        //                dd2 = "111" + dd2;
        //                break;
        //            case 4:
        //                dd2 = "1111" + dd2;
        //                break;
        //            case 5:
        //                dd2 = "11111" + dd2;
        //                break;
        //            case 6:
        //                dd2 = "111111" + dd2;
        //                break;
        //            default:
        //                break;
        //        }
        //        ShenashSabd = Convert.ToInt64(dd1) + Convert.ToInt64(dd2);
        //        #endregion
        //    }

        //    int TempOrder_PriceAOF_ = (Convert.ToInt32(TempOrder_Price)) - OfForUser;
        //    //دریافت زمان سیستم
        //    DateTime now = DateTime.Now;
        //    DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        //    ISession session = OpenNHibertnateSession.OpenSession();
        //    //try
        //    //{

        //    ///ذخیر موقت اطلاعات
        //    TNan TNanId = new TNan
        //    {
        //        NanId = tNan_ID
        //    };
        //    TDay TDayId = new TDay
        //    {
        //        DayId = tDay_ID
        //    };
        //    TShift TShiftId = new TShift
        //    {
        //        ShiftId = tShift_ID
        //    };
        //    TMahale TMahaleId = new TMahale
        //    {
        //        MahaleId = tMahale_ID
        //    };
        //    TUser TUserId = new TUser
        //    {
        //        UserId = tUser_ID
        //    };
        //    TAddress TAddressId = new TAddress
        //    {
        //        AddressId = tAddress_ID
        //    };
        //    TPeyk TPeykId = new TPeyk
        //    {
        //        PeykId = tPeyk_ID
        //    };
        //    TNanvaei TNanvaeiId = new TNanvaei
        //    {
        //        NanvaeiId = tNanvaei_ID
        //    };

        //    TLNoePay TLNoePayId = new TLNoePay
        //    {
        //        IdNoePay = 1
        //    };

        //    TLVaziatPardakht TLVaziatPardakhtId = new TLVaziatPardakht
        //    {
        //        IdVaziatPardakht = tVaziat_Pardakht_ID
        //    };
        //    TBazaryab TBazaryabId = new TBazaryab
        //    {
        //        BazaryabId = BazaryabId_
        //    };

        //    TTemporder Temporder = new TTemporder
        //    {
        //        TemporderDate = TempOrder_Date,
        //        TemporderPrice = Convert.ToInt32(TempOrder_Price),
        //        TNan = TNanId,
        //        NanTedad = tNan_Tedad,
        //        TDay = TDayId,
        //        TShift = TShiftId,
        //        TMahale = TMahaleId,
        //        TUser = TUserId,
        //        TAddress = TAddressId,
        //        TPeyk = TPeykId,
        //        TNanvaei = TNanvaeiId,
        //        TLVaziatPardakht = TLVaziatPardakhtId,
        //        TemporderNow = Time_Now,
        //        TLNoePay = TLNoePayId,
        //        TBazaryab = TBazaryabId,
        //        TakhfifPardakhti = OfForUser,
        //        TemporderPriceAof = TempOrder_PriceAOF_,
        //        ShenaseSefaresh = ShenashSabd

        //    };

        //    try
        //    {
        //        using (ITransaction transaction = session.BeginTransaction())
        //        {

        //            session.Save(Temporder);
        //            transaction.Commit();
        //        }
        //        session.Close();
        //        if (ShenashSabd_ == 0)
        //        {
        //            return ("ثبت موقت سفارش" + "ShenaseSefaresh:" + Convert.ToString(ShenashSabd));
        //        }
        //        else
        //        {
        //            return ("ثبت موقت سفارش");
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        return ("error_Nagdi_insert");
        //    }


        //    return ("ثبت موقت سفارش");


        //}

        //public static string Naghdipay_Pey_Order(string Phone, DateTime TempOrder_Date, uint TempOrder_Price, Int32 tNan_ID, int tNan_Tedad, int tDay_ID, int tShift_ID, int tMahale_ID, long tUser_ID, long tAddress_ID, int tPeyk_ID, int tNanvaei_ID, int tVaziat_Pardakht_ID, int BazaryabId_, int OfForUser, long ShenashSabd_)
        //{

        //    int TempOrder_PriceAOF_ = (Convert.ToInt32(TempOrder_Price)) - OfForUser;
        //    //دریافت زمان سیستم
        //    DateTime now = DateTime.Now;
        //    DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        //    ISession session = OpenNHibertnateSession.OpenSession();
        //    //try
        //    //{

        //    ///ذخیر موقت اطلاعات
        //    TNan TNanId = new TNan
        //    {
        //        NanId = tNan_ID
        //    };
        //    TDay TDayId = new TDay
        //    {
        //        DayId = tDay_ID
        //    };
        //    TShift TShiftId = new TShift
        //    {
        //        ShiftId = tShift_ID
        //    };
        //    TMahale TMahaleId = new TMahale
        //    {
        //        MahaleId = tMahale_ID
        //    };
        //    TUser TUserId = new TUser
        //    {
        //        UserId = tUser_ID
        //    };
        //    TAddress TAddressId = new TAddress
        //    {
        //        AddressId = tAddress_ID
        //    };
        //    TPeyk TPeykId = new TPeyk
        //    {
        //        PeykId = tPeyk_ID
        //    };
        //    TNanvaei TNanvaeiId = new TNanvaei
        //    {
        //        NanvaeiId = tNanvaei_ID
        //    };

        //    TLNoePay TLNoePayId = new TLNoePay
        //    {
        //        IdNoePay = 1
        //    };

        //    TLVaziatPardakht TLVaziatPardakhtId = new TLVaziatPardakht
        //    {
        //        IdVaziatPardakht = tVaziat_Pardakht_ID
        //    };
        //    TBazaryab TBazaryabId = new TBazaryab
        //    {
        //        BazaryabId = BazaryabId_
        //    };


        //    TOrder order = new TOrder
        //    {
        //        OrderDate = TempOrder_Date,
        //        OrderPrice = Convert.ToInt32(TempOrder_Price),
        //        TNan = TNanId,
        //        NanTedad = tNan_Tedad,
        //        TDay = TDayId,
        //        TShift = TShiftId,
        //        TMahale = TMahaleId,
        //        TUser = TUserId,
        //        TAddress = TAddressId,
        //        TPeyk = TPeykId,
        //        TNanvaei = TNanvaeiId,
        //        TLVaziatPardakht = TLVaziatPardakhtId,
        //        OrderNow = Time_Now,
        //        TLNoePay = TLNoePayId,
        //        TBazaryab = TBazaryabId,
        //        TakhfifPardakhti = OfForUser,
        //        OrderPriceAof = TempOrder_PriceAOF_,
        //        ShenaseSefaresh = ShenashSabd_

        //    };

        //    try
        //    {
        //        using (ITransaction transaction = session.BeginTransaction())
        //        {

        //            session.Save(order);
        //            transaction.Commit();
        //        }
        //        //session.Close();
        //        //return ("ثبت موقت سفارش");
        //    }
        //    catch (Exception)
        //    {
        //        return ("error_Nagdi_insert");
        //    }


        //    //int LatOrderId = session.QueryOver<TOrder>().Where(p => p.TBazaryab.BazaryabId== BazaryabId_ && p.TUser.UserId == tUser_ID).Select(p => p.Latitude).SingleOrDefault<decimal>();
        //    #region ثبت اطلاعات در جدول درامدهای بازاریاب
        //    var LatOrderId = session.Query<TOrder>()
        //        .Where(p => p.TBazaryab.BazaryabId == BazaryabId_ && p.TUser.UserId == tUser_ID)
        //                           .Select(x => new
        //                           {
        //                               x.OrderId,
        //                               x.TBazaryab.BazaryabId
        //                           }).OrderByDescending(p => p.OrderId).Take(1).ToList();


        //    //DateTime date2 = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
        //    string format = "dd/MM/yyyy HH:mm:ss.ff";
        //    string str2 = DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
        //    DateTime date2 = DateTime.ParseExact(str2, format, CultureInfo.InvariantCulture);

        //    TBazaryab TBazaryabId2 = new TBazaryab
        //    {
        //        BazaryabId = LatOrderId[0].BazaryabId
        //    };

        //    TOrder TOrderId2 = new TOrder
        //    {
        //        OrderId = LatOrderId[0].OrderId
        //    };
        //    TLBazaryabVtasviyeh TLBazaryabVtasviyehId2 = new TLBazaryabVtasviyeh
        //    {
        //        IdVaziyatTasviyeh = 3
        //    };
        //    TBazaryabDaramad BazaryabDaramad = new TBazaryabDaramad
        //    {
        //        TBazaryab = TBazaryabId2,
        //        MablaghPey = TempOrder_PriceAOF_,
        //        Date = date2,
        //        TOrder = TOrderId2,
        //        TLBazaryabVtasviyeh = TLBazaryabVtasviyehId2
        //    };
        //    try
        //    {
        //        using (ITransaction transaction = session.BeginTransaction())
        //        {
        //            session.Save(BazaryabDaramad);
        //            transaction.Commit();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return ("error_Naghdi");
        //    }

        //    #endregion
        //    return ("ثبت موقت سفارش");


        //}



        [HttpGet]
        [Route("api/G1/GetSendMassageShift1/{Unique_code}")]
        public IHttpActionResult GetSendMassageShift1(string Unique_code)
        {
            if (Unique_code != "PlatformV19932569412541254!")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();




            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var ListApplicationVersion = session.Query<TOrder>()
                                         .Where(p => ((p.OrderDate.Date.Year == Time_Now.Year && p.OrderDate.Date.Month == Time_Now.Month && p.OrderDate.Date.Day == Time_Now.Day && (p.TShift.IdShift == 1)) || (p.Datetahvil.Date.Year == Time_Now.Year && p.Datetahvil.Date.Month == Time_Now.Month && p.Datetahvil.Date.Day == Time_Now.Day && (p.TShift2.IdShift == 1))))
                                         .Select(x =>
           new {

               //LawMoshtari = new
               //{
               Phon = x.TServicer.Phone1
               //}
           });

            var ListApplicationVersion2 = ListApplicationVersion.ToList();
            int C = ListApplicationVersion2.Count();
            for (int i = 0; i < C; i++)
            {
                #region ارسال پیامک
                var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64("09190608912"),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                               {
                                        new UltraFastParameters()
                                           {
                                                 Parameter = "Name" , ParameterValue = ""
                                           }
                                  }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                if (ultraFastSendRespone.IsSuccessful)
                {

                }
                #endregion
                #region ارسال پیامک
                var token2 = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend2 = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64(ListApplicationVersion2[i].Phon),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "Name" , ParameterValue = ""
                                       }
                              }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone2 = new UltraFast().Send(token2, ultraFastSend2);


                #endregion
            }
            var json = JsonConvert.SerializeObject(new
            {
                MyLaw = ListApplicationVersion2
            });



            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetSendMassageShift2/{Unique_code}")]
        public IHttpActionResult GetSendMassageShift2(string Unique_code)
        {
            if (Unique_code != "PlatformV19932569412541254!")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();




            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var ListApplicationVersion = session.Query<TOrder>()
                                         .Where(p => ((p.OrderDate.Date.Year == Time_Now.Year && p.OrderDate.Date.Month == Time_Now.Month && p.OrderDate.Date.Day == Time_Now.Day && (p.TShift.IdShift == 2)) || (p.Datetahvil.Date.Year == Time_Now.Year && p.Datetahvil.Date.Month == Time_Now.Month && p.Datetahvil.Date.Day == Time_Now.Day && (p.TShift2.IdShift == 2))))
                                         .Select(x =>
           new {

               //LawMoshtari = new
               //{
               Phon = x.TServicer.Phone1
               //}
           });

            var ListApplicationVersion2 = ListApplicationVersion.ToList();
            int C = ListApplicationVersion2.Count();
            for (int i = 0; i < C; i++)
            {
                #region ارسال پیامک
                var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64("09190608912"),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                               {
                                        new UltraFastParameters()
                                           {
                                                 Parameter = "Name" , ParameterValue = ""
                                           }
                                  }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                if (ultraFastSendRespone.IsSuccessful)
                {

                }
                #endregion
                #region ارسال پیامک
                var token2 = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend2 = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64(ListApplicationVersion2[i].Phon),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "Name" , ParameterValue = ""
                                       }
                              }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone2 = new UltraFast().Send(token2, ultraFastSend2);


                #endregion
            }
            var json = JsonConvert.SerializeObject(new
            {
                MyLaw = ListApplicationVersion2
            });



            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetSendMassageShift3/{Unique_code}")]
        public IHttpActionResult GetSendMassageShift3(string Unique_code)
        {
            if (Unique_code != "PlatformV19932569412541254!")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();




            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var ListApplicationVersion = session.Query<TOrder>()
                                         .Where(p => ((p.OrderDate.Date.Year == Time_Now.Year && p.OrderDate.Date.Month == Time_Now.Month && p.OrderDate.Date.Day == Time_Now.Day && (p.TShift.IdShift == 3)) || (p.Datetahvil.Date.Year == Time_Now.Year && p.Datetahvil.Date.Month == Time_Now.Month && p.Datetahvil.Date.Day == Time_Now.Day && (p.TShift2.IdShift == 3))))
                                         .Select(x =>
           new {

               //LawMoshtari = new
               //{
               Phon = x.TServicer.Phone1
               //}
           });

            var ListApplicationVersion2 = ListApplicationVersion.ToList();
            int C = ListApplicationVersion2.Count();
            for (int i = 0; i < C; i++)
            {
                #region ارسال پیامک
                var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64("09190608912"),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                               {
                                        new UltraFastParameters()
                                           {
                                                 Parameter = "Name" , ParameterValue = ""
                                           }
                                  }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                if (ultraFastSendRespone.IsSuccessful)
                {

                }
                #endregion
                #region ارسال پیامک
                var token2 = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend2 = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64(ListApplicationVersion2[i].Phon),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "Name" , ParameterValue = ""
                                       }
                              }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone2 = new UltraFast().Send(token2, ultraFastSend2);


                #endregion
            }
            var json = JsonConvert.SerializeObject(new
            {
                MyLaw = ListApplicationVersion2
            });



            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetSendMassageShift4/{Unique_code}")]
        public IHttpActionResult GetSendMassageShift4(string Unique_code)
        {
            if (Unique_code != "PlatformV19932569412541254!")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();




            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var ListApplicationVersion = session.Query<TOrder>()
                                         .Where(p => ((p.OrderDate.Date.Year == Time_Now.Year && p.OrderDate.Date.Month == Time_Now.Month && p.OrderDate.Date.Day == Time_Now.Day && (p.TShift.IdShift == 4)) || (p.Datetahvil.Date.Year == Time_Now.Year && p.Datetahvil.Date.Month == Time_Now.Month && p.Datetahvil.Date.Day == Time_Now.Day && (p.TShift2.IdShift == 4))))
                                         .Select(x =>
           new {

               //LawMoshtari = new
               //{
               Phon = x.TServicer.Phone1
               //}
           });

            var ListApplicationVersion2 = ListApplicationVersion.ToList();
            int C = ListApplicationVersion2.Count();
            for (int i = 0; i < C; i++)
            {
                #region ارسال پیامک
                var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64("09190608912"),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                               {
                                        new UltraFastParameters()
                                           {
                                                 Parameter = "Name" , ParameterValue = ""
                                           }
                                  }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                if (ultraFastSendRespone.IsSuccessful)
                {

                }
                #endregion
                #region ارسال پیامک
                var token2 = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend2 = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64(ListApplicationVersion2[i].Phon),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "Name" , ParameterValue = ""
                                       }
                              }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone2 = new UltraFast().Send(token2, ultraFastSend2);


                #endregion
            }
            var json = JsonConvert.SerializeObject(new
            {
                MyLaw = ListApplicationVersion2
            });



            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetSendMassageShift5/{Unique_code}")]
        public IHttpActionResult GetSendMassageShift5(string Unique_code)
        {
            if (Unique_code != "PlatformV19932569412541254!")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();




            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var ListApplicationVersion = session.Query<TOrder>()
                                         .Where(p => ((p.OrderDate.Date.Year == Time_Now.Year && p.OrderDate.Date.Month == Time_Now.Month && p.OrderDate.Date.Day == Time_Now.Day && (p.TShift.IdShift == 5)) || (p.Datetahvil.Date.Year == Time_Now.Year && p.Datetahvil.Date.Month == Time_Now.Month && p.Datetahvil.Date.Day == Time_Now.Day && (p.TShift2.IdShift == 5))))
                                         .Select(x =>
           new {

               //LawMoshtari = new
               //{
               Phon = x.TServicer.Phone1
               //}
           });

            var ListApplicationVersion2 = ListApplicationVersion.ToList();
            int C = ListApplicationVersion2.Count();
            for (int i = 0; i < C; i++)
            {
                #region ارسال پیامک
                var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64("09190608912"),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                               {
                                        new UltraFastParameters()
                                           {
                                                 Parameter = "Name" , ParameterValue = ""
                                           }
                                  }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                if (ultraFastSendRespone.IsSuccessful)
                {

                }
                #endregion
                #region ارسال پیامک
                var token2 = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                var ultraFastSend2 = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64(ListApplicationVersion2[i].Phon),
                    TemplateId = 44257,
                    ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "Name" , ParameterValue = ""
                                       }
                              }.ToArray()

                };
                UltraFastSendRespone ultraFastSendRespone2 = new UltraFast().Send(token2, ultraFastSend2);


                #endregion
            }
            var json = JsonConvert.SerializeObject(new
            {
                MyLaw = ListApplicationVersion2
            });



            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetCodeTakhfif/{Unique_code},{User_ID},{IsPhoneRegistered},{CodeTakhfif}")]
        public IHttpActionResult GetCodeTakhfif(string Unique_code, string User_ID, string IsPhoneRegistered, string CodeTakhfif)
        {
            var List_Orders = new List<ListTakhfif>();
            if (Unique_code != "PlatformV199325694")
            {
                var List_Orders0 = new ListTakhfif();
                List_Orders0.IdCodeTakhfif = 0;
                List_Orders0.DarsadTakhfif = "";
                List_Orders0.MablaghTakhfif = "";
                List_Orders0.TakhfifForMinmablagh = "";
                List_Orders0.Titels = "کلید نادرست";
                List_Orders.Add(List_Orders0);
                //return Ok("کلید نادرست");
            }
            long UserId = 0;
            UserId = Convert.ToInt64(User_ID);
            long _UserId = 0;
            ISession session = OpenNHibertnateSession.OpenSession();
            string _CodeTakhfif = "";
            _CodeTakhfif = CodeTakhfif;
            DateTime _Now = DateTime.Now;
            _UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered && p.IdUsrer == UserId).Select(p => p.IdUsrer).SingleOrDefault<long>();
            if (_UserId == 0 && List_Orders.Count == 0)
            {
                var List_Orders0 = new ListTakhfif();
                List_Orders0.IdCodeTakhfif = 0;
                List_Orders0.DarsadTakhfif = "";
                List_Orders0.MablaghTakhfif = "";
                List_Orders0.TakhfifForMinmablagh = "";
                List_Orders0.Titels = "کاربر یافت نشد";
                List_Orders.Add(List_Orders0);
            }
            var List_Order = (session.Query<TCodeTakhfif>().Where(a => a.CodeTakfif == CodeTakhfif && (_Now >= a.DateAz && _Now <= a.DateTa) && a.TLActive.IdActive == 1).Select(a => new
            {
                a.IdCodeTakhfif,
                a.DarsadTakhfif,
                a.MablaghTakhfif,
                a.TakhfifForMinmablagh

            }
            ));
            Int32 C_List = 0;
            C_List = List_Order.Count();


            if (C_List == 0 && _UserId != 0 && List_Orders.Count==0)
            {
                var List_Orders0 = new ListTakhfif();
                List_Orders0.IdCodeTakhfif = 0;
                List_Orders0.DarsadTakhfif = "";
                List_Orders0.MablaghTakhfif = "";
                List_Orders0.TakhfifForMinmablagh = "";
                List_Orders0.Titels = "کد تخفیف نا معتبر";
                List_Orders.Add(List_Orders0);
            }



            else if (C_List != 0 && _UserId != 0 && List_Orders.Count == 0)
            {
                var _List_Order = List_Order.ToList();
                #region اگر کد تخفیف قبلا استفاده شده باشد
                int _CodeTakhfifId = 0;
                _CodeTakhfifId = _List_Order[0].IdCodeTakhfif;
                long _ID_Order = 0;
                _ID_Order = session.QueryOver<TOrder>().Where(p => p.TUsers.IdUsrer == _UserId && p.TCodeTakhfif.IdCodeTakhfif == _CodeTakhfifId).Select(p => p.IdOrder).SingleOrDefault<long>();
                if (_List_Order[0].DarsadTakhfif != null && _ID_Order != 0)
                {
                    var List_Orders0 = new ListTakhfif();
                    List_Orders0.IdCodeTakhfif = 0;
                    List_Orders0.DarsadTakhfif = "";
                    List_Orders0.MablaghTakhfif = "";
                    List_Orders0.TakhfifForMinmablagh = "";
                    List_Orders0.Titels = "کد تخفیف قبلا استفاده شده";
                    List_Orders.Add(List_Orders0);
                }
                #endregion
                else
                {
                    for (int i = 0; i < C_List; i++)
                    {
                        var List_Orders02 = new ListTakhfif();
                        List_Orders02.IdCodeTakhfif = _List_Order[i].IdCodeTakhfif;
                        List_Orders02.DarsadTakhfif = _List_Order[i].DarsadTakhfif.ToString();
                        List_Orders02.MablaghTakhfif = _List_Order[i].MablaghTakhfif.ToString();
                        List_Orders02.TakhfifForMinmablagh = _List_Order[i].TakhfifForMinmablagh.ToString();
                        List_Orders02.Titels = "";
                        List_Orders.Add(List_Orders02);
                    }
                }
                
            }


            var json = JsonConvert.SerializeObject(new
            {
                MyTakhfif = List_Orders.OrderByDescending(a => a.IdCodeTakhfif)
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
        }


        public static string Temp_order(string Phone, DateTime TempOrder_Date, int TempOrder_Price, long tNoeProduct_ID, int tNoeKhedmat_ID, int t_Tedad, int tShift_ID, int tMahale_ID, long tUser_ID, long tAddress_ID, long tPeyk_ID, string _Tozihat, int tServicer_ID, int tVaziat_Pardakht_ID, long TemporderId_, int End_Record_, int tShiftIdTahvil, DateTime tDateTahvil, int tCodeTakhfif_ID_)
        {
            long _TemporderId = 0;
            _TemporderId = TemporderId_;
            //دریافت زمان سیستم
            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            ISession session = OpenNHibertnateSession.OpenSession();
            //try
            //{
            //string _Authority = "0";
            //string Authority = "0";
            ///ذخیر موقت اطلاعات
            
         
    

            TNoeProduct TNoeProductId = new TNoeProduct
            {
                IdNoeProduct = tNoeProduct_ID
            };
            TNoeKhedmat TNoeKhedmatId = new TNoeKhedmat
            {
                IdNoeKhedmat = tNoeKhedmat_ID
            };
            //TDay TDayId = new TDay
            //{
            //    DayId = tDay_ID
            //};
            TShift TIdShift = new TShift
            {
                IdShift = tShift_ID
            };

            TShift TIdShift2 = new TShift
            {
                IdShift = tShiftIdTahvil
            };
            TLMahaleh TLMahalehId = new TLMahaleh
            {
                IdMahaleh = tMahale_ID
            };
            TUsers TUsersId = new TUsers
            {
                IdUsrer = tUser_ID
            };
            TAddresses TAddressesId = new TAddresses
            {
                IdAddresses = tAddress_ID
            };
            TPeyks TPeyksId = new TPeyks
            {
                IdPeyks = tPeyk_ID
            };
            TServicer TServicerId = new TServicer
            {
                IdServicer = tServicer_ID
            };

            TLNoePay TLNoePayId = new TLNoePay
            {
                IdNoePay = 2
            };

            TLVaziyatVarizi TLVaziyatVariziId = new TLVaziyatVarizi
            {
                IdVaziyatVarizi = 5
            };


            //#region تخفیف بابت کد بازاریاب 
            //decimal Darsad_T_Bazaryab = 0;
            //decimal Darsad_Sahme_Bazaryab = 0;
            //long _Bazaryab_Id = 0;
            //_Bazaryab_Id = session.QueryOver<TUsers>().Where(p => p.IdUsrer == tUser_ID).Select(p => p.TBazaryab.BazaryabId).SingleOrDefault<long>();
            //if (_Bazaryab_Id != 0)
            //{
            //    Darsad_T_Bazaryab = session.QueryOver<TBazaryab>().Where(p => p.BazaryabId == _Bazaryab_Id && p.TLActive.Id == 1).Select(p => p.DarsadTakhfifBazaryab).SingleOrDefault<decimal>();
            //    Darsad_Sahme_Bazaryab = session.QueryOver<TBazaryab>().Where(p => p.BazaryabId == _Bazaryab_Id && p.TLActive.Id == 1).Select(p => p.DarsadTakhfifBazaryab).SingleOrDefault<decimal>();
            //}
            //#endregion
            //int _TakhfifPardakhti = 0;
            //_TakhfifPardakhti = Convert.ToInt32(((Convert.ToInt32(TempOrder_Price)) / 100) * Darsad_T_Bazaryab);

            //int _Sahmebazaryab = 0;
            //_Sahmebazaryab = Convert.ToInt32(((Convert.ToInt32(TempOrder_Price)) / 100) * Darsad_Sahme_Bazaryab);


            //TBazaryab _TBazaryabId = new TBazaryab
            //{

            //    BazaryabId = _Bazaryab_Id
            //};

            int _TemporderPriceBof = 0;
            _TemporderPriceBof = Convert.ToInt32(TempOrder_Price);



            TLVaziyatSabad TLVaziyatSabadId = new TLVaziyatSabad
            {
                IdVaziyatSabad = 1
            };

            #region واکشی هزینه پیک
            Int32 _Qeymat_Peyk = 0;
            int _ID_No_Hazine_Peyk = 0;
            _ID_No_Hazine_Peyk = session.Query<TServicerForMahaleh>().Where(p => p.TServicer.IdServicer == tServicer_ID && p.TLMahaleh.IdMahaleh == tMahale_ID).Select(p => p.TLNoeHPeyk.IdNoeHPeyk).SingleOrDefault<int>();
            var HazinehList = session.Query<TLNoeHPeyk>()
                                          .Where(p => p.IdNoeHPeyk == _ID_No_Hazine_Peyk)
                                          .Select(x =>
            new {
                x.TitelsNoeHPeyk,
                x.TitelsNoeHPeykElse,
                x.HazinehKolSefareshKamtarAz,
                x.HazinehKolSefareshBishtarAz
            }).ToList();
            if (_TemporderPriceBof >= HazinehList[0].HazinehKolSefareshBishtarAz && _TemporderPriceBof <= HazinehList[0].HazinehKolSefareshKamtarAz)
            {
                _Qeymat_Peyk = HazinehList[0].TitelsNoeHPeyk;
            }

            else
            {
                _Qeymat_Peyk = HazinehList[0].TitelsNoeHPeykElse;
            }
            //_Qeymat_Peyk = Convert.ToInt32(session.Query<TServicerForMahaleh>().Where(p => p.TServicer.IdServicer == tServicer_ID && p.TLMahaleh.IdMahaleh== tMahale_ID).Select(p => p.TLNoeHPeyk.TitelsNoeHPeyk).SingleOrDefault<string>());
            #endregion

            int PriceAll = 0;///هزینه نهایی قابل پرداخت
            //PriceAll = (_TemporderPriceBof - _TakhfifPardakhti) + _Qeymat_Peyk;وجود تخفیف
            #region اعمال کد تخفیف
            DateTime _Now = DateTime.Now;
            int _MizanTakhfif = 0;
            if (tCodeTakhfif_ID_!=0)
            {

            
            var List_codeTakhfif = (session.Query<TCodeTakhfif>().Where(a => a.IdCodeTakhfif == tCodeTakhfif_ID_ && (_Now >= a.DateAz && _Now <= a.DateTa) && a.TLActive.IdActive == 1).Select(a => new
            {
                a.IdCodeTakhfif,
                a.DarsadTakhfif,
                a.MablaghTakhfif,
                a.TakhfifForMinmablagh
            }
           ));
            Int32 C_List = 0;
            C_List = List_codeTakhfif.Count();
            var _List_codeTakhfif = List_codeTakhfif.ToList();
            long _TorderIDTakhfif = 0;
            _TorderIDTakhfif = session.QueryOver<TOrder>().Where(p => p.TCodeTakhfif.IdCodeTakhfif == tCodeTakhfif_ID_ && p.TUsers.IdUsrer == tUser_ID).Select(p => p.IdOrder).SingleOrDefault<long>();
            if (C_List != 0 && _TorderIDTakhfif == 0)
            {
                if (_List_codeTakhfif[0].DarsadTakhfif != null)
                {
                    _MizanTakhfif = Convert.ToInt32(_TemporderPriceBof / 100) * Convert.ToInt32(_List_codeTakhfif[0].DarsadTakhfif);
                    _TemporderPriceBof = _TemporderPriceBof - _MizanTakhfif;
                }

                else if (_List_codeTakhfif[0].MablaghTakhfif != null)
                {
                    if (_TemporderPriceBof > _List_codeTakhfif[0].TakhfifForMinmablagh)
                    {
                        _TemporderPriceBof = _TemporderPriceBof - Convert.ToInt32(_List_codeTakhfif[0].MablaghTakhfif);
                        _MizanTakhfif = Convert.ToInt32(_List_codeTakhfif[0].MablaghTakhfif);
                    }
                }
            }
            }
            #endregion
            PriceAll = (_TemporderPriceBof) + _Qeymat_Peyk;
            #region افزودن مقدار ارزش افزوده
            Int32 C_Arzeshafzodeh = 0;
            C_Arzeshafzodeh = Convert.ToInt32((PriceAll / 100) * 9);
            PriceAll = PriceAll + C_Arzeshafzodeh;
            #endregion
            int PriceSabad = 0;///هزینه نهایی محصول 
            //PriceSabad = (_TemporderPriceBof - _TakhfifPardakhti);وجود تخفیف
            PriceSabad = (_TemporderPriceBof);
            _TemporderId = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == _TemporderId).Select(p => p.IDTemporder).SingleOrDefault<long>();
            #region اگر Temp وجود نداشت
            if (_TemporderId == 0)
            {
                #region ایجاد شناسه سبد خرید
                long ShenashSabd;
                string format = "dd/MM/yyyy HH:mm:ss.ff";
                string str = DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
                DateTime date = DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
                //int msec = date.Millisecond;
                string dd1 = Convert.ToString(date.Year) + Convert.ToString(date.Month) + Convert.ToString(date.Day);
                string dd2 = Convert.ToString(date.Hour) + Convert.ToString(date.Minute) + Convert.ToString(date.Second) + Convert.ToString(date.Millisecond);

                int Lat = dd1.Length;
                int caseSwitch = 9 - Lat;

                switch (caseSwitch)
                {
                    case 0:
                        break;
                    case 1:
                        dd1 = "1" + dd1;
                        break;
                    case 2:
                        dd1 = "11" + dd1;
                        break;
                    case 3:
                        dd1 = "111" + dd1;
                        break;
                    case 4:
                        dd1 = "1111" + dd1;
                        break;
                    case 5:
                        dd1 = "11111" + dd1;
                        break;
                    case 6:
                        dd1 = "111111" + dd1;
                        break;
                    default:
                        break;
                }

                int Lat2 = dd2.Length;
                int caseSwitch2 = 9 - Lat2;

                switch (caseSwitch2)
                {
                    case 0:
                        break;
                    case 1:
                        dd2 = "1" + dd2;
                        break;
                    case 2:
                        dd2 = "11" + dd2;
                        break;
                    case 3:
                        dd2 = "111" + dd2;
                        break;
                    case 4:
                        dd2 = "1111" + dd2;
                        break;
                    case 5:
                        dd2 = "11111" + dd2;
                        break;
                    case 6:
                        dd2 = "111111" + dd2;
                        break;
                    default:
                        break;
                }
                ShenashSabd = Convert.ToInt64(dd1) + Convert.ToInt64(dd2);
                #endregion
                //if (_Bazaryab_Id != 0)
                //{
                //    TTemporder Temporder = new TTemporder
                //    {

                //        TemporderDate = TempOrder_Date,
                //        TemporderPrice = PriceAll,
                //        //TNan = TNanId,
                //        //NanTedad = tProduct_Tedad,
                //        TDay = TDayId,
                //        TShift = TIdShift,
                //        TLMahaleh = TLMahalehId,
                //        TUsers = TUsersId,
                //        TAddresses = TAddressesId,
                //        TPeyks = TPeyksId,
                //        TServicer = TServicerId,
                //        TLVaziyatVarizi = TLVaziyatVariziId,
                //        //Authority = Authority,
                //        TemporderNow = Time_Now,
                //        TLNoePay = TLNoePayId,
                //        TakhfifPardakhti = _TakhfifPardakhti,
                //        TBazaryab = _TBazaryabId,
                //        TemporderPriceBof = _TemporderPriceBof,
                //        ShenaseSefaresh = ShenashSabd,
                //        TLVaziyatSabad = TLVaziyatSabadId,
                //        PeykPrice = _Qeymat_Peyk
                //    };
                //    using (ITransaction transaction = session.BeginTransaction())
                //    {
                //        session.Save(Temporder);
                //        transaction.Commit();
                //    }
                //}
                //else
                //{
                int Y_TempOrder_Date = TempOrder_Date.Year;
                int M_TempOrder_Date = TempOrder_Date.Month;
                int D_TempOrder_Date = TempOrder_Date.Day;

                int Y_tDateTahvil = tDateTahvil.Year;
                int M_tDateTahvil = tDateTahvil.Month;
                int D_tDateTahvil = tDateTahvil.Day;

                PersianCalendar pc = new PersianCalendar();
                DateTime dt = new DateTime(Y_tDateTahvil, M_tDateTahvil, D_tDateTahvil, pc);
                DateTime dt011 = new DateTime(Y_TempOrder_Date, M_TempOrder_Date, D_TempOrder_Date, pc);

                if (tCodeTakhfif_ID_ != 0)
                {
                    TCodeTakhfif TCodeTakhfifId = new TCodeTakhfif
                    {
                        IdCodeTakhfif = tCodeTakhfif_ID_
                    };

                    TTemporder Temporder = new TTemporder
                    {

                        //TemporderDate =TempOrder_Date,
                        //TemporderDate = new DateTime(Y_TempOrder_Date, M_TempOrder_Date, D_TempOrder_Date, now.Hour, now.Minute, now.Second),
                        TemporderDate = TempOrder_Date,
                        TemporderPrice = PriceAll,
                        //TNan = TNanId,
                        //NanTedad = tProduct_Tedad,
                        //TDay = TDayId,
                        TShift = TIdShift,
                        TLMahaleh = TLMahalehId,
                        TUsers = TUsersId,
                        TAddresses = TAddressesId,
                        TPeyks = TPeyksId,
                        TServicer = TServicerId,
                        TLVaziyatVarizi = TLVaziyatVariziId,
                        //Authority = Authority,
                        TemporderNow = Time_Now,
                        TLNoePay = TLNoePayId,
                        //TakhfifPardakhti = _TakhfifPardakhti,وجود تخفیف
                        //TBazaryab = _TBazaryabId,
                        TemporderPriceBof = _TemporderPriceBof,
                        ShenaseSefaresh = ShenashSabd,
                        TLVaziyatSabad = TLVaziyatSabadId,
                        PeykPrice = _Qeymat_Peyk,
                        TShift2 = TIdShift2,
                        Datetahvil = tDateTahvil,
                        MizanTakhfif = _MizanTakhfif,
                        TCodeTakhfif = TCodeTakhfifId
                    };
                    using (ITransaction transaction = session.BeginTransaction())
                    {

                        session.Save(Temporder);
                        transaction.Commit();
                    }
                }

                if (tCodeTakhfif_ID_ == 0)
                {
                    

                    TTemporder Temporder = new TTemporder
                    {

                        //TemporderDate =TempOrder_Date,
                        //TemporderDate = new DateTime(Y_TempOrder_Date, M_TempOrder_Date, D_TempOrder_Date, now.Hour, now.Minute, now.Second),
                        TemporderDate = TempOrder_Date,
                        TemporderPrice = PriceAll,
                        //TNan = TNanId,
                        //NanTedad = tProduct_Tedad,
                        //TDay = TDayId,
                        TShift = TIdShift,
                        TLMahaleh = TLMahalehId,
                        TUsers = TUsersId,
                        TAddresses = TAddressesId,
                        TPeyks = TPeyksId,
                        TServicer = TServicerId,
                        TLVaziyatVarizi = TLVaziyatVariziId,
                        //Authority = Authority,
                        TemporderNow = Time_Now,
                        TLNoePay = TLNoePayId,
                        //TakhfifPardakhti = _TakhfifPardakhti,وجود تخفیف
                        //TBazaryab = _TBazaryabId,
                        TemporderPriceBof = _TemporderPriceBof,
                        ShenaseSefaresh = ShenashSabd,
                        TLVaziyatSabad = TLVaziyatSabadId,
                        PeykPrice = _Qeymat_Peyk,
                        TShift2 = TIdShift2,
                        Datetahvil = tDateTahvil,
                        MizanTakhfif = _MizanTakhfif
                    };
                    using (ITransaction transaction = session.BeginTransaction())
                    {

                        session.Save(Temporder);
                        transaction.Commit();
                    }
                }


                //}

                //try
                //{


                #region ثبت در سبد خرید
                var ListTTempOrder = session.Query<TTemporder>()
                         .Where(p => p.TUsers.IdUsrer == tUser_ID)
                         .Select(x => new
                         {
                             IDTemp = x.IDTemporder,
                             x.TemporderPrice,
                             x.TakhfifPardakhti,
                             x.TemporderPriceBof,
                             x.ShenaseSefaresh
                         }).OrderByDescending(s => s.IDTemp).Take(1).ToList();
                TTemporder Temporder2 = new TTemporder
                {
                    IDTemporder = ListTTempOrder[0].IDTemp
                };
                //if (_Bazaryab_Id != 0)
                //{
                //    TSabad Sabad = new TSabad
                //    {
                //        TNan = TNanId,
                //        NanTedad = tProduct_Tedad,
                //        TTemporder = Temporder2,
                //        ShenasehSefaresh = ShenashSabd,
                //        TBazaryab = _TBazaryabId,
                //        SahmeBazaryab = _Sahmebazaryab,
                //        TakhfifBazaryab = _TakhfifPardakhti,
                //        PriceAll = PriceSabad,
                //        PriceBof = _TemporderPriceBof,
                //        Date = TempOrder_Date,
                //        TLVaziyatSabad = TLVaziyatSabadId
                //    };
                //    using (ITransaction transaction = session.BeginTransaction())
                //    {
                //        session.Save(Sabad);
                //        transaction.Commit();
                //    }
                //}
                //if (_Bazaryab_Id == 0)
                //{
                TSabad Sabad = new TSabad
                {
                    TNoeProduct = TNoeProductId,
                    TNoeKhedmat = TNoeKhedmatId,
                    Tedad = t_Tedad,
                    TTemporder = Temporder2,
                    ShenasehSefaresh = ShenashSabd,
                    //TBazaryab = _TBazaryabId,
                    //SahmeBazaryab = _Sahmebazaryab,
                    //TakhfifBazaryab = _TakhfifPardakhti,
                    PriceAll = PriceSabad,
                    PriceBof = _TemporderPriceBof,
                    DateCreate = Time_Now,
                    TLVaziyatSabad = TLVaziyatSabadId,
                    TozihatMoshtari = _Tozihat

                };
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(Sabad);
                    transaction.Commit();
                }
                //}
                #endregion
                #region آپدیت مقدار پرداختی ها و تخفیفات در جدول Temp
                int y = 0;
                y = session.QueryOver<TSabad>().Where(p => p.ShenasehSefaresh == ShenashSabd).Select(s => s.IdSabad).RowCount();
                if (y != 1 && y != 0)
                {
                    int TemporderPrice1 = 0;
                    int TakhfifPardakhti1 = 0;
                    int TemporderPriceBof1 = 0;
                    TemporderPrice1 = Convert.ToInt32(ListTTempOrder[0].TemporderPrice) + PriceSabad;
                    //TakhfifPardakhti1 = Convert.ToInt32(ListTTempOrder[0].TakhfifPardakhti) + _TakhfifPardakhti;
                    TemporderPriceBof1 = Convert.ToInt32(ListTTempOrder[0].TemporderPriceBof) + _TemporderPriceBof;

                    var emp5 = session.Get<TTemporder>(ListTTempOrder[0].IDTemp);
                    emp5.TemporderPrice = TemporderPrice1;
                    emp5.TakhfifPardakhti = TakhfifPardakhti1;
                    emp5.TemporderPriceBof = TemporderPriceBof1;
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(emp5);
                        transaction.Commit();
                    }
                }

                #endregion
                //session.Close();
                int ForPay = 0;
                //return (" IDTemp=" + ListTTempOrder[0].IDTemp + " ShenaseSefaresh=" + ListTTempOrder[0].ShenaseSefaresh);
                return (ListTTempOrder[0].IDTemp.ToString());
                //}
                //catch (Exception e) { return String.Format("error_Zarinpal_insert"); }
                ////System.Environment.Exit(0);
                ////session.Close();
            }
            #endregion

            #region اگر Temp از قبل وجود داشت و یا این مقادیر بعدی سبد خرید می باشد
            else
            {
                #region ثبت در سبد خرید
                var ListTTempOrder3 = session.Query<TTemporder>()
                         .Where(p => p.TUsers.IdUsrer == tUser_ID && p.IDTemporder == _TemporderId)
                         .Select(x => new
                         {
                             IDTemp = x.IDTemporder,
                             x.TemporderPrice,
                             x.TakhfifPardakhti,
                             x.TemporderPriceBof,
                             x.ShenaseSefaresh
                         }).OrderByDescending(s => s.IDTemp).Take(1).ToList();
                TTemporder Temporder4 = new TTemporder
                {
                    IDTemporder = ListTTempOrder3[0].IDTemp
                };
                //if (_Bazaryab_Id != 0)
                //{
                //    TSabad Sabad2 = new TSabad
                //    {
                //        TNan = TNanId,
                //        NanTedad = tProduct_Tedad,
                //        TTemporder = Temporder4,
                //        ShenasehSefaresh = ListTTempOrder3[0].ShenaseSefaresh,
                //        TBazaryab = _TBazaryabId,
                //        SahmeBazaryab = _Sahmebazaryab,
                //        TakhfifBazaryab = _TakhfifPardakhti,
                //        PriceAll = PriceSabad,
                //        PriceBof = _TemporderPriceBof,
                //        Date = TempOrder_Date,
                //        TLVaziyatSabad = TLVaziyatSabadId
                //    };
                //    using (ITransaction transaction = session.BeginTransaction())
                //    {
                //        session.Save(Sabad2);
                //        transaction.Commit();

                //    }
                //}
                //else if (_Bazaryab_Id == 0)
                //{
                TSabad Sabad2 = new TSabad
                {
                    TNoeProduct = TNoeProductId,
                    TNoeKhedmat = TNoeKhedmatId,
                    Tedad = t_Tedad,
                    TTemporder = Temporder4,
                    ShenasehSefaresh = ListTTempOrder3[0].ShenaseSefaresh,
                    //TBazaryab = _TBazaryabId,
                    //SahmeBazaryab = _Sahmebazaryab,
                    //TakhfifBazaryab = _TakhfifPardakhti,
                    PriceAll = PriceSabad,
                    PriceBof = _TemporderPriceBof,
                    DateCreate = Time_Now,
                    TLVaziyatSabad = TLVaziyatSabadId

                };
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(Sabad2);
                    transaction.Commit();

                }
                //}

                #endregion
                #region آپدیت مقدار پرداختی ها و تخفیفات در جدول Temp
                int TemporderPrice = 0;
                int TakhfifPardakhti = 0;
                int TemporderPriceBof = 0;
                TemporderPrice = Convert.ToInt32(ListTTempOrder3[0].TemporderPrice) + PriceSabad;
                //TakhfifPardakhti = Convert.ToInt32(ListTTempOrder3[0].TakhfifPardakhti) + _TakhfifPardakhti;
                TemporderPriceBof = Convert.ToInt32(ListTTempOrder3[0].TemporderPriceBof) + _TemporderPriceBof;

                var emp2 = session.Get<TTemporder>(ListTTempOrder3[0].IDTemp);
                emp2.TemporderPrice = TemporderPrice;
                emp2.TakhfifPardakhti = TakhfifPardakhti;
                emp2.TemporderPriceBof = TemporderPriceBof;
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(emp2);
                    transaction.Commit();
                }
                #endregion

                return ("ثبت در سبد با موفیت انجام پذیرفت");
            }
            #endregion

            //}
            //catch (Exception)
            //{
            //    return ("error_Zarinpal");
            //}
            //return ("error_Zarinpal");
        }

        internal class Massege_Temp
        {
            public long IDTemp { get; set; }
            public string Shenaseh { get; set; }
            public string Types { get; set; }

        }

        internal class ListOrders
        {
            public long IdOrder { get; set; }
            public string OrderDate { get; set; }
            public string ShiftDaryaft { get; set; }
            public string ShiftTahvil { get; set; }
            public string DateDaryaft { get; set; }
            public string Datetahvil { get; set; }
            public string DayDaryaft { get; set; }
            public string Daytahvil { get; set; }
            public string AddressUser { get; set; }
            public string NameAddress { get; set; }
            public string PhoneAddress { get; set; }
            public string NameServicer { get; set; }
            public string AddressServicer { get; set; }
            public string VaziyatTahvil { get; set; }
            public int VaziyatTahvil_ID { get; set; }
            public string NoePay { get; set; }
            public long? ShenaseSefaresh { get; set; }
            public long? OrderPriceBof { get; set; }
            public string Type_Erore { get; set; }
            public int? PeykPrice { get; set; }
            public int? OrderPrice { get; set; }

        }

        internal class ListSabad
        {
            public long IdSabad { get; set; }
            public string NameGroup { get; set; }
            public string GroupProductTitels { get; set; }
            public string TitelsNoeKhedmat { get; set; }
            public string TozihatMoshtari { get; set; }
            public int? PriceBof { get; set; }
            public int? Tedad { get; set; }
            public int Washing { get; set; }
            public int WashingIroning { get; set; }
            public string Titels { get; set; }

        }

        internal class ListTakhfif
        {
            public int IdCodeTakhfif { get; set; }
            public string DarsadTakhfif { get; set; }
            public string MablaghTakhfif { get; set; }
            public string TakhfifForMinmablagh { get; set; }
            public string Titels { get; set; }

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

        //var Massege_Temp_ = new List<Massege_Temp>();
        //var List1 = new Massege_Temp();
        //List1.IDTemp = 0;
        //        List1.Shenaseh = "";
        //        List1.Types = "ثبت در سبد با موفیت انجام پذیرفت";
        //        Massege_Temp_.Add(List1);

        //        var json = JsonConvert.SerializeObject(new
        //        {
        //            //Allserviser = ListServiserZarfiyat_Mahale_
        //            Allserviser = Massege_Temp_
        //        });

        //JObject json5 = JObject.Parse(json);
        //session.Close();
        //        return ok(json5);

    }
}

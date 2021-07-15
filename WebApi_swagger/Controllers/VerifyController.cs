using NHibernate;
using NHibernate.Linq;
using SmsIrRestful;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApi_swagger.Models;

namespace WebApi_swagger.Controllers
{
    public class VerifyController : Controller
    {
        // GET: Verify
        public ActionResult Index()
        {
            try
            {
                ISession session = OpenNHibertnateSession.OpenSession();
                if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
                {
                    if (Request.QueryString["Status"].ToString().Equals("OK"))
                    {

                        //چک کردن Authority با تاریخ امروز تو جدول temp
                        string _Authority0 = Request.QueryString["Authority"];
                        DateTime now = DateTime.Now;
                        DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                        long _ATemporderId = 0;
                        _ATemporderId = session.QueryOver<TTemporder>().Where(p => p.Authority == _Authority0).Select(p => p.IDTemporder).SingleOrDefault<long>();
                        DateTime _TemporderNow = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == _ATemporderId).Select(p => p.TemporderNow).SingleOrDefault<DateTime>();
                        string long_time = TimeAgo_code(_TemporderNow);
                        if (long_time == "0")
                        {
                            ViewBag.Success = ("خطا!زمان برگشت از بانک نامعتبر ");
                            return View();
                        }
                        //چک کردن قیمت رفت با مقدار موجود در جدول temp
                        int _TemporderPrice = session.QueryOver<TTemporder>().Where(p => p.IDTemporder == _ATemporderId).Select(p => p.TemporderPrice).SingleOrDefault<int>();
                        int Amount = _TemporderPrice;
                        long RefID;
                        System.Net.ServicePointManager.Expect100Continue = false;
                        Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();

                        int Status = zp.PaymentVerification("6576b501-0b36-430d-83af-9761ba88fb95", Request.QueryString["Authority"].ToString(), Amount, out RefID);

                        if (Status == 100)
                            //if (true)
                            {
                            #region  آپدیت اطلاعات
                            if (_ATemporderId == 0)
                            {
                                //session.Close();
                                ViewBag.Success = ("خطا! اطلاعات نادرست ");
                                //    return View();
                            }
                            var emp = session.Get<TTemporder>(_ATemporderId);
                            emp.TLVaziyatVarizi = session.QueryOver<TLVaziyatVarizi>().Where(n => n.IdVaziyatVarizi == 6).SingleOrDefault();
                            using (ITransaction transaction = session.BeginTransaction())
                            {
                                session.Save(emp);
                                transaction.Commit();
                            }
                            #endregion
                            #region  ذخیره و نهایی کردن اطلاعات
                            var TTemporder = from p in session.Query<TTemporder>()
                                             where (p.IDTemporder == _ATemporderId && p.TLVaziyatVarizi.IdVaziyatVarizi == 6)
                                             select new
                                             {
                                                 p.IDTemporder,
                                                 p.TemporderDate,
                                                 p.TemporderPrice,
                                                 //p.TNan.NanId,
                                                 //p.NanTedad,
                                                 DayId = p.DayId,
                                                 p.TShift.IdShift,
                                                 p.TLMahaleh.IdMahaleh,
                                                 p.TUsers.IdUsrer,
                                                 p.TAddresses.IdAddresses,
                                                 p.TPeyks.IdPeyks,
                                                 p.TServicer.IdServicer,
                                                 p.TLVaziyatVarizi.IdVaziyatVarizi,
                                                 p.Authority,
                                                 p.TemporderNow,
                                                 p.ShenaseSefaresh,
                                                 p.TemporderPriceBof,
                                                 p.TLNoePay.IdNoePay,
                                                 p.TakhfifPardakhti,
                                                 //p.TBazaryab.BazaryabId,
                                                 p.PeykPrice,
                                                 Shift_IDTahvil= p.TShift2.IdShift,
                                                 TCodeTakhfifID_ = p.TCodeTakhfif,
                                                 p.MizanTakhfif,
                                                 p.Datetahvil

                                             };
                            var TTemporder_mojud = TTemporder.ToList();
                            var Sabad = from p in session.Query<TSabad>()
                                        where (p.TTemporder.IDTemporder == TTemporder_mojud[0].IDTemporder)
                                        select new
                                        {
                                            p.IdSabad,
                                            p.TNoeProduct.IdNoeProduct,
                                            p.TNoeKhedmat.IdNoeKhedmat,
                                            p.Tedad,
                                            p.TTemporder.IDTemporder,
                                            p.ShenasehSefaresh,
                                            //p.TBazaryab.BazaryabId,
                                            //p.SahmeBazaryab,
                                            //p.TakhfifBazaryab,
                                            p.PriceAll,
                                            p.PriceBof,
                                            p.DateCreate,
                                            p.TLVaziyatSabad.IdVaziyatSabad,
                                            p.TozihatMoshtari,
                                            p.TozihatSevviser

                                        };
                            var Sabad_mojud = Sabad.ToList();
                            Int64 TAddressId2 = TTemporder_mojud[0].IdAddresses;
                            var TTOrder = from p in session.Query<TOrder>()
                                          where (p.Authority == _Authority0)
                                          select new
                                          {
                                              p.Authority
                                          };
                            var TTOrder_mojud = TTOrder.ToList();
                            if (TTOrder_mojud.Count() != 0 && TTOrder_mojud != null)
                            {
                                ViewBag.Success = ("خطای درج با پشتیبانی تماس بگیرید");
                                session.Close();
                                return View();
                            }
                            ///ذخیر موقت اطلاعات
                            //TNan TNanId = new TNan
                            //{
                            //    NanId = TTemporder_mojud[0].NanId
                            //};
                            //TDayId TDayId = new TDay
                            //{
                            //    DayId = TTemporder_mojud[0].DayId
                            //};
                            
                            
                            TShift TShiftId = new TShift
                            {
                                IdShift = TTemporder_mojud[0].IdShift
                            };
                            TLMahaleh TMahaleId = new TLMahaleh
                            {
                                IdMahaleh = TTemporder_mojud[0].IdMahaleh
                            };
                            TUsers TUserId = new TUsers
                            {
                                IdUsrer = TTemporder_mojud[0].IdUsrer
                            };
                            TAddresses TAddressId = new TAddresses
                            {
                                IdAddresses = TTemporder_mojud[0].IdAddresses
                            };
                            TPeyks TPeykId = new TPeyks
                            {
                                IdPeyks = TTemporder_mojud[0].IdPeyks
                            };

                            TServicer TServicerId = new TServicer
                            {
                                IdServicer = TTemporder_mojud[0].IdServicer
                            };

                            TLVaziyatVarizi TLVaziyatVariziId = new TLVaziyatVarizi
                            {
                                IdVaziyatVarizi = TTemporder_mojud[0].IdVaziyatVarizi
                            };

                            TLNoePay TLNoePayId = new TLNoePay
                            {
                                IdNoePay = TTemporder_mojud[0].IdNoePay
                            };

                            TShift Shift_IDTahvil = new TShift
                            {
                                IdShift = TTemporder_mojud[0].Shift_IDTahvil
                            };

                            TLVaziyatSabad VaziyatSabadId = new TLVaziyatSabad
                            {
                                IdVaziyatSabad = 1
                            };

                            TLVaziyatTahvil TLVaziyatTahvilId = new TLVaziyatTahvil
                            {
                                IdVaziyatTahvil = 6
                            };
                            Int64 RefID2 = Convert.ToInt64(RefID);
                            int _CodeTakhfifID = 0;
                            if (TTemporder_mojud[0].TCodeTakhfifID_ != null)
                            {
                                _CodeTakhfifID = Convert.ToInt32(TTemporder_mojud[0].TCodeTakhfifID_);
                                TCodeTakhfif TCodeTakhfifId = new TCodeTakhfif
                                {
                                    IdCodeTakhfif = _CodeTakhfifID
                                };
                                TOrder Temporder = new TOrder
                                {

                                    OrderDate = TTemporder_mojud[0].TemporderDate,
                                    OrderPrice = TTemporder_mojud[0].TemporderPrice,
                                    TShift = TShiftId,
                                    TShift2 = Shift_IDTahvil,
                                    TLMahaleh = TMahaleId,
                                    RahgiriDate = Time_Now,
                                    RahgiriCode = RefID2,
                                    TUsers = TUserId,
                                    TAddresses = TAddressId,
                                    TPeyks = TPeykId,
                                    TServicer = TServicerId,
                                    Authority = _Authority0,
                                    TLVaziyatVarizi = TLVaziyatVariziId,
                                    TLNoePay = TLNoePayId,
                                    TakhfifPardakhti = TTemporder_mojud[0].TakhfifPardakhti,
                                    OrderPriceBof = TTemporder_mojud[0].TemporderPriceBof,
                                    OrderNow = Time_Now,
                                    ShenaseSefaresh = TTemporder_mojud[0].ShenaseSefaresh,
                                    TLVaziyatSabad = VaziyatSabadId,
                                    PeykPrice = TTemporder_mojud[0].PeykPrice,
                                    TLVaziyatTahvil = TLVaziyatTahvilId,
                                    MizanTakhfif = TTemporder_mojud[0].MizanTakhfif,
                                    Datetahvil = TTemporder_mojud[0].Datetahvil,
                                    TCodeTakhfif = TCodeTakhfifId,

                                };

                                try
                                {
                                    using (ITransaction transaction = session.BeginTransaction())
                                    {
                                        session.Save(Temporder);
                                        transaction.Commit();

                                    }
                                    #region آپدیت orderId در سبد خرید
                                    //Sabad_mojud
                                    long _OrderId = session.QueryOver<TOrder>().Where(p => p.TUsers.IdUsrer == TTemporder_mojud[0].IdUsrer && p.Authority == _Authority0 && p.RahgiriCode == RefID2).Select(p => p.IdOrder).SingleOrDefault<long>();
                                    int c_sabad = 0;
                                    c_sabad = Sabad_mojud.Count();
                                    for (int i = 0; i < c_sabad; i++)
                                    {
                                        var emp22 = session.Get<TSabad>(Sabad_mojud[i].IdSabad);
                                        emp22.TOrder = session.QueryOver<TOrder>().Where(n => n.IdOrder == _OrderId).SingleOrDefault(); ;
                                        using (ITransaction transaction = session.BeginTransaction())
                                        {
                                            session.Save(emp22);
                                            transaction.Commit();
                                        }
                                    }

                                    #endregion
                                    ////var emp32 = session.Get<TTemporder>(_ATemporderId);
                                    ////emp32.VaziatPardakht = "1";
                                    ////using (ITransaction transaction = session.BeginTransaction())
                                    ////{
                                    ////    session.Save(emp32);
                                    ////    transaction.Commit();
                                    ////}
                                    #endregion
                                    #region اپدیت اطلاعات ای دی اخرین سفارش در جدول آدرس
                                    Int64 _orderid = session.QueryOver<TOrder>().Where(p => p.RahgiriCode == RefID2 && p.Authority == _Authority0).Select(p => p.IdOrder).SingleOrDefault<Int64>();
                                    var emp2 = session.Get<TAddresses>(TAddressId2);
                                    emp2.Lastorderid = _orderid;
                                    using (ITransaction transaction = session.BeginTransaction())
                                    {
                                        session.Save(emp2);
                                        transaction.Commit();
                                    }

                                    #region  آپدیت اطلاعات جدول تجمعی شیفتی
                                    int c_sabad2 = 0;
                                    c_sabad2 = Sabad_mojud.Count();
                                    for (int i = 0; i < c_sabad2; i++)
                                    {
                                        Int64 _TCGoZarfiyatShPeyksid = 0;
                                        _TCGoZarfiyatShPeyksid = session.QueryOver<TCGoZarfiyatShPeyks>().Where(p => p.TPeyks.IdPeyks == (TTemporder_mojud[0].IdPeyks) && p.TShift.IdShift == TTemporder_mojud[0].IdShift && p.Datenowgo == TTemporder_mojud[0].TemporderDate).Select(p => p.IdCGoZarfiyatShPeyk).SingleOrDefault<Int64>();

                                        if (_TCGoZarfiyatShPeyksid != 0)
                                        {
                                            var emp3 = session.Get<TCGoZarfiyatShPeyks>(_TCGoZarfiyatShPeyksid);
                                            emp3.CountGoPeyks++;
                                            //emp3.ZarfiatereservedTedadNanva = emp3.ZarfiatereservedTedadNanva + Convert.ToInt16(Sabad_mojud[i].NanTedad);
                                            using (ITransaction transaction = session.BeginTransaction())
                                            {
                                                session.Save(emp3);
                                                transaction.Commit();
                                            }
                                        }
                                        else//درج اطلاعات جدید تو جدول shs
                                        {

                                            TCGoZarfiyatShPeyks TSh = new TCGoZarfiyatShPeyks
                                            {
                                                Datenowgo = TTemporder_mojud[0].TemporderDate,
                                                TShift = TShiftId,
                                                TPeyks = TPeykId,
                                                CountGoPeyks = 1
                                            };
                                            using (ITransaction transaction = session.BeginTransaction())
                                            {
                                                session.Save(TSh);
                                                transaction.Commit();
                                            }
                                        }

                                        Int64 _TCGoZarfiyatShServiserid = 0;
                                        _TCGoZarfiyatShServiserid = session.QueryOver<TCGoZarfiyatShServiser>().Where(p => p.TServicer.IdServicer == (TTemporder_mojud[0].IdServicer) && p.ShiftId == TTemporder_mojud[0].IdShift && p.Datenowgo == TTemporder_mojud[0].TemporderDate).Select(p => p.IdCGoZarfiyatShServiser).SingleOrDefault<Int64>();

                                        if (_TCGoZarfiyatShServiserid != 0)
                                        {
                                            var emp3 = session.Get<TCGoZarfiyatShServiser>(_TCGoZarfiyatShServiserid);
                                            emp3.CountGoService++;
                                            emp3.CountDeliveryService++;
                                            //emp3.ZarfiatereservedTedadNanva = emp3.ZarfiatereservedTedadNanva + Convert.ToInt16(Sabad_mojud[i].NanTedad);
                                            using (ITransaction transaction = session.BeginTransaction())
                                            {
                                                session.Save(emp3);
                                                transaction.Commit();
                                            }
                                        }
                                        else//درج اطلاعات جدید تو جدول shs
                                        {

                                            TCGoZarfiyatShServiser TSh = new TCGoZarfiyatShServiser
                                            {
                                                Datenowgo = TTemporder_mojud[0].TemporderDate,
                                                ShiftId = TTemporder_mojud[0].IdShift,
                                                TServicer = TServicerId,
                                                CountGoService = 1,
                                                CountDeliveryService = 1
                                            };
                                            using (ITransaction transaction = session.BeginTransaction())
                                            {
                                                session.Save(TSh);
                                                transaction.Commit();
                                            }
                                        }

                                    }

                                    #endregion
                                }
                                catch (Exception e)
                                {
                                    ViewBag.Success = ("خطای درج با پشتیبانی تماس بگیرید");
                                    session.Close();
                                    return View();
                                }
                            }

                            else if (TTemporder_mojud[0].TCodeTakhfifID_ == null)
                            {
                               
                                TOrder Temporder = new TOrder
                                {

                                    OrderDate = TTemporder_mojud[0].TemporderDate,
                                    OrderPrice = TTemporder_mojud[0].TemporderPrice,
                                    TShift = TShiftId,
                                    TShift2 = Shift_IDTahvil,
                                    TLMahaleh = TMahaleId,
                                    RahgiriDate = Time_Now,
                                    RahgiriCode = RefID2,
                                    TUsers = TUserId,
                                    TAddresses = TAddressId,
                                    TPeyks = TPeykId,
                                    TServicer = TServicerId,
                                    Authority = _Authority0,
                                    TLVaziyatVarizi = TLVaziyatVariziId,
                                    TLNoePay = TLNoePayId,
                                    TakhfifPardakhti = TTemporder_mojud[0].TakhfifPardakhti,
                                    OrderPriceBof = TTemporder_mojud[0].TemporderPriceBof,
                                    OrderNow = Time_Now,
                                    ShenaseSefaresh = TTemporder_mojud[0].ShenaseSefaresh,
                                    TLVaziyatSabad = VaziyatSabadId,
                                    PeykPrice = TTemporder_mojud[0].PeykPrice,
                                    TLVaziyatTahvil = TLVaziyatTahvilId,
                                    MizanTakhfif = TTemporder_mojud[0].MizanTakhfif,
                                    Datetahvil= TTemporder_mojud[0].Datetahvil

                                };

                                try
                                {
                                    using (ITransaction transaction = session.BeginTransaction())
                                    {
                                        session.Save(Temporder);
                                        transaction.Commit();

                                    }
                                    #region آپدیت orderId در سبد خرید
                                    //Sabad_mojud
                                    long _OrderId = session.QueryOver<TOrder>().Where(p => p.TUsers.IdUsrer == TTemporder_mojud[0].IdUsrer && p.Authority == _Authority0 && p.RahgiriCode == RefID2).Select(p => p.IdOrder).SingleOrDefault<long>();
                                    int c_sabad = 0;
                                    c_sabad = Sabad_mojud.Count();
                                    for (int i = 0; i < c_sabad; i++)
                                    {
                                        var emp22 = session.Get<TSabad>(Sabad_mojud[i].IdSabad);
                                        emp22.TOrder = session.QueryOver<TOrder>().Where(n => n.IdOrder == _OrderId).SingleOrDefault(); ;
                                        using (ITransaction transaction = session.BeginTransaction())
                                        {
                                            session.Save(emp22);
                                            transaction.Commit();
                                        }
                                    }

                                    #endregion
                                    ////var emp32 = session.Get<TTemporder>(_ATemporderId);
                                    ////emp32.VaziatPardakht = "1";
                                    ////using (ITransaction transaction = session.BeginTransaction())
                                    ////{
                                    ////    session.Save(emp32);
                                    ////    transaction.Commit();
                                    ////}
                                    #endregion
                                    #region اپدیت اطلاعات ای دی اخرین سفارش در جدول آدرس
                                    Int64 _orderid = session.QueryOver<TOrder>().Where(p => p.RahgiriCode == RefID2 && p.Authority == _Authority0).Select(p => p.IdOrder).SingleOrDefault<Int64>();
                                    var emp2 = session.Get<TAddresses>(TAddressId2);
                                    emp2.Lastorderid = _orderid;
                                    using (ITransaction transaction = session.BeginTransaction())
                                    {
                                        session.Save(emp2);
                                        transaction.Commit();
                                    }

                                    #region  آپدیت اطلاعات جدول تجمعی شیفتی
                                    int c_sabad2 = 0;
                                    c_sabad2 = Sabad_mojud.Count();
                                    for (int i = 0; i < c_sabad2; i++)
                                    {
                                        Int64 _TCGoZarfiyatShPeyksid = 0;
                                        _TCGoZarfiyatShPeyksid = session.QueryOver<TCGoZarfiyatShPeyks>().Where(p => p.TPeyks.IdPeyks == (TTemporder_mojud[0].IdPeyks) && p.TShift.IdShift == TTemporder_mojud[0].IdShift && p.Datenowgo == TTemporder_mojud[0].TemporderDate).Select(p => p.IdCGoZarfiyatShPeyk).SingleOrDefault<Int64>();

                                        if (_TCGoZarfiyatShPeyksid != 0)
                                        {
                                            var emp3 = session.Get<TCGoZarfiyatShPeyks>(_TCGoZarfiyatShPeyksid);
                                            emp3.CountGoPeyks++;
                                            //emp3.ZarfiatereservedTedadNanva = emp3.ZarfiatereservedTedadNanva + Convert.ToInt16(Sabad_mojud[i].NanTedad);
                                            using (ITransaction transaction = session.BeginTransaction())
                                            {
                                                session.Save(emp3);
                                                transaction.Commit();
                                            }
                                        }
                                        else//درج اطلاعات جدید تو جدول shs
                                        {

                                            TCGoZarfiyatShPeyks TSh = new TCGoZarfiyatShPeyks
                                            {
                                                Datenowgo = TTemporder_mojud[0].TemporderDate,
                                                TShift = TShiftId,
                                                TPeyks = TPeykId,
                                                CountGoPeyks = 1
                                            };
                                            using (ITransaction transaction = session.BeginTransaction())
                                            {
                                                session.Save(TSh);
                                                transaction.Commit();
                                            }
                                        }

                                        Int64 _TCGoZarfiyatShServiserid = 0;
                                        _TCGoZarfiyatShServiserid = session.QueryOver<TCGoZarfiyatShServiser>().Where(p => p.TServicer.IdServicer == (TTemporder_mojud[0].IdServicer) && p.ShiftId == TTemporder_mojud[0].IdShift && p.Datenowgo == TTemporder_mojud[0].TemporderDate).Select(p => p.IdCGoZarfiyatShServiser).SingleOrDefault<Int64>();

                                        if (_TCGoZarfiyatShServiserid != 0)
                                        {
                                            var emp3 = session.Get<TCGoZarfiyatShServiser>(_TCGoZarfiyatShServiserid);
                                            emp3.CountGoService++;
                                            emp3.CountDeliveryService++;
                                            //emp3.ZarfiatereservedTedadNanva = emp3.ZarfiatereservedTedadNanva + Convert.ToInt16(Sabad_mojud[i].NanTedad);
                                            using (ITransaction transaction = session.BeginTransaction())
                                            {
                                                session.Save(emp3);
                                                transaction.Commit();
                                            }
                                        }
                                        else//درج اطلاعات جدید تو جدول shs
                                        {

                                            TCGoZarfiyatShServiser TSh = new TCGoZarfiyatShServiser
                                            {
                                                Datenowgo = TTemporder_mojud[0].TemporderDate,
                                                ShiftId = TTemporder_mojud[0].IdShift,
                                                TServicer = TServicerId,
                                                CountGoService = 1,
                                                CountDeliveryService = 1
                                            };
                                            using (ITransaction transaction = session.BeginTransaction())
                                            {
                                                session.Save(TSh);
                                                transaction.Commit();
                                            }
                                        }

                                    }

                                    #endregion
                                }
                                catch (Exception e)
                                {
                                    ViewBag.Success = ("خطای درج با پشتیبانی تماس بگیرید");
                                    session.Close();
                                    return View();
                                }
                            }
                            #endregion
                            //return View("Success! RefId: " + RefID);
                            #region ثبت اطلاعات در جدول درامدهای بازاریاب
                            var LatOrderId = session.Query<TOrder>()
                                .Where(p => p.ShenaseSefaresh == TTemporder_mojud[0].ShenaseSefaresh && p.TUsers.IdUsrer == TTemporder_mojud[0].IdUsrer && p.OrderDate == TTemporder_mojud[0].TemporderDate)
                                                   .Select(x => new
                                                   {
                                                       x.IdOrder,
                                                       //x.TBazaryab.BazaryabId
                                                   }).OrderByDescending(p => p.IdOrder).Take(1).ToList();

                           

                                string format = "dd/MM/yyyy HH:mm:ss.ff";
                                string str2 = DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
                                DateTime date2 = DateTime.ParseExact(str2, format, CultureInfo.InvariantCulture);

                               

                         

                            #endregion

                            #region ارسال پیام سفارش جدید به آقای سروری
                            var token = new Token().GetToken("db0ed44afb68e91ebb82c7c9", "64886488@!20%");

                            var ultraFastSend = new UltraFastSend()
                            {
                                Mobile = Convert.ToInt64("09185074942"),
                                TemplateId = 7334,
                                ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                       Parameter = "Name" , ParameterValue = ""
                                       },

                                          new UltraFastParameters()
                                     {
                                       Parameter = "Shift" , ParameterValue = ""
                                       }
                              }.ToArray()

                            };
                            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);
                            #endregion

                            ViewBag.Titr = (" تراکنش موفق ");
                            ViewBag.Success = (" :کد رهگیری شما " + " " + RefID);
                            ViewBag.ok = ("با تشکر سفارش شما در زمان مورد نظر انجام خواهد شد");
                            session.Close();
                            return View();
                        }
                        else
                        {
                            if (Status == 101)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("تراكنش انجام شده است");
                                session.Close();
                                return View();
                            }
                            else if (Status == -54)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("درخواست مورد نظر آرشيو شده است");
                                session.Close();
                                return View();
                            }
                            else if (Status == -42)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("مدت زمان معتبر طول عمر شناسه پرداخت بايد بين 30 دقيه تا 45 روز مي باشد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -41)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("اطلاعات ارسال شده مربوط به" + "AdditionalData" + "غيرمعتبر ميباشد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -40)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("اجازه دسترسي به متد مربوطه وجود ندارد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -34)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("سقف تقسيم تراكنش از لحاظ تعداد يا رقم عبور نموده است");
                                session.Close();
                                return View();
                            }
                            else if (Status == -33)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("رقم تراكنش با رقم پرداخت شده مطابقت ندارد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -22)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("تراكنش نا موفق ميباشد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -21)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("هيچ نوع عمليات مالي براي اين تراكنش يافت نشد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -12)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("امكان ويرايش درخواست ميسر نمي باشد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -11)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("درخواست مورد نظر يافت نشد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -4)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("سطح تاييد پذيرنده پايين تر از سطح نقره اي است");
                                session.Close();
                                return View();
                            }
                            else if (Status == -3)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("با توجه به محدوديت هاي شاپرك امكان پرداخت با رقم درخواست شده ميسر نمي باشد");
                                session.Close();
                                return View();
                            }
                            else if (Status == -2)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("و يا مرچنت كد پذيرنده صحيح نيست. IP");
                                session.Close();
                                return View();
                            }
                            else if (Status == -1)
                            {
                                ViewBag.Titr = ("خطا!\n" + "کد: " + Status);
                                ViewBag.Status = ("اطلاعات ارسال شده ناقص است");
                                session.Close();
                                return View();
                            }
                            //ViewBag.Status = ("Error! Status: " + Status);
                            //return View();
                            //return View("Error! Status: " + Status);
                        }

                    }
                    else
                    {
                        ViewBag.Titr = ("خطا در پرداخت!");
                        ViewBag.Authority = (" شناسه: " + Request.QueryString["Authority"].ToString() + " کد خطا: " + Request.QueryString["Status"].ToString());
                        return View();
                        //return View("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
                    }
                }
                else
                {
                    ViewBag.Invalid = ("خطا! ارتباط با سرور وجود ندارد");
                    return View();
                    //return View("Invalid Input");
                }
                ViewBag.Invalid = ("خطا! ارتباط با سرور وجود ندارد");
                return View();
        }
            catch (Exception)
            {
                //Response.AddHeader("Refresh", "1");
                //ViewBag.Invalid = ("صفحه رو دوباره لود کنید");
                ViewBag.Invalid = ("خطا با پشتیبان  هلکو تماس بگیرید");
                return View();
                throw;
            }

}

        public static string TimeAgo_code(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("0");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("0");
            }
            if (span.Days > 0)
                return String.Format("0");
            if (span.Hours > 0 || span.Hours < 0)
                return String.Format("0");
            if (span.Minutes >= 20 || span.Minutes < 0)
                return String.Format("0");
            if (span.Minutes < 20)
                return String.Format("1");
            return string.Empty;
        }
    }
}

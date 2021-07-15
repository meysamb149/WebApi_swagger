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
using System.Threading;
using System.Web.Http;
using WebApi_swagger.Models;

namespace WebApi_swagger.Controllers
{
    public class AppServicesController : ApiController
    {
        ISession session = OpenNHibertnateSession.OpenSession();
        string _LongTime = "0";

        [HttpGet]
        [Route("api/G1/GetCreateTServicer_Login/{Unique_code},{Phone},{Pass},{Unique_device_code},{PaykId},{ServicerId},{sp}")]
        public IHttpActionResult GetCreateTServicer_Login(string Unique_code, string Phone,string Pass, string Unique_device_code,long PaykId ,int ServicerId,Int16 sp)
        {
            if (Phone == "{Phone}" || Unique_device_code == "{Unique_device_code}" )
            {
                session.Close();
                #region نمایش به صورت jason
                Massege massege = new Massege
                {
                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "مقادیر خالی"
                };
                var carss = massege;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    Error = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss);
                #endregion
                //return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                #region نمایش به صورت jason
                Massege massege = new Massege
                {

                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "کلید نادرست"
                };
                var carss = massege;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    Error = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss);
                #endregion
                //return Ok("کلید نادرست");
            }

            string IsPhoneRegistered = Phone;
            int IsPhoneRegistered_Count = IsPhoneRegistered.Length;
            if (IsPhoneRegistered_Count != 11)
            {
                session.Close();
                #region نمایش به صورت jason
                Massege massege = new Massege
                {
                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "شماره تلفن اشتباه می باشد"
                };
                var carss = massege;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    Error = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss);
                #endregion
                //return Ok("شماره تلفن اشتباه می باشد");
                //return Ok(IsPhoneRegistered);
            }

            Int16 is_splash = sp;
            string IsDevice_ID = Unique_device_code;
            string name_family = "0";
            //long UserId = Convert.ToInt64(UsersId);
            //long _UserId = 0;
            long _UserIdForis_user = 0;
            //long _ActivationForis_user = 0;
            int _ActivationForis_user = 0;
            //long _UserId2 = Convert.ToInt64(UsersId);
            int _Activation = 00;
            int _IsDeleted = 0;//اگر مساوی 0 باشد یعنی کاربر کدی درخواست نداده
            string _LongTime_Khatakar = "0";
            string _Device_ID = Unique_device_code;
            string _Name = "0";

            long _PeykId = 0;
            int _ServicerId = 0;
            long _PeykIdOK = 0;
            int _ServicerIdOK = 0;

            long _PeykId2 = PaykId;
            int _ServicerId2 = ServicerId;

            //_UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered).Select(p => p.IdUsrer).SingleOrDefault<long>();
            _PeykId = session.QueryOver<TPeyks>().Where(p => p.Phone == IsPhoneRegistered).Select(p => p.IdPeyks).SingleOrDefault<long>();
            _ServicerId = session.QueryOver<TServicer>().Where(p => p.PhoneAdmin == IsPhoneRegistered).Select(p => p.IdServicer).SingleOrDefault<int>();

            if (_PeykId==0 && _ServicerId==0)
            {
                return Ok("کاربر یافت نشد اطلاعات ارسالی نادرست می باشد");
            }
            if (_PeykId!=0)
            {
                _PeykIdOK = session.QueryOver<TPeyks>().Where(p => p.Phone == IsPhoneRegistered && p.Pass==Pass).Select(p => p.IdPeyks).SingleOrDefault<long>();
                if (_PeykIdOK==0)
                {
                    return Ok("رمز عبور صحیح نمی باشد");
                }

            }

            else if (_ServicerId != 0)
            {
                _ServicerId = session.QueryOver<TServicer>().Where(p => p.PhoneAdmin == IsPhoneRegistered && p.Pass==Pass).Select(p => p.IdServicer).SingleOrDefault<int>();
                if (_ServicerId == 0)
                {
                    return Ok("رمز عبور صحیح نمی باشد");
                }

            }

            Int64 _End_AcId = 0;
            int _Is_Ins = 0;///اگر مساوی 1 شود کاربر میتواند کد درخواس بدهد
            DateTime _IS_time;

            #region لاگین سرویس دهنده
            if (_ServicerId != 0)
            {
                if (_ServicerId != 0) // یعنی کاربر قبلا ثبت نام کرده
                {

                    _UserIdForis_user = _ServicerId;
                    //_ActivationForis_user = session.QueryOver<TActivationCode>().Where(p => p.TUser.UserId == _UserId && p.DeviceId == IsDevice_ID).Select(p => p.AcId).SingleOrDefault<long>();
                    _ActivationForis_user = session.QueryOver<TServicer>().Where(p => p.IdServicer == _ServicerId && p.DeviceIdLogin == IsDevice_ID).Select(p => p.Activation).SingleOrDefault<int>();//  این کاربر با کدام گوشی

                    _Activation = session.QueryOver<TServicer>().Where(p => p.IdServicer == _ServicerId).Select(p => p.Activation).SingleOrDefault<int>();
                    _IsDeleted = session.QueryOver<TServicer>().Where(p => p.IdServicer == _ServicerId).Select(p => p.IsDeleted).SingleOrDefault<int>();
                    if (_UserIdForis_user != 0 && _ActivationForis_user == 1 && _ServicerId2 == _ServicerId)
                    {

                        _Name = session.QueryOver<TServicer>().Where(p => p.IdServicer == _ServicerId).Select(p => p.NameServicer).SingleOrDefault<string>();
                        if (_Name != "0")
                        {
                            session.Close();
                            //return Ok("ورد به پنل نان با نام");
                            #region نمایش به صورت jason
                            Massege massege = new Massege
                            {
                                Userid = "Servicer",
                                TimeIs = "",
                                Names = _Name,
                                Counts = 0,
                                Types = "",


                            };
                            var carss = massege;
                            return Ok(carss);
                            #endregion
                        }


                    }

                }
            }


             _End_AcId = 0;


            _Is_Ins = 0;///اگر مساوی 1 شود کاربر میتواند کد درخواس بدهد

           var _End_AcIdList1 = session.Query<TActivationCode>().Where(p => p.DeviceId == IsDevice_ID)
                 .Select(x => new { x.IdActivationCode, x.CodeGenerationTime })
                 .OrderByDescending(x => x.IdActivationCode).Take(1).ToList();
            if (_End_AcIdList1.Count != 0)
            {
                _End_AcId = _End_AcIdList1[0].IdActivationCode;
            }


            if (_End_AcId != 0)
            {
                _IS_time = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _End_AcId).Select(x => x.CodeGenerationTime).SingleOrDefault<DateTime>();
                DateTime _Is_Deleted_Time = _IS_time;
                _Is_Deleted_Time = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _End_AcId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                if (_Is_Deleted_Time != _IS_time)
                {
                    _LongTime_Khatakar = TimeAgo(_Is_Deleted_Time);
                }
                _LongTime = TimeAgo(_IS_time);
                if (_LongTime == "0" && _LongTime_Khatakar == "0")//کاربر می تواند درخواست کد یا ورود و یا ثبت نام کند
                {
                    _Is_Ins = 1;
                }
                else
                {
                    if (_IsDeleted == 0)///کاربردرست کار می باشد و تایم آن اعلام میشود
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege = new Massege
                        {
                            Userid = "Servicer",
                            TimeIs = _LongTime,
                            Names = "",
                            Counts = 0,
                            Types = "D"
                        };
                        var carss = massege;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    DoroskarT = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss);
                        #endregion
                        //return Ok("D:" + _LongTime);
                    }
                    else if (_IsDeleted == 1)///کاربر خطا کار می باشد و تایم آن اعلام میشود
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege = new Massege
                        {
                            Userid = "Servicer",
                            TimeIs = _LongTime_Khatakar,
                            Names = "",
                            Counts = 0,
                            Types = "K"
                        };
                        var carss = massege;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    DoroskarT = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss);
                        #endregion
                        //return Ok("K:" + _LongTime_Khatakar);
                    }

                }
                //return Ok(_IS_time);
            }
            else
            {
                _Is_Ins = 1;
            }


            if ((_ServicerId != 0))
            {
                if (_ActivationForis_user == 0 || _UserIdForis_user == 0)
                {
                    if ((_Activation == 0 || _Activation == 2 || _Activation == 1))
                    {
                        if (_IsDeleted == 0 || _Is_Ins == 1)
                        {
                            if (is_splash == 1)
                            {
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege9 = new Massege
                                {
                                    Userid = "Servicer",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس و ورود به اکتیویتی شماره تلفن"
                                };
                                var carss9 = massege9;
                                //var json29 = JsonConvert.SerializeObject(new
                                //{
                                //    Error = carss9
                                //});
                                //JObject json219 = JObject.Parse(json29);
                                return Ok(carss9);
                                #endregion
                                //return Ok("کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس و ورود به اکتیویتی شماره تلفن");
                            }
                            else
                            {
                                //ایجاد کد
                                var chars = "123456789";
                                var stringChars = new char[5];
                                var random = new Random();
                                for (int i = 0; i < stringChars.Length; i++)
                                {
                                    stringChars[i] = chars[random.Next(chars.Length)];
                                }
                                var finalString = new String(stringChars);
                                //دریافت زمان سیستم
                                DateTime now = DateTime.Now;
                                DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                                //////کد آپدیت
                                ////ISession session2 = OpenNHibertnateSession.OpenSession();
                                //ITransaction tx = session.BeginTransaction();
                                //string hqlVersionedUpdate = "update TActivationCode set Code = :Code2 , DeviceId=:DeviceId2 , CodeGenerationTime=:CodeGenerationTime2 , EnterCount=:EnterCount2 where Tell = :Tells";
                                //int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                //        .SetString("Code2", finalString)
                                //        .SetString("Tells", IsPhoneRegistered)
                                //        .SetString("DeviceId2", IsDevice_ID)
                                //        .SetParameter("CodeGenerationTime2", Time_Now)
                                //        .SetParameter("EnterCount2", 0)
                                //        //.SetDateTime()
                                //        .ExecuteUpdate();
                                //tx.Commit();

                                //var emp = session.Get<TActivationCode>(IsPhoneRegistered);
                                var emp = session.QueryOver<TActivationCode>().Where(p => p.Tell == IsPhoneRegistered).SingleOrDefault();
                                emp.Code = finalString;
                                emp.DeviceId = IsDevice_ID;
                                emp.CodeGenerationTime = Time_Now;
                                emp.EnterCount = 0;
                                emp.TServicer = session.QueryOver<TServicer>().Where(p => p.PhoneAdmin == IsPhoneRegistered).SingleOrDefault();
                                using (ITransaction transaction = session.BeginTransaction())
                                {
                                    session.Save(emp);
                                    transaction.Commit();
                                }

                                ////ITransaction tx_user = session.BeginTransaction();
                                ////string hqlVersionedUpdate_user = "update TUser set IsDeleted = :IsDeleted2  where Tell = :Tells";
                                ////int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_user)
                                ////        .SetString("IsDeleted2", "0")
                                ////        .SetString("Tells", IsPhoneRegistered)
                                ////        //.SetDateTime()
                                ////        .ExecuteUpdate();
                                ////tx_user.Commit();

                                var emp2 = session.QueryOver<TServicer>().Where(p => p.PhoneAdmin == IsPhoneRegistered).SingleOrDefault();
                                emp2.IsDeleted = 0;
                                using (ITransaction transaction = session.BeginTransaction())
                                {
                                    session.Save(emp2);
                                    transaction.Commit();

                                }

                                session.Close();
                                //کد آپدیت
                                ///ارسال کد
                                #region اسال پیامک بدون تبلیغاتی ها
                                //var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                                //var messageSendObject = new MessageSendObject()
                                //{
                                //    Messages = new List<string> { "با سلام کد فعال سازی شما در پیک نان:" + finalString }.ToArray(),
                                //    MobileNumbers = new List<string> { IsPhoneRegistered }.ToArray(),
                                //    LineNumber = "30004747470516",
                                //    SendDateTime = null,
                                //    CanContinueInCaseOfError = true
                                //};

                                //MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

                                //if (messageSendResponseObject.IsSuccessful)
                                //{
                                //    session.Close();
                                //    return Ok("کاربر به تازگی ثبت نام کرده یا خارج شده پس ارسال کد");
                                //}
                                //else
                                //{
                                //    session.Close();
                                //    return Ok("خظای ارسال پیام");
                                //}

                                #endregion

                                var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                                var ultraFastSend = new UltraFastSend()
                                {
                                    Mobile = Convert.ToInt64(IsPhoneRegistered),
                                    TemplateId = 28733,
                                    ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "VerificationCode" , ParameterValue = finalString
                                       }
                              }.ToArray()

                                };
                                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                                if (ultraFastSendRespone.IsSuccessful)
                                {
                                    //session.Close();
                                    #region نمایش به صورت jason
                                    Massege massege8 = new Massege
                                    {
                                        Userid = "Servicer",
                                        TimeIs = "",
                                        Names = "",
                                        Counts = 0,
                                        Types = "کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس پس ارسال کد"
                                    };
                                    var carss8 = massege8;
                                    //var json28 = JsonConvert.SerializeObject(new
                                    //{
                                    //    Error = carss8
                                    //});
                                    //JObject json218 = JObject.Parse(json28);
                                    return Ok(carss8);
                                    #endregion
                                    //return Ok("کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس پس ارسال کد");
                                }
                                else
                                {
                                    //session.Close();
                                    #region نمایش به صورت jason
                                    Massege massege7 = new Massege
                                    {
                                        Userid = "Servicer",
                                        TimeIs = "",
                                        Names = "",
                                        Counts = 0,
                                        Types = "خطای ارسال پیام"
                                    };
                                    var carss7 = massege7;
                                    //var json27 = JsonConvert.SerializeObject(new
                                    //{
                                    //    Error = carss7
                                    //});
                                    //JObject json217 = JObject.Parse(json27);
                                    return Ok(carss7);
                                    #endregion
                                    //return Ok(" خطای ارسال پیام");
                                }
                            }

                        }

                    }
                }

                else if (_ActivationForis_user != 0 && _UserIdForis_user != 0)
                {
                    if (_Activation == 0 || _Activation == 2)
                    {
                        //Int64 _End_AcId2 = 0;
                        //_End_AcId2 = session.QueryOver<TActivationCode>().Where(p => p.DeviceId == IsDevice_ID && p.Tell == IsPhoneRegistered).Select(x => x.AcId).SingleOrDefault<Int64>();
                        //if (_End_AcId2 == 0)
                        //{
                        //فقط ایجاد کد فعال سازی و ارسال آن
                        if (_IsDeleted == 0 || _Is_Ins == 1)
                        {
                            //ایجاد کد
                            var chars = "123456789";
                            var stringChars = new char[5];
                            var random = new Random();
                            for (int i = 0; i < stringChars.Length; i++)
                            {
                                stringChars[i] = chars[random.Next(chars.Length)];
                            }
                            var finalString = new String(stringChars);
                            //دریافت زمان سیستم
                            DateTime now = DateTime.Now;
                            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                            //////کد آپدیت
                            //////ISession session2 = OpenNHibertnateSession.OpenSession();
                            ////ITransaction tx = session.BeginTransaction();
                            ////string hqlVersionedUpdate = "update TActivationCode set Code = :Code2  , CodeGenerationTime=:CodeGenerationTime2 , EnterCount=:EnterCount2 where Tell = :Tells";
                            ////int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                            ////        .SetString("Code2", finalString)
                            ////        .SetString("Tells", IsPhoneRegistered)
                            ////        .SetParameter("CodeGenerationTime2", Time_Now)
                            ////        .SetParameter("EnterCount2", "0")
                            ////        //.SetDateTime()
                            ////        .ExecuteUpdate();
                            ////tx.Commit();
                            var emp = session.QueryOver<TActivationCode>().Where(p => p.Tell == IsPhoneRegistered).SingleOrDefault();
                            emp.Code = finalString;
                            emp.CodeGenerationTime = Time_Now;
                            emp.EnterCount = 0;
                            emp.TServicer = session.QueryOver<TServicer>().Where(p => p.PhoneAdmin == IsPhoneRegistered).SingleOrDefault();
                            using (ITransaction transaction = session.BeginTransaction())
                            {
                                session.Save(emp);
                                transaction.Commit();
                            }


                            ////ITransaction tx_user = session.BeginTransaction();
                            ////string hqlVersionedUpdate_user = "update TUser set IsDeleted = :IsDeleted2  where Tell = :Tells";
                            ////int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_user)
                            ////        .SetString("IsDeleted2", "0")
                            ////        .SetString("Tells", IsPhoneRegistered)
                            ////        //.SetDateTime()
                            ////        .ExecuteUpdate();
                            ////tx_user.Commit();

                            var emp2 = session.QueryOver<TServicer>().Where(p => p.PhoneAdmin == IsPhoneRegistered).SingleOrDefault();
                            emp2.IsDeleted = 0;
                            using (ITransaction transaction = session.BeginTransaction())
                            {
                                session.Save(emp2);
                                transaction.Commit();
                            }

                            session.Close();
                            //کد آپدیت
                            ///ارسال کد
                            #region اسال پیامک بدون تبلیغاتی ها
                            //var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                            //var messageSendObject = new MessageSendObject()
                            //{
                            //    Messages = new List<string> { "با سلام کد فعال سازی شما در پیک نان:" + finalString }.ToArray(),
                            //    MobileNumbers = new List<string> { IsPhoneRegistered }.ToArray(),
                            //    LineNumber = "30004747470516",
                            //    SendDateTime = null,
                            //    CanContinueInCaseOfError = true
                            //};

                            //MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

                            //if (messageSendResponseObject.IsSuccessful)
                            //{
                            //    session.Close();
                            //    return Ok("کاربر به تازگی ثبت نام کرده یا خارج شده پس ارسال کد");
                            //}
                            //else
                            //{
                            //    session.Close();
                            //    return Ok("خظای ارسال پیام");
                            //}

                            #endregion

                            var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                            var ultraFastSend = new UltraFastSend()
                            {
                                Mobile = Convert.ToInt64(IsPhoneRegistered),
                                TemplateId = 28733,
                                ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "VerificationCode" , ParameterValue = finalString
                                       }
                              }.ToArray()

                            };
                            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                            if (ultraFastSendRespone.IsSuccessful)
                            {
                                //session.Close();
                                #region نمایش به صورت jason
                                Massege massege7 = new Massege
                                {
                                    Userid = "Servicer",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس پس ارسال کد"
                                };
                                var carss7 = massege7;
                                //var json27 = JsonConvert.SerializeObject(new
                                //{
                                //    Error = carss7
                                //});
                                //JObject json217 = JObject.Parse(json27);
                                return Ok(carss7);
                                #endregion
                                //return Ok("کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس پس ارسال کد");
                            }
                            else
                            {
                                //session.Close();
                                #region نمایش به صورت jason
                                Massege massege6 = new Massege
                                {
                                    Userid = "Servicer",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "خطای ارسال پیام"
                                };
                                var carss6 = massege6;
                                //var json26 = JsonConvert.SerializeObject(new
                                //{
                                //    Error = carss6
                                //});
                                //JObject json216 = JObject.Parse(json26);
                                return Ok(carss6);
                                #endregion
                                //return Ok("خطای ارسال پیام");
                            }

                        }
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege61 = new Massege
                        {
                            Userid = "Servicer",
                            TimeIs = "",
                            Names = "",
                            Counts = 0,
                            Types = "دفعه 1"
                        };
                        var carss61 = massege61;
                        //var json261 = JsonConvert.SerializeObject(new
                        //{
                        //    Error = carss61
                        //});
                        //JObject json2161 = JObject.Parse(json261);
                        return Ok(carss61);
                        #endregion
                        //return Ok("دفعه 1");
                        //}
                        //else if (_Activation == 1 && _UserId2 == _UserId)
                        //{

                        //    session.Close();
                        //    return Ok("ورود به پنل نان");
                        //}

                    }
                    else if (_Activation == 1 && _PeykId2 == _PeykId)
                    {

                        //string _Name = "0";
                        _Name = session.QueryOver<TServicer>().Where(p => p.IdServicer == _ServicerId).Select(p => p.NameServicer).SingleOrDefault<string>();
                        if (_Name != "0")
                        {
                            session.Close();
                            //return Ok("ورد به پنل نان با نام");
                            #region نمایش به صورت jason
                            Massege massege6 = new Massege
                            {
                                Userid = "Servicer",
                                TimeIs = "",
                                Names = _Name,
                                Counts = 0,
                                Types = _ServicerId.ToString()
                            };
                            var carss6 = massege6;
                            //var json26 = JsonConvert.SerializeObject(new
                            //{
                            //    showFormserviser = carss6
                            //});
                            //JObject json216 = JObject.Parse(json26);
                            return Ok(carss6);
                            #endregion
                            //return Ok(_Name + "_UsId:" + _UserId);
                        }

                        if (_Name == "0")
                        {
                            session.Close();
                            //return Ok("ورد به پنل نان بدون نام");
                            #region نمایش به صورت jason
                            Massege massege15 = new Massege
                            {
                                Userid = "Servicer",
                                TimeIs = "",
                                Names = "",
                                Counts = 0,
                                Types = "Set Name for User"
                            };
                            var carss15 = massege15;
                            //var json2115 = JsonConvert.SerializeObject(new
                            //{
                            //    Error = carss15
                            //});
                            //JObject json21115 = JObject.Parse(json2115);
                            return Ok(carss15);
                            #endregion
                            //return Ok("Set Name for User");
                        }

                    }

                }
                session.Close();
                //return Ok("ورد به پنل نان بدون نام");
                #region نمایش به صورت jason
                Massege massege = new Massege
                {
                    Userid = "Servicer",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "error"
                };
                var carss = massege;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    Error = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss);
                #endregion
                //return Ok(("error"));

            }
            #endregion

            #region لاگین پیک
            if (_PeykId!=0)
            {
                if (_PeykId != 0) // یعنی کاربر قبلا ثبت نام کرده
                {

                    _UserIdForis_user = _PeykId;
                    //_ActivationForis_user = session.QueryOver<TActivationCode>().Where(p => p.TUser.UserId == _UserId && p.DeviceId == IsDevice_ID).Select(p => p.AcId).SingleOrDefault<long>();
                    _ActivationForis_user = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == _PeykId && p.DeviceIdLogin == IsDevice_ID).Select(p => p.Activation).SingleOrDefault<int>();//  این کاربر با کدام گوشی

                    _Activation = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == _PeykId).Select(p => p.Activation).SingleOrDefault<int>();
                    _IsDeleted = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == _PeykId).Select(p => p.IsDeleted).SingleOrDefault<int>();
                    if (_UserIdForis_user != 0 && _ActivationForis_user == 1 && _PeykId2 == _PeykId)
                    {

                        _Name = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == _PeykId).Select(p => p.NameFamily).SingleOrDefault<string>();
                        if (_Name != "0")
                        {
                            session.Close();
                            //return Ok("ورد به پنل نان با نام");
                            #region نمایش به صورت jason
                            Massege massege = new Massege
                            {
                                Userid = "Peyk",
                                TimeIs = "",
                                Names = _Name,
                                Counts = 0,
                                Types = "",


                            };
                            var carss = massege;
                            return Ok(carss);
                            #endregion
                        }

                       
                    }

                }
            }


             
         


           

            var _End_AcIdList = session.Query<TActivationCode>().Where(p => p.DeviceId == IsDevice_ID)
                 .Select(x => new { x.IdActivationCode, x.CodeGenerationTime })
                 .OrderByDescending(x => x.IdActivationCode).Take(1).ToList();
            if (_End_AcIdList.Count != 0)
            {
                _End_AcId = _End_AcIdList[0].IdActivationCode;
            }


            if (_End_AcId != 0)
            {
                _IS_time = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _End_AcId).Select(x => x.CodeGenerationTime).SingleOrDefault<DateTime>();
                DateTime _Is_Deleted_Time = _IS_time;
                _Is_Deleted_Time = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _End_AcId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                if (_Is_Deleted_Time != _IS_time)
                {
                    _LongTime_Khatakar = TimeAgo(_Is_Deleted_Time);
                }
                _LongTime = TimeAgo(_IS_time);
                if (_LongTime == "0" && _LongTime_Khatakar == "0")//کاربر می تواند درخواست کد یا ورود و یا ثبت نام کند
                {
                    _Is_Ins = 1;
                }
                else
                {
                    if (_IsDeleted == 0)///کاربردرست کار می باشد و تایم آن اعلام میشود
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege = new Massege
                        {
                            Userid = "Peyk",
                            TimeIs = _LongTime,
                            Names = "",
                            Counts = 0,
                            Types = "D"
                        };
                        var carss = massege;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    DoroskarT = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss);
                        #endregion
                        //return Ok("D:" + _LongTime);
                    }
                    else if (_IsDeleted == 1)///کاربر خطا کار می باشد و تایم آن اعلام میشود
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege = new Massege
                        {
                            Userid = "Peyk",
                            TimeIs = _LongTime_Khatakar,
                            Names = "",
                            Counts = 0,
                            Types = "K"
                        };
                        var carss = massege;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    DoroskarT = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss);
                        #endregion
                        //return Ok("K:" + _LongTime_Khatakar);
                    }

                }
                //return Ok(_IS_time);
            }
            else
            {
                _Is_Ins = 1;
            }


            if ((_PeykId != 0))
            {
                if (_ActivationForis_user == 0 || _UserIdForis_user == 0)
                {
                    if ((_Activation == 0 || _Activation == 2 || _Activation == 1))
                    {
                        if (_IsDeleted == 0 || _Is_Ins == 1)
                        {
                            if (is_splash == 1)
                            {
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege9 = new Massege
                                {
                                    Userid = "Peyk",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس و ورود به اکتیویتی شماره تلفن"
                                };
                                var carss9 = massege9;
                                //var json29 = JsonConvert.SerializeObject(new
                                //{
                                //    Error = carss9
                                //});
                                //JObject json219 = JObject.Parse(json29);
                                return Ok(carss9);
                                #endregion
                                //return Ok("کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس و ورود به اکتیویتی شماره تلفن");
                            }
                            else
                            {
                                //ایجاد کد
                                var chars = "123456789";
                                var stringChars = new char[5];
                                var random = new Random();
                                for (int i = 0; i < stringChars.Length; i++)
                                {
                                    stringChars[i] = chars[random.Next(chars.Length)];
                                }
                                var finalString = new String(stringChars);
                                //دریافت زمان سیستم
                                DateTime now = DateTime.Now;
                                DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                                //////کد آپدیت
                                ////ISession session2 = OpenNHibertnateSession.OpenSession();
                                //ITransaction tx = session.BeginTransaction();
                                //string hqlVersionedUpdate = "update TActivationCode set Code = :Code2 , DeviceId=:DeviceId2 , CodeGenerationTime=:CodeGenerationTime2 , EnterCount=:EnterCount2 where Tell = :Tells";
                                //int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                //        .SetString("Code2", finalString)
                                //        .SetString("Tells", IsPhoneRegistered)
                                //        .SetString("DeviceId2", IsDevice_ID)
                                //        .SetParameter("CodeGenerationTime2", Time_Now)
                                //        .SetParameter("EnterCount2", 0)
                                //        //.SetDateTime()
                                //        .ExecuteUpdate();
                                //tx.Commit();

                                //var emp = session.Get<TActivationCode>(IsPhoneRegistered);
                                var emp = session.QueryOver<TActivationCode>().Where(p => p.Tell == IsPhoneRegistered).Take(1).SingleOrDefault();
                                emp.Code = finalString;
                                emp.DeviceId = IsDevice_ID;
                                emp.CodeGenerationTime = Time_Now;
                                emp.EnterCount = 0;
                                emp.TPeyks = session.QueryOver<TPeyks>().Where(p => p.Phone == IsPhoneRegistered).SingleOrDefault();
                                using (ITransaction transaction = session.BeginTransaction())
                                {
                                    session.Save(emp);
                                    transaction.Commit();
                                }

                                ////ITransaction tx_user = session.BeginTransaction();
                                ////string hqlVersionedUpdate_user = "update TUser set IsDeleted = :IsDeleted2  where Tell = :Tells";
                                ////int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_user)
                                ////        .SetString("IsDeleted2", "0")
                                ////        .SetString("Tells", IsPhoneRegistered)
                                ////        //.SetDateTime()
                                ////        .ExecuteUpdate();
                                ////tx_user.Commit();

                                var emp2 = session.QueryOver<TPeyks>().Where(p => p.Phone == IsPhoneRegistered).SingleOrDefault();
                                emp2.IsDeleted = 0;
                                using (ITransaction transaction = session.BeginTransaction())
                                {
                                    session.Save(emp2);
                                    transaction.Commit();

                                }

                                session.Close();
                                //کد آپدیت
                                ///ارسال کد
                                #region اسال پیامک بدون تبلیغاتی ها
                                //var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                                //var messageSendObject = new MessageSendObject()
                                //{
                                //    Messages = new List<string> { "با سلام کد فعال سازی شما در پیک نان:" + finalString }.ToArray(),
                                //    MobileNumbers = new List<string> { IsPhoneRegistered }.ToArray(),
                                //    LineNumber = "30004747470516",
                                //    SendDateTime = null,
                                //    CanContinueInCaseOfError = true
                                //};

                                //MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

                                //if (messageSendResponseObject.IsSuccessful)
                                //{
                                //    session.Close();
                                //    return Ok("کاربر به تازگی ثبت نام کرده یا خارج شده پس ارسال کد");
                                //}
                                //else
                                //{
                                //    session.Close();
                                //    return Ok("خظای ارسال پیام");
                                //}

                                #endregion

                                var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                                var ultraFastSend = new UltraFastSend()
                                {
                                    Mobile = Convert.ToInt64(IsPhoneRegistered),
                                    TemplateId = 28733,
                                    ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "VerificationCode" , ParameterValue = finalString
                                       }
                              }.ToArray()

                                };
                                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                                if (ultraFastSendRespone.IsSuccessful)
                                {
                                    //session.Close();
                                    #region نمایش به صورت jason
                                    Massege massege8 = new Massege
                                    {
                                        Userid = "Peyk",
                                        TimeIs = "",
                                        Names = "",
                                        Counts = 0,
                                        Types = "کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس پس ارسال کد"
                                    };
                                    var carss8 = massege8;
                                    //var json28 = JsonConvert.SerializeObject(new
                                    //{
                                    //    Error = carss8
                                    //});
                                    //JObject json218 = JObject.Parse(json28);
                                    return Ok(carss8);
                                    #endregion
                                    //return Ok("کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس پس ارسال کد");
                                }
                                else
                                {
                                    //session.Close();
                                    #region نمایش به صورت jason
                                    Massege massege7 = new Massege
                                    {
                                        Userid = "Peyk",
                                        TimeIs = "",
                                        Names = "",
                                        Counts = 0,
                                        Types = "خطای ارسال پیام"
                                    };
                                    var carss7 = massege7;
                                    //var json27 = JsonConvert.SerializeObject(new
                                    //{
                                    //    Error = carss7
                                    //});
                                    //JObject json217 = JObject.Parse(json27);
                                    return Ok(carss7);
                                    #endregion
                                    //return Ok(" خطای ارسال پیام");
                                }
                            }

                        }

                    }
                }

                else if (_ActivationForis_user != 0 && _UserIdForis_user != 0)
                {
                    if (_Activation == 0 || _Activation == 2)
                    {
                        //Int64 _End_AcId2 = 0;
                        //_End_AcId2 = session.QueryOver<TActivationCode>().Where(p => p.DeviceId == IsDevice_ID && p.Tell == IsPhoneRegistered).Select(x => x.AcId).SingleOrDefault<Int64>();
                        //if (_End_AcId2 == 0)
                        //{
                        //فقط ایجاد کد فعال سازی و ارسال آن
                        if (_IsDeleted == 0 || _Is_Ins == 1)
                        {
                            //ایجاد کد
                            var chars = "123456789";
                            var stringChars = new char[5];
                            var random = new Random();
                            for (int i = 0; i < stringChars.Length; i++)
                            {
                                stringChars[i] = chars[random.Next(chars.Length)];
                            }
                            var finalString = new String(stringChars);
                            //دریافت زمان سیستم
                            DateTime now = DateTime.Now;
                            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                            //////کد آپدیت
                            //////ISession session2 = OpenNHibertnateSession.OpenSession();
                            ////ITransaction tx = session.BeginTransaction();
                            ////string hqlVersionedUpdate = "update TActivationCode set Code = :Code2  , CodeGenerationTime=:CodeGenerationTime2 , EnterCount=:EnterCount2 where Tell = :Tells";
                            ////int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                            ////        .SetString("Code2", finalString)
                            ////        .SetString("Tells", IsPhoneRegistered)
                            ////        .SetParameter("CodeGenerationTime2", Time_Now)
                            ////        .SetParameter("EnterCount2", "0")
                            ////        //.SetDateTime()
                            ////        .ExecuteUpdate();
                            ////tx.Commit();
                            var emp = session.QueryOver<TActivationCode>().Where(p => p.Tell == IsPhoneRegistered).SingleOrDefault();
                            emp.Code = finalString;
                            emp.CodeGenerationTime = Time_Now;
                            emp.EnterCount = 0;
                            emp.TPeyks = session.QueryOver<TPeyks>().Where(p => p.Phone == IsPhoneRegistered).SingleOrDefault();
                            using (ITransaction transaction = session.BeginTransaction())
                            {
                                session.Save(emp);
                                transaction.Commit();
                            }


                            ////ITransaction tx_user = session.BeginTransaction();
                            ////string hqlVersionedUpdate_user = "update TUser set IsDeleted = :IsDeleted2  where Tell = :Tells";
                            ////int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_user)
                            ////        .SetString("IsDeleted2", "0")
                            ////        .SetString("Tells", IsPhoneRegistered)
                            ////        //.SetDateTime()
                            ////        .ExecuteUpdate();
                            ////tx_user.Commit();

                            var emp2 = session.QueryOver<TPeyks>().Where(p => p.Phone == IsPhoneRegistered).SingleOrDefault();
                            emp2.IsDeleted = 0;
                            using (ITransaction transaction = session.BeginTransaction())
                            {
                                session.Save(emp2);
                                transaction.Commit();
                            }

                            session.Close();
                            //کد آپدیت
                            ///ارسال کد
                            #region اسال پیامک بدون تبلیغاتی ها
                            //var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                            //var messageSendObject = new MessageSendObject()
                            //{
                            //    Messages = new List<string> { "با سلام کد فعال سازی شما در پیک نان:" + finalString }.ToArray(),
                            //    MobileNumbers = new List<string> { IsPhoneRegistered }.ToArray(),
                            //    LineNumber = "30004747470516",
                            //    SendDateTime = null,
                            //    CanContinueInCaseOfError = true
                            //};

                            //MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

                            //if (messageSendResponseObject.IsSuccessful)
                            //{
                            //    session.Close();
                            //    return Ok("کاربر به تازگی ثبت نام کرده یا خارج شده پس ارسال کد");
                            //}
                            //else
                            //{
                            //    session.Close();
                            //    return Ok("خظای ارسال پیام");
                            //}

                            #endregion

                            var token = new Token().GetToken("bd4d1e53a1d0beb7a2df1e53", "Pelatform990409");

                            var ultraFastSend = new UltraFastSend()
                            {
                                Mobile = Convert.ToInt64(IsPhoneRegistered),
                                TemplateId = 28733,
                                ParameterArray = new List<UltraFastParameters>()
                           {
                                    new UltraFastParameters()
                                       {
                                             Parameter = "VerificationCode" , ParameterValue = finalString
                                       }
                              }.ToArray()

                            };
                            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

                            if (ultraFastSendRespone.IsSuccessful)
                            {
                                //session.Close();
                                #region نمایش به صورت jason
                                Massege massege7 = new Massege
                                {
                                    Userid = "Peyk",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس پس ارسال کد"
                                };
                                var carss7 = massege7;
                                //var json27 = JsonConvert.SerializeObject(new
                                //{
                                //    Error = carss7
                                //});
                                //JObject json217 = JObject.Parse(json27);
                                return Ok(carss7);
                                #endregion
                                //return Ok("کاربر وارد شده ولی خروج نکرده ولی دستگاه ناشناس پس ارسال کد");
                            }
                            else
                            {
                                //session.Close();
                                #region نمایش به صورت jason
                                Massege massege6 = new Massege
                                {
                                    Userid = "Peyk",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "خطای ارسال پیام"
                                };
                                var carss6 = massege6;
                                //var json26 = JsonConvert.SerializeObject(new
                                //{
                                //    Error = carss6
                                //});
                                //JObject json216 = JObject.Parse(json26);
                                return Ok(carss6);
                                #endregion
                                //return Ok("خطای ارسال پیام");
                            }

                        }
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege61 = new Massege
                        {
                            Userid = "Peyk",
                            TimeIs = "",
                            Names = "",
                            Counts = 0,
                            Types = "دفعه 1"
                        };
                        var carss61 = massege61;
                        //var json261 = JsonConvert.SerializeObject(new
                        //{
                        //    Error = carss61
                        //});
                        //JObject json2161 = JObject.Parse(json261);
                        return Ok(carss61);
                        #endregion
                        //return Ok("دفعه 1");
                        //}
                        //else if (_Activation == 1 && _UserId2 == _UserId)
                        //{

                        //    session.Close();
                        //    return Ok("ورود به پنل نان");
                        //}

                    }
                    else if (_Activation == 1 && _PeykId2 == _PeykId)
                    {

                        //string _Name = "0";
                        _Name = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == _PeykId).Select(p => p.NameFamily).SingleOrDefault<string>();
                        if (_Name != "0")
                        {
                            session.Close();
                            //return Ok("ورد به پنل نان با نام");
                            #region نمایش به صورت jason
                            Massege massege6 = new Massege
                            {
                                Userid = "Peyk",
                                TimeIs = "",
                                Names = _Name,
                                Counts = 0,
                                Types = ""
                            };
                            var carss6 = massege6;
                            //var json26 = JsonConvert.SerializeObject(new
                            //{
                            //    showFormserviser = carss6
                            //});
                            //JObject json216 = JObject.Parse(json26);
                            return Ok(carss6);
                            #endregion
                            //return Ok(_Name + "_UsId:" + _UserId);
                        }

                        if (_Name == "0")
                        {
                            session.Close();
                            //return Ok("ورد به پنل نان بدون نام");
                            #region نمایش به صورت jason
                            Massege massege15 = new Massege
                            {
                                Userid = "Peyk",
                                TimeIs = "",
                                Names = "",
                                Counts = 0,
                                Types = "Set Name for User"
                            };
                            var carss15 = massege15;
                            //var json2115 = JsonConvert.SerializeObject(new
                            //{
                            //    Error = carss15
                            //});
                            //JObject json21115 = JObject.Parse(json2115);
                            return Ok(carss15);
                            #endregion
                            //return Ok("Set Name for User");
                        }

                    }

                }
                session.Close();
                //return Ok("ورد به پنل نان بدون نام");
                #region نمایش به صورت jason
                Massege massege = new Massege
                {
                    Userid = "Peyk",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "error"
                };
                var carss = massege;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    Error = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss);
                #endregion
                //return Ok(("error"));

            }
            #endregion

            

            return Ok("Eroore");
        }

        [HttpGet]
        [Route("api/G1/GetCheckCodeServicer/{Unique_code},{Phone},{Unique_device_code},{code},{None_User}")]
        public IHttpActionResult GetCheckCodeServicer(string Unique_code, string Phone, string Unique_device_code, string code,string None_User)
        {
            if (Phone == "{Phone}" || Unique_device_code == "{Unique_device_code}" || code == "{code}")
            {
                session.Close();
                return Ok("مقادیر خالی");
            }
            if (Unique_code != "PlatformV199325694")
            {
                #region نمایش به صورت jason
                Massege massege1 = new Massege
                {
                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "کلید نادرست"
                };
                var carss1 = massege1;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    Error = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss1);
                #endregion
                //return Ok("کلید نادرست");
            }
            string IsCode = code;
            if (IsCode == "0")
            {
                session.Close();
                #region نمایش به صورت jason
                Massege massege1 = new Massege
                {
                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "مقدار کد وارد شده غیر مجاز می باشد"
                };
                var carss1 = massege1;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    Error = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss1);
                #endregion
                //return Ok("مقدار کد وارد شده غیر مجاز می باشد");
            }
            string IsPhoneRegistered = Phone;
            string IsDevice_ID = Unique_device_code;
            string _IsDevice_ID = "0";
            string _IsCode = "null";
            long _UserId = 0;
            DateTime _IS_time;
            DateTime _IS_time_KH;
            long _TActivationCodeId = 0;
            int _Activation = 00;
            int _EnterCount = 0;
            string _Kh_LongTime = "0";
            int _IsDeleted = 0;//اگر مساوی 0 باشد یعنی کاربر کدی درخواست نداده

            long _Peyk_ID = 0;
            Int32 _Servicer_ID = 0;

            #region اگر کاربر پیک بود
            if (None_User == "Peyk")
            {
                _Peyk_ID = session.QueryOver<TPeyks>().Where(p => p.Phone == IsPhoneRegistered).Select(p => p.IdPeyks).SingleOrDefault<long>();
                _TActivationCodeId = session.QueryOver<TActivationCode>().Where(p => p.TPeyks.IdPeyks == _Peyk_ID ).Select(p => p.IdActivationCode).SingleOrDefault<long>();
                _IsDevice_ID = session.QueryOver<TActivationCode>().Where(p => p.TPeyks.IdPeyks == _Peyk_ID ).Select(p => p.DeviceId).SingleOrDefault<string>();

                if (_IsDevice_ID != IsDevice_ID)
                {
                    session.Close();
                    #region نمایش به صورت jason
                    Massege massege1 = new Massege
                    {
                        Userid = "",
                        TimeIs = "",
                        Names = "",
                        Counts = 0,
                        Types = "کد یکتای دستگاه ناشناس می باشد"
                    };
                    var carss1 = massege1;
                    //var json2 = JsonConvert.SerializeObject(new
                    //{
                    //    Error = carss
                    //});
                    //JObject json21 = JObject.Parse(json2);
                    return Ok(carss1);
                    #endregion
                    //return Ok("کد یکتای دستگاه ناشناس می باشد");
                }
                else if (_Peyk_ID != 0 && _IsDevice_ID == IsDevice_ID)
                {
                    //_Activation = session.QueryOver<TUser>().Where(p => p.UserId == _UserId).Select(p => p.Activation).SingleOrDefault<int>();
                    _IsDeleted = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == _Peyk_ID).Select(p => p.IsDeleted).SingleOrDefault<int>();
                    _IsCode = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(p => p.Code).SingleOrDefault<string>();
                    _IS_time = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.CodeGenerationTime).SingleOrDefault<DateTime>();
                    _EnterCount = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.EnterCount).SingleOrDefault<int>();

                    _IS_time_KH = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                    _Kh_LongTime = TimeAgo(_IS_time_KH);
                    _LongTime = TimeAgo_code(_IS_time);
                    string _D_LongTime = TimeAgo(_IS_time);


                    if (_LongTime == "0")
                    {
                        #region کد نامعتبر بیش از 10 دقیقه از صدور کد گذشته - مجدد سعی شود
                        //return Ok("کد نامعتبر بیش از 10 دقیقه از صدور کد گذشته - مجدد سعی شود");
                        if (_LongTime == "0" && _IsDeleted == 0 && _IsCode != IsCode)
                        {
                            if (_EnterCount < 5)
                            {
                                int M = 4 - _EnterCount;
                                string S = Convert.ToString(_EnterCount + 1);
                                string _TActivationCodeId2 = Convert.ToString(_TActivationCodeId);
                                ITransaction tx = session.BeginTransaction();
                                string hqlVersionedUpdate = "update TActivationCode set EnterCount = :EnterCount2  where ID_Activation_Code = :AcIds";
                                int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                        .SetString("EnterCount2", S)
                                        .SetString("AcIds", _TActivationCodeId2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx.Commit();
                                session.Close();
                                //if (_D_LongTime == "0" && _IsDeleted == 0)
                                //{
                                //    return Ok("کار بر درست کار می تواند کد درخواست دهد ");
                                //}
                                //یعنی کد نامعتبر می باشد و تعداد برای اینکه اگه هک شدیم قفلش کنه
                                Massege massege8 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = M,
                                    Types = "RE"
                                };
                                var carss8 = massege8;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    CountRe = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss8);
                                //return Ok("RE:" + M);
                            }
                        }


                        ///////////////
                        if (_EnterCount >= 4 || _IsDeleted == 1)
                        {
                            //if (_Kh_LongTime!="0" || _IsDeleted==1)
                            //{
                            if (_EnterCount == 5)
                            {
                                string _TActivationCodeId2 = Convert.ToString(_TActivationCodeId);
                                ITransaction tx = session.BeginTransaction();
                                DateTime now = DateTime.Now;
                                DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                                string hqlVersionedUpdate = "update TActivationCode set EnterCount =: EnterCount2, IsDeletedTime=:IsDeletedTime2  where ID_Activation_Code = :AcIds";
                                int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                        .SetString("EnterCount2", "6")
                                        .SetString("AcIds", _TActivationCodeId2)
                                        .SetParameter("IsDeletedTime2", Time_Now)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx.Commit();

                                _IS_time_KH = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                                _Kh_LongTime = TimeAgo(_IS_time_KH);

                                string _PeykId2 = Convert.ToString(_Peyk_ID);
                                ITransaction tx_IsDeleted = session.BeginTransaction();
                                string hqlVersionedUpdate_IsDeleted = "update TPeyks set IsDeleted = :IsDeleted2  where IdPeyks = :PeykIds";
                                int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                        .SetString("IsDeleted2", "1")
                                        .SetString("PeykIds", _PeykId2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx_IsDeleted.Commit();
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege9 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = _Kh_LongTime,
                                    Names = "",
                                    Counts = 0,
                                    Types = "KE"
                                };
                                var carss9 = massege9;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    khatakar = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss9);
                                #endregion

                                //return Ok("KE:" + _Kh_LongTime);
                            }

                            //if (_Kh_LongTime == "0" && _IsDeleted == 1)
                            //{
                            //    return Ok("کار بر خطا کار می تواند کد درخواست دهد ");
                            //}
                            //return Ok("کار بر خطا کار می باشد " + _LongTime + "زمان باقی مانده");
                            #region نمایش به صورت jason
                            Massege massege7 = new Massege
                            {
                                Userid = "",
                                TimeIs = _Kh_LongTime,
                                Names = "",
                                Counts = 0,
                                Types = "K"
                            };
                            var carss7 = massege7;
                            //var json2 = JsonConvert.SerializeObject(new
                            //{
                            //    khatakar = carss
                            //});
                            //JObject json21 = JObject.Parse(json2);
                            return Ok(carss7);
                            #endregion
                            //return Ok("K:" + _Kh_LongTime);
                        }
                    }
                    #endregion
                    if (IsCode == _IsCode && IsCode != "0")
                    {
                        if (_LongTime != "0")
                        {
                            //کد آپدیت
                            //ISession session2 = OpenNHibertnateSession.OpenSession();
                            ITransaction tx = session.BeginTransaction();
                            string hqlVersionedUpdate = "update TActivationCode set Code = :Code2  , EnterCount=:EnterCount2 where ID_Activation_Code = :ID";
                            int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                    .SetString("Code2", "0")
                                    .SetString("ID", Convert.ToString(_TActivationCodeId))
                                    .SetParameter("EnterCount2", "0")
                                    //.SetDateTime()
                                    .ExecuteUpdate();
                            tx.Commit();

                            ITransaction tx_user = session.BeginTransaction();
                            string hqlVersionedUpdate_user = "update TPeyks set IsDeleted = :IsDeleted2 ,Device_ID_login=:Device_ID_login2 , Activation=:Activation2 where IdPeyks = :ID";
                            int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_user)
                                    .SetString("IsDeleted2", "0")
                                    .SetString("Activation2", "1")
                                    .SetString("Device_ID_login2", IsDevice_ID)
                                    .SetString("ID", Convert.ToString(_Peyk_ID))
                                    //.SetDateTime()
                                    .ExecuteUpdate();
                            tx_user.Commit();
                            //session.Close();
                            //کد آپدیت
                            string UsId = Convert.ToString(_Peyk_ID);

                            string _Name_Family = "0";
                            _Name_Family = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == _Peyk_ID).Select(p => p.NameFamily).SingleOrDefault<string>();
                            if (_Name_Family == "0")
                            {
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege6 = new Massege
                                {

                                    Userid = UsId,
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "NO_Name",
                                };
                                var carss6 = massege6;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    No_Name = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss6);
                                #endregion
                                //return Ok("NO_Name:" + UsId);
                            }
                            if (_Name_Family != "0")
                            {
                                session.Close();
                                ///کد صحیح است و ادامه مراحل و گرفتن نام و نام خانوادگی
                                #region نمایش به صورت jason
                                Massege massege5 = new Massege
                                {
                                    Userid = UsId,
                                    TimeIs = "",
                                    Names = _Name_Family,
                                    Counts = 0,
                                    Types = "",
                                };
                                var carss5 = massege5;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    OKName = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss5);
                                #endregion
                                //return Ok(_Name_Family + "_UsId:" + UsId);
                            }

                        }
                        else
                        {
                            if (_IsDeleted == 0)
                            {
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege51 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کد منقضی شده است کاربر می توان درخواست کد بدهد"
                                };
                                var carss51 = massege51;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    OKName = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss51);
                                #endregion

                                //return Ok(_LongTime);
                            }
                            if (_IsDeleted != 0)
                            {
                                string _TPeyksId2 = Convert.ToString(_Peyk_ID);
                                ITransaction tx_IsDeleted = session.BeginTransaction();
                                string hqlVersionedUpdate_IsDeleted = "update TPeyks set IsDeleted = :IsDeleted2  where IdPeyks = :PeykIds";
                                int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                        .SetString("IsDeleted2", "0")
                                        .SetString("PeykIds", _TPeyksId2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx_IsDeleted.Commit();
                                session.Close();

                                #region نمایش به صورت jason
                                Massege massege4 = new Massege
                                {

                                    Userid = "",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کاربر می تواند دوباره درخواست کد دهد",
                                };
                                var carss4 = massege4;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    No_Name = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss4);
                                #endregion
                                //return Ok("کاربر می تواند دوباره درخواست کد دهد");
                                //کد آپدیت
                            }

                        }

                    }

                    else if (IsCode != _IsCode)
                    {
                        #region کد ورودی نامعتبر می باشد
                        if (_LongTime != "0" && _IsDeleted == 0)
                        {
                            if (_EnterCount < 5)///می تواند تا 5 بار کد را وارد کند سپس کاربر قفل میشود برای 5 دقیقه
                            {
                                int M = 5 - _EnterCount;
                                string S = Convert.ToString(_EnterCount + 1);
                                string _TActivationCodeId2 = Convert.ToString(_TActivationCodeId);
                                ITransaction tx = session.BeginTransaction();
                                string hqlVersionedUpdate = "update TActivationCode set EnterCount = :EnterCount2  where ID_Activation_Code = :AcIds";
                                int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                        .SetString("EnterCount2", S)
                                        .SetString("AcIds", _TActivationCodeId2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx.Commit();
                                session.Close();
                                //if (_D_LongTime == "0" && _IsDeleted == 0)
                                //{
                                //    return Ok("کار بر درست کار می تواند کد درخواست دهد ");
                                //}
                                //return Ok("کد اشتباه شما می توانید"+ M +"بار دیگر اقدام نمایید");
                                #region نمایش به صورت jason
                                Massege massege3 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = _LongTime,
                                    Names = "",
                                    Counts = M,
                                    Types = "R"
                                };
                                var carss3 = massege3;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    TimeRM = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss3);
                                #endregion
                                //return Ok("R:" + M + "T:" + _LongTime);
                            }
                        }


                        ///////////////
                        if (_EnterCount >= 4 || _IsDeleted == 1)
                        {
                            if (_EnterCount == 5)
                            {
                                string _TActivationCodeId2 = Convert.ToString(_TActivationCodeId);
                                ITransaction tx = session.BeginTransaction();
                                DateTime now = DateTime.Now;
                                DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                                string hqlVersionedUpdate = "update TActivationCode set EnterCount =: EnterCount2, IsDeletedTime=:IsDeletedTime2  where ID_Activation_Code = :AcIds";
                                int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                        .SetString("EnterCount2", "6")
                                        .SetString("AcIds", _TActivationCodeId2)
                                        .SetParameter("IsDeletedTime2", Time_Now)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx.Commit();

                                _IS_time_KH = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                                _Kh_LongTime = TimeAgo(_IS_time_KH);

                                string _PeykId2 = Convert.ToString(_Peyk_ID);
                                ITransaction tx_IsDeleted = session.BeginTransaction();
                                string hqlVersionedUpdate_IsDeleted = "update TPeyks set IsDeleted = :IsDeleted2  where IdPeyks = :PeykIds";
                                int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                        .SetString("IsDeleted2", "1")
                                        .SetString("PeykIds", _PeykId2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx_IsDeleted.Commit();
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege22 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = _Kh_LongTime,
                                    Names = "",
                                    Counts = 0,
                                    Types = "K"
                                };
                                var carss22 = massege22;
                                //var json22 = JsonConvert.SerializeObject(new
                                //{
                                //    KatakarTime = carss2
                                //});
                                //JObject json212 = JObject.Parse(json22);
                                return Ok(carss22);
                                #endregion
                                //return Ok("K:" + _Kh_LongTime);
                            }

                            //if (_Kh_LongTime == "0" && _IsDeleted == 1)
                            //{
                            //    return Ok("کار بر خطا کار می تواند کد درخواست دهد ");
                            //}
                            //return Ok("کار بر خطا کار می باشد " + _LongTime + "زمان باقی مانده");
                            #region نمایش به صورت jason
                            Massege massege2 = new Massege
                            {
                                Userid = "",
                                TimeIs = _Kh_LongTime,
                                Names = "",
                                Counts = 0,
                                Types = "K"
                            };
                            var carss2 = massege2;
                            //var json2 = JsonConvert.SerializeObject(new
                            //{
                            //    KatakarTime = carss
                            //});
                            //JObject json21 = JObject.Parse(json2);
                            return Ok(carss2);
                            #endregion
                            //return Ok("K:" + _Kh_LongTime);
                        }
                    }
                    #endregion
                }

                else if (_Peyk_ID == 0)
                {
                    #region نمایش به صورت jason
                    Massege massege1 = new Massege
                    {

                        Userid = "",
                        TimeIs = "",
                        Names = "",
                        Counts = 0,
                        Types = "کاربر یافت نشد",
                    };
                    var carss1 = massege1;
                    //var json2 = JsonConvert.SerializeObject(new
                    //{
                    //    No_Name = carss
                    //});
                    //JObject json21 = JObject.Parse(json2);
                    return Ok(carss1);
                    #endregion
                    //return Ok("کاربر یافت نشد");

                }
                session.Close();
                #region نمایش به صورت jason
                Massege massege = new Massege
                {

                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "مشکل دوباره ارسال شود"
                };
                var carss = massege;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    No_Name = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss);
                #endregion
            }
            #endregion

            #region اگر کاربر سرویس دهنده بود 
            if (None_User == "Servicer")
            {
                _Servicer_ID = session.QueryOver<TServicer>().Where(p => p.PhoneAdmin == IsPhoneRegistered).Select(p => p.IdServicer).SingleOrDefault<int>();
                _TActivationCodeId = session.QueryOver<TActivationCode>().Where(p => p.TServicer.IdServicer == _Servicer_ID && p.DeviceId == IsDevice_ID && p.Tell == IsPhoneRegistered).Select(p => p.IdActivationCode).SingleOrDefault<long>();
                _IsDevice_ID = session.QueryOver<TActivationCode>().Where(p => p.TServicer.IdServicer == _Servicer_ID && p.DeviceId == IsDevice_ID && p.Tell == IsPhoneRegistered).Select(p => p.DeviceId).SingleOrDefault<string>();

                if (_IsDevice_ID != IsDevice_ID)
                {
                    session.Close();
                    #region نمایش به صورت jason
                    Massege massege1 = new Massege
                    {
                        Userid = "",
                        TimeIs = "",
                        Names = "",
                        Counts = 0,
                        Types = "کد یکتای دستگاه ناشناس می باشد"
                    };
                    var carss1 = massege1;
                    //var json2 = JsonConvert.SerializeObject(new
                    //{
                    //    Error = carss
                    //});
                    //JObject json21 = JObject.Parse(json2);
                    return Ok(carss1);
                    #endregion
                    //return Ok("کد یکتای دستگاه ناشناس می باشد");
                }
                else if (_Servicer_ID != 0 && _IsDevice_ID == IsDevice_ID)
                {
                    _IsDeleted = session.QueryOver<TServicer>().Where(p => p.IdServicer == _Servicer_ID).Select(p => p.IsDeleted).SingleOrDefault<int>();
                    _IsCode = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(p => p.Code).SingleOrDefault<string>();
                    _IS_time = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.CodeGenerationTime).SingleOrDefault<DateTime>();
                    _EnterCount = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.EnterCount).SingleOrDefault<int>();

                    _IS_time_KH = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                    _Kh_LongTime = TimeAgo(_IS_time_KH);
                    _LongTime = TimeAgo_code(_IS_time);
                    string _D_LongTime = TimeAgo(_IS_time);


                    if (_LongTime == "0")
                    {
                        #region کد نامعتبر بیش از 10 دقیقه از صدور کد گذشته - مجدد سعی شود
                        //return Ok("کد نامعتبر بیش از 10 دقیقه از صدور کد گذشته - مجدد سعی شود");
                        if (_LongTime == "0" && _IsDeleted == 0 && _IsCode != IsCode)
                        {
                            if (_EnterCount < 5)
                            {
                                int M = 4 - _EnterCount;
                                string S = Convert.ToString(_EnterCount + 1);
                                string _TActivationCodeId2 = Convert.ToString(_TActivationCodeId);
                                ITransaction tx = session.BeginTransaction();
                                string hqlVersionedUpdate = "update TActivationCode set EnterCount = :EnterCount2  where ID_Activation_Code = :AcIds";
                                int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                        .SetString("EnterCount2", S)
                                        .SetString("AcIds", _TActivationCodeId2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx.Commit();
                                session.Close();
                                //if (_D_LongTime == "0" && _IsDeleted == 0)
                                //{
                                //    return Ok("کار بر درست کار می تواند کد درخواست دهد ");
                                //}
                                //یعنی کد نامعتبر می باشد و تعداد برای اینکه اگه هک شدیم قفلش کنه
                                Massege massege8 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = M,
                                    Types = "RE"
                                };
                                var carss8 = massege8;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    CountRe = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss8);
                                //return Ok("RE:" + M);
                            }
                        }


                        ///////////////
                        if (_EnterCount >= 4 || _IsDeleted == 1)
                        {
                            //if (_Kh_LongTime!="0" || _IsDeleted==1)
                            //{
                            if (_EnterCount == 5)
                            {
                                string _TActivationCodeId2 = Convert.ToString(_TActivationCodeId);
                                ITransaction tx = session.BeginTransaction();
                                DateTime now = DateTime.Now;
                                DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                                string hqlVersionedUpdate = "update TActivationCode set EnterCount =: EnterCount2, IsDeletedTime=:IsDeletedTime2  where ID_Activation_Code = :AcIds";
                                int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                        .SetString("EnterCount2", "6")
                                        .SetString("AcIds", _TActivationCodeId2)
                                        .SetParameter("IsDeletedTime2", Time_Now)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx.Commit();

                                _IS_time_KH = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                                _Kh_LongTime = TimeAgo(_IS_time_KH);

                                string _Servicer_ID2 = Convert.ToString(_Servicer_ID);
                                ITransaction tx_IsDeleted = session.BeginTransaction();
                                string hqlVersionedUpdate_IsDeleted = "update TServicer set IsDeleted = :IsDeleted2  where IdServicer = :ServicerIds";
                                int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                        .SetString("IsDeleted2", "1")
                                        .SetString("ServicerIds", _Servicer_ID2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx_IsDeleted.Commit();
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege9 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = _Kh_LongTime,
                                    Names = "",
                                    Counts = 0,
                                    Types = "KE"
                                };
                                var carss9 = massege9;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    khatakar = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss9);
                                #endregion

                                //return Ok("KE:" + _Kh_LongTime);
                            }

                            //if (_Kh_LongTime == "0" && _IsDeleted == 1)
                            //{
                            //    return Ok("کار بر خطا کار می تواند کد درخواست دهد ");
                            //}
                            //return Ok("کار بر خطا کار می باشد " + _LongTime + "زمان باقی مانده");
                            #region نمایش به صورت jason
                            Massege massege7 = new Massege
                            {
                                Userid = "",
                                TimeIs = _Kh_LongTime,
                                Names = "",
                                Counts = 0,
                                Types = "K"
                            };
                            var carss7 = massege7;
                            //var json2 = JsonConvert.SerializeObject(new
                            //{
                            //    khatakar = carss
                            //});
                            //JObject json21 = JObject.Parse(json2);
                            return Ok(carss7);
                            #endregion
                            //return Ok("K:" + _Kh_LongTime);
                        }
                    }
                    #endregion
                    if (IsCode == _IsCode && IsCode != "0")
                    {
                        if (_LongTime != "0")
                        {
                            //کد آپدیت
                            //ISession session2 = OpenNHibertnateSession.OpenSession();
                            ITransaction tx = session.BeginTransaction();
                            string hqlVersionedUpdate = "update TActivationCode set Code = :Code2  , EnterCount=:EnterCount2 where ID_Activation_Code = :ID";
                            int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                    .SetString("Code2", "0")
                                    .SetString("ID", Convert.ToString(_TActivationCodeId))
                                    .SetParameter("EnterCount2", "0")
                                    //.SetDateTime()
                                    .ExecuteUpdate();
                            tx.Commit();

                            ITransaction tx_user = session.BeginTransaction();
                            string hqlVersionedUpdate_user = "update TServicer set IsDeleted = :IsDeleted2 ,Device_ID_login=:Device_ID_login2 , Activation=:Activation2 where IdServicer = :ID";
                            int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_user)
                                    .SetString("IsDeleted2", "0")
                                    .SetString("Activation2", "1")
                                    .SetString("Device_ID_login2", IsDevice_ID)
                                    .SetString("ID", Convert.ToString(_Servicer_ID))
                                    //.SetDateTime()
                                    .ExecuteUpdate();
                            tx_user.Commit();
                            //session.Close();
                            //کد آپدیت
                            string UsId = Convert.ToString(_Servicer_ID);

                            string _Name_Family = "0";
                            _Name_Family = session.QueryOver<TServicer>().Where(p => p.IdServicer == _Servicer_ID).Select(p => p.NameServicer).SingleOrDefault<string>();
                            if (_Name_Family == "0")
                            {
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege6 = new Massege
                                {

                                    Userid = UsId,
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "NO_Name",
                                };
                                var carss6 = massege6;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    No_Name = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss6);
                                #endregion
                                //return Ok("NO_Name:" + UsId);
                            }
                            if (_Name_Family != "0")
                            {
                                session.Close();
                                ///کد صحیح است و ادامه مراحل و گرفتن نام و نام خانوادگی
                                #region نمایش به صورت jason
                                Massege massege5 = new Massege
                                {
                                    Userid = UsId,
                                    TimeIs = "",
                                    Names = _Name_Family,
                                    Counts = 0,
                                    Types = "",
                                };
                                var carss5 = massege5;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    OKName = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss5);
                                #endregion
                                //return Ok(_Name_Family + "_UsId:" + UsId);
                            }

                        }
                        else
                        {
                            if (_IsDeleted == 0)
                            {
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege51 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کد منقضی شده است کاربر می توان درخواست کد بدهد"
                                };
                                var carss51 = massege51;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    OKName = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss51);
                                #endregion

                                //return Ok(_LongTime);
                            }
                            if (_IsDeleted != 0)
                            {
                                string _TServicerId2 = Convert.ToString(_Servicer_ID);
                                ITransaction tx_IsDeleted = session.BeginTransaction();
                                string hqlVersionedUpdate_IsDeleted = "update TServicer set IsDeleted = :IsDeleted2  where IdServicer = :ServicerIds";
                                int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                        .SetString("IsDeleted2", "0")
                                        .SetString("ServicerIds", _TServicerId2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx_IsDeleted.Commit();
                                session.Close();

                                #region نمایش به صورت jason
                                Massege massege4 = new Massege
                                {

                                    Userid = "",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کاربر می تواند دوباره درخواست کد دهد",
                                };
                                var carss4 = massege4;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    No_Name = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss4);
                                #endregion
                                //return Ok("کاربر می تواند دوباره درخواست کد دهد");
                                //کد آپدیت
                            }

                        }

                    }

                    else if (IsCode != _IsCode)
                    {
                        #region کد ورودی نامعتبر می باشد
                        if (_LongTime != "0" && _IsDeleted == 0)
                        {
                            if (_EnterCount < 5)///می تواند تا 5 بار کد را وارد کند سپس کاربر قفل میشود برای 5 دقیقه
                            {
                                int M = 5 - _EnterCount;
                                string S = Convert.ToString(_EnterCount + 1);
                                string _TActivationCodeId2 = Convert.ToString(_TActivationCodeId);
                                ITransaction tx = session.BeginTransaction();
                                string hqlVersionedUpdate = "update TActivationCode set EnterCount = :EnterCount2  where ID_Activation_Code = :AcIds";
                                int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                        .SetString("EnterCount2", S)
                                        .SetString("AcIds", _TActivationCodeId2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx.Commit();
                                session.Close();
                                //if (_D_LongTime == "0" && _IsDeleted == 0)
                                //{
                                //    return Ok("کار بر درست کار می تواند کد درخواست دهد ");
                                //}
                                //return Ok("کد اشتباه شما می توانید"+ M +"بار دیگر اقدام نمایید");
                                #region نمایش به صورت jason
                                Massege massege3 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = _LongTime,
                                    Names = "",
                                    Counts = M,
                                    Types = "R"
                                };
                                var carss3 = massege3;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    TimeRM = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss3);
                                #endregion
                                //return Ok("R:" + M + "T:" + _LongTime);
                            }
                        }


                        ///////////////
                        if (_EnterCount >= 4 || _IsDeleted == 1)
                        {
                            if (_EnterCount == 5)
                            {
                                string _TActivationCodeId2 = Convert.ToString(_TActivationCodeId);
                                ITransaction tx = session.BeginTransaction();
                                DateTime now = DateTime.Now;
                                DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                                string hqlVersionedUpdate = "update TActivationCode set EnterCount =: EnterCount2, IsDeletedTime=:IsDeletedTime2  where ID_Activation_Code = :AcIds";
                                int updatedEntities = session.CreateQuery(hqlVersionedUpdate)
                                        .SetString("EnterCount2", "6")
                                        .SetString("AcIds", _TActivationCodeId2)
                                        .SetParameter("IsDeletedTime2", Time_Now)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx.Commit();

                                _IS_time_KH = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _TActivationCodeId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                                _Kh_LongTime = TimeAgo(_IS_time_KH);

                                string _Servicer_ID2 = Convert.ToString(_Servicer_ID);
                                ITransaction tx_IsDeleted = session.BeginTransaction();
                                string hqlVersionedUpdate_IsDeleted = "update TServicer set IsDeleted = :IsDeleted2  where IdServicer = :ServicerIds";
                                int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                        .SetString("IsDeleted2", "1")
                                        .SetString("ServicerIds", _Servicer_ID2)
                                        //.SetDateTime()
                                        .ExecuteUpdate();
                                tx_IsDeleted.Commit();
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege22 = new Massege
                                {
                                    Userid = "",
                                    TimeIs = _Kh_LongTime,
                                    Names = "",
                                    Counts = 0,
                                    Types = "K"
                                };
                                var carss22 = massege22;
                                //var json22 = JsonConvert.SerializeObject(new
                                //{
                                //    KatakarTime = carss2
                                //});
                                //JObject json212 = JObject.Parse(json22);
                                return Ok(carss22);
                                #endregion
                                //return Ok("K:" + _Kh_LongTime);
                            }

                            //if (_Kh_LongTime == "0" && _IsDeleted == 1)
                            //{
                            //    return Ok("کار بر خطا کار می تواند کد درخواست دهد ");
                            //}
                            //return Ok("کار بر خطا کار می باشد " + _LongTime + "زمان باقی مانده");
                            #region نمایش به صورت jason
                            Massege massege2 = new Massege
                            {
                                Userid = "",
                                TimeIs = _Kh_LongTime,
                                Names = "",
                                Counts = 0,
                                Types = "K"
                            };
                            var carss2 = massege2;
                            //var json2 = JsonConvert.SerializeObject(new
                            //{
                            //    KatakarTime = carss
                            //});
                            //JObject json21 = JObject.Parse(json2);
                            return Ok(carss2);
                            #endregion
                            //return Ok("K:" + _Kh_LongTime);
                        }
                    }
                    #endregion
                }

                else if (_Peyk_ID == 0)
                {
                    #region نمایش به صورت jason
                    Massege massege1 = new Massege
                    {

                        Userid = "",
                        TimeIs = "",
                        Names = "",
                        Counts = 0,
                        Types = "کاربر یافت نشد",
                    };
                    var carss1 = massege1;
                    //var json2 = JsonConvert.SerializeObject(new
                    //{
                    //    No_Name = carss
                    //});
                    //JObject json21 = JObject.Parse(json2);
                    return Ok(carss1);
                    #endregion
                    //return Ok("کاربر یافت نشد");

                }
                session.Close();
                #region نمایش به صورت jason
                Massege massege = new Massege
                {

                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "مشکل دوباره ارسال شود"
                };
                var carss = massege;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    No_Name = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss);
                #endregion
            }
            #endregion


            return Ok("مشکل دوباره ارسال شود");

        }

        [HttpGet]
        [Route("api/G1/GetListTDay/{Unique_code},{Services_ID},{Payk_ID}")]
        public IHttpActionResult GetListTDay(string Unique_code, int Services_ID, long Payk_ID)
        {
            if (Services_ID == 0 && Payk_ID == 0 )
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            int _ServiceId = 0;

            if (Payk_ID != 0)
            {
                _ServiceId = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == Payk_ID).Select(p => p.ServicerId).SingleOrDefault<int>();
            }

            else if (Services_ID != 0)
            {
                _ServiceId = session.QueryOver<TServicer>().Where(p => p.IdServicer == Services_ID).Select(p => p.IdServicer).SingleOrDefault<int>();
            }

            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day);
            int _DayId = Convert.ToInt32(DayNumber(Time_Now.DayOfWeek.ToString()));
            string Time_Now_Day = DayName(Time_Now.DayOfWeek.ToString());
            #region نمایش خروجی
            var Lists = new List<Days>();
            if (_ServiceId!=0)
            {
                for (int i = -1; i < 6; i++)
                {
                    //if (i==_DayId)
                    //{
                    //    var Group = new Days();
                    //    Group.DayID = i;
                    //    Group.TitelsDay = Time_Now_Day;
                    //    Group.Active = 1;
                    //    Group.Date = Time_Now.ToString("yyyy/MM/dd");
                    //    Group.Titels = "";
                    //    Lists.Add(Group);
                    //}
                    //else
 if (i == -1)
                    {
                        DateTime now0 = DateTime.Now.AddDays(-1);
                        DateTime Time_Now0 = new DateTime(now0.Year, now0.Month, now0.Day);
                        int _DayId0 = Convert.ToInt32(DayNumber(Time_Now0.DayOfWeek.ToString()));
                        string Time_Now_Day0 = DayName(Time_Now0.DayOfWeek.ToString());
                        var Group = new Days();
                        Group.DayID = _DayId0;
                        Group.TitelsDay = Time_Now_Day0;
                        Group.Active = 0;
                        Group.Date = Time_Now0.ToString("yyyy/MM/dd");
                        Group.Titels = "";
                        Lists.Add(Group);
                    }
                    else
                    {
                        //if (i==6)
                        //{
                        //    DateTime now02 = DateTime.Now.AddDays(i-1);
                        //    DateTime Time_Now02 = new DateTime(now02.Year, now02.Month, now02.Day);
                        //    int _DayId02 = Convert.ToInt32(DayNumber(Time_Now02.DayOfWeek.ToString()));
                        //    string Time_Now_Day02 = DayName(Time_Now02.DayOfWeek.ToString());
                        //    var Group2= new Days();
                        //    Group2.DayID = _DayId02;
                        //    Group2.TitelsDay = Time_Now_Day02;
                        //    Group2.Active = 0;
                        //    Group2.Date = Time_Now02.ToString("yyyy/MM/dd");
                        //    Group2.Titels = "";
                        //    Lists.Add(Group2);
                        //}
                        //else
                        //{
                            DateTime now0 = DateTime.Now.AddDays(i);
                            DateTime Time_Now0 = new DateTime(now0.Year, now0.Month, now0.Day);
                            int _DayId0 = Convert.ToInt32(DayNumber(Time_Now0.DayOfWeek.ToString()));
                            string Time_Now_Day0 = DayName(Time_Now0.DayOfWeek.ToString());
                            var Group = new Days();
                            Group.DayID = _DayId0;
                            Group.TitelsDay = Time_Now_Day0;
                        if (_DayId0 == _DayId)
                        {
                            Group.Active = 1;
                        }
                        else
                        {
                            Group.Active = 0;
                        }
                            
                            Group.Date = Time_Now0.ToString("yyyy/MM/dd");
                            Group.Titels = "";
                            Lists.Add(Group);
                        //}
                        
                    }
                }
                    
            }

            else if (_ServiceId == 0) 
            {
                var Group = new Days();
                Group.DayID = 0;
                Group.TitelsDay = "";
                Group.Active = 0;
                Group.Date = "";
                Group.Titels = "موردی یافت نشد";
                Lists.Add(Group);

            }
            //int ww= ListGroups_G[0]._GroupID.IdShift

            var json = JsonConvert.SerializeObject(new
            {
                ListDay = Lists.OrderBy(p=>p.Date)
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
            #endregion

        }

        [HttpGet]
        [Route("api/G1/GetListAllOrder/{Unique_code},{Services_ID},{Payk_ID},{Date}")]
        public IHttpActionResult GetListAllOrder(string Unique_code, int Services_ID, long Payk_ID, string Date)
        {
            if (Services_ID == 0 && Payk_ID == 0)
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }

            DateTime _Date = Convert.ToDateTime(Date);

            ISession session = OpenNHibertnateSession.OpenSession();

            int _ServiceId = 0;

            if (Payk_ID != 0)
            {
                _ServiceId = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == Payk_ID).Select(p => p.ServicerId).SingleOrDefault<int>();
            }

            else if (Services_ID != 0)
            {
                _ServiceId = session.QueryOver<TServicer>().Where(p => p.IdServicer == Services_ID).Select(p => p.IdServicer).SingleOrDefault<int>();
            }

            var Lists = new List<ListOrder>();

            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day);
            int _DayId = Convert.ToInt32(DayNumber(Time_Now.DayOfWeek.ToString()));
            string Time_Now_Day = DayName(Time_Now.DayOfWeek.ToString());
            #region نمایش خروجی
            if (_ServiceId != 0)
            {
                #region واکشی مقادیر مربوط به سفارشات
                var OrderServiser = session.Query<TOrder>()
                                         .Where(p => p.TServicer.IdServicer== _ServiceId 
                                        && (p.Datetahvil== _Date || p.OrderDate== _Date)
                                         //&& p.OrderDate.Date== _Date  //بعد از تست فعال شود حتما تا تاریخ ها را به روز بیارد
                                         )
                                         .Select(x =>
           new {
               IDOrder = x.IdOrder,
               ShenaseSefaresh=x.ShenaseSefaresh,
              Date=x.OrderDate.ToString("yyyy/MM/dd"),
              Price=x.OrderPrice,
               Addresses=x.TAddresses.Titels_Address ,
               Phone_Addresss = x.TAddresses.Phone_Addresss,
               VaziyatId =x.TLVaziyatTahvil.IdVaziyatTahvil, 
               VaziyatTitels = x.TLVaziyatTahvil.TextForServiser,
               ShiftDaryaft=x.TShift.ShiftText,
               ShiftTahvil = x.TShift2.ShiftText,
               NameFamily=x.TUsers.NameFamily,
               DateTahvil=x.Datetahvil.ToString("yyyy/MM/dd"),
               NoePay=x.TLNoePay.Title,
               Latitude= x.TAddresses.Latitude,
               Longitude=x.TAddresses.Longitude
               
           });
                var ListOrderServiser = OrderServiser.ToList();

                var ListSadad = session.Query<TSabad>()
                    .Where(a=>a.TOrder.TServicer.IdServicer== _ServiceId)
                                                .GroupBy(s => s.TOrder.IdOrder)
                                                 .Select(
                                                     gg => new
                                                          {
                                                              _OrderID = gg.Key,
                                                              counts = gg.Count()
                                                          }).ToList();

                int C_Order = 0;
                C_Order = ListOrderServiser.Count;
                int C_Sabad = 0;
                C_Sabad = ListSadad.Count;

                if (C_Sabad!=0 && C_Order!=0)
                {
                    var ListServiserProduct =
                            from LOrder in ListOrderServiser 
                            join Lsabad in ListSadad
                                             on LOrder.IDOrder equals Lsabad._OrderID
                    
                       select
                       new
                       {
                          LOrder.IDOrder,
                          LOrder.ShenaseSefaresh,
                          LOrder. Date,
                          LOrder. Price ,
                           LOrder.Addresses ,
                           LOrder.Phone_Addresss,
                           LOrder. VaziyatId ,
                           LOrder.VaziyatTitels,
                           Lsabad.counts,
                           LOrder.ShiftDaryaft,
                           LOrder.ShiftTahvil,
                           LOrder.NameFamily,
                           LOrder.DateTahvil,
                           LOrder.NoePay,
                           LOrder.Latitude,
                           LOrder.Longitude
                       };
                    var ListServiserProduct2 = ListServiserProduct.ToList();
                    #region قرار دادن در خروجی
                    int C_List = 0;
                    C_List = ListServiserProduct2.Count;
                    for (int i = 0; i < C_List; i++)
                    {
                        var Group = new ListOrder();
                        Group.IDOrder = ListServiserProduct2[i].IDOrder;
                        Group.ShenaseSefaresh = ListServiserProduct2[i].ShenaseSefaresh;
                        Group.Date = ListServiserProduct2[i].Date;
                        Group.Price = ListServiserProduct2[i].Price;
                        Group.Addresses = ListServiserProduct2[i].Addresses;
                        Group.Phone_Address = ListServiserProduct2[i].Phone_Addresss;
                        Group.VaziyatId = ListServiserProduct2[i].VaziyatId;
                        Group.VaziyatTitels = ListServiserProduct2[i].VaziyatTitels;
                        Group.counts = ListServiserProduct2[i].counts;
                        Group.NoePay= ListServiserProduct2[i].NoePay;
                        Group.ShiftDaryaft = ListServiserProduct2[i].ShiftDaryaft;
                        Group.ShiftTahvil = ListServiserProduct2[i].ShiftTahvil;
                        Group.NameFamilyUser = ListServiserProduct2[i].NameFamily;
                        Group.Date_Tavil = ListServiserProduct2[i].DateTahvil;
                        Group.Latitude = ListServiserProduct2[i].Latitude.ToString();
                        Group.Longitude = ListServiserProduct2[i].Longitude.ToString();

                        Group.Titels = "";
                        Lists.Add(Group);
                    }
                    #endregion

                    
                }



                #endregion

            }

            else if (_ServiceId == 0)
            {
                var Group = new ListOrder();
                Group.IDOrder = 0;
                Group.ShenaseSefaresh = 0;
                Group.Date = "";
                Group.Price = 0;
                Group.Addresses = "";
                Group.Phone_Address = "";
                Group.VaziyatId = 0;
                Group.VaziyatTitels = "";
                Group.counts = 0;
                Group.ShiftDaryaft = "";
                Group.ShiftTahvil = "";
                Group.NameFamilyUser = "";
                Group.Latitude = "";
                Group.Longitude = "";
                Group.Titels = "موردی یافت نشد";
                Lists.Add(Group);

            }
            //int ww= ListGroups_G[0]._GroupID.IdShift

            var json = JsonConvert.SerializeObject(new
            {
                ListDay = Lists.OrderBy(p => p.Date)
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
            #endregion

        }

        [HttpGet]
        [Route("api/G1/GetListFilterAllOrder/{Unique_code},{Services_ID},{Payk_ID},{Date},{Shift_ID}")]
        public IHttpActionResult GetListFilterAllOrder(string Unique_code, int Services_ID, long Payk_ID, string Date, int Shift_ID)
        {
            if (Services_ID == 0 && Payk_ID == 0)
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }

            DateTime _Date = Convert.ToDateTime(Date);

            ISession session = OpenNHibertnateSession.OpenSession();

            int _ServiceId = 0;

            if (Payk_ID != 0)
            {
                _ServiceId = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == Payk_ID).Select(p => p.ServicerId).SingleOrDefault<int>();
            }

            else if (Services_ID != 0)
            {
                _ServiceId = session.QueryOver<TServicer>().Where(p => p.IdServicer == Services_ID).Select(p => p.IdServicer).SingleOrDefault<int>();
            }

            var Lists = new List<ListOrder>();

            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day);
            int _DayId = Convert.ToInt32(DayNumber(Time_Now.DayOfWeek.ToString()));
            string Time_Now_Day = DayName(Time_Now.DayOfWeek.ToString());
            #region نمایش خروجی
            if (_ServiceId != 0)
            {
                #region همه حالت ها که وضعیت ست نشده
                #region واکشی مقادیر مربوط به سفارشات
                var OrderServiser = session.Query<TOrder>()
                                         .Where(p => p.TServicer.IdServicer == _ServiceId
                                         && p.OrderDate.Date == _Date  //بعد از تست فعال شود حتما تا تاریخ ها را به روز بیارد
                                         && p.TShift.IdShift == Shift_ID
                                         //&& p.TLVaziyatTahvil.IdVaziyatTahvil == Vaziyat_ID
                                         )
                                         .Select(x =>
           new {
               IDOrder = x.IdOrder,
               ShenaseSefaresh = x.ShenaseSefaresh,
               Date = x.OrderDate.ToString("yyyy/MM/dd"),
               Price = x.OrderPriceBof,
               Addresses = x.TAddresses.Titels_Address,
               Phone_Addresss = x.TAddresses.Phone_Addresss,
               VaziyatId = x.TLVaziyatTahvil.IdVaziyatTahvil,
               VaziyatTitels = x.TLVaziyatTahvil.TextForServiser,
               NameFamilyUser = x.TUsers.NameFamily,
               NoePay = x.TLNoePay.Title
           });
                int dd = OrderServiser.Count();
                var ListOrderServiser = OrderServiser.ToList();

                var ListSadad = session.Query<TSabad>()
                    .Where(a => a.TOrder.TServicer.IdServicer == _ServiceId)
                                                .GroupBy(s => s.TOrder.IdOrder)
                                                 .Select(
                                                     gg => new
                                                     {
                                                         _OrderID = gg.Key,
                                                         counts = gg.Count()
                                                     }).ToList();

                int C_Order = 0;
                C_Order = ListOrderServiser.Count();
                int C_Sabad = 0;
                C_Sabad = ListSadad.Count;

                if (C_Sabad != 0 && C_Order != 0)
                {

                    var ListServiserProduct =
                            from LOrder in ListOrderServiser
                            join Lsabad in ListSadad
                                             on LOrder.IDOrder equals Lsabad._OrderID

                            select
                            new
                            {
                                LOrder.IDOrder,
                                LOrder.ShenaseSefaresh,
                                LOrder.Date,
                                LOrder.Price,
                                LOrder.Addresses,
                                LOrder.Phone_Addresss,
                                LOrder.VaziyatId,
                                LOrder.VaziyatTitels,
                                Lsabad.counts,
                                LOrder.NameFamilyUser,
                                LOrder.NoePay
                            };
                    var ListServiserProduct2 = ListServiserProduct.ToList();
                    #region قرار دادن در خروجی
                    int C_List = 0;
                    C_List = ListServiserProduct2.Count();
                    for (int i = 0; i < C_List; i++)
                    {
                        var Group = new ListOrder();
                        Group.IDOrder = ListServiserProduct2[i].IDOrder;
                        Group.ShenaseSefaresh = ListServiserProduct2[i].ShenaseSefaresh;
                        Group.Date = ListServiserProduct2[i].Date;
                        Group.Price = ListServiserProduct2[i].Price;
                        Group.Addresses = ListServiserProduct2[i].Addresses;
                        Group.Phone_Address = ListServiserProduct2[i].Phone_Addresss;
                        Group.VaziyatId = ListServiserProduct2[i].VaziyatId;
                        Group.VaziyatTitels = ListServiserProduct2[i].VaziyatTitels;
                        Group.counts = ListServiserProduct2[i].counts;
                        Group.NameFamilyUser = ListServiserProduct2[i].NameFamilyUser;
                        Group.NoePay = ListServiserProduct2[i].NoePay;
                        Group.Titels = "";
                        Lists.Add(Group);
                    }
                    #endregion


                }



                #endregion
                #endregion

                #region وضعیت دریافت شده فیلتر میشود 
                #region واکشی مقادیر مربوط به سفارشات
                var OrderServiser2 = session.Query<TOrder>()
                                         .Where(p => p.TServicer.IdServicer == _ServiceId
                                         && p.OrderDate.Date == _Date  //بعد از تست فعال شود حتما تا تاریخ ها را به روز بیارد
                                         && p.TShift.IdShift == Shift_ID
                                        
                                         )
                                         .Select(x =>
           new {
               IDOrder = x.IdOrder,
               ShenaseSefaresh = x.ShenaseSefaresh,
               Date = x.OrderDate.ToString("yyyy/MM/dd"),
               Price = x.OrderPriceBof,
               Addresses = x.TAddresses.Titels_Address,
               Phone_Addresss = x.TAddresses.Phone_Addresss,
               VaziyatId = x.TLVaziyatTahvil.IdVaziyatTahvil,
               VaziyatTitels = x.TLVaziyatTahvil.TextForServiser,
               NameFamilyUser = x.TUsers.NameFamily,
               NoePay = x.TLNoePay.Title
           });
                var ListOrderServiser2 = OrderServiser2.ToList();

                var ListSadad2 = session.Query<TSabad>()
                    .Where(a => a.TOrder.TServicer.IdServicer == _ServiceId)
                                                .GroupBy(s => s.TOrder.IdOrder)
                                                 .Select(
                                                     gg => new
                                                     {
                                                         _OrderID = gg.Key,
                                                         counts = gg.Count()
                                                     }).ToList();

                int C_Order2 = 0;
                C_Order2 = ListOrderServiser2.Count;
                int C_Sabad2 = 0;
                C_Sabad2 = ListSadad2.Count;

                if (C_Sabad2 != 0 && C_Order2 != 0)
                {

                    var ListServiserProduct2 =
                            from LOrder in ListOrderServiser2
                            join Lsabad in ListSadad2
                                             on LOrder.IDOrder equals Lsabad._OrderID

                            select
                            new
                            {
                                LOrder.IDOrder,
                                LOrder.ShenaseSefaresh,
                                LOrder.Date,
                                LOrder.Price,
                                LOrder.Addresses,
                                LOrder.Phone_Addresss,
                                LOrder.VaziyatId,
                                LOrder.VaziyatTitels,
                                Lsabad.counts,
                                LOrder.NameFamilyUser,
                                LOrder.NoePay
                            };
                    var ListServiserProduct22 = ListServiserProduct2.ToList();
                    #region قرار دادن در خروجی
                    int C_List2 = 0;
                    C_List2 = ListServiserProduct22.Count;
                    for (int i = 0; i < C_List2; i++)
                    {
                        var Group = new ListOrder();
                        Group.IDOrder = ListServiserProduct22[i].IDOrder;
                        Group.ShenaseSefaresh = ListServiserProduct22[i].ShenaseSefaresh;
                        Group.Date = ListServiserProduct22[i].Date;
                        Group.Price = ListServiserProduct22[i].Price;
                        Group.Addresses = ListServiserProduct22[i].Addresses;
                        Group.Phone_Address = ListServiserProduct22[i].Phone_Addresss;
                        Group.VaziyatId = ListServiserProduct22[i].VaziyatId;
                        Group.VaziyatTitels = ListServiserProduct22[i].VaziyatTitels;
                        Group.counts = ListServiserProduct22[i].counts;
                        Group.NameFamilyUser = ListServiserProduct22[i].NameFamilyUser;
                        Group.NoePay = ListServiserProduct22[i].NoePay;
                        Group.Titels = "";
                        Lists.Add(Group);
                    }
                    #endregion


                }



                #endregion
                #endregion



            }

            else if (_ServiceId == 0)
            {
                var Group = new ListOrder();
                Group.IDOrder = 0;
                Group.ShenaseSefaresh = 0;
                Group.Date = "";
                Group.Price = 0;
                Group.Addresses = "";
                Group.Phone_Address = "";
                Group.VaziyatId = 0;
                Group.VaziyatTitels = "";
                Group.counts = 0;
                Group.NameFamilyUser = "";
                Group.NoePay = "";
                Group.Titels = "موردی یافت نشد";
                Lists.Add(Group);

            }
            //int ww= ListGroups_G[0]._GroupID.IdShift

            var json = JsonConvert.SerializeObject(new
            {
                ListDay = Lists.OrderBy(p => p.Date)
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);
            #endregion

        }


        //[HttpGet]
        //[Route("api/G1/GetListFilterAllOrder/{Unique_code},{Services_ID},{Payk_ID},{Date},{Shift_ID},{Vaziyat_ID}")]
        //public IHttpActionResult GetListFilterAllOrder(string Unique_code, int Services_ID, long Payk_ID, string Date,int Shift_ID , int Vaziyat_ID)
        //{
        //    if (Services_ID == 0 && Payk_ID == 0)
        //    {
        //        return Ok("مقادیر خالی");
        //    }

        //    if (Unique_code != "PlatformV199325694")
        //    {
        //        return Ok("کلید نادرست");
        //    }

        //    DateTime _Date = Convert.ToDateTime(Date);

        //    ISession session = OpenNHibertnateSession.OpenSession();

        //    int _ServiceId = 0;

        //    if (Payk_ID != 0)
        //    {
        //        _ServiceId = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == Payk_ID).Select(p => p.ServicerId).SingleOrDefault<int>();
        //    }

        //    else if (Services_ID != 0)
        //    {
        //        _ServiceId = session.QueryOver<TServicer>().Where(p => p.IdServicer == Services_ID).Select(p => p.IdServicer).SingleOrDefault<int>();
        //    }

        //    var Lists = new List<ListOrder>();

        //    DateTime now = DateTime.Now;
        //    DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day);
        //    int _DayId = Convert.ToInt32(DayNumber(Time_Now.DayOfWeek.ToString()));
        //    string Time_Now_Day = DayName(Time_Now.DayOfWeek.ToString());
        //    #region نمایش خروجی
        //    if (_ServiceId != 0)
        //    {
        //        #region همه حالت ها که وضعیت ست نشده
        //        #region واکشی مقادیر مربوط به سفارشات
        //        var OrderServiser = session.Query<TOrder>()
        //                                 .Where(p => p.TServicer.IdServicer == _ServiceId
        //                                 && p.OrderDate.Date == _Date  //بعد از تست فعال شود حتما تا تاریخ ها را به روز بیارد
        //                                 && p.TShift.IdShift == Shift_ID
        //                                 //&& p.TLVaziyatTahvil.IdVaziyatTahvil == Vaziyat_ID
        //                                 )
        //                                 .Select(x =>
        //   new {
        //       IDOrder = x.IdOrder,
        //       ShenaseSefaresh = x.ShenaseSefaresh,
        //       Date = x.OrderDate.ToString("yyyy/MM/dd"),
        //       Price = x.OrderPriceBof,
        //       Addresses = x.TAddresses.Titels_Address,
        //       Phone_Addresss = x.TAddresses.Phone_Addresss,
        //       VaziyatId = x.TLVaziyatTahvil.IdVaziyatTahvil,
        //       VaziyatTitels = x.TLVaziyatTahvil.TitelsVaziyatTahvil,
        //       NameFamilyUser= x.TUsers.NameFamily,
        //      NoePay=x.TLNoePay.Title
        //   });
        //        int dd = OrderServiser.Count();
        //        var ListOrderServiser = OrderServiser.ToList();

        //        var ListSadad = session.Query<TSabad>()
        //            .Where(a => a.TOrder.TServicer.IdServicer == _ServiceId)
        //                                        .GroupBy(s => s.TOrder.IdOrder)
        //                                         .Select(
        //                                             gg => new
        //                                             {
        //                                                 _OrderID = gg.Key,
        //                                                 counts = gg.Count()
        //                                             }).ToList();

        //        int C_Order = 0;
        //        C_Order = ListOrderServiser.Count();
        //        int C_Sabad = 0;
        //        C_Sabad = ListSadad.Count;

        //        if (C_Sabad != 0 && C_Order != 0)
        //        {

        //            var ListServiserProduct =
        //                    from LOrder in ListOrderServiser
        //                    join Lsabad in ListSadad
        //                                     on LOrder.IDOrder equals Lsabad._OrderID

        //                    select
        //                    new
        //                    {
        //                        LOrder.IDOrder,
        //                        LOrder.ShenaseSefaresh,
        //                        LOrder.Date,
        //                        LOrder.Price,
        //                        LOrder.Addresses,
        //                        LOrder.Phone_Addresss,
        //                        LOrder.VaziyatId,
        //                        LOrder.VaziyatTitels,
        //                        Lsabad.counts,
        //                        LOrder.NameFamilyUser,
        //                        LOrder.NoePay
        //                    };
        //            var ListServiserProduct2 = ListServiserProduct.ToList();
        //            #region قرار دادن در خروجی
        //            int C_List = 0;
        //            C_List = ListServiserProduct2.Count();
        //            for (int i = 0; i < C_List; i++)
        //            {
        //                var Group = new ListOrder();
        //                Group.IDOrder = ListServiserProduct2[i].IDOrder;
        //                Group.ShenaseSefaresh = ListServiserProduct2[i].ShenaseSefaresh;
        //                Group.Date = ListServiserProduct2[i].Date;
        //                Group.Price = ListServiserProduct2[i].Price;
        //                Group.Addresses = ListServiserProduct2[i].Addresses;
        //                Group.Phone_Address = ListServiserProduct2[i].Phone_Addresss;
        //                Group.VaziyatId = ListServiserProduct2[i].VaziyatId;
        //                Group.VaziyatTitels = ListServiserProduct2[i].VaziyatTitels;
        //                Group.counts = ListServiserProduct2[i].counts;
        //                Group.NameFamilyUser = ListServiserProduct2[i].NameFamilyUser;
        //                Group.NoePay = ListServiserProduct2[i].NoePay;
        //                Group.Titels = "";
        //                Lists.Add(Group);
        //            }
        //            #endregion


        //        }



        //        #endregion
        //        #endregion

        //        #region وضعیت دریافت شده فیلتر میشود 
        //        #region واکشی مقادیر مربوط به سفارشات
        //        var OrderServiser2 = session.Query<TOrder>()
        //                                 .Where(p => p.TServicer.IdServicer == _ServiceId
        //                                 && p.OrderDate.Date == _Date  //بعد از تست فعال شود حتما تا تاریخ ها را به روز بیارد
        //                                 && p.TShift.IdShift == Shift_ID
        //                                 && p.TLVaziyatTahvil.IdVaziyatTahvil == Vaziyat_ID
        //                                 )
        //                                 .Select(x =>
        //   new {
        //       IDOrder = x.IdOrder,
        //       ShenaseSefaresh = x.ShenaseSefaresh,
        //       Date = x.OrderDate.ToString("yyyy/MM/dd"),
        //       Price = x.OrderPriceBof,
        //       Addresses = x.TAddresses.Titels_Address,
        //       Phone_Addresss = x.TAddresses.Phone_Addresss,
        //       VaziyatId = x.TLVaziyatTahvil.IdVaziyatTahvil,
        //       VaziyatTitels = x.TLVaziyatTahvil.TitelsVaziyatTahvil,
        //       NameFamilyUser = x.TUsers.NameFamily,
        //       NoePay = x.TLNoePay.Title
        //   });
        //        var ListOrderServiser2 = OrderServiser2.ToList();

        //        var ListSadad2 = session.Query<TSabad>()
        //            .Where(a => a.TOrder.TServicer.IdServicer == _ServiceId)
        //                                        .GroupBy(s => s.TOrder.IdOrder)
        //                                         .Select(
        //                                             gg => new
        //                                             {
        //                                                 _OrderID = gg.Key,
        //                                                 counts = gg.Count()
        //                                             }).ToList();

        //        int C_Order2 = 0;
        //        C_Order2 = ListOrderServiser2.Count;
        //        int C_Sabad2 = 0;
        //        C_Sabad2 = ListSadad2.Count;

        //        if (C_Sabad2 != 0 && C_Order2 != 0)
        //        {

        //            var ListServiserProduct2 =
        //                    from LOrder in ListOrderServiser2
        //                    join Lsabad in ListSadad2
        //                                     on LOrder.IDOrder equals Lsabad._OrderID

        //                    select
        //                    new
        //                    {
        //                        LOrder.IDOrder,
        //                        LOrder.ShenaseSefaresh,
        //                        LOrder.Date,
        //                        LOrder.Price,
        //                        LOrder.Addresses,
        //                        LOrder.Phone_Addresss,
        //                        LOrder.VaziyatId,
        //                        LOrder.VaziyatTitels,
        //                        Lsabad.counts,
        //                        LOrder.NameFamilyUser,
        //                        LOrder.NoePay
        //                    };
        //            var ListServiserProduct22 = ListServiserProduct2.ToList();
        //            #region قرار دادن در خروجی
        //            int C_List2 = 0;
        //            C_List2 = ListServiserProduct22.Count;
        //            for (int i = 0; i < C_List2; i++)
        //            {
        //                var Group = new ListOrder();
        //                Group.IDOrder = ListServiserProduct22[i].IDOrder;
        //                Group.ShenaseSefaresh = ListServiserProduct22[i].ShenaseSefaresh;
        //                Group.Date = ListServiserProduct22[i].Date;
        //                Group.Price = ListServiserProduct22[i].Price;
        //                Group.Addresses = ListServiserProduct22[i].Addresses;
        //                Group.Phone_Address = ListServiserProduct22[i].Phone_Addresss;
        //                Group.VaziyatId = ListServiserProduct22[i].VaziyatId;
        //                Group.VaziyatTitels = ListServiserProduct22[i].VaziyatTitels;
        //                Group.counts = ListServiserProduct22[i].counts;
        //                Group.NameFamilyUser = ListServiserProduct22[i].NameFamilyUser;
        //                Group.NoePay = ListServiserProduct22[i].NoePay;
        //                Group.Titels = "";
        //                Lists.Add(Group);
        //            }
        //            #endregion


        //        }



        //        #endregion
        //        #endregion



        //    }

        //    else if (_ServiceId == 0)
        //    {
        //        var Group = new ListOrder();
        //        Group.IDOrder = 0;
        //        Group.ShenaseSefaresh = 0;
        //        Group.Date = "";
        //        Group.Price = 0;
        //        Group.Addresses = "";
        //        Group.Phone_Address = "";
        //        Group.VaziyatId = 0;
        //        Group.VaziyatTitels = "";
        //        Group.counts = 0;
        //        Group.NameFamilyUser = "";
        //        Group.NoePay = "";
        //        Group.Titels = "موردی یافت نشد";
        //        Lists.Add(Group);

        //    }
        //    //int ww= ListGroups_G[0]._GroupID.IdShift

        //    var json = JsonConvert.SerializeObject(new
        //    {
        //        ListDay = Lists.OrderBy(p => p.Date)
        //    });

        //    JObject json5 = JObject.Parse(json);

        //    session.Close();
        //    return Ok(json5);
        //    #endregion

        //}

        [HttpGet]
        [Route("api/G1/GetListTVaziyatTahvil/{Unique_code}")]
        public IHttpActionResult GetListTVaziyatTahvil(string Unique_code)
        {
            if (Unique_code != "PlatformV199325694")
            {

                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            var ListVaziyat = session.Query<TLVaziyatTahvil>()
                //.Where(a=>a.IdVaziyatTahvil==1 || a.IdVaziyatTahvil == 6 || a.IdVaziyatTahvil == 7)
                                         .Select(x =>
           new {
               IdVaziyatTahvil=x.IdVaziyatTahvil,
               TitelsVaziyatTahvil=x.TextForServiser
           });
            var ListVaziyat2 = ListVaziyat.ToList();
            var json = JsonConvert.SerializeObject(new
            {
                Vaziyat = ListVaziyat2
            });
            
            JObject json5 = JObject.Parse(json);
            session.Close();
            return Ok(json5);
        }

        [HttpGet]
        [Route("api/G1/GetListAllSabad/{Unique_code},{Services_ID},{Payk_ID},{Order_ID}")]
        public IHttpActionResult GetListAllSabad(string Unique_code, int Services_ID, long Payk_ID, long Order_ID)
        {
            if (Services_ID == 0 && Payk_ID == 0)
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }

            ISession session = OpenNHibertnateSession.OpenSession();

            int _ServiceId = 0;

            if (Payk_ID != 0)
            {
                _ServiceId = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == Payk_ID).Select(p => p.ServicerId).SingleOrDefault<int>();
            }

            else if (Services_ID != 0)
            {
                _ServiceId = session.QueryOver<TServicer>().Where(p => p.IdServicer == Services_ID).Select(p => p.IdServicer).SingleOrDefault<int>();
            }

            var Lists = new List<ListSabad>();

            DateTime now = DateTime.Now;
            DateTime Time_Now = new DateTime(now.Year, now.Month, now.Day);
            int _DayId = Convert.ToInt32(DayNumber(Time_Now.DayOfWeek.ToString()));
            string Time_Now_Day = DayName(Time_Now.DayOfWeek.ToString());
            #region نمایش خروجی
            if (_ServiceId != 0)
            {
                #region واکشی مقادیر مربوط به سفارشات
                var OrderSabad = session.Query<TSabad>()
                                         .Where(p => p.TOrder.IdOrder==Order_ID )
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
               
                if (C_OrderSabad != 0 )
                {
                    int _WashingIroning = 0;
                    _WashingIroning= ListOrderSabad.Where(m => m.IdNoeKhedmat == 1).Count();
                    int _Washing = 0;
                    _Washing=ListOrderSabad.Where(m => m.IdNoeKhedmat == 2).Count();
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

            else if (_ServiceId == 0)
            {
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
                Group.Titels = "موردی یافت نشد";
                Lists.Add(Group);

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
        [Route("api/G1/GetListSetTozihServicer/{Unique_code},{Services_ID},{Payk_ID},{Sabad_ID},{Tozihat}")]
        public IHttpActionResult GetListSetTozihServicer(string Unique_code, int Services_ID, long Payk_ID, long Sabad_ID ,string Tozihat)
        {
            if (Services_ID == 0 && Payk_ID == 0)
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }

            ISession session = OpenNHibertnateSession.OpenSession();

            int _ServiceId = 0;

            if (Payk_ID != 0)
            {
                _ServiceId = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == Payk_ID).Select(p => p.ServicerId).SingleOrDefault<int>();
            }

            else if (Services_ID != 0)
            {
                _ServiceId = session.QueryOver<TServicer>().Where(p => p.IdServicer == Services_ID).Select(p => p.IdServicer).SingleOrDefault<int>();
            }

            long _Sabad_ID = 0;
            _Sabad_ID= session.QueryOver<TSabad>().Where(p => p.IdSabad == Sabad_ID).Select(p => p.IdSabad).SingleOrDefault<long>();

            #region نمایش خروجی
            if (_ServiceId != 0)
            {
                #region آپدیث سبد خرید
                var emp = session.QueryOver<TSabad>().Where(p => p.IdSabad == _Sabad_ID).SingleOrDefault();
                emp.TozihatSevviser = Tozihat;
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(emp);
                    transaction.Commit();
                    return Ok("مورد ثبت شد");
                }
                #endregion

            }

            else if (_ServiceId == 0)
            {
                return Ok("مورد یافت نشد");                
            }

            else if (_Sabad_ID == 0)
            {
                return Ok("مورد یافت نشد");
            }
            return Ok("مورد یافت نشد");

            #endregion

        }


        [HttpGet]
        [Route("api/G1/GetListSetVaziyatTahvil/{Unique_code},{Services_ID},{Payk_ID},{Order_ID},{Vaziyat_ID}")]
        public IHttpActionResult GetListSetVaziyatTahvil(string Unique_code, int Services_ID, long Payk_ID, long Order_ID,int Vaziyat_ID)
        {
            if (Services_ID == 0 && Payk_ID == 0)
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }

            ISession session = OpenNHibertnateSession.OpenSession();

            int _ServiceId = 0;

            if (Payk_ID != 0)
            {
                _ServiceId = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == Payk_ID).Select(p => p.ServicerId).SingleOrDefault<int>();

            }

            else if (Services_ID != 0)
            {
                _ServiceId = session.QueryOver<TServicer>().Where(p => p.IdServicer == Services_ID).Select(p => p.IdServicer).SingleOrDefault<int>();
            }


            long _Order_ID = 0;
            _Order_ID = session.QueryOver<TOrder>().Where(p => p.IdOrder == Order_ID && (p.TServicer.IdServicer== _ServiceId || p.TPeyks.IdPeyks==_ServiceId)).Select(p => p.IdOrder).SingleOrDefault<long>();

            if (_Order_ID!=0)
            {
                int _IdVaziyatTahvil = 0;
                _IdVaziyatTahvil = session.QueryOver<TOrder>().Where(p => p.IdOrder == Order_ID && (p.TServicer.IdServicer == _ServiceId || p.TPeyks.IdPeyks == _ServiceId))
                             .Select(p => p.TLVaziyatTahvil.IdVaziyatTahvil).SingleOrDefault<int>();

                //if ((_IdVaziyatTahvil==1 || _IdVaziyatTahvil == 6) && (Vaziyat_ID!=2 || Vaziyat_ID !=3 || Vaziyat_ID != 4 ))
                if ((_IdVaziyatTahvil == 5 || _IdVaziyatTahvil == 8) )
                {
                    return Ok("شما نمی توانید مورد را تغییر بدهید");
                }
                //else if (_IdVaziyatTahvil>= Vaziyat_ID && _IdVaziyatTahvil!=1008)
                //{
                //    return Ok("شما نمی توانید مورد را تغییر بدهید");
                //}

               

            }

            #region نمایش خروجی
            if (_ServiceId != 0 && _Order_ID!=0)
            {
                #region آپدیث سبد خرید
                var emp = session.QueryOver<TOrder>().Where(p => p.IdOrder == _Order_ID).SingleOrDefault();
                if (emp.TLNoePay.IdNoePay==1 && Vaziyat_ID==8)
                {
                    emp.TLVaziyatVarizi = session.QueryOver<TLVaziyatVarizi>().Where(p => p.IdVaziyatVarizi == 7).SingleOrDefault();
                }
                emp.TLVaziyatTahvil = session.QueryOver<TLVaziyatTahvil>().Where(p => p.IdVaziyatTahvil == Vaziyat_ID).SingleOrDefault();
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(emp);
                    transaction.Commit();
                    return Ok("مورد ثبت شد");
                }
                #endregion

            }

            else if (_ServiceId == 0 )
            {
                return Ok("مورد یافت نشد");
            }

            else if (_Order_ID == 0)
            {
                return Ok("مورد یافت نشد");
            }
            return Ok("مورد یافت نشد");

            #endregion

        }



        [HttpGet]
        [Route("api/G1/GetListShift/{Unique_code},{Services_ID},{Payk_ID}")]
        public IHttpActionResult GetListShift(string Unique_code, int Services_ID, long Payk_ID)
        {
            var ListAll = new List<ListAllShift>();
            if (Services_ID == 0 && Payk_ID == 0)
            {
                //return Ok("مقادیر خالی");
                var Group = new ListAllShift();
                Group.id = 0;
                Group.ShiftID = 0;
                Group.ShiftText = "";
                Group.Type_Erore = "مقادیر خالی";
                ListAll.Add(Group);
            }

            if (Unique_code != "PlatformV199325694")
            {
                //return Ok("کلید نادرست");
                var Group = new ListAllShift();
                Group.id = 0;
                Group.ShiftID = 0;
                Group.ShiftText = "";
                Group.Type_Erore = "کلید نادرست";
                ListAll.Add(Group);
            }
            ISession session = OpenNHibertnateSession.OpenSession();

            int _ServiceId = 0;

            if (Payk_ID != 0)
            {
                _ServiceId = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == Payk_ID).Select(p => p.ServicerId).SingleOrDefault<int>();
            }

            else if (Services_ID != 0)
            {
                _ServiceId = session.QueryOver<TServicer>().Where(p => p.IdServicer == Services_ID).Select(p => p.IdServicer).SingleOrDefault<int>();
            }

          
      var ListShift= session.Query<TServicerForMahalehByPeyk>()
                                         .Where(p => p.TServicerForMahaleh.TServicer.IdServicer== _ServiceId)
                                         .Select(x =>
           new {
               x.IdServicerForMahalehByPeyk,
             ShiftId=x.TShift.IdShift,
             ShiftT=x.TShift.ShiftText
           }).ToList();
            //.Select(x => new
            //{
            //    LawId = x.IdLaw,
            //    LawText = x.LawText
            //});
            int count_Item = ListShift.Count();
            int C = 0;
            //if (count_Item != 0 && count_Item != 1)
            //{
            //    for (int i = 0; i < count_Item; i++)
            //    {
            //        ListShift.RemoveAll(x => x.ShiftId == ListShift[i].ShiftId);
            //        C = ListShift.Count();
            //    }

            //}
            if (count_Item == 0)
            {
                var Group = new ListAllShift();
                Group.id = 0;
                Group.ShiftID = 0;
                Group.ShiftText = "";
                Group.Type_Erore = "موردی یافت نشد";
                ListAll.Add(Group);
            }
            if (count_Item != 0 && count_Item != 1)
            {
                var ListShift02 = ListShift
                                                 .GroupBy(s => s.ShiftId)
                                                 .Select(grp => grp.FirstOrDefault())
                                                 .OrderBy(s => s.ShiftId)
                                                 .ToList();
                var ListShift23 = ListShift02.ToList();
                int C23 = 0;
                C23 = ListShift23.Count;


                    for (int i = 0; i < C23; i++)
                    {
                        var Group = new ListAllShift();
                        //Group.id = ListShift23[i].IdServicerForMahalehByPeyk;
                        Group.ShiftID = ListShift23[i].ShiftId;
                        Group.ShiftText = ListShift23[i].ShiftT;
                        Group.Type_Erore = "";
                    ListAll.Add(Group);


                    }

                var json3 = JsonConvert.SerializeObject(new
                {
                    Myshift = ListAll
                });

                JObject json53 = JObject.Parse(json3);

                session.Close();
                return Ok(json53);
            }

            var ListShift0 = ListShift.ToList();
            var json = JsonConvert.SerializeObject(new
            {
                Myshift = ListAll
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);



        }

        [HttpGet]
        [Route("api/G1/GetListTNazarat/{Unique_code},{Services_ID}")]
        public IHttpActionResult GetListTNazarat(string Unique_code, long Services_ID)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            #region واکشی نظرات
            var ListAll = session.Query<TNazaratForServicer>().Where(s => s.TServicer.IdServicer == Services_ID && s.TLActive.IdActive == 1)
              .Select(x =>
          new
          {
              x.TUsers.NameFamily,
              DateCreate = x.DateCreate.ToString("yyyy/MM/dd"),
              x.NumRank,
              x.TextNazar
          }).ToList();
            Int32 C_List = 0;
            C_List = ListAll.Count();
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
        [Route("api/G1/GetListAllOrders/{Unique_code},{Services_ID}")]
        public IHttpActionResult GetListAllOrders(string Unique_code, string Services_ID)
        {
            if (Services_ID == "{Services_ID}" || Services_ID == "")
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            long _Services_ID = Convert.ToInt32(Services_ID);


            System.Globalization.PersianCalendar persianCalandar =
                                                new System.Globalization.PersianCalendar();//تاریخ شمسی

            var ListOrder = session.Query<TOrder>()
                                         .Where(p => p.TServicer.IdServicer == _Services_ID && (p.TLVaziyatTahvil.IdVaziyatTahvil==8 ))
                                         .Select(x =>
           new {
               x.OrderPriceBof,
               x.OrderDate,
               //OrderDate= persianCalandar.GetYear(x.OrderDate).ToString()+"/"+ persianCalandar.GetMonth(x.OrderDate).ToString()+"/"+ persianCalandar.GetDayOfMonth(x.OrderDate).ToString(),
               x.TShift.ShiftText,
               x.TPeyks.NameFamily,
               x.PeykPrice

           });
            var ListOrder2 = ListOrder.ToList();


            var ListGroups_G = ListOrder2
                .GroupBy(a => a.OrderDate)
                .Select(
              gg => new
              {
                  _List = gg.FirstOrDefault(),
                  counts = gg.Count(),
                  sums=(gg.Sum(a=>a.OrderPriceBof ))+ (gg.Sum(a => a.PeykPrice))
              }
              ).ToList();

            int C = 0;
            C = ListGroups_G.Count;
            var ListGroupss = new List<ListAllOrders>();
            if (C != 0)
            {
                for (int i = 0; i < C; i++)
                {
                    var Group = new ListAllOrders();
                    Group.OrderPriceBof = ListGroups_G[i].sums;
                    Group.OrderDate = persianCalandar.GetYear(ListGroups_G[i]._List.OrderDate).ToString() + "/" + persianCalandar.GetMonth(ListGroups_G[i]._List.OrderDate).ToString() + "/" + persianCalandar.GetDayOfMonth(ListGroups_G[i]._List.OrderDate).ToString() ;
                    Group.IdShift = ListGroups_G[i]._List.ShiftText;
                    Group.NameFamily = ListGroups_G[i]._List.NameFamily ;
                    Group.Count = ListGroups_G[i].counts;
                    ListGroupss.Add(Group);
                }
               
            }
            if (C == 0)
            {

                var Group = new ListAllOrders();
                Group.OrderPriceBof = 0;
                Group.OrderDate = null;
                Group.IdShift = "0";
                Group.NameFamily = "0";
                Group.Count = 0;
                Group.Titels = "موردی وجود ندارد";
                ListGroupss.Add(Group);


            }
            //int ww= ListGroups_G[0]._GroupID.IdShift
            //int C_Order = 0;
            //C_Order = ListOrder2.Count();
            //if (C_Order == 0)
            //{
            //    return Ok("با پشتیبانی تماس حاصل نمایید");
            //}

            //else
            //{
                var json = JsonConvert.SerializeObject(new
                {
                    Orders = ListGroupss
                });

                JObject json5 = JObject.Parse(json);

                session.Close();
                return Ok(json5);
            //}

        }

        [HttpGet]
        [Route("api/G1/GetListServicPeyk/{Unique_code},{Peyks_ID}")]
        public IHttpActionResult GetListServicPeyk(string Unique_code, string Peyks_ID)
        {
            if (Peyks_ID == "{Services_ID}" || Peyks_ID == "")
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            long _Peyks_ID = Convert.ToInt32(Peyks_ID);

            System.Globalization.PersianCalendar persianCalandar =
                                          new System.Globalization.PersianCalendar();

            var ListOrder = session.Query<TOrder>()
                                         .Where(p => p.TPeyks.IdPeyks== _Peyks_ID && (p.TLVaziyatVarizi.IdVaziyatVarizi == 1 || p.TLVaziyatVarizi.IdVaziyatVarizi == 6 || p.TLVaziyatVarizi.IdVaziyatVarizi == 7)
                                         && (p.TLVaziyatTahvil.IdVaziyatTahvil == 2 || p.TLVaziyatVarizi.IdVaziyatVarizi == 3 ))
                                         .Select(x =>
           new {
               x.OrderPriceBof,
               x.OrderDate,
              x.TShift.ShiftText,
               x.TPeyks.NameFamily

           });
            var ListOrder2 = ListOrder.ToList();


            var ListGroups_G = ListOrder2
                .GroupBy(a => a.OrderDate)
                .Select(
              gg => new
              {
                  _List = gg.FirstOrDefault(),
                  counts = gg.Count(),
                  sums = gg.Sum(a => a.OrderPriceBof)
              }
              ).ToList();

            int C = 0;
            C = ListGroups_G.Count;
            var ListGroupss = new List<ListAllOrders>();
            if (C != 0)
            {
                for (int i = 0; i < C; i++)
                {
                    var Group = new ListAllOrders();
                    Group.OrderPriceBof = ListGroups_G[i].sums;
                    //Group.OrderDate = ListGroups_G[i]._List.OrderDate;
                    Group.OrderDate = persianCalandar.GetYear(ListGroups_G[i]._List.OrderDate).ToString() + "/" + persianCalandar.GetMonth(ListGroups_G[i]._List.OrderDate).ToString() + "/" + persianCalandar.GetDayOfMonth(ListGroups_G[i]._List.OrderDate).ToString();

                    Group.IdShift = ListGroups_G[i]._List.ShiftText;
                    Group.NameFamily = ListGroups_G[i]._List.NameFamily;
                    Group.Count = ListGroups_G[i].counts;
                    Group.Titels = "انجام شده";
                    ListGroupss.Add(Group);
                }

            }

            if (C == 0)
            {
                
                    var Group = new ListAllOrders();
                    Group.OrderPriceBof = 0;
                    Group.OrderDate = null;
                Group.IdShift = "0";
                Group.NameFamily = "0";
                Group.Count = 0;
                    Group.Titels = "موردی وجود ندارد";
                    ListGroupss.Add(Group);
             

            }
            //int ww= ListGroups_G[0]._GroupID.IdShift
         

            //else
            //{
                var json = JsonConvert.SerializeObject(new
                {
                    OrdersPeyk = ListGroupss
                });

                JObject json5 = JObject.Parse(json);

                session.Close();
                return Ok(json5);
            //}

        }

        [HttpGet]
        [Route("api/G1/GetListServicCardBank/{Unique_code},{Services_ID}")]
        public IHttpActionResult GetListServicCardBank(string Unique_code, string Services_ID)
        {
            if (Services_ID == "{Services_ID}" || Services_ID == "")
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Services_ID = Convert.ToInt32(Services_ID);

            var List = session.Query<TServicer>()
                                         .Where(p => p.IdServicer== _Services_ID && p.NumberCartBank!=null)
                                         .Select(x =>
           new {
               x.UsernameCartBank,
               x.NumberCartBank,
               x.NameBank

           });
            var List2 = List.ToList();


           
            int C = 0;
            C = List2.Count;
            var Lists = new List<ListCardBank>();
            if (C != 0)
            {
                for (int i = 0; i < C; i++)
                {
                    var Group = new ListCardBank();
                    Group.Name_User = List2[i].UsernameCartBank;
                    Group.Number = List2[i].NumberCartBank;
                    Group.Name_Bank = List2[i].NameBank;
                    Group.Titels = "";
                    Lists.Add(Group);
                }

            }

            if (C == 0)
            {

                var Group = new ListCardBank();
                Group.Name_User = "";
                Group.Number = "";
                Group.Name_Bank = "";
                Group.Titels = "کارت بانکی ثبت نشده لطفا با پشتیبانی تماس حاصل نمایید";
                Lists.Add(Group);


            }
            //int ww= ListGroups_G[0]._GroupID.IdShift

                var json = JsonConvert.SerializeObject(new
                {
                    CardBank = Lists
                });

                JObject json5 = JObject.Parse(json);

                session.Close();
                return Ok(json5);


        }

        [HttpPost]
        [Route("api/G1/GetListAddPeyForServiser/{Unique_code},{Services_ID},{Mablagh}")]
        public IHttpActionResult GetListAddPeyForServiser(string Unique_code, string Services_ID,Int32 Mablagh)
        {
            if (Services_ID == "{Services_ID}" || Services_ID == "")
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Services_ID = Convert.ToInt32(Services_ID);

            var List = session.Query<TServicer>()
                                         .Where(p => p.IdServicer == _Services_ID && p.NumberCartBank != null)
                                         .Select(x =>
           new {
               x.UsernameCartBank,
               x.NumberCartBank,
               x.NameBank

           });
            var List2 = List.ToList();



            int C = 0;
            C = List2.Count;



            var Lists = new List<ListCardBank>();
            if (C != 0)
            {
                TServicer TServicerId = new TServicer
                {
                    IdServicer = _Services_ID
                };

                TLVaziyatVarizi TLVaziyatVariziId = new TLVaziyatVarizi
                {
                    IdVaziyatVarizi = 4
                };

                TPayForServicer PayForServicer = new TPayForServicer
                {
                    TServicer = TServicerId,
                    MablaghVarizi = Mablagh,
                    DateVarizi = DateTime.Now,
                    TLVaziyatVarizi= TLVaziyatVariziId

                };
                using (ITransaction transaction = session.BeginTransaction())
                {

                    session.Save(PayForServicer);
                    transaction.Commit();
                    session.Close();
                    return Ok("مورد با موفقیت ثبت شد");
                }

            }


            //var json = JsonConvert.SerializeObject(new
            //{
            //    CardBank = Lists
            //});

            //JObject json5 = JObject.Parse(json);

            //session.Close();
            //return Ok(json5);
            session.Close();
            return Ok("خطا در ثبت با پشتیبانی تماس حاصل نمایید");

        }

        [HttpGet]
        [Route("api/G1/GetListServicSoratMali/{Unique_code},{Services_ID}")]
        public IHttpActionResult GetListServicSoratMali(string Unique_code, string Services_ID)
        {
            if (Services_ID == "{Services_ID}" || Services_ID == "")
            {
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Services_ID = Convert.ToInt32(Services_ID);

            Int32 Hajm_Karkard = 0;
           var Hajm_KarkardList = session.Query<TOrder>()
                 //.Where(p => p.TServicer.IdServicer == _Services_ID && (p.TLVaziyatTahvil.IdVaziyatTahvil == 2 || p.TLVaziyatTahvil.IdVaziyatTahvil == 3 || p.TLVaziyatTahvil.IdVaziyatTahvil == 4))
                 .Where(p => p.TServicer.IdServicer == _Services_ID && (p.TLVaziyatTahvil.IdVaziyatTahvil == 8 ))
                                             .GroupBy(s => s.TServicer.IdServicer)
                                             .Select(sums => (sums.Sum(s => s.OrderPriceBof))+ (sums.Sum(s => s.PeykPrice)))
                                             .ToList();
            if (Hajm_KarkardList.Count!=0)
            {
                Hajm_Karkard = Hajm_KarkardList[0].Value;
            }
          
            Int32 ColMablagDaryafti = 0;

            var ColMablagDaryaftiList = session.Query<TPayForServicer>()
           .Where(p => p.TServicer.IdServicer == _Services_ID && p.TLVaziyatVarizi.IdVaziyatVarizi == 1).GroupBy(s => s.TServicer.IdServicer)
                                         .Select(sums => sums.Sum(s => s.MablaghVarizi))
                                         .ToList();
            if (ColMablagDaryaftiList.Count != 0)
            {
                ColMablagDaryafti = ColMablagDaryaftiList[0].Value;
            }

            Int32  KolServis = 0;
            KolServis = session.Query<TOrder>()
                                         .Where(p => p.TServicer.IdServicer == _Services_ID && (p.TLVaziyatTahvil.IdVaziyatTahvil==8 || p.TLVaziyatTahvil.IdVaziyatTahvil == 9))
                                         .Count();


            Int32 MablaghTalab = 0;
            MablaghTalab = Hajm_Karkard - ColMablagDaryafti;


            var Lists = new List<ListSoratMali>();
                   var Group = new ListSoratMali();
                    Group.HajmKarkad = Hajm_Karkard.ToString();
                    Group.KolMablagh = ColMablagDaryafti.ToString();
                    Group.KolServic = KolServis.ToString();
                    Group.MablagTalab = MablaghTalab.ToString();
                    Group.Titels = "";
                    Lists.Add(Group);

            var json = JsonConvert.SerializeObject(new
            {
                SoratMali = Lists
            });

            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }


        [HttpGet]
        [Route("api/G1/GetListAllPayServiser/{Unique_code},{Serviser_Id}")]
        public IHttpActionResult GetListAllPayServiser(string Unique_code, string Serviser_Id)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            Int32 _Serviser_Id = Convert.ToInt32(Serviser_Id);
            var ListAllPay = from s in session.Query<TPayForServicer>()
                             where (s.TServicer.IdServicer == _Serviser_Id)
                             select new
                             {
                                 MablaghVarizi = s.MablaghVarizi,
                                 DateVarizi = s.DateVarizi,
                                 VaziyatVarizi = s.TLVaziyatVarizi.TitelsVaziyatVarizi
                             };
            var ListAllPays = ListAllPay.ToList();


            int C = 0;
            C = ListAllPays.Count;

            var ListAllPayss = new List<ListAllPay>();
            if (C == 0)
            {
                var Group = new ListAllPay();
                Group.Mablagh = 0;
                Group.Date = null;
                Group.Vaziyat = "";
                Group.Type_Erore = "موردی یافت نشد";
                ListAllPayss.Add(Group);
            }
            else
            {

                for (int i = 0; i < C; i++)
                {
                    var Group = new ListAllPay();
                    Group.Mablagh = ListAllPays[i].MablaghVarizi;
                    Group.Date = (ListAllPays[i].DateVarizi).ToString("yyyy/MM/dd");
                    Group.Vaziyat = ListAllPays[i].VaziyatVarizi;
                    Group.Type_Erore = "";
                    ListAllPayss.Add(Group);


                }
            }


            var json = JsonConvert.SerializeObject(new
            {
                //Allserviser = ListServiserZarfiyat_Mahale_
                AllPay = ListAllPayss
            });


            JObject json5 = JObject.Parse(json);

            session.Close();
            return Ok(json5);


        }

        [HttpGet]
        [Route("api/G1/GetPeyk_Logout/{Unique_code},{Phone},{Unique_device_code},{Peyk_Id}")]
        public IHttpActionResult GetPeyk_Logout(string Unique_code, string Phone, string Unique_device_code, string Peyk_Id)
        {
            if (Phone == "{Phone}" || Unique_device_code == "{Unique_device_code}" || Peyk_Id == "Peyk_Id")
            {
                return Ok("مقادیر خالی");
            }
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            string IsPhoneRegistered = Phone;
            int IsPhoneRegistered_Count = IsPhoneRegistered.Length;
            if (IsPhoneRegistered_Count != 11)
            {
                return Ok("شماره تلفن اشتباه می باشد");
            }
            string IsDevice_ID = Unique_device_code;
            long PeykId = Convert.ToInt64(Peyk_Id);
            long _PeykId = 0;
            long _PeykId2 = Convert.ToInt64(Peyk_Id);
            int _Activation = 00;
            ISession session = OpenNHibertnateSession.OpenSession();
            _PeykId = session.QueryOver<TPeyks>().Where(p => p.Phone == IsPhoneRegistered && p.DeviceIdLogin == IsDevice_ID).Select(p => p.IdPeyks).SingleOrDefault<long>();

            if (_PeykId != 0)
            {
                _Activation = session.QueryOver<TPeyks>().Where(p => p.IdPeyks == _PeykId && p.Activation == 1).Select(p => p.Activation).SingleOrDefault<int>();
                if (_Activation == 1)
                {
                    var emp = session.Get<TPeyks>(_PeykId);
                    emp.Activation = 2;
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(emp);
                        transaction.Commit();
                        return Ok("پیک خارج شد");
                    }
                }
                else
                {
                    return Ok("خروج غیر مجاز");
                }
            }
            else
            {
                return Ok("پیک یافت نشد");
            }
            return Ok("Error");
        }

        [HttpGet]
        [Route("api/G1/GetServiser_Logout/{Unique_code},{Phone},{Unique_device_code},{Serviser_Id}")]
        public IHttpActionResult GetServiser_Logout(string Unique_code, string Phone, string Unique_device_code, string Serviser_Id)
        {
            if (Phone == "{Phone}" || Unique_device_code == "{Unique_device_code}" || Serviser_Id == "Serviser_Id")
            {
                return Ok("مقادیر خالی");
            }
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            string IsPhoneRegistered = Phone;
            int IsPhoneRegistered_Count = IsPhoneRegistered.Length;
            if (IsPhoneRegistered_Count != 11)
            {
                return Ok("شماره تلفن اشتباه می باشد");
            }
            string IsDevice_ID = Unique_device_code;
            Int32 ServiserId = Convert.ToInt32(Serviser_Id);
            int _ServiserId = 0;
            int _Serviser2 = Convert.ToInt32(Serviser_Id);
            int _Activation = 00;
            ISession session = OpenNHibertnateSession.OpenSession();
            _ServiserId = session.QueryOver<TServicer>().Where(p => p.PhoneAdmin == IsPhoneRegistered && p.DeviceIdLogin == IsDevice_ID).Select(p => p.IdServicer).SingleOrDefault<int>();

            if (_ServiserId != 0)
            {
                _Activation = session.QueryOver<TServicer>().Where(p => p.IdServicer == _ServiserId && p.Activation == 1).Select(p => p.Activation).SingleOrDefault<int>();
                if (_Activation == 1)
                {
                    var emp = session.Get<TServicer>(_ServiserId);
                    emp.Activation = 2;
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(emp);
                        transaction.Commit();
                        return Ok("سرویس دهنده خارج شد");
                    }
                }
                else
                {
                    return Ok("خروج غیر مجاز");
                }
            }
            else
            {
                return Ok("سرویس دهنده یافت نشد");
            }
            return Ok("Error");
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

        public static string TimeAgo(DateTime dt)
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
            if (span.Minutes >= 5 || span.Minutes < 0)
                return String.Format("0");
            if (span.Minutes < 5)
                return String.Format("{0}:{1}", span.Minutes, span.Seconds);
            if (span.Seconds >= 1)
                return String.Format("0:{0}", span.Seconds);
            return string.Empty;
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
            if (span.Minutes >= 5 || span.Minutes < 0)
                return String.Format("0");
            if (span.Minutes < 5)
                return String.Format("{0}:{1}", span.Minutes, span.Seconds);
            if (span.Seconds >= 1)
                return String.Format("0:{0}", span.Seconds);
            return string.Empty;
        }

        internal class ListAllOrders
        {
            public int? OrderPriceBof { get; set; }
            public string OrderDate { get; set; }
            public string IdShift { get; set; }
            public string NameFamily { get; set; }
            public int? Count { get; set; }
            public string Titels { get; set; }

        }

        internal class ListCardBank
        {
            public string Number { get; set; }
            public string Name_User { get; set; }
            public string Name_Bank { get; set; }
            public string Titels { get; set; }

        }

        internal class ListSoratMali
        {
            public string HajmKarkad { get; set; }
            public string KolMablagh { get; set; }
            public string KolServic { get; set; }
            public string MablagTalab { get; set; }
            public string Titels { get; set; }

        }

        internal class Massege
        {
            //public string[] Sizes { get; set; }
            public string Userid { get; set; }
            public string TimeIs { get; set; }
            public string Names { get; set; }
            public string Types { get; set; }
            public Int32 Counts { get; set; }

        }


        internal class Days
        {
            public int DayID { get; set; }
            public string Date { get; set; }
            public string TitelsDay { get; set; }
            public int Active { get; set; }
            public string Titels { get; set; }

        }

        internal class ListOrder
        {
            public long IDOrder { get; set; }
            public long? ShenaseSefaresh { get; set; }
            public string Date { get; set; }
            public int? Price { get; set; }
            public string Addresses { get; set; }
            public string Phone_Address { get; set; }
            public int VaziyatId { get; set; }
            public string VaziyatTitels { get; set; }
            public int counts { get; set; }
            public string NameFamilyUser { get; set; }
            public string NoePay { get; set; }
            public string ShiftDaryaft { get; set; }
            public string ShiftTahvil { get; set; }
            public string Date_Tavil { get; set; }
            public string Titels { get; set; }
            public string Longitude { get; set; }
            public string Latitude { get; set; }
            
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

        internal class ListAllPay
        {
            public int? Mablagh { get; set; }
            public string Date { get; set; }
            public string Vaziyat { get; set; }
            public string Type_Erore { get; set; }
        }

        internal class ListAllShift
        {
            public long id { get; set; }
            public int? ShiftID { get; set; }
            public string ShiftText { get; set; }
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
    }
}


using System.Web.Http;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WebApi_swagger.Models;
using SmsIrRestful;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApi_swagger.Controllers
{
    public class TUsersController : ApiController
    {
        ISession session = OpenNHibertnateSession.OpenSession();
        string _LongTime = "0";

        [HttpGet]
        [Route("api/G1/GetCreateTUsers_Login/{Unique_code},{Phone},{Unique_device_code},{UsersId},{sp}")]
        public IHttpActionResult GetCreateTUsers_Login(string Unique_code,string Phone, string Unique_device_code, string UsersId, Int16 sp  )
        {
            if (Phone == "{Phone}" || Unique_device_code == "{Unique_device_code}" || UsersId == "Id")
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
        Counts=0,
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
            long UserId = Convert.ToInt64(UsersId);
            long _UserId = 0;
            long _UserIdForis_user = 0;
            //long _ActivationForis_user = 0;
            int _ActivationForis_user = 0;
            long _UserId2 = Convert.ToInt64(UsersId);
            int _Activation = 00;
            int _IsDeleted = 0;//اگر مساوی 0 باشد یعنی کاربر کدی درخواست نداده
            string _LongTime_Khatakar = "0";
            string _Device_ID = Unique_device_code;
            string _Name = "0";
            _UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered).Select(p => p.IdUsrer).SingleOrDefault<long>();

            if (_UserId != 0) // یعنی کاربر قبلا ثبت نام کرده
            {

                _UserIdForis_user = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered && p.IdUsrer == UserId).Select(p => p.IdUsrer).SingleOrDefault<long>();
                //_ActivationForis_user = session.QueryOver<TActivationCode>().Where(p => p.TUser.UserId == _UserId && p.DeviceId == IsDevice_ID).Select(p => p.AcId).SingleOrDefault<long>();
                _ActivationForis_user = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId && p.DeviceIdLogin == IsDevice_ID).Select(p => p.Activation).SingleOrDefault<int>();//  این کاربر با کدام گوشی

                _Activation = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId).Select(p => p.Activation).SingleOrDefault<int>();
                _IsDeleted = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId).Select(p => p.IsDeleted).SingleOrDefault<int>();
                if (_UserIdForis_user != 0 && _ActivationForis_user == 1 && _UserId2 == _UserId)
                {

                    _Name = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId).Select(p => p.NameFamily).SingleOrDefault<string>();
                    if (_Name != "0")
                    {
                        session.Close();
                        //return Ok("ورد به پنل نان با نام");
                        #region نمایش به صورت jason
                        Massege massege = new Massege
                        {
                            Userid = _UserId.ToString(),
                            TimeIs = "",
                            Names = _Name,
                            Counts = 0,
                            Types = "",
                           
                           
                        };
                        var carss = massege;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    Okname = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss);
                        #endregion
                        //return Ok(_Name + "_UsId:" + _UserId);
                    }

                    if (_Name == "0")
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege = new Massege
                        {
                           
                            Userid = "",
                            TimeIs = "",
                            Names = "",
                            Counts = 0,
                            Types = "Set Name for User"
                        };
                        var carss = massege;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    Noname = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss);
                        #endregion
                        //return Ok("Set Name for User");
                    }
                }

            }




            Int64 _End_AcId = 0;
            DateTime _IS_time;


            int _Is_Ins = 0;///اگر مساوی 1 شود کاربر میتواند کد درخواس بدهد

            var _End_AcIdList = session.Query<TActivationCode>().Where(p => p.DeviceId == IsDevice_ID)
                 .Select(x => new { x.IdActivationCode, x.CodeGenerationTime })
                 .OrderByDescending(x => x.IdActivationCode).Take(1).ToList();
            if (_End_AcIdList.Count != 0)
            {
                _End_AcId = _End_AcIdList[0].IdActivationCode;
            }

            //_End_AcId = session.QueryOver<TActivationCode>().Where(p => p.DeviceId == IsDevice_ID).Select(Projections.Max<TActivationCode>(x => x.AcId)).SingleOrDefault<Int64>();
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
                            Userid = "",
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
                             Userid = "",
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
                //return Ok(_End_AcId);
            }

            //یوزر وجود دارد
            if ((_UserId != 0))
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
                                    Userid = "",
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
                                emp.TUsers = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered).SingleOrDefault();
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

                                var emp2 = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered).SingleOrDefault();
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
                                    TemplateId = 36207,
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
                                        Userid = "",
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
                                        Userid = "",
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
                            emp.TUsers = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered).SingleOrDefault();
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

                            var emp2 = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered).SingleOrDefault();
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
                                TemplateId = 36207,
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
                                    Userid = "",
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
                                    Userid = "",
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
                            Userid = "",
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
                    else if (_Activation == 1 && _UserId2 == _UserId)
                    {

                        //string _Name = "0";
                        _Name = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId).Select(p => p.NameFamily).SingleOrDefault<string>();
                        if (_Name != "0")
                        {
                            session.Close();
                            //return Ok("ورد به پنل نان با نام");
                            #region نمایش به صورت jason
                            Massege massege6 = new Massege
                            {
                                Userid = _UserId.ToString(),
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
                                Userid = "",
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
                    Userid = "",
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
            //////////////////////////یوزر وجود ندارد
            else
            {
                //return Ok("دستگاه ناشناس");

                if (_Is_Ins == 1)
                {
                    //return Ok("دفعه 1");

                    var _ins_user = ins_user(IsPhoneRegistered, _Device_ID, _UserId);
                    string finalString = _ins_user;
                    if (_ins_user != "0")
                    {
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
                            TemplateId = 36207,
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
                            session.Close();
                            #region نمایش به صورت jason
                            Massege massege = new Massege
                            {
                                Userid = "",
                                TimeIs = "",
                                Names = "",
                                Counts = 0,
                                Types = "کاربر به تازگی ثبت نام کرده یا خارج شده پس ارسال کد"
                            };
                            var carss = massege;
                            //var json2 = JsonConvert.SerializeObject(new
                            //{
                            //    Error = carss
                            //});
                            //JObject json21 = JObject.Parse(json2);
                            return Ok(carss);
                            #endregion
                            //return Ok("کاربر به تازگی ثبت نام کرده یا خارج شده پس ارسال کد");
                        }
                        else
                        {
                            session.Close();
                            #region نمایش به صورت jason
                            Massege massege = new Massege
                            {
                                Userid = "",
                                TimeIs = "",
                                Names = "",
                                Counts = 0,
                                Types = "خطای ارسال پیام"
                            };
                            var carss = massege;
                            //var json2 = JsonConvert.SerializeObject(new
                            //{
                            //    Error = carss
                            //});
                            //JObject json21 = JObject.Parse(json2);
                            return Ok(carss);
                            #endregion
                            //return Ok("خظای ارسال پیام");

                        }


                    }
                    else
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege = new Massege
                        {
                            Userid = "",
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
                        //return Ok("error");
                    }
                }
                else
                {
                    //DateTime _IsDateTime;
                    //_IsDateTime = session.QueryOver<TActivationCode>().Where(p => p.AcId == _End_AcId).Select(x => x.CodeGenerationTime).SingleOrDefault<DateTime>();
                    ////_IsDateTime = (session.QueryOver<TActivationCode>().Where(p => p.Tell == IsPhoneRegistered).Select(p => p.CodeGenerationTime).SingleOrDefault<DateTime>());
                    ////DateTime now = DateTime.Now;
                    ////DateTime TimeNow = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                    //var _Long_Time = TimeAgo(_IsDateTime);

                    if (_LongTime == "0" && _Is_Ins == 1 && _LongTime_Khatakar == "0")
                    {
                        var _ins_user = ins_user(IsPhoneRegistered, _Device_ID, _UserId);
                        string finalString = _ins_user;
                        if (_ins_user != "0")
                        {
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
                                TemplateId = 36207,
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
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege = new Massege
                                {
                                    Userid = "",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "کاربر به تازگی ثبت نام کرده یا خارج شده پس ارسال کد"
                                };
                                var carss = massege;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    Error = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss);
                                #endregion
                                //return Ok("کاربر به تازگی ثبت نام کرده یا خارج شده پس ارسال کد");
                            }
                            else
                            {
                                session.Close();
                                #region نمایش به صورت jason
                                Massege massege = new Massege
                                {
                                    Userid = "",
                                    TimeIs = "",
                                    Names = "",
                                    Counts = 0,
                                    Types = "خطای ارسال پیام"
                                };
                                var carss = massege;
                                //var json2 = JsonConvert.SerializeObject(new
                                //{
                                //    Error = carss
                                //});
                                //JObject json21 = JObject.Parse(json2);
                                return Ok(carss);
                                #endregion
                                //return Ok("خظای ارسال پیام");
                            }
                        }
                        else
                        {
                            session.Close();
                            #region نمایش به صورت jason
                            Massege massege = new Massege
                            {
                                Userid = "",
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
                            //return Ok("error");
                        }
                    }
                    else
                    {
                        if (_LongTime_Khatakar != "0")
                        {
                            session.Close();
                            #region نمایش به صورت jason
                            Massege massege = new Massege
                            {
                                Userid = "",
                                TimeIs = _LongTime_Khatakar,
                                Names = "",
                                Counts = 0,
                                Types = "K"
                                
                            };
                            var carss = massege;
                            //var json2 = JsonConvert.SerializeObject(new
                            //{
                            //    KhatakarTime = carss
                            //});
                            //JObject json21 = JObject.Parse(json2);
                            return Ok(carss);
                            #endregion
                            //return Ok("K:" + _LongTime_Khatakar);
                        }
                        else if (_LongTime != "0")
                        {
                            session.Close();
                            #region نمایش به صورت jason
                            Massege massege = new Massege
                            {
                                Userid = "",
                                TimeIs = _LongTime,
                                Names = "",
                                Counts = 0,
                                Types = "D"
                                
                            };
                            var carss = massege;
                            //var json2 = JsonConvert.SerializeObject(new
                            //{
                            //    DorostkarTime = carss
                            //});
                            //JObject json21 = JObject.Parse(json2);
                            return Ok(carss);
                            #endregion
                            //return Ok("D:" + _LongTime);
                        }
                        session.Close();
                        #region نمایش به صورت jason
                            Massege massege4 = new Massege
                            {
                                Userid = "",
                                TimeIs = "",
                                Names = "",
                                Counts = 0,
                                Types = "error"
                            };
                            var carss4 = massege4;
                            //var json24 = JsonConvert.SerializeObject(new
                            //{
                            //    Error = carss4
                            //});
                            //JObject json214 = JObject.Parse(json24);
                            return Ok(carss4);
                            #endregion
                        //return Ok("error");
                    }
                }
            }
        }

        [HttpPost]
        [Route("api/G1/SetNameForUser/{Unique_code},{Phone},{Name}")]
        public IHttpActionResult SetNameForUser(string Unique_code, string Phone, string Name)
        {
            if (Unique_code == "{Unique_code}" || Phone == "Phone" || Unique_code == "" || Phone == "")
            {
                return Ok("مقادیر خالی");
            }
            if (Unique_code != "PlatformV199325694")
            {
                #region نمایش به صورت jason
                Massege massege92 = new Massege
                {
                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "کلید نادرست"
                };
                var carss92 = massege92;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    khatakar = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss92);
                #endregion
                //return Ok("کلید نادرست");
            }
            ISession session = OpenNHibertnateSession.OpenSession();
            
            #region جستجو و آپدیت نام
            long UserId_ = 0;
            UserId_ = session.QueryOver<TUsers>().Where(p => p.Tell == Phone ).Select(p => p.IdUsrer).SingleOrDefault<long>();
            if (UserId_ == 0)
            {
                #region نمایش به صورت jason
                Massege massege91 = new Massege
                {
                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "کاربر یافت نشد"
                };
                var carss91 = massege91;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    khatakar = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss91);
                #endregion
                //return Ok("کاربر یافت نشد");

            }

            else
            {
                var emp = session.QueryOver<TUsers>().Where(p => p.IdUsrer == UserId_).SingleOrDefault();
                emp.NameFamily = Name;
                
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(emp);
                    transaction.Commit();
                }
            }
            #endregion

            session.Close();
            #region نمایش به صورت jason
            Massege massege9 = new Massege
            {
                Userid = UserId_.ToString(),
                TimeIs = "",
                Names = "",
                Counts = 0,
                Types = " مورد ثبت شد هدایت به صفحه اصلی"
            };
            var carss9 = massege9;
            //var json2 = JsonConvert.SerializeObject(new
            //{
            //    khatakar = carss
            //});
            //JObject json21 = JObject.Parse(json2);
            return Ok(carss9);
            #endregion
            //return Ok("مورد ثبت شد");

        }

        [HttpGet]
        [Route("api/G1/GetCheckCode/{Unique_code},{Phone},{Unique_device_code},{code}")]
        public IHttpActionResult GetCheckCode(string Unique_code, string Phone, string Unique_device_code, string code)
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
            _UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered).Select(p => p.IdUsrer).SingleOrDefault<long>();
            _TActivationCodeId = session.QueryOver<TActivationCode>().Where(p => p.TUsers.IdUsrer == _UserId).Select(p => p.IdActivationCode).SingleOrDefault<long>();
            _IsDevice_ID = session.QueryOver<TActivationCode>().Where(p => p.TUsers.IdUsrer == _UserId).Select(p => p.DeviceId).SingleOrDefault<string>();

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
            else if (_UserId != 0 && _IsDevice_ID == IsDevice_ID)
            {
                //_Activation = session.QueryOver<TUser>().Where(p => p.UserId == _UserId).Select(p => p.Activation).SingleOrDefault<int>();
                _IsDeleted = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId).Select(p => p.IsDeleted).SingleOrDefault<int>();
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

                            string _UserId2 = Convert.ToString(_UserId);
                            ITransaction tx_IsDeleted = session.BeginTransaction();
                            string hqlVersionedUpdate_IsDeleted = "update TUsers set IsDeleted = :IsDeleted2  where ID_Usrer = :UserIds";
                            int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                    .SetString("IsDeleted2", "1")
                                    .SetString("UserIds", _UserId2)
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
                        string hqlVersionedUpdate_user = "update TUsers set IsDeleted = :IsDeleted2 ,Device_ID_login=:Device_ID_login2 , Activation=:Activation2 where ID_Usrer = :ID";
                        int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_user)
                                .SetString("IsDeleted2", "0")
                                .SetString("Activation2", "1")
                                .SetString("Device_ID_login2", IsDevice_ID)
                                .SetString("ID", Convert.ToString(_UserId))
                                //.SetDateTime()
                                .ExecuteUpdate();
                        tx_user.Commit();
                        //session.Close();
                        //کد آپدیت
                        string UsId = Convert.ToString(_UserId);

                        string _Name_Family = "0";
                        _Name_Family = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId).Select(p => p.NameFamily).SingleOrDefault<string>();
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
                            string _UserId2 = Convert.ToString(_UserId);
                            ITransaction tx_IsDeleted = session.BeginTransaction();
                            string hqlVersionedUpdate_IsDeleted = "update TUsers set IsDeleted = :IsDeleted2  where ID_Usrer = :UserIds";
                            int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                    .SetString("IsDeleted2", "0")
                                    .SetString("UserIds", _UserId2)
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

                            string _UserId2 = Convert.ToString(_UserId);
                            ITransaction tx_IsDeleted = session.BeginTransaction();
                            string hqlVersionedUpdate_IsDeleted = "update TUsers set IsDeleted = :IsDeleted2  where ID_Usrer = :UserIds";
                            int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_IsDeleted)
                                    .SetString("IsDeleted2", "1")
                                    .SetString("UserIds", _UserId2)
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

            else if (_UserId == 0)
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
            //return Ok("مشکل دوباره ارسال شود");

        }

        [HttpGet]
        [Route("api/G1/GetChekUsers_Time/{Unique_code},{Unique_device_code}")]
        public IHttpActionResult GetChekUsers_Time(string Unique_code,string Unique_device_code)
        {
            if (Unique_device_code == "{Unique_device_code}")
            {
                session.Close();
                return Ok("مقادیر خالی");
            }
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            string IsDevice_ID = Unique_device_code;
            Int64 _End_AcId = 0;
            var _End_AcIdList = session.Query<TActivationCode>().Where(p => p.DeviceId == IsDevice_ID)
                 .Select(x => new { x.IdActivationCode })
                 .OrderByDescending(x => x.IdActivationCode).Take(1).ToList();
            //_End_AcId = _End_AcIdList[0].AcId;

            if (_End_AcIdList.Count != 0)
            {
                _End_AcId = _End_AcIdList[0].IdActivationCode;
            }

            if (_End_AcId != 0)
            {
                Int64 _IsUserId = 0;
                int _IsDeleted = 0;
                string _LongTime_Khatakar = "0";
                //_IsDeleted = session.QueryOver<TActivationCode>().Where(p => p.DeviceId == IsDevice_ID).Select(p => p.TUser.IsDeleted).SingleOrDefault<int>();
                _IsUserId = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _End_AcId).Select(p => p.TUsers.IdUsrer).SingleOrDefault<Int64>();
                _IsDeleted = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _IsUserId).Select(p => p.IsDeleted).SingleOrDefault<int>();
                DateTime _IS_time = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _End_AcId).Select(x => x.CodeGenerationTime).SingleOrDefault<DateTime>();
                DateTime _Is_Deleted_Time = _IS_time;
                _Is_Deleted_Time = session.QueryOver<TActivationCode>().Where(p => p.IdActivationCode == _End_AcId).Select(x => x.IsDeletedTime).SingleOrDefault<DateTime>();
                if (_Is_Deleted_Time != _IS_time)
                {
                    _LongTime_Khatakar = TimeAgo(_Is_Deleted_Time);
                }
                string _LongTime_5min = TimeAgo(_IS_time);
                //string _LongTime_3min = TimeAgo(_IS_time);

                if (_IsDeleted == 0)
                {
                    if (_LongTime_5min != "0")
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege3 = new Massege
                        {
                            Userid = "",
                            TimeIs = _LongTime_5min,
                            Names = "",
                            Counts = 0,
                            Types = "D"
                        };
                        var carss3 = massege3;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    DorostkarChek = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss3);
                        #endregion
                        //return Ok("D:" + _LongTime_5min);
                    }
                    else if (_LongTime_5min == "0")
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege2 = new Massege
                        {
                            Userid = "",
                            TimeIs = "",
                            Names = "",
                            Counts = 0,
                            Types = "کاربر درست کار ورود به اکتیویتی لاگین"
                        };
                        var carss2 = massege2;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    DorostkarChek = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss2);
                        #endregion
                        //return Ok("کاربر درست کار ورود به اکتیویتی لاگین");
                    }
                }
                else if (_IsDeleted == 1)
                {
                    if (_LongTime_Khatakar != "0")
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege1 = new Massege
                        {
                            Userid = "",
                            TimeIs = _LongTime_Khatakar,
                            Names = "",
                            Counts = 0,
                            Types = "K"
                        };
                        var carss1 = massege1;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    KhatakarChek = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss1);
                        #endregion
                        //return Ok("K:" + _LongTime_Khatakar);
                    }
                    else if (_LongTime_Khatakar == "0")
                    {
                        session.Close();
                        #region نمایش به صورت jason
                        Massege massege9 = new Massege
                        {
                            Userid = "",
                            TimeIs = "",
                            Names = "",
                            Counts = 0,
                            Types = "کاربر خطاکار کار ورود به اکتیویتی لاگین"
                        };
                        var carss9 = massege9;
                        //var json2 = JsonConvert.SerializeObject(new
                        //{
                        //    KhatakarChek = carss
                        //});
                        //JObject json21 = JObject.Parse(json2);
                        return Ok(carss9);
                        #endregion
                        //return Ok("کاربر خطاکار کار ورود به اکتیویتی لاگین");
                    }
                }
            }

            else if (_End_AcId == 0)
            {
                session.Close();
                #region نمایش به صورت jason
                Massege massege9 = new Massege
                {
                    Userid = "",
                    TimeIs = "",
                    Names = "",
                    Counts = 0,
                    Types = "ورود به اکتیویتی ورود شماره تلفن"
                };
                var carss9 = massege9;
                //var json2 = JsonConvert.SerializeObject(new
                //{
                //    KhatakarChek = carss
                //});
                //JObject json21 = JObject.Parse(json2);
                return Ok(carss9);
                #endregion
                //return Ok("ورود به اکتیویتی ورود شماره تلفن");
            }
            session.Close();
            #region نمایش به صورت jason
            Massege massege = new Massege
            {
                Userid = "",
                TimeIs = "",
                Names = "",
                Counts = 0,
                Types = "error"
            };
            var carss = massege;
            //var json2 = JsonConvert.SerializeObject(new
            //{
            //    KhatakarChek = carss
            //});
            //JObject json21 = JObject.Parse(json2);
            return Ok(carss);
            #endregion
            //return Ok("error");
        }

        //[HttpGet]
        //[Route("api/G1/GetUsers_Logout/{Unique_code},{Phone},{Unique_device_code},{Id}")]
        //public IHttpActionResult GetUsers_Logout(string Unique_code, string Phone, string Unique_device_code, string Id)
        //{
        //    if (Phone == "{Phone}" || Unique_device_code == "{Unique_device_code}" || Id == "Id")
        //    {
        //        session.Close();
        //        return Ok("مقادیر خالی");
        //    }
        //    if (Unique_code != "PlatformV199325694")
        //    {
        //        return Ok("کلید نادرست");
        //    }
        //    string IsPhoneRegistered = Phone;
        //    int IsPhoneRegistered_Count = IsPhoneRegistered.Length;
        //    if (IsPhoneRegistered_Count != 11)
        //    {
        //        session.Close();
        //        return Ok("شماره تلفن اشتباه می باشد");
        //    }
        //    string IsDevice_ID = Unique_device_code;
        //    long UserId = Convert.ToInt64(Id);
        //    long _UserId = 0;
        //    long _UserId2 = Convert.ToInt64(Id);
        //    int _Activation = 00;
        //    _UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered && p.DeviceIdLogin == IsDevice_ID).Select(p => p.IdUsrer).SingleOrDefault<long>();

        //    if (_UserId != 0)
        //    {
        //        _Activation = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId && p.Activation == 1).Select(p => p.Activation).SingleOrDefault<int>();
        //        if (_Activation == 1)
        //        {
        //            var emp = session.Get<TUsers>(_UserId);
        //            emp.Activation = 2;
        //            using (ITransaction transaction = session.BeginTransaction())
        //            {
        //                session.Save(emp);
        //                transaction.Commit();
        //                return Ok("کاربر خارج شد");
        //            }
        //        }
        //        else
        //        {
        //            return Ok("خروج غیر مجاز");
        //        }
        //    }
        //    else
        //    {
        //        return Ok("کاربر یافت نشد");
        //    }
        //    return Ok("Error");
        //}

        [HttpGet]
        [Route("api/G1/GetUpdate_Name_Family/{Unique_code},{Phone},{Name_Family},{Id},{Law_Id}")]
        public IHttpActionResult GetUpdate_Name_Family(string Unique_code, string Phone, string Name_Family, string Id, string Law_Id)
        {

            if (Phone == "{Phone}" || Name_Family == "{Name_Family}" || Id == "{Id}" || Law_Id == "{Law_Id}" || Name_Family == "{}" || Name_Family == "" || Name_Family == " ")
            {
                session.Close();
                return Ok("مقادیر خالی");
            }

            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            string IsPhoneRegistered = Phone;
            long UserId = Convert.ToInt64(Id);
            long _UserId = 0;
            string _Name_Family = "0";
            _UserId = session.QueryOver<TUsers>().Where(p => p.Tell == IsPhoneRegistered && p.IdUsrer == UserId).Select(p => p.IdUsrer).SingleOrDefault<long>();
            if (_UserId != 0)
            {
                _Name_Family = session.QueryOver<TUsers>().Where(p => p.IdUsrer == _UserId).Select(p => p.NameFamily).SingleOrDefault<string>();
                //if (_Name_Family=="0")
                //{
                ///آپدیت نام و نام خانوادگی
                ITransaction tx_user = session.BeginTransaction();
                string hqlVersionedUpdate_user = "update TUsers set Name_Family = :Name_Family2 , LastLawAccepted =:LastLawAccepted2 where UserId = :ID";
                int updatedEntities_user = session.CreateQuery(hqlVersionedUpdate_user)
                        .SetString("Name_Family2", Name_Family)
                        .SetString("LastLawAccepted2", Convert.ToString(Law_Id))
                        .SetString("ID", Convert.ToString(_UserId))
                        //.SetDateTime()
                        .ExecuteUpdate();
                tx_user.Commit();
                session.Close();
                return Ok("آپدیت شد");
                //}
                //else
                //{
                //    session.Close();
                //    return Ok("اطلاعات نام و نام خانوادگی موجود است ورود به اکتیویتی نان");
                //}

            }
            else
            {
                session.Close();
                return Ok("کاربر وجود ندارد");
            }

        }

        public static string ins_user(string IsPhoneRegistered, string _Device_ID, long _UserId)
        {
            ///ایجاد کد رندم
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
            #region //کاربر وجود ندارد
            if (_UserId == 0)
            {

                TUsers User = new TUsers
                {
                    Tell = IsPhoneRegistered,
                    Activation = 0,
                    TLaw = null,
                    NameFamily = "0"
                };
                TActivationCode ActivationCode = new TActivationCode
                {
                    Tell = IsPhoneRegistered,
                    DeviceId = _Device_ID,
                    TUsers = User,///تو جاهایی که داریم جوین میکنیم باید یک ایست از او جدول وجود داشته باشد
                    CodeGenerationTime = Time_Now,
                    Code = finalString
                };

                ISession session = OpenNHibertnateSession.OpenSession();
                try
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {

                        session.Save(User);
                        session.Save(ActivationCode);
                        transaction.Commit();
                    }
                    //return Ok("ادامه مراحل ارسال پیامک");
                    session.Close();
                    return String.Format(finalString);
                }
                catch (Exception e) { return String.Format("0"); }
                System.Environment.Exit(0);
                session.Close();
            }
            #endregion
            else if (_UserId != 0)
            {
                ISession session = OpenNHibertnateSession.OpenSession();
                long _Activation_Device_ID = 0;
                _Activation_Device_ID = session.QueryOver<TActivationCode>().Where(p => p.DeviceId == _Device_ID).Select(p => p.IdActivationCode).SingleOrDefault<long>();
                if (_Activation_Device_ID == 0)
                {
                    TUsers TUserId = new TUsers
                    {
                        IdUsrer = _UserId
                    };
                    TActivationCode ActivationCode = new TActivationCode
                    {
                        Tell = IsPhoneRegistered,
                        DeviceId = _Device_ID,
                        TUsers = TUserId,///تو جاهایی که داریم جوین میکنیم باید یک ایست از او جدول وجود داشته باشد
                        CodeGenerationTime = Time_Now,
                        Code = finalString
                    };
                    try
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(ActivationCode);
                            transaction.Commit();
                        }
                        //return Ok("ادامه مراحل ارسال پیامک");
                        session.Close();
                        return String.Format(finalString);
                    }
                    catch (Exception e) { return String.Format("0"); }
                    System.Environment.Exit(0);
                    session.Close();
                }

                else if (_Activation_Device_ID != 0)
                {
                    TUsers TUserId = new TUsers
                    {
                        IdUsrer = _UserId
                    };
                    var emp = session.Get<TActivationCode>(_Activation_Device_ID);
                    emp.Tell = IsPhoneRegistered;
                    emp.TUsers.IdUsrer = _UserId;
                    emp.CodeGenerationTime = Time_Now;
                    emp.Code = finalString;
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(emp);
                        transaction.Commit();
                    }
                }

            }

            return String.Format("");
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

        internal class Massege
        {
            //public string[] Sizes { get; set; }
            public string Userid { get; set; }
            public string TimeIs { get; set; }
            public string Names { get; set; }
            public string Types { get; set; }
            public Int32 Counts { get; set; }

        }
    }
}

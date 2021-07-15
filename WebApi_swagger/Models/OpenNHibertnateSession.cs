using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_swagger.Models
{
    public class OpenNHibertnateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath
                (@"~\Models\NHibernate\nhibernate.configuration.xml");
            configuration.Configure(configurationPath);

          
            var TActivationCodeConfigurationFile = HttpContext.Current.Server.MapPath
               (@"~\Models\NHibernate\TActivationCode.hbm.xml");
            configuration.AddFile(TActivationCodeConfigurationFile);

            var TAddressesConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TAddresses.hbm.xml");
            configuration.AddFile(TAddressesConfigurationFile);

            var TCartForPayServicerConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TCartForPayServicer.hbm.xml");
            configuration.AddFile(TCartForPayServicerConfigurationFile);

            var TGroupConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TGroup.hbm.xml");
            configuration.AddFile(TGroupConfigurationFile);

            var TGroupProductConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TGroupProduct.hbm.xml");
            configuration.AddFile(TGroupProductConfigurationFile);

            var TLActiveConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLActive.hbm.xml");
            configuration.AddFile(TLActiveConfigurationFile);

            var TLawConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLaw.hbm.xml");
            configuration.AddFile(TLawConfigurationFile);

            var TLCityConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLCity.hbm.xml");
            configuration.AddFile(TLCityConfigurationFile);

            var TLMahalehConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLMahaleh.hbm.xml");
            configuration.AddFile(TLMahalehConfigurationFile);

            var TLNoeHPeykConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLNoeHPeyk.hbm.xml");
            configuration.AddFile(TLNoeHPeykConfigurationFile);

            var TLNoeVasilehConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLNoeVasileh.hbm.xml");
            configuration.AddFile(TLNoeVasilehConfigurationFile);

            var TLOstanConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLOstan.hbm.xml");
            configuration.AddFile(TLOstanConfigurationFile);


            var TLVaziyatVariziConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLVaziyatVarizi.hbm.xml");
            configuration.AddFile(TLVaziyatVariziConfigurationFile);

            var TNazaratForServicerConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TNazaratForServicer.hbm.xml");
            configuration.AddFile(TNazaratForServicerConfigurationFile);

            var TNoeKhedmatConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TNoeKhedmat.hbm.xml");
            configuration.AddFile(TNoeKhedmatConfigurationFile);

            var TNoeProductConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TNoeProduct.hbm.xml");
            configuration.AddFile(TNoeProductConfigurationFile);

            var TPayForServicerConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPayForServicer.hbm.xml");
            configuration.AddFile(TPayForServicerConfigurationFile);

            var TPeyksConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPeyks.hbm.xml");
            configuration.AddFile(TPeyksConfigurationFile);

            var TServicerConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TServicer.hbm.xml");
            configuration.AddFile(TServicerConfigurationFile);

            var TServicerForMahalehConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TServicerForMahaleh.hbm.xml");
            configuration.AddFile(TServicerForMahalehConfigurationFile);

            var TServicerForMahalehByPeykConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TServicerForMahalehByPeyk.hbm.xml");
            configuration.AddFile(TServicerForMahalehByPeykConfigurationFile);

            var TShiftConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TShift.hbm.xml");
            configuration.AddFile(TShiftConfigurationFile);

            var TUserRolConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TUserRol.hbm.xml");
            configuration.AddFile(TUserRolConfigurationFile);

            var TUsersConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TUsers.hbm.xml");
            configuration.AddFile(TUsersConfigurationFile);

            var TVersionAppConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TVersionApp.hbm.xml");
            configuration.AddFile(TVersionAppConfigurationFile);

            var TZarfiyatShiftPeyksConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TZarfiyatShiftPeyks.hbm.xml");
            configuration.AddFile(TZarfiyatShiftPeyksConfigurationFile);

            var TZarfiyatShiftServicerConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TZarfiyatShiftServicer.hbm.xml");
            configuration.AddFile(TZarfiyatShiftServicerConfigurationFile);

            var TCGoZarfiyatShServiserConfigurationFile = HttpContext.Current.Server.MapPath
          (@"~\Models\NHibernate\TCGoZarfiyatShServiser.hbm.xml");
            configuration.AddFile(TCGoZarfiyatShServiserConfigurationFile);

            var TLNoePayConfigurationFile = HttpContext.Current.Server.MapPath
          (@"~\Models\NHibernate\TLNoePay.hbm.xml");
            configuration.AddFile(TLNoePayConfigurationFile);

            var TLVaziyatSabadConfigurationFile = HttpContext.Current.Server.MapPath
          (@"~\Models\NHibernate\TLVaziyatSabad.hbm.xml");
            configuration.AddFile(TLVaziyatSabadConfigurationFile);

            var TTemporderConfigurationFile = HttpContext.Current.Server.MapPath
         (@"~\Models\NHibernate\TTemporder.hbm.xml");
            configuration.AddFile(TTemporderConfigurationFile);

            var TSabadConfigurationFile = HttpContext.Current.Server.MapPath
         (@"~\Models\NHibernate\TSabad.hbm.xml");
            configuration.AddFile(TSabadConfigurationFile);

            var TCGoZarfiyatShPeyksConfigurationFile = HttpContext.Current.Server.MapPath
         (@"~\Models\NHibernate\TCGoZarfiyatShPeyks.hbm.xml");
            configuration.AddFile(TCGoZarfiyatShPeyksConfigurationFile);

            var THolidayConfigurationFile = HttpContext.Current.Server.MapPath
      (@"~\Models\NHibernate\THoliday.hbm.xml");
            configuration.AddFile(THolidayConfigurationFile);

            var TOrderConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TOrder.hbm.xml");
            configuration.AddFile(TOrderConfigurationFile);

            
                 var TLVaziyatTahvilConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TLVaziyatTahvil.hbm.xml");
            configuration.AddFile(TLVaziyatTahvilConfigurationFile);

            var TTamasbamaConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TTamasbama.hbm.xml");
            configuration.AddFile(TTamasbamaConfigurationFile);

            var TAllQuestionsConfigurationFile = HttpContext.Current.Server.MapPath
    (@"~\Models\NHibernate\TAllQuestions.hbm.xml");
            configuration.AddFile(TAllQuestionsConfigurationFile);

            var TCodeTakhfifConfigurationFile = HttpContext.Current.Server.MapPath
   (@"~\Models\NHibernate\TCodeTakhfif.hbm.xml");
            configuration.AddFile(TCodeTakhfifConfigurationFile);


            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();



        }
    }
}
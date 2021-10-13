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

            var TCodeDiscountConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TCodeDiscount.hbm.xml");
            configuration.AddFile(TCodeDiscountConfigurationFile);

            var THomeAppConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\THomeApp.hbm.xml");
            configuration.AddFile(THomeAppConfigurationFile);

            var THomeProductsConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\THomeProducts.hbm.xml");
            configuration.AddFile(THomeProductsConfigurationFile);

            var THomeProductsTempSabadConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\THomeProductsTempSabad.hbm.xml");
            configuration.AddFile(THomeProductsTempSabadConfigurationFile);

            var TLActiveConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLActive.hbm.xml");
            configuration.AddFile(TLActiveConfigurationFile);

            var TLCityConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLCity.hbm.xml");
            configuration.AddFile(TLCityConfigurationFile);

            var TLMahalehConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLMahaleh.hbm.xml");
            configuration.AddFile(TLMahalehConfigurationFile);

            var TLNoePayConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLNoePay.hbm.xml");
            configuration.AddFile(TLNoePayConfigurationFile);

            var TLOstanConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLOstan.hbm.xml");
            configuration.AddFile(TLOstanConfigurationFile);

            var TLPortionPizzaConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLPortionPizza.hbm.xml");
            configuration.AddFile(TLPortionPizzaConfigurationFile);


            var TLShowPriorityConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLShowPriority.hbm.xml");
            configuration.AddFile(TLShowPriorityConfigurationFile);

            var TLTransportConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLTransport.hbm.xml");
            configuration.AddFile(TLTransportConfigurationFile);

            var TLVaziyatSabadConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLVaziyatSabad.hbm.xml");
            configuration.AddFile(TLVaziyatSabadConfigurationFile);

            var TLVaziyatVariziConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TLVaziyatVarizi.hbm.xml");
            configuration.AddFile(TLVaziyatVariziConfigurationFile);

            var TPeyksConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPeyks.hbm.xml");
            configuration.AddFile(TPeyksConfigurationFile);

            var TPizzaCheeseConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaCheese.hbm.xml");
            configuration.AddFile(TPizzaCheeseConfigurationFile);

            var TPizzaCrustConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaCrust.hbm.xml");
            configuration.AddFile(TPizzaCrustConfigurationFile);

            var TPizzaSauceConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaSauce.hbm.xml");
            configuration.AddFile(TPizzaSauceConfigurationFile);

            var TPizzaSizeBreadConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaSizeBread.hbm.xml");
            configuration.AddFile(TPizzaSizeBreadConfigurationFile);

            var TPizzaTempSabadConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaTempSabad.hbm.xml");
            configuration.AddFile(TPizzaTempSabadConfigurationFile);

            var TPizzaTempSabadCheeseConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaTempSabadCheese.hbm.xml");
            configuration.AddFile(TPizzaTempSabadCheeseConfigurationFile);

            var TPizzaTempSabadSauceConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaTempSabadSauce.hbm.xml");
            configuration.AddFile(TPizzaTempSabadSauceConfigurationFile);

            var TPizzaTempSabadToppingConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaTempSabadTopping.hbm.xml");
            configuration.AddFile(TPizzaTempSabadToppingConfigurationFile);

            var TPizzaToppingConfigurationFile = HttpContext.Current.Server.MapPath
              (@"~\Models\NHibernate\TPizzaTopping.hbm.xml");
            configuration.AddFile(TPizzaToppingConfigurationFile);

            var TServicerConfigurationFile = HttpContext.Current.Server.MapPath
          (@"~\Models\NHibernate\TServicer.hbm.xml");
            configuration.AddFile(TServicerConfigurationFile);

            var TSubCheeseConfigurationFile = HttpContext.Current.Server.MapPath
          (@"~\Models\NHibernate\TSubCheese.hbm.xml");
            configuration.AddFile(TSubCheeseConfigurationFile);

            var TSubSizeBreadConfigurationFile = HttpContext.Current.Server.MapPath
          (@"~\Models\NHibernate\TSubSizeBread.hbm.xml");
            configuration.AddFile(TSubSizeBreadConfigurationFile);

            var TSubTempSabadConfigurationFile = HttpContext.Current.Server.MapPath
         (@"~\Models\NHibernate\TSubTempSabad.hbm.xml");
            configuration.AddFile(TSubTempSabadConfigurationFile);

            var TSubTempSabadCheeseConfigurationFile = HttpContext.Current.Server.MapPath
         (@"~\Models\NHibernate\TSubTempSabadCheese.hbm.xml");
            configuration.AddFile(TSubTempSabadCheeseConfigurationFile);

            var TSubTempSabadToppingConfigurationFile = HttpContext.Current.Server.MapPath
         (@"~\Models\NHibernate\TSubTempSabadTopping.hbm.xml");
            configuration.AddFile(TSubTempSabadToppingConfigurationFile);

            var TSubToppingConfigurationFile = HttpContext.Current.Server.MapPath
      (@"~\Models\NHibernate\TSubTopping.hbm.xml");
            configuration.AddFile(TSubToppingConfigurationFile);

            var TSubTypeBreadConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TSubTypeBread.hbm.xml");
            configuration.AddFile(TSubTypeBreadConfigurationFile);

            
                 var TTempOrderConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TTempOrder.hbm.xml");
            configuration.AddFile(TTempOrderConfigurationFile);

            var TUsersConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TUsers.hbm.xml");
            configuration.AddFile(TUsersConfigurationFile);


            var TLawConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TLaw.hbm.xml");
            configuration.AddFile(TLawConfigurationFile);

            var TContactusConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TContactus.hbm.xml");
            configuration.AddFile(TContactusConfigurationFile);

            var TAllQuestionsConfigurationFile = HttpContext.Current.Server.MapPath
     (@"~\Models\NHibernate\TAllQuestions.hbm.xml");
            configuration.AddFile(TAllQuestionsConfigurationFile);


            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();



        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_swagger.Models;

namespace WebApi_swagger.Controllers
{
    public class TVersion_AppSController : ApiController
    {

        ISession session = OpenNHibertnateSession.OpenSession();
        //Get All Employee 
        [HttpGet]
        [Route("api/G1/GetListTApplicationVersionMoshtari/{Unique_code}")]
        public IHttpActionResult GetListTApplicationVersionMoshtari(string Unique_code)
        {

                if (Unique_code!= "PlatformV199325694")
                {
                    return Ok("کلید نادرست");
                }
                
            //var ListApplicationVersion = session.Query<TApplicationVersion>()
            //                          .Select(x => new
            //                          {
            //                              x.VersionId,
            //                              VersionAppType = x.VersionAppType,
            //                              VersionCode = x.VersionCode
            //                          }).OrderByDescending(p => p.VersionId)
            //                          .FirstOrDefault(s => s.VersionAppType == "moshtari");
            //                          }).ToList();

            var ListApplicationVersion = session.Query<TVersionApp>().Where(p => p.VersionAppType == "moshtari")
                 .Select(x => new
                 {
                         x.IdVersionApp,
                         VersionAppType = x.VersionAppType,
                         VersionCode = x.VersionCode,
                     TextForVersion = x.TextForVersion
                    
                 })
                 .OrderByDescending(x => x.IdVersionApp).Take(1).ToList();

            var json = JsonConvert.SerializeObject(new
            {
                MyVersion = ListApplicationVersion
            });
            JObject json_Out = JObject.Parse(json);
            session.Close();
            return Ok(json_Out);
           
        }


        [HttpGet]
        [Route("api/G1/GetListTApplicationVersionServiser/{Unique_code}")]
        public IHttpActionResult GetListTApplicationVersionServiser(string Unique_code)
        {
            if (Unique_code != "PlatformV199325694")
            {
                return Ok("کلید نادرست");
            }
            //var ListApplicationVersion = session.Query<TApplicationVersion>()
            //                          .Select(x => new
            //                          {
            //                              x.VersionId,
            //                              VersionAppType = x.VersionAppType,
            //                              VersionCode = x.VersionCode
            //                          }).OrderByDescending(p => p.VersionId)
            //                          .FirstOrDefault(s => s.VersionAppType == "moshtari");
            //                          }).ToList();

            var ListApplicationVersion = session.Query<TVersionApp>().Where(p => p.VersionAppType == "Serviser")
                 .Select(x => new
                 {
                   
                         x.IdVersionApp,
                         VersionAppType = x.VersionAppType,
                         VersionCode = x.VersionCode,
                     TextForVersion = x.TextForVersion

                 })
                 .OrderByDescending(x => x.IdVersionApp).Take(1).ToList();
            var json = JsonConvert.SerializeObject(new 
            {
                MyVersion = ListApplicationVersion
            });
            JObject json_Out = JObject.Parse(json);
            session.Close();
            return Ok(json_Out);

            
        }

    }
}

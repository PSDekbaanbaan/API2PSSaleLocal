using API2PSSale.Class;
using API2PSSale.Class.Online;
using API2PSSale.Class.Standard;
using API2PSSale.Models.WebService.Response.Online;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading;

namespace API2PSSale.Controllers
{   /// <summary>
    /// Information Class online
    /// </summary>
    /// 
    //[RoutePrefix(cCS.tCS_APIVer + "/CheckOnline")]
    //*Ton 64-05-18
    [ApiController]
    [Route(cCS.tCS_APIVer + "/CheckOnline")]
    public class cCheckOnlineController : ControllerBase
    {
        /// <summary>
        /// Function check online 
        /// </summary>
        /// <returns>
        /// System process status.<br/>
        ///&#8195;     1   : success.<br/>    
        ///&#8195;     900 : service process false.<br/>
        ///&#8195;     904 : key not allowed to use method.<br/>
        ///&#8195;     905 : cannot connect database.<br/>
        /// </returns>
        [Route("IsOnline")]
        [HttpGet]
        public cmlResIsOnline GET_CHKoIsOnline()
        {
            cMS oMsg;
            cOnline oOnline;
            cmlResIsOnline oResult;
            string tErrCode, tErrDesc, tDBName, tErrAPI;
            bool bChkMQ;
            cDatabase oDatabase;
            cSP oSP;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oResult = new cmlResIsOnline();
                oMsg = new cMS();
                oOnline = new cOnline();
                oSP = new cSP();
                
                bChkMQ = false;
                cRabbitMQ oRQ = new cRabbitMQ();
                oRQ.C_GETbLoadConfigMQ();
                if (!string.IsNullOrEmpty(cRabbitMQ.tC_HostName) && !string.IsNullOrEmpty(cRabbitMQ.tC_QueueSale))
                {
                    if (oRQ.C_bCHKMQConnection())
                    {
                        bChkMQ = true;
                        oResult.rtResult = "API2PSSale : Ready";
                        oResult.rtCode = cMS.tMS_RespCode001;
                        oResult.rtDesc = cMS.tMS_RespDesc001;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
                return oResult;
            }
            catch (Exception)
            {
                oResult = new cmlResIsOnline();
                oResult.rtCode = cMS.tMS_RespCode900;
                oResult.rtDesc = cMS.tMS_RespDesc900;
                return oResult;
            }
            finally
            {
                oMsg = null;
                //*Ton 64-05-18
                //oResult = null;
                oDatabase = null;
                tDBName = null;
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
        }

        [Route("IsOnlineDB")]
        [HttpGet]
        public cmlResIsOnline GET_CHKoIsOnlineDB()
        {
            cMS oMsg;
            cOnline oOnline;
            cmlResIsOnline oResult;
            string tErrCode, tErrDesc, tDBName, tErrAPI;
            cDatabase oDatabase;
            cSP oSP;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oResult = new cmlResIsOnline();
                oMsg = new cMS();
                oOnline = new cOnline();
                oSP = new cSP();

                //*Arm 62-09-20           
                oDatabase = new cDatabase();
                tDBName = (oDatabase.C_CONoDatabase()).Database;

                oResult.rtResult = tDBName + " :Ready";
                oResult.rtCode = cMS.tMS_RespCode001;
                oResult.rtDesc = cMS.tMS_RespDesc001;
                return oResult;
                ////*Arm 62-09-20
            }
            catch (Exception)
            {
                oResult = new cmlResIsOnline();
                oResult.rtCode = cMS.tMS_RespCode900;
                oResult.rtDesc = cMS.tMS_RespDesc900;
                return oResult;
            }
            finally
            {
                oMsg = null;
                //*Ton 64-05-18
                //oResult = null;
                oDatabase = null;
                tDBName = null;
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
        }
    }

}

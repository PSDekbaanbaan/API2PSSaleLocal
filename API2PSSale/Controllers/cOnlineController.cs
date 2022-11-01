using API2PSSale.Class;
using API2PSSale.Class.Online;
using API2PSSale.Class.Standard;
using API2PSSale.Models.WebService.Response.Online;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading;

namespace API2PSSale.Controllers
{
    /// <summary>
    /// Controller check online
    /// </summary>
    //[RoutePrefix(cCS.tCS_APIVer + "/Online")]
    //*Ton 64-05-18
    [ApiController]
    [Route(cCS.tCS_APIVer + "/Online")]
    public class cOnlineController : ControllerBase
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
        [Route("Check")]
        [HttpPost]
        public cmlResIsOnline POST_CHKoIsOnline()
        {
            cMS oMsg;
            cOnline oOnline;
            cmlResIsOnline oResult;
            string tErrCode, tErrDesc, tDBName, tErrAPI;
            bool bChk;
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

                #region Check API Key
                //*Ton 64-05-18
                if (oSP.SP_CHKbKeyApi(HttpContext, out tErrAPI) == false)
                {
                    if (tErrAPI == "-1")
                    {
                        oResult.rtCode = cMS.tMS_RespCode905;
                        oResult.rtDesc = cMS.tMS_RespDesc905;
                        return oResult;
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode904;
                        oResult.rtDesc = cMS.tMS_RespDesc904;
                        return oResult;
                    }
                }
                #endregion

                bChk = oOnline.C_CHKbOnline(out tErrCode, out tErrDesc);
                if (bChk == true)
                {
                    oDatabase = new cDatabase();
                    tDBName = oDatabase.C_GETtSQLScalarString("SELECT NAME FROM SYS.sysdatabases WHERE DBID=db_id()");
                    oResult.rtResult = tDBName + " :Ready";
                    oResult.rtCode = cMS.tMS_RespCode001;
                    oResult.rtDesc = cMS.tMS_RespDesc001;
                    return oResult;
                }
                else
                {
                    oResult.rtCode = tErrCode;
                    oResult.rtDesc = tErrDesc;
                    return oResult;
                }
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

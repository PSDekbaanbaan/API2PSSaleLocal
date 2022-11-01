using API2PSSale.Class;
using API2PSSale.Class.ServiceDwn;
using API2PSSale.Class.Standard;
using API2PSSale.Models.WebService.Request;
using API2PSSale.Models.WebService.Response.Base;
using API2PSSale.Models.WebService.Response.SalDT;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace API2PSSale.Controllers
{
    /// <summary>
    /// Controller Service Download
    /// </summary>
    //[RoutePrefix(cCS.tCS_APIVer + "/Service")]
    //*Ton 64-05-18
    [ApiController]
    [Route(cCS.tCS_APIVer + "/Service")]
    public class cServicesDwnController : ControllerBase
    {

        /// <summary>
        /// Download Sale DT
        /// </summary>
        /// <param name="poPara">cmlReqSalDT</param>
        /// <returns>
        ///&#8195;     1   : Success.<br/>
        ///&#8195;     701 : Validate parameter model false.<br/>
        ///&#8195;     800 : Data not found.<br/>
        ///&#8195;     900 : Service process false.<br/>
        ///&#8195;     904 : Key not allowed to use method.<br/>
        ///&#8195;     905 : Cannot connect database.<br/>
        /// </returns>
        [Route("Data/DwnSalDT")]
        [HttpPost]
        public cmlResItem<cmlResSalDT> POST_DWNoSaleDT(cmlReqSalDT poPara)
        {
            cmlResItem<cmlResSalDT> oResult;
            cmlResSalDT oSalDT;
            List<cmlResTPSTSalDT> aoTPSTSalDT;
            cServiceDwn oServiceDwn;
            cDatabase oDatabase;
            cMS oMsg;
            cSP oSP;
            string tErrCode, tErrDesc, tErrAPI;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oResult = new cmlResItem<cmlResSalDT>();
                oSalDT = new cmlResSalDT();
                aoTPSTSalDT = new List<cmlResTPSTSalDT>();

                oServiceDwn = new cServiceDwn();
                oDatabase = new cDatabase();
                oMsg = new cMS();
                oSP = new cSP();

                #region Check API Key and check comnnect database
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

                #region Get TPSTSalDT 
                if (!oServiceDwn.C_GETbSalDT(poPara,out aoTPSTSalDT,out tErrCode,out tErrDesc))
                {
                    oResult.rtCode = tErrCode;
                    oResult.rtDesc = tErrDesc;
                    return oResult;
                }
                #endregion

                #region Set Response
                oSalDT.raSalDT = aoTPSTSalDT;

                oResult.roItem = oSalDT;
                oResult.rtCode = cMS.tMS_RespCode001;
                oResult.rtDesc = cMS.tMS_RespDesc001;
                return oResult;
                #endregion
            }
            catch (Exception)
            {
                // Return error.
                oResult = new cmlResItem<cmlResSalDT>();
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
                oResult = null;
                oSalDT = null;
                aoTPSTSalDT = null;
                oServiceDwn = null;
                oDatabase = null;
                oMsg = null;
                oSP = null;
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
        }
    }
}

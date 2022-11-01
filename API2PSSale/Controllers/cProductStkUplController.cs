using API2PSSale.Class;
using API2PSSale.Class.Standard;
using API2PSSale.Models.UpdProductStk;
using API2PSSale.Models.WebService.Response.ResProductStk;
using API2PSSale.Models.WebService.Response.UpdPdtStkCrd;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading;

namespace API2PSSale.Controllers
{
    //[RoutePrefix(cCS.tCS_APIVer + "/PdtStock")]
    //*Ton 64-05-18
    [ApiController]
    [Route(cCS.tCS_APIVer + "/PdtStock")]
    public class cProductStkUplController : ControllerBase
    {
        /// <summary>
        /// Upload Pdt Stock Card To MQ
        /// </summary>
        /// <returns>
        /// System process status.<br/>
        ///&#8195;     1   : Success.<br/>
        ///&#8195;     801 : Data is duplicate.<br/>    
        ///&#8195;     900 : Service process false.<br/>
        ///&#8195;     903 : Validate parameter encrypt false..<br/>
        ///&#8195;     904 : Key not allowed to use method.<br/>
        ///&#8195;     905 : Cannot connect database.<br/>
        /// </returns>
        //*BOY 62-11-19
        [Route("Data/UplPdtStkCrd")]
        [HttpPost]
        public cmlResUpdPdtStkCrd POST_UPLoStkCrdUpload(cmlPdtStkCrd poPdtStkCrd)
        {
            //TransactionScope oTrans = null/* TODO Change to default(_) if this is not a reference type */;
            cmlResUpdPdtStkCrd oResult = new cmlResUpdPdtStkCrd();
            cMS oMsg;
            cSP oSP;
            string tErrCode, tErrDesc, tErrAPI;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oMsg = new cMS();
                oSP = new cSP();

                oResult.rtCode = "";
                oResult.rtDesc = "";

                if (poPdtStkCrd == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtDesc = cMS.tMS_RespDesc700;
                    return oResult;
                }

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

                cRabbitMQ oRQ = new cRabbitMQ();
                oRQ.C_GETbLoadConfigMQ();
                if (!string.IsNullOrEmpty(cRabbitMQ.tC_HostName))
                {
                    if (!string.IsNullOrEmpty(cRabbitMQ.tC_QueueStkCrd))
                    {
                        //*Ton 64-05-18
                        string tConnStr = cAppSetting.Default.tConnDB;
                        string tMsgJson = oRQ.C_CRTtMsgDataUpload(Newtonsoft.Json.JsonConvert.SerializeObject(poPdtStkCrd), tConnStr);
                        if (oRQ.C_PRCbSendData2Srv(tMsgJson, cRabbitMQ.tC_QueueStkCrd))
                        {
                            oResult.rtCode = cMS.tMS_RespCode001;
                            oResult.rtDesc = cMS.tMS_RespDesc001;
                        }
                        else
                        {
                            oResult.rtCode = cMS.tMS_RespCode907;
                            oResult.rtDesc = cMS.tMS_RespDesc907;
                        }
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode907;
                        oResult.rtDesc = cMS.tMS_RespDesc907;
                    }
                }
                else
                {
                    oResult.rtCode = cMS.tMS_RespCode907;
                    oResult.rtDesc = cMS.tMS_RespDesc907;
                }
            }
            catch (Exception oEx)
            {
                oResult.rtCode = cMS.tMS_RespCode900;
                oResult.rtDesc = oEx.Message.ToString();
            }
            finally
            {
                oMsg = null;
                oSP = null;
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            return oResult;
        }

        /// <summary>
        /// UPload Product Stock Bal To MQ
        /// </summary>
        /// <param name="poPdtStkBal"></param>
        /// <returns></returns>
        //*BOY 62-11-19
        [Route("Data/UplPdtStkBal")]
        [HttpPost]
        public cmlResUpdPdtStkBal POST_UPLoStkBalUpload(cmlPdtStkBal poPdtStkBal)
        {
            //TransactionScope oTrans = null/* TODO Change to default(_) if this is not a reference type */;
            cmlResUpdPdtStkBal oResult = new cmlResUpdPdtStkBal();
            cMS oMsg;
            cSP oSP;
            string tErrCode, tErrDesc, tErrAPI;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oMsg = new cMS();
                oSP = new cSP();

                oResult.rtCode = "";
                oResult.rtDesc = "";

                if (poPdtStkBal == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtDesc = cMS.tMS_RespDesc700;
                    return oResult;
                }

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

                cRabbitMQ oRQ = new cRabbitMQ();
                oRQ.C_GETbLoadConfigMQ();
                if (!string.IsNullOrEmpty(cRabbitMQ.tC_HostName))
                {
                    if (!string.IsNullOrEmpty(cRabbitMQ.tC_QueueStkBal))
                    {
                        //*Ton 64-05-18
                        string tConnStr = cAppSetting.Default.tConnDB;
                        string tMsgJson = oRQ.C_CRTtMsgDataUpload(Newtonsoft.Json.JsonConvert.SerializeObject(poPdtStkBal), tConnStr);
                        if (oRQ.C_PRCbSendData2Srv(tMsgJson, cRabbitMQ.tC_QueueStkBal))
                        {
                            oResult.rtCode = cMS.tMS_RespCode001;
                            oResult.rtDesc = cMS.tMS_RespDesc001;
                        }
                        else
                        {
                            oResult.rtCode = cMS.tMS_RespCode907;
                            oResult.rtDesc = cMS.tMS_RespDesc907;
                        }
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode907;
                        oResult.rtDesc = cMS.tMS_RespDesc907;
                    }
                }
                else
                {
                    oResult.rtCode = cMS.tMS_RespCode907;
                    oResult.rtDesc = cMS.tMS_RespDesc907;
                }
            }
            catch (Exception oEx)
            {
                oResult.rtCode = cMS.tMS_RespCode900;
                oResult.rtDesc = oEx.Message.ToString();
            }
            finally
            {
                oMsg = null;
                oSP = null;
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            return oResult;
        }
    }
}


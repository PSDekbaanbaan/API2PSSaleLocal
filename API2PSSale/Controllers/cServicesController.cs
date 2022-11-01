using API2PSSale.Class;
using API2PSSale.Class.Standard;
using API2PSSale.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading;

namespace API2PSSale.Controllers
{
    /// <summary>
    ///     Service upload.
    /// </summary>
    //[RoutePrefix(cCS.tCS_APIVer + "/Service")]
    //*Ton 64-05-18
    [ApiController]
    [Route(cCS.tCS_APIVer + "/Service")]
    public class cServicesController : ControllerBase
    {
        [Route("Upload/Sale")]
        [HttpPost]
        public cResult<int> POST_UPLoSaleUpload(cmlTPSTSal poSale)
        {
            //TransactionScope oTrans = null/* TODO Change to default(_) if this is not a reference type */;
            cResult<int> oResult = new cResult<int>();
            cMS oMsg;
            cSP oSP;
            string tErrCode, tErrDesc, tErrAPI;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oMsg = new cMS();
                oSP = new cSP();

                oResult.roItem = 0;
                oResult.rnCount = 0;
                oResult.rtMsg = "";
                oResult.rtCode = "";

                if (poSale == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtMsg = cMS.tMS_RespDesc700;
                    return oResult;
                }
                if (poSale.aoTPSTSalHD == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtMsg = cMS.tMS_RespDesc700;
                    return oResult;
                }

                #region Check API Key and check comnnect database
                //if (oSP.SP_CHKbKeyApi(HttpContext.Current, out tErrAPI) == false)
                //*Ton 64-05-18
                if (oSP.SP_CHKbKeyApiConfig(HttpContext, out tErrAPI) == false) //*Arm 63-07-31 ยกมาจาก Moshi
                {
                    if (tErrAPI == "-1")
                    {
                        oResult.rtCode = cMS.tMS_RespCode905;
                        oResult.rtMsg = cMS.tMS_RespDesc905;
                        return oResult;
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode904;
                        oResult.rtMsg = cMS.tMS_RespDesc904;
                        return oResult;
                    }
                }
                #endregion

                cRabbitMQ oRQ = new cRabbitMQ();
                oRQ.C_GETbLoadConfigMQ();
                if (!string.IsNullOrEmpty(cRabbitMQ.tC_HostName))
                {
                    if (!string.IsNullOrEmpty(cRabbitMQ.tC_QueueSale))
                    {
                        //*Ton 64-05-18
                        string tConnStr = cAppSetting.Default.tConnDB;
                        string tMsgJson = oRQ.C_CRTtMsgDataUpload(Newtonsoft.Json.JsonConvert.SerializeObject(poSale), tConnStr);
                        if (oRQ.C_PRCbSendData2Srv(tMsgJson, cRabbitMQ.tC_QueueSale))
                        {
                            oResult.rtCode = cMS.tMS_RespCode001;
                            oResult.rtMsg = cMS.tMS_RespDesc001;
                        }
                        else
                        {
                            oResult.rtCode = cMS.tMS_RespCode907;
                            oResult.rtMsg = cMS.tMS_RespDesc907;
                        }
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode907;
                        oResult.rtMsg = cMS.tMS_RespDesc907;
                    }
                }
                else
                {
                    oResult.rtCode = cMS.tMS_RespCode907;
                    oResult.rtMsg = cMS.tMS_RespDesc907;
                }
            }
            catch (Exception oEx)
            {
                oResult.rtCode = cMS.tMS_RespCode900;
                oResult.rtMsg = oEx.Message.ToString();
            }
            finally
            {
                oMsg = null;
                oSP = null;
            }
            return oResult;

        }

        [Route("Upload/Tax")]
        [HttpPost]
        public cResult<int> POST_UPLoTaxUpload(cmlTPSTTax poTax)
        {
            //TransactionScope oTrans = null/* TODO Change to default(_) if this is not a reference type */;
            cResult<int> oResult = new cResult<int>();
            cMS oMsg;
            cSP oSP;
            string tErrCode, tErrDesc, tErrAPI;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oMsg = new cMS();
                oSP = new cSP();

                oResult.roItem = 0;
                oResult.rnCount = 0;
                oResult.rtMsg = "";
                oResult.rtCode = "";

                if (poTax == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtMsg = cMS.tMS_RespDesc700;
                    return oResult;
                }
                if (poTax.aoTPSTTaxHD == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtMsg = cMS.tMS_RespDesc700;
                    return oResult;
                }

                #region Check API Key and check comnnect database
                //if (oSP.SP_CHKbKeyApi(HttpContext.Current, out tErrAPI) == false)
                if (oSP.SP_CHKbKeyApiConfig(HttpContext, out tErrAPI) == false) //*Arm 63-07-31 ยกมาจาก Moshi
                {
                    if (tErrAPI == "-1")
                    {
                        oResult.rtCode = cMS.tMS_RespCode905;
                        oResult.rtMsg = cMS.tMS_RespDesc905;
                        return oResult;
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode904;
                        oResult.rtMsg = cMS.tMS_RespDesc904;
                        return oResult;
                    }
                }
                #endregion

                cRabbitMQ oRQ = new cRabbitMQ();
                oRQ.C_GETbLoadConfigMQ();
                if (!string.IsNullOrEmpty(cRabbitMQ.tC_HostName))
                {
                    if (!string.IsNullOrEmpty(cRabbitMQ.tC_QueueTax))
                    {
                        //*Ton 64-05-18
                        string tConnStr = cAppSetting.Default.tConnDB;
                        string tMsgJson = oRQ.C_CRTtMsgDataUpload(Newtonsoft.Json.JsonConvert.SerializeObject(poTax), tConnStr);
                        if (oRQ.C_PRCbSendData2Srv(tMsgJson, cRabbitMQ.tC_QueueTax))
                        {
                            oResult.rtCode = cMS.tMS_RespCode001;
                            oResult.rtMsg = cMS.tMS_RespCode001;
                        }
                        else
                        {
                            oResult.rtCode = cMS.tMS_RespCode907;
                            oResult.rtMsg = cMS.tMS_RespDesc907;
                        }
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode907;
                        oResult.rtMsg = cMS.tMS_RespDesc907;
                    }
                }
                else
                {
                    oResult.rtCode = cMS.tMS_RespCode907;
                    oResult.rtMsg = cMS.tMS_RespDesc907;
                }
            }
            catch (Exception oEx)
            {
                oResult.rtCode = cMS.tMS_RespCode900;
                oResult.rtMsg = oEx.Message.ToString();
            }
            finally
            {
                oMsg = null;
                oSP = null;
            }
            return oResult;
        }

        [Route("Upload/ShiftSale")]
        [HttpPost]
        public cResult<int> POST_UPLoShiftUpload(cmlTPSTShift poShift)
        {
            //TransactionScope oTrans = null/* TODO Change to default(_) if this is not a reference type */;
            cResult<int> oResult = new cResult<int>();
            cMS oMsg;
            cSP oSP;
            string tErrCode, tErrDesc, tErrAPI;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oMsg = new cMS();
                oSP = new cSP();

                oResult.roItem = 0;
                oResult.rnCount = 0;
                oResult.rtMsg = "";
                oResult.rtCode = "";

                if (poShift == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtMsg = cMS.tMS_RespDesc700;
                    return oResult;
                }
                if (poShift.aoTPSTShiftHD == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtMsg = cMS.tMS_RespDesc700;
                    return oResult;
                }

                #region Check API Key and check comnnect database
                //if (oSP.SP_CHKbKeyApi(HttpContext.Current, out tErrAPI) == false)
                if (oSP.SP_CHKbKeyApiConfig(HttpContext, out tErrAPI) == false) //*Arm 63-07-31
                {
                    if (tErrAPI == "-1")
                    {
                        oResult.rtCode = cMS.tMS_RespCode905;
                        oResult.rtMsg = cMS.tMS_RespDesc905;
                        return oResult;
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode904;
                        oResult.rtMsg = cMS.tMS_RespDesc904;
                        return oResult;
                    }
                }
                #endregion

                cRabbitMQ oRQ = new cRabbitMQ();
                oRQ.C_GETbLoadConfigMQ();
                if (!string.IsNullOrEmpty(cRabbitMQ.tC_HostName))
                {
                    if (!string.IsNullOrEmpty(cRabbitMQ.tC_QueueShift))
                    {
                        //*Ton 64-05-18
                        string tConnStr = cAppSetting.Default.tConnDB;
                        string tMsgJson = oRQ.C_CRTtMsgDataUpload(Newtonsoft.Json.JsonConvert.SerializeObject(poShift), tConnStr);
                        if (oRQ.C_PRCbSendData2Srv(tMsgJson, cRabbitMQ.tC_QueueShift))
                        {
                            oResult.rtCode = cMS.tMS_RespCode001;
                            oResult.rtMsg = cMS.tMS_RespCode001;
                        }
                        else
                        {
                            oResult.rtCode = cMS.tMS_RespCode907;
                            oResult.rtMsg = cMS.tMS_RespDesc907;
                        }
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode907;
                        oResult.rtMsg = cMS.tMS_RespDesc907;
                    }
                }
                else
                {
                    oResult.rtCode = cMS.tMS_RespCode907;
                    oResult.rtMsg = cMS.tMS_RespDesc907;
                }
            }
            catch (Exception oEx)
            {
                oResult.rtCode = cMS.tMS_RespCode900;
                oResult.rtMsg = oEx.Message.ToString();
            }
            finally
            {
                oMsg = null;
                oSP = null;
            }
            return oResult;
        }

        [Route("Upload/Void")]
        [HttpPost]
        public cResult<int> POST_UPLoVoidUpload(cmlTPSTVoid poVoid)
        {
            //TransactionScope oTrans = null/* TODO Change to default(_) if this is not a reference type */;
            cResult<int> oResult = new cResult<int>();
            cMS oMsg;
            cSP oSP;
            string tErrCode, tErrDesc, tErrAPI;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                oMsg = new cMS();
                oSP = new cSP();

                oResult.roItem = 0;
                oResult.rnCount = 0;
                oResult.rtMsg = "";
                oResult.rtCode = "";

                if (poVoid == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtMsg = cMS.tMS_RespDesc700;
                    return oResult;
                }
                if (poVoid.aoTPSTVoidDT == null)
                {
                    oResult.rtCode = cMS.tMS_RespCode700;
                    oResult.rtMsg = cMS.tMS_RespDesc700;
                    return oResult;
                }

                #region Check API Key and check comnnect database
                //if (oSP.SP_CHKbKeyApi(HttpContext.Current, out tErrAPI) == false)
                //*Ton 64-05-18
                if (oSP.SP_CHKbKeyApiConfig(HttpContext, out tErrAPI) == false) //*Arm 63-07-31 ยกมาจาก Moshi
                {
                    if (tErrAPI == "-1")
                    {
                        oResult.rtCode = cMS.tMS_RespCode905;
                        oResult.rtMsg = cMS.tMS_RespDesc905;
                        return oResult;
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode904;
                        oResult.rtMsg = cMS.tMS_RespDesc904;
                        return oResult;
                    }
                }
                #endregion

                cRabbitMQ oRQ = new cRabbitMQ();
                oRQ.C_GETbLoadConfigMQ();
                if (!string.IsNullOrEmpty(cRabbitMQ.tC_HostName))
                {
                    if (!string.IsNullOrEmpty(cRabbitMQ.tC_QueueVoid))
                    {
                        //*Ton 64-05-18
                        string tConnStr = cAppSetting.Default.tConnDB;
                        string tMsgJson = oRQ.C_CRTtMsgDataUpload(Newtonsoft.Json.JsonConvert.SerializeObject(poVoid), tConnStr);
                        if (oRQ.C_PRCbSendData2Srv(tMsgJson, cRabbitMQ.tC_QueueVoid))
                        {
                            oResult.rtCode = cMS.tMS_RespCode001;
                            oResult.rtMsg = cMS.tMS_RespCode001;
                        }
                        else
                        {
                            oResult.rtCode = cMS.tMS_RespCode907;
                            oResult.rtMsg = cMS.tMS_RespDesc907;
                        }
                    }
                    else
                    {
                        oResult.rtCode = cMS.tMS_RespCode907;
                        oResult.rtMsg = cMS.tMS_RespDesc907;
                    }
                }
                else
                {
                    oResult.rtCode = cMS.tMS_RespCode907;
                    oResult.rtMsg = cMS.tMS_RespDesc907;
                }
            }
            catch (Exception oEx)
            {
                oResult.rtCode = cMS.tMS_RespCode900;
                oResult.rtMsg = oEx.Message.ToString();
            }
            finally
            {
                oMsg = null;
                oSP = null;
            }
            return oResult;
        }
    }
}

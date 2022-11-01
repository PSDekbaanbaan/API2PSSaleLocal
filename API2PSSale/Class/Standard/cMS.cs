using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API2PSSale.Class.Standard
{
    /// <summary>
    /// Class message
    /// </summary>
    public class cMS
    {
        //*Ton 64-05-18 เปลี่ยน public readonly เป็น public const
        #region Response code.

        #region Success
        public const string tMS_RespCode001 = "001";
        #endregion

        #region Parameter
        public const string tMS_RespCode700 = "700";
        public const string tMS_RespCode710 = "710";
        public const string tMS_RespCode701 = "701";
        #endregion

        #region Data
        public const string tMS_RespCode800 = "800";
        public const string tMS_RespCode801 = "801";
        public const string tMS_RespCode802 = "802";
        public const string tMS_RespCode803 = "803";     //*Arm 63-07-31 ยกมาจาก Moshi
        #endregion

        #region System
        public const string tMS_RespCode900 = "900";
        public const string tMS_RespCode903 = "903";
        public const string tMS_RespCode904 = "904";
        public const string tMS_RespCode905 = "905";
        public const string tMS_RespCode907 = "907";     //*Em 62-08-30
        #endregion

        #endregion

        #region Response descript.

        #region Success
        public const string tMS_RespDesc001 = "success.";
        #endregion

        #region Parameter
        public const string tMS_RespDesc700 = "all parameter is null.";
        public const string tMS_RespDesc701 = "validate parameter model false.|";
        #endregion

        #region Data
        public const string tMS_RespDesc800 = "data not found.";
        public const string tMS_RespDesc801 = "Card Date expired";
        public const string tMS_RespDesc802 = "Product received.";
        public const string tMS_RespDesc803 = "Can not gen TaxNo.";  //*Arm 63-07-31 ยกมาจาก Moshi
        #endregion

        #region System
        public const string tMS_RespDesc900 = "service process false.";
        public const string tMS_RespDesc903 = "validate parameter encrypt false.";
        public const string tMS_RespDesc904 = "key not allowed to use method.";
        public const string tMS_RespDesc905 = "cannot connect database.";
        public const string tMS_RespDesc907 = "cannot connect server MQ";    //*Em 62-08-30
        #endregion

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API2PSSale.Models.Database
{
    public class cmlTLKTSalHDCstAddr_L
    {
        /// <summary>
        /// รหัสสาขา
        /// </summary>
        public string FTBchCode { get; set; }

        /// <summary>
        /// เลขที่เอกสาร
        /// </summary>
        public string FTXshDocNo { get; set; }

        /// <summary>
        /// รหัสลูกค้า
        /// </summary>
        public string FTCstCode { get; set; }

        /// <summary>
        /// ชื่อ
        /// </summary>
        public string FTCstFName { get; set; } //*Arm 64-05-11

        /// <summary>
        /// นามสกุล
        /// </summary>
        public string FTCstSName { get; set; } //*Arm 64-05-11

        /// <summary>
        /// บ้านเลขที่
        /// </summary>
        public string FTXshAddV1No { get; set; }

        /// <summary>
        /// ตำบล
        /// </summary>
        public string FTCstV1SubDist { get; set; } //*Arm 64-05-11

        /// <summary>
        /// อำเภอ
        /// </summary>
        public string FTCstV1Dist { get; set; } //*Arm 64-05-11

        /// <summary>
        /// จังหวัด
        /// </summary>
        public string FTXshAddV1Pvn { get; set; }

        /// <summary>
        /// ประเทศ
        /// </summary>
        public string FTXshAddCountry { get; set; }

        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string FTXshAddV1PostCode { get; set; }

        /// <summary>
        /// เลขที่ประจำตัวผู้เสียภาษี
        /// </summary>
        public string FTXshCstTaxNo { get; set; }

        /// <summary>
        /// วันที่ปรับปรุงรายการล่าสุด
        /// </summary>
        public Nullable<DateTime> FDLastUpdOn { get; set; }

        /// <summary>
        /// ผู้ปรับปรุงรายการล่าสุด 
        /// </summary>
        public string FTLastUpdBy { get; set; }

        /// <summary>
        /// วันที่สร้างรายการ
        /// </summary>
        public Nullable<DateTime> FDCreateOn { get; set; }

        /// <summary>
        /// ผู้สร้างรายการ
        /// </summary>
        public string FTCreateBy { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API2PSSale.Models.WebService.Response.Base
{
    /// <summary>
    /// Class Response base
    /// </summary>
    public class cmlResBase
    {
        /// <summary>
        /// System process status.
        /// </summary>
        public string rtCode { get; set; }

        /// <summary>
        /// System process description.
        /// </summary>
        public string rtDesc { get; set; }
    }
}
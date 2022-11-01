using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace API2PSSale.Models
{
    //*Ton 64-05-18 เพิ่ม Class Model
    public class cmlAppSetting
    {
        public string tName { get; set; }
        public string tRQHost { get; set; }
        public string tRQUsr { get; set; }
        public string tRQPwd { get; set; }
        public string tRQVirtual { get; set; }
        public string tAccess { get; set; }
        public string tConnDB { get; set; }
    }
}

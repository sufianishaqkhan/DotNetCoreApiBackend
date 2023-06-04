using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HRMIS_API.Models
{
    public class BaseResponse
    {
        public dynamic data { get; set; } = string.Empty;
        public bool status { get; set; } = false;
        public int totalrecords { get; set; } = 0;
        public string message { get; set; } = string.Empty;
        public DateTime datetime { get; set; } = DateTime.Now;
    }
}

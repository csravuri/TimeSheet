using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TImeSheet.BusinessLogic
{
    public class TimeSheetDetailsTable
    {
        public int ID { get; set; }
        public string TaskID { get; set; }
        public string TaskName { get; set; }
        public string ClientName { get; set; }
        public string TaskDate { get; set; }
        public string TaskStartTime { get; set; }
        public string TaskEndTime { get; set; }
        public string IsCoded { get; set; }
        public string IsReviewed { get; set; }
        public string IsCheckin { get; set; }
        public string Comment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class SchedulePerDay
    {
        public string DayName { get; set; }
        public DateTime DateTimeBegin { get; set; }
        public DateTime DateTimeEnd { get; set; }
    }
}

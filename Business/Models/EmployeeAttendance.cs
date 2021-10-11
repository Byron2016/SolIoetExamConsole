using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class EmployeeAttendance
    {
        public EmployeeAttendance()
        {
            DaySchedules = new List<SchedulePerDay>();
            errorRegister = new ErrorRegister();
        }
        public string EmployeeName { get; set; }
        public string FileLine { get; set; }
        public List<SchedulePerDay> DaySchedules { get; set; }
        public ErrorRegister errorRegister { get; set; }
    }
}

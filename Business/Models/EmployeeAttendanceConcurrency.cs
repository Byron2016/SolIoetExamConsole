using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class EmployeeAttendanceConcurrency
    {
        public string PeopleName { get; set; }
        public int ConcurrencyNumber { get; set; }
    }
}

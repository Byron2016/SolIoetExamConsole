using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class EmployeeAttendanceViewModel
    {
        public EmployeeAttendanceViewModel()
        {
            ConcurrencyAttendanceProcesedList = new List<EmployeeAttendanceConcurrency>();
        }

        public List<EmployeeAttendanceConcurrency> ConcurrencyAttendanceProcesedList { get; set; }

        public string File_Path { get; set; }
    }
}

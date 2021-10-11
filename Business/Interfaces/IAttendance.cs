using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAttendance
    {
        public IEnumerable<EmployeeAttendanceConcurrency> GetConcurrency(List<EmployeeAttendance> concurrencyAttendances);
        public IEnumerable<EmployeeAttendance> GetModel(string filePath, string splitOne, string splitTwo, string dateString);
    }
}

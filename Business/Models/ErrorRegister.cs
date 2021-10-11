using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ErrorRegister
    {
        public bool HasError { get; set; }
        public string FileLine { get; set; }
        public int FileLineNumber { get; set; }
        public string File_Path { get; set; }
    }
}

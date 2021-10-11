using Business.Interfaces;
using Business.Models;
using Business.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Controller
{
    public class AttendanceController
    {
        private IAttendance _attendanceRepository { get; set; }

        public AttendanceController(IAttendance attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        //
        // Summary:
        //     Process input file.
        //
        // Returns:
        //     Nothing.
        public void Index()
        {
            List<EmployeeAttendanceConcurrency> employeesConcurrency = new List<EmployeeAttendanceConcurrency>();
            IEnumerable<EmployeeAttendance> employeesAttendance = new List<EmployeeAttendance>();

            string filePath = @"C:\temp\Ioet.txt";
            
            MessagesPrint("INPUT_FILEPATH", filePath);
            string filePathAux = Console.ReadLine();
            if (filePathAux.Trim().Length > 0)
            {
                filePath = filePathAux.Trim();
            }
            if (!File.Exists(filePath))
            {
                MessagesPrint("FILE_NOEXIST");
            }
            else
            {
                employeesAttendance = _attendanceRepository.GetModel(filePath, "=", ",", "1900/01/01");
                
                ErrorPrint(filePath, employeesAttendance); //Error register

                employeesConcurrency = _attendanceRepository.GetConcurrency(employeesAttendance.ToList()).ToList();
                if (employeesConcurrency.Count > 0)
                {
                    var emploreeAttendanceViewModel = new EmployeeAttendanceViewModel();

                    emploreeAttendanceViewModel.File_Path = filePath;
                    emploreeAttendanceViewModel.ConcurrencyAttendanceProcesedList = employeesConcurrency;

                    MessagesPrint("OUTPUT_BEGIN", filePath);
                    foreach (var element in emploreeAttendanceViewModel.ConcurrencyAttendanceProcesedList)
                    {
                        Console.WriteLine(element.PeopleName + ":" + element.ConcurrencyNumber);
                    }
                    MessagesPrint("OUTPUT_END");
                }
                else
                {
                    MessagesPrint("OUTPUT_EMPTY");
                    MessagesPrint("OUTPUT_END");
                }
            }
            Console.ReadLine();
        }

        //
        // Summary:
        //     Print messages to console.
        //
        // Parameters:
        //   printCaso:
        //     Print case to print
        //   filePath:
        //     Input file path.
        //
        // Returns:
        //     Nothing.
        public void MessagesPrint(string printCaso,  string filePath = null)
        {
            switch (printCaso.ToUpper())
            {
                case "INPUT_FILEPATH":
                    Console.Write("Please enter File Name (Full Path)");
                    Console.WriteLine("");
                    Console.Write("Or press enter to accept: " + filePath);
                    Console.WriteLine("");
                    break;
                case "FILE_NOEXIST":
                    Console.WriteLine("");
                    Console.WriteLine("*********************************************");
                    Console.Write("File does not exist; thanks for use this program.");
                    Console.WriteLine("");
                    Console.WriteLine("*********************************************");
                    Console.WriteLine("");
                    break;
                case "OUTPUT_BEGIN":
                    Console.WriteLine("");
                    Console.WriteLine("**********************");
                    Console.WriteLine("");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("OUTPUT:");
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("Processed file: " + filePath);
                    Console.WriteLine("");
                    break;
                case "OUTPUT_END":
                    Console.WriteLine("");
                    Console.WriteLine("**********************");
                    Console.WriteLine("");
                    Console.Write("Thanks for use this program.");
                    Console.WriteLine("");
                    break;
                case "OUTPUT_EMPTY":
                    Console.WriteLine("");
                    Console.WriteLine("**********************");
                    Console.WriteLine("");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("OUTPUT:");
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("There are not concorrency ");
                    Console.WriteLine("");
                    break;
                case "ERROR_BEGIN":
                    Console.WriteLine("");
                    Console.WriteLine("**********************");
                    Console.WriteLine("");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("THERE ARE ERRORS INTO INPUT FILE:");
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("Processed file: " + filePath);
                    Console.WriteLine("");
                    break;
                case "ERROR_END":
                    Console.WriteLine("");
                    break;
                default:
                    Console.WriteLine("");
                    Console.WriteLine("Case is not soported.");
                    Console.WriteLine("");
                    break;
            }

        }

        //
        // Summary:
        //     Print error messages to console.
        //
        // Parameters:
        //   employeesAttendance:
        //     Input file lines with errors.
        //
        // Returns:
        //     Nothing.
        private void ErrorPrint(string filePath, IEnumerable<EmployeeAttendance> employeesAttendance)
        {
            bool errorExist = false;
            foreach (var element in employeesAttendance)
            {
                if (element.errorRegister.HasError)
                {
                    errorExist = true;
                    break;
                }
            }
            if (errorExist)
            {
                MessagesPrint("ERROR_BEGIN",  filePath);
                
                foreach (var element in employeesAttendance)
                {
                    if (element.errorRegister.HasError)
                    {
                        var errorString = "Line number: " + element.errorRegister.FileLineNumber + " ";
                        errorString = errorString + "Line content: " + element.errorRegister.FileLine;
                        Console.WriteLine(errorString);
                    }
                }
                MessagesPrint("ERROR_END",  filePath);
            }
        }
        
    }
}

using Business.Enums;
using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class AttendanceRepository : IAttendance
    {
        //
        // Summary:
        //     Analize input file object and process concurrencies.
        //
        // Parameters:
        //   employeesAttendance:
        //     Input file transformed to object.
        //
        // Returns:
        //     Object type IEnumerable<EmployeeAttendanceConcurrency> with concurrencies.
        public IEnumerable<EmployeeAttendanceConcurrency> GetConcurrency(List<EmployeeAttendance> employeesAttendance)
        {
            List<EmployeeAttendanceConcurrency> employeesAttendanceConcurrency = new List<EmployeeAttendanceConcurrency>();
            List<EmployeeAttendance> emploreeAttendanceB = new List<EmployeeAttendance>();

            emploreeAttendanceB = employeesAttendance;

            for (var i = 0; i < employeesAttendance.Count; i++)
            {
                var elementBase = employeesAttendance[i];
                var daySchedulesBase = elementBase.DaySchedules;
                int cont = 0;

                if (i + 1 < emploreeAttendanceB.Count)
                {
                    for (var j = i + 1; j <= emploreeAttendanceB.Count - 1; j++)
                    {
                        var elementB = emploreeAttendanceB[j];

                        var elementIterator = emploreeAttendanceB[j];
                        if (elementBase.EmployeeName != elementIterator.EmployeeName)
                        {
                            var daySchedulesBaseB = elementIterator.DaySchedules;

                            cont = GetScheduleConcurrency(daySchedulesBase, daySchedulesBaseB);
                            if (cont > 0)
                            {
                                var employeeAttendanceConcurrency = new EmployeeAttendanceConcurrency();
                                employeeAttendanceConcurrency.ConcurrencyNumber = cont;
                                employeeAttendanceConcurrency.PeopleName = elementBase.EmployeeName + " - " + elementIterator.EmployeeName;
                                employeesAttendanceConcurrency.Add(employeeAttendanceConcurrency);
                            }

                        }
                    }
                }
            }

            return employeesAttendanceConcurrency;
        }

        //
        // Summary:
        //     Process input file into an object.
        //
        // Parameters:
        //   filePath:
        //     Input file path.
        //   splitOne:
        //     String that infor how to divide each line, it refers employee name and schedule.
        //   splitTwo:
        //     String that infor how to divide each line, it refers to employee´s schedule.
        //   dateString:
        //     String that inform if it is necesary to add a year/month/day string.
        //
        // Returns:
        //     Object type IEnumerable<EmployeeAttendance> with input file information.
        public IEnumerable<EmployeeAttendance> GetModel(string filePath, string splitOne, string splitTwo, string dateString)
        {
            var employeesAttendance = new List<EmployeeAttendance>();
            int lineNumber = 0;

            using (StreamReader ReaderObject = new StreamReader(filePath))
            {
                string Line;
                while ((Line = ReaderObject.ReadLine()) != null)
                {
                    if (Line.Trim() != "")
                    {
                        string[] words = Line.Split(splitOne);
                        string[] sectionDays = words[1].Split(splitTwo);
                        var employeeAttendance = new EmployeeAttendance();

                        employeeAttendance.EmployeeName = words[0].Trim();
                        employeeAttendance.FileLine = Line.Trim();
                        employeeAttendance.errorRegister.HasError = false;
                        bool isOk = true;
                        foreach (var element in sectionDays)
                        {
                            employeeAttendance = AnalizeLine(employeeAttendance, element, dateString);
                            if (employeeAttendance.EmployeeName == null)
                            {
                                lineNumber++;
                                employeeAttendance.errorRegister.HasError = true;
                                employeeAttendance.errorRegister.FileLine = Line.Trim();
                                employeeAttendance.errorRegister.FileLineNumber = lineNumber;
                                employeeAttendance.errorRegister.File_Path = filePath;

                                break; //there was a error with Line processing.
                            }
                        }
                            employeesAttendance.Add(employeeAttendance);
                    } 
                    else
                    {
                        lineNumber++;
                    }
                }
            }

            
            return employeesAttendance;
        }

        //
        // Summary:
        //     Process input file's and search for concurrencies. 
        //
        // Parameters:
        //   ListA:
        //     Input lines collections.
        //   ListB:
        //     Input lines collections to be compared vs ListA
        //
        // Returns:
        //     EmployeeAttendance.
        private int GetScheduleConcurrency(List<SchedulePerDay> ListA, List<SchedulePerDay> ListB)
        {
            int cont = 0;

            foreach (var elementA in ListA)
            {
                foreach (var elementB in ListB)
                {
                    if (elementA.DayName.ToUpper() == elementB.DayName.ToUpper())
                    {
                        if (elementA.DateTimeBegin >= elementB.DateTimeBegin && elementA.DateTimeEnd <= elementB.DateTimeEnd)
                        {
                            cont++;
                            break;
                        }
                    }
                }
            }

            return cont;
        }

        //
        // Summary:
        //     Process input file's lines and convert them to objects. 
        //
        // Parameters:
        //   employeeAttendance:
        //     Initial information about employee.
        //   element:
        //     Input file's line schedule to be processed.
        //   dateString:
        //     DateTime or empty to add to time.
        //
        // Returns:
        //     EmployeeAttendance.
        private EmployeeAttendance AnalizeLine(EmployeeAttendance employeeAttendance, string element, string dateString)
        {
            DateTime dateTimeBegin;
            DateTime dateTimeEnd;
            bool isOk = true;
            string dayName = element.Substring(0, 2);
            string dateTimeString = element.Substring(2, 5);
            //dateName
            if (validateData("IS_DAY", dayName, "") && isOk)
            {
                if (dateString == "")
                {
                    dateTimeString = element.Substring(2, 5);
                }
                else
                {
                    dateTimeString = dateString + " " + element.Substring(2, 5);
                }
                //dateTimeBegin
                if (validateData("IS_DATETIME", dateTimeString, "") && isOk)
                {
                    dateTimeBegin = Convert.ToDateTime(dateTimeString);

                    if (dateString == "")
                    {
                        dateTimeString = element.Substring(8, 5);
                    }
                    else
                    {
                        dateTimeString = dateString + " " + element.Substring(8, 5);
                    }
                    //dateTimeEnd
                    if (validateData("IS_DATETIME", dateTimeString, "") && isOk) 
                    {
                        dateTimeEnd = Convert.ToDateTime(dateTimeString);

                        SchedulePerDay schedulePerDay = new SchedulePerDay()
                        {
                            DayName = dayName,
                            DateTimeBegin = dateTimeBegin,
                            DateTimeEnd = dateTimeEnd
                        };

                        employeeAttendance.DaySchedules.Add(schedulePerDay);
                    }
                    else
                    {
                        isOk = false; //dateTimeEnd
                    }
                }
                else
                {
                    isOk = false; //dateTimeBegin
                }
            }
            else
            {
                isOk = false; //dateName
            }

            if (isOk)
            {
                return employeeAttendance;
            }

            return new EmployeeAttendance();
        }

        //
        // Summary:
        //     Process input file and validate a day name and a valid data time.
        //
        // Parameters:
        //   option:
        //     Validate data case.
        //   value:
        //     DateTime to be validated.
        //   stringToSearch:
        //     String to validate exist into a value.
        //
        // Returns:
        //     Bool.
        private bool validateData(string option, string value, string stringToSearch)
        {
            bool notFound = false;
            switch (option.ToUpper())
            {
                case "IS_DAY":
                    if (Enum.IsDefined(typeof(Days), value))
                        return true;

                    return notFound;
                case "IS_DATETIME":
                    DateTime temp;
                    if (DateTime.TryParse(value, out temp))
                        return true;

                    return notFound;
                case "HAS_STRING":
                    if(value.Contains(stringToSearch))
                        return true;

                    return notFound;
                default:
                    break;
            }
            return false;
        }
    }
}

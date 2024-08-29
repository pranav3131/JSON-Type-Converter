using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSON_Type_Convert.Models
{
    // "Employee" is the model/class name 
    public class Employee
    {
        //Declaring the properties
        public string name { get; set; }    // Name of the employee
        public DateTime birthdate { get; set; }  // Birthdate of the employee
        public double salary {  get; set; }      // Salary of the employee
        public string address {  get; set; }     // Address of the employee
        public bool married {  get; set; }       // Marital status of the employee 

    }
}
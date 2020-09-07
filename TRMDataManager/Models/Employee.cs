using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRMDataManager.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
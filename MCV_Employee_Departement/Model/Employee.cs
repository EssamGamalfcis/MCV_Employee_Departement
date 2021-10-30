using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCV_Employee_Department.API.Model
{
    public class Employee : SimpleBase
    {
        [Required]
        public DateTime HiringDate { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string Title { get; set; }

        [ForeignKey("Department")]

        public long? DepartmentID { get; set; }

        public virtual Department Department { get; set; }
        [NotMapped]
        public string DepartmentName { get; set; }
    }
}

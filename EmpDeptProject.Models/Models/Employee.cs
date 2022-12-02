using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDeptProject.Models.Models
{
    public class Employee
    {
        
        

        [Key]
        public int EmpId { get; set; }

        [StringLength(20, ErrorMessage = "Length should be 20 characater only")]
        [Required]
        [Display(Name = "Employee Name")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Please Enter the valid Employee name")]

        public string EmpName { get; set; }

        [Display(Name = "Employee Salary")]
        [Range(1, int.MaxValue, ErrorMessage = "The Salary {0} must be greater than {1}.")]
        public int EmpSalary { get; set; }

        [Display(Name = "Employee DeptId")]
        public int DeptId { get; set; }

        [ForeignKey("DeptId")]

        public Department? Departments { get; set; }

    }
}

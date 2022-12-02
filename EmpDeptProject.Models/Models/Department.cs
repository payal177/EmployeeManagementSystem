using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDeptProject.Models.Models
{
    public class Department
    {
       
       


        [Key]

        public int DeptId { get; set; }
        [StringLength(20)]
        [Required]
        [Display(Name = "Department Name")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Please Enter the valid Department name")]

        public string DeptName { get; set; }
    }
}

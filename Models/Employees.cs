using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsidTestApplication.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "CEO")]
        public bool IsCEO { get; set; }
        [Display(Name = "Manager")]
        public bool IsManager { get; set; }
        public int? ManagerId { get; set; }
        public string FullName { get { return this.FirstName + " " + this.LastName;}}
        public Employees()
        {

        }

    }
}

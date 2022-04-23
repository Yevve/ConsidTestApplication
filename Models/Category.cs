using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsidTestApplication.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        //public List<LibraryItem> LibraryItems { get; set; }
        public Category()
        {

        }
    }
}

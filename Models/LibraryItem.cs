using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsidTestApplication.Models
{
    public class LibraryItem
    {
        [Key]
        public int ItemID { get; set; }
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? Pages { get; set; }
        [Display(Name = "Duration")]
        public int? RunTimeMinutes { get; set; }
        [Display(Name = "Borrowable")]
        public bool IsBorrowable { get; set; }
        public string? Borrower { get; set; }
        [Display(Name = "Borrow Date")]
        public DateTime? BorrowDate { get; set; }
        public string Type { get; set; }

        public LibraryItem()
        {

        }


    }
}

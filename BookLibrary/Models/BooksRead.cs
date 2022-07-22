using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Models
{
    public class BooksRead
    {
        public int BooksReadId { get; set; }
        [ValidateNever]
        public Book Book { get; set; }
        public int BookId { get; set; }
        [ValidateNever]
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }
        [NotMapped]
        public TimeSpan TimeToFinish { get {
                if (EndDate != null && StartDate != null) return EndDate.Value - StartDate.Value;
                else return new TimeSpan(0, 0, 0);
            } }
    }
}

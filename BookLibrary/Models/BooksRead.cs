using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [NotMapped]
        public TimeSpan TimeToFinish { get { return EndDate.Value - StartDate.Value; } }
    }
}

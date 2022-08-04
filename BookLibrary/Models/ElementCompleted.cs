namespace BookLibrary.Models
{
    public class ElementCompleted
    {
        public int ElementCompletedId { get; set; }
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }
        public ChallengeElement ChallengeElement { get; set; }
        public int ChallengeElementId { get; set; }
        public BooksRead BooksRead { get; set; }
        public int BooksReadId { get; set; }
    }
}

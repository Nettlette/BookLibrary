using System.ComponentModel;

namespace BookLibrary.Models
{
    public class ReaderStats
    {
        public int ReaderId { get; set; }
        // Average Time to Finish
        [DisplayName("Average Time To Finish Fiction")]
        public decimal FictionAvg { get; set; }
        [DisplayName("Average Time To Finish Nonfiction")]
        public decimal NonFictionAvg { get; set; }
        [DisplayName("Average Time To Finish")]
        public decimal TotalAvg { get; set; }
        //  Total Counts
        [DisplayName("Number of Fiction Read")]
        public int FictionCount { get; set; }
        [DisplayName("Number of Nonfiction Read")]
        public int NonfictionCount { get; set; }
        [DisplayName("Total Books Read")]
        public int TotalCount { get; set; }
        // Total Pages
        [DisplayName("Total Fiction Pages Read")]
        public int FictionPg { get; set; }
        [DisplayName("Total Nonfiction Pages Read")]
        public int NonfictionPg { get; set; }
        [DisplayName("Total Pages Read")]
        public int TotalPg { get; set; }
        // Total Hours
        [DisplayName("Total Fiction Hours Read")]
        public decimal FictionHr { get; set; }
        [DisplayName("Total Nonfiction Hours Read")]
        public decimal NonfictionHr { get; set; }
        [DisplayName("Total Hours Read")]
        public decimal TotalHr { get; set; }
        // Fastest Read by Page
        [DisplayName("Fastest Book Read by Pages/Day")]
        public int FastPgBookId { get; set; }
        public Book FastPgBook { get; set; }
        [DisplayName("Time To Finish")]
        public decimal FastPgTimeToFinish { get; set; }
        [DisplayName("Pages/Day Speed")]
        public decimal FastPgSpeedPg { get; set; }
        [DisplayName("Hours/Day Speed")]
        public decimal? FastPgSpeedHr { get; set; }
        // Fastest Read by Hour
        [DisplayName("Fastest Book Read by Hours/Day")]
        public int FastHrBookId { get; set; }
        public Book FastHrBook { get; set; }
        [DisplayName("Time To Finish")]
        public decimal FastHrTimeToFinish { get; set; }
        [DisplayName("Pages/Day Speed")]
        public decimal FastHrSpeedPg { get; set; }
        [DisplayName("Hours/Day Speed")]
        public decimal FastHrSpeedHr { get; set; }
        // Slowest Read by Page
        [DisplayName("Slowest Book Read by Pages/Day")]
        public int SlowPgBookId { get; set; }
        public Book SlowPgBook { get; set; }
        [DisplayName("Time To Finish")]
        public decimal SlowPgTimeToFinish { get; set; }
        [DisplayName("Pages/Day Speed")]
        public decimal? SlowPgSpeedPg { get; set; }
        [DisplayName("Hours/Day Speed")]
        public decimal? SlowPgSpeedHr { get; set; }
        //Slowest Read by Hour
        [DisplayName("Slowest Book Read by Hours/Day")]
        public int SlowHrBookId { get; set; }
        public Book SlowHrBook { get; set; }
        [DisplayName("Time To Finish")]
        public decimal SlowHrTimeToFinish { get; set; }
        [DisplayName("Pages/Day Speed")]
        public decimal? SlowHrSpeedPg { get; set; }
        [DisplayName("Hours/Day Speed")]
        public decimal? SlowHrSpeedHr { get; set; }
        // Newest, Oldest, Longest, Shortest
        [DisplayName("Newest Book")]
        public int NewBookId { get; set; }
        public Book NewBook { get; set; }
        [DisplayName("Oldest Book")]
        public int OldBookId { get; set; }
        public Book OldBook { get; set; }
        [DisplayName("Longest Book")]
        public int LongBookId { get; set; }
        public Book LongBook { get; set; }
        [DisplayName("Shortest Book")]
        public int ShortBookId { get; set; }
        public Book ShortBook { get; set; }
    }
}

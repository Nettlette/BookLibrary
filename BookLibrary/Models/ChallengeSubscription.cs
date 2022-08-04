namespace BookLibrary.Models
{
    public class ChallengeSubscription
    {
        public int ChallengeSubscriptionId { get; set; }
        public Challenge Challenge { get; set; }
        public int ChallengeId { get; set; }
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }
    }
}

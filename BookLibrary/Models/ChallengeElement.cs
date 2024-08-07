﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookLibrary.Models
{
    public class ChallengeElement
    {
        public int ChallengeElementId { get; set; }
        [ValidateNever]
        public Challenge Challenge { get; set; }
        public int ChallengeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Order { get; set; }
    }
}

using System;
using Beeffective.Common;

namespace Beeffective.Models
{
    class ItemModel : Observable
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Categories { get; set; }

        public bool IsSelected { get; set; }

        public int Iterations { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Details { get; set; }

        public DateTime CompletedAt { get; set; }

        public int Urgency { get; set; }

        public int Importance { get; set; }

        public TimeSpan AggregatedTime { get; set; }
    }
}

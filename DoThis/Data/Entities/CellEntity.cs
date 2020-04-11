using System;

namespace Beeffective.Data.Entities
{
    public class CellEntity : Entity
    {
        public string Title { get; set; }
        public double Importance { get; set; }
        public double Urgency { get; set; }
        public string Goal { get; set; }
        public string Tags { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }
}
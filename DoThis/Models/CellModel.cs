﻿using System;
using Beeffective.Common;

namespace Beeffective.Models
{
    public class CellModel : Observable, IEquatable<CellModel>
    {
        private string title;
        private double importance;
        private double urgency;
        private string goal;
        private string tags;
        private TimeSpan timeSpent;

        public int Id { get; set; }

        public string Title
        {
            get => title;
            set
            {
                if (Equals(title, value)) return;
                title = value;
                OnPropertyChanged();
            }
        }

        public double Importance
        {
            get => importance;
            set
            {
                if (Equals(importance, value)) return;
                importance = value;
                OnPropertyChanged();
            }
        }

        public double Urgency
        {
            get => urgency;
            set
            {
                if (Equals(urgency, value)) return;
                urgency = value;
                OnPropertyChanged();
            }
        }

        public string Goal
        {
            get => goal;
            set
            {
                if (Equals(goal, value)) return;
                goal = value;
                OnPropertyChanged();
            }
        }

        public string Tags
        {
            get => tags;
            set
            {
                if (Equals(tags, value)) return;
                tags = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan TimeSpent
        {
            get => timeSpent;
            set
            {
                if (Equals(timeSpent, value)) return;
                timeSpent = value;
                OnPropertyChanged();
            }
        }

        public bool Equals(CellModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CellModel) obj);
        }

        public override int GetHashCode() => Id;

        public static bool operator ==(CellModel left, CellModel right) => 
            Equals(left, right);

        public static bool operator !=(CellModel left, CellModel right) => 
            !Equals(left, right);
    }
}
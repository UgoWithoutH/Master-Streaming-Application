using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class OeuvreWatch : IEquatable<OeuvreWatch>, IComparable<OeuvreWatch>, IComparable
    {
        public DateTime TimeAdd { get; }

        public Oeuvre Oeuvre { get; set; }

        public OeuvreWatch(DateTime timeAdd, Oeuvre oeuvre)
        {
            TimeAdd = timeAdd;
            Oeuvre = oeuvre;
        }

        public override string ToString()
        {
            return $"{Oeuvre.Titre} , {Oeuvre.DateSortie} , {Oeuvre.Note} , {Oeuvre.Description}";
        }

        public bool Equals(OeuvreWatch other)
        {
            return Oeuvre.Titre.Equals(other.Oeuvre.Titre);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as OeuvreWatch);
        }

        public int CompareTo(OeuvreWatch other)
        {
            return TimeAdd.CompareTo(other.TimeAdd);
        }

        int IComparable.CompareTo(object obj)
        {
            if (!(obj is OeuvreWatch))
            {
                throw new ArgumentException("Argument is not an OeuvreWatch", "obj");
            }
            OeuvreWatch otheroeuvre = obj as OeuvreWatch;
            return this.CompareTo(otheroeuvre);
        }

        public static bool operator <(OeuvreWatch left, OeuvreWatch right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(OeuvreWatch left, OeuvreWatch right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(OeuvreWatch left, OeuvreWatch right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(OeuvreWatch left, OeuvreWatch right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}

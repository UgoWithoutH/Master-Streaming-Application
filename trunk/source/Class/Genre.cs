using System;
using System.Collections.Generic;

namespace Class
{
    public class Genre : IEquatable<Genre>, IComparable<Genre>, IComparable
    {
        public string Nom { 
            get => nom;
            private set => nom = value.ToUpper();
           }
        private string nom;

        public Genre (string nom)
        {
            Nom = nom;
        }

        public string getNom()
        {
            return Nom;
        }

        public override string ToString()
        {
            return Nom;
        }

        public bool Equals(Genre other)
        {
            if (other == null) return false;
            return Nom.Equals(other.Nom);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType().Equals(obj.GetType())) return false;

            return Equals(obj as Genre);
        }
        public override int GetHashCode()
        {
            int hashCode = 1067912786;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nom);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nom);
            return hashCode;
        }

        public int CompareTo(Genre other)
        {
            return Nom.CompareTo(other.Nom);
        }

        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Genre))
            {
                throw new ArgumentException("Argument is not a Genre", "obj");
            }
            Genre othergenre = obj as Genre;
            return this.CompareTo(othergenre);
        }

        public static bool operator <(Genre left, Genre right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Genre left, Genre right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Genre left, Genre right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Genre left, Genre right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}

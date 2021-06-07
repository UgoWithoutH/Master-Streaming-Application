using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Class
{
    /// <summary>
    /// Grande famille de films/séries (ex : Humour, Horreur, ...)
    /// </summary>
    [DataContract]
    public class Genre : ObservableObject, IEquatable<Genre>, IComparable<Genre>, IComparable
    {
        /// <summary>
        /// nom du genre
        /// </summary>
        public string Nom { 
            get => nom;
            private set 
            { 
                nom = value.ToUpper();
            }
        }
        [DataMember]
        private string nom;

        public Genre (string nom)
        {
            Nom = nom;
        }

        public override string ToString()
        {
            return Nom;
        }

        /// <summary>
        /// On considère que 2 Genre sont égaux si leurs attributs Nom sont égaux
        /// </summary>
        /// <param name="other">Genre avec lequel comparer</param>
        /// <returns>true si leurs attributs Nom sont égaux et si le Nom du Genre avec lequel comparer n'est pas null, false sinon</returns>
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
            return Nom.GetHashCode();
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

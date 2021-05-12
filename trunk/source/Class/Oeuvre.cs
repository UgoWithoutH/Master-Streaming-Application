using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public abstract class Oeuvre : IEquatable<Oeuvre>, IComparable<Oeuvre>, IComparable
    {

        public string Titre { get; set; }

        public DateTime DateSortie { get; set; }

        public int? Note {
            get
            {
                if (note == null) return 0;
                else return note;
            }
            set
            {
                if (value < 0) note = 0;
                else if (value > 5) note = 5;
                else note = value;
            }
        }
        private int? note;
        public string Description { get; set; }

        public string ImageName { get; set; }

        public List<Auteur> ListAuteur { get; set; } = null;

        public HashSet<Genre> TagsGenres { get; private set; }

        protected Oeuvre(string titre, DateTime dateSortie, string description, int? note,string imageName,HashSet<Genre> tagsgenres)
        {
            Titre = titre;
            DateSortie = dateSortie;
            Description = description;
            Note = note;
            ImageName = imageName;
            TagsGenres = tagsgenres;
        }

        protected Oeuvre(string titre, DateTime dateSortie, string description, int? note, string imageName, List<Auteur> listAuteurs, HashSet<Genre> tagsgenres)
            : this(titre, dateSortie, description, note,imageName,tagsgenres)
        {
            ListAuteur = listAuteurs;
        }

        public override string ToString()
        {
            return ToString(); //équivalant à this.ToString(), faisant référence au ToString de la classe dérivée
        }

        public bool Equals(Oeuvre other)
        {
            if (other == null) return false;
            return Titre.Equals(other.Titre);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType().Equals(obj.GetType())) return false;

            return Equals(obj as Oeuvre);
        }

        public void addTagsGenres(HashSet<Genre> tagsGenres)
        {
            TagsGenres = tagsGenres;
        }

        public override int GetHashCode()
        {
            return 590323563 + EqualityComparer<string>.Default.GetHashCode(Titre);
        }

        public int CompareTo(Oeuvre other)
        {
            return Titre.CompareTo(other.Titre);
        }

        int IComparable.CompareTo(object obj)
        {
            if(!(obj is Oeuvre))
            {
                throw new ArgumentException("Argument is not an Oeuvre","obj");
            }
            Oeuvre otheroeuvre = obj as Oeuvre;
            return this.CompareTo(otheroeuvre);
        }

        public static bool operator <(Oeuvre left, Oeuvre right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Oeuvre left, Oeuvre right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Oeuvre left, Oeuvre right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Oeuvre left, Oeuvre right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}

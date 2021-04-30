using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public abstract class Oeuvre : IEquatable<Oeuvre>
    {

        public string Titre { get; set; }

        public DateTime DateSortie { get; set; }

        public int? Note {
            get => note;
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

        protected Oeuvre(string titre, DateTime dateSortie, string description, string imageName,HashSet<Genre> tagsgenres)
        {
            Titre = titre;
            DateSortie = dateSortie;
            Description = description;
            ImageName = imageName;
            TagsGenres = tagsgenres;
        }

        protected Oeuvre(string titre, DateTime dateSortie, int? note, string description, string imageName, List<Auteur> listAuteurs, HashSet<Genre> tagsgenres)
            : this(titre, dateSortie, description, imageName,tagsgenres)
        {
            Note = note;
            ListAuteur = listAuteurs;
        }

        public override string ToString()
        {
            return ToString(); //équivalant à this.ToString(), faisant référence au ToString de la classe dérivée
        }

        public bool Equals(Oeuvre other)
        {
            return Titre.Equals(other.Titre);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (GetType() != obj.GetType()) return false;

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
    }
}

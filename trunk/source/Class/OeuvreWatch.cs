using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class OeuvreWatch : Oeuvre
    {
        public DateTime TimeAdd { get; }
        public Oeuvre OriginalOeuvre { get; }

        public OeuvreWatch (DateTime timeAdd, Oeuvre o)
            :base(o.Titre, o.DateSortie, o.Description, o.Note, o.ImageName, o.TagsGenres)
        {
            TimeAdd = timeAdd;
            OriginalOeuvre = o;
        }

        public OeuvreWatch (DateTime timeAdd, Oeuvre o, string titre, DateTime dateSortie, string description, int? note, string imageName, HashSet<Genre> tagsgenres)
            :base (titre, dateSortie, description, note, imageName, tagsgenres)
        {
            TimeAdd = timeAdd;
            OriginalOeuvre = o;
        }

        public OeuvreWatch(DateTime timeAdd, Oeuvre o, int? note, List<Auteur> listAuteurs, string titre, DateTime dateSortie, string description, string imageName, HashSet<Genre> tagsgenres)
           : base(titre, dateSortie, description, note, imageName, listAuteurs, tagsgenres)
        {
            TimeAdd = timeAdd;
            OriginalOeuvre = o;
        }
    }
}

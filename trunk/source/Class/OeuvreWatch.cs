using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class OeuvreWatch : Oeuvre
    {
        public DateTime TimeAdd { get; }

        public OeuvreWatch(DateTime timeAdd, Oeuvre o)
           : base(o.Titre, o.DateSortie, o.Description, o.Note, o.ImageName, o.ListAuteur,o.TagsGenres)
        {
            TimeAdd = timeAdd;
        }

        public override string ToString()
        {
            return $"{Titre} , {DateSortie} , {Note} , {Description}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Class
{
    [DataContract]
    public class OeuvreWatch : IEquatable<OeuvreWatch>
    {
        [DataMember]
        public DateTime TimeAdd { get; set; }

        [DataMember]
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
            if (GetType() != (obj.GetType())) return false;

            return Equals(obj as OeuvreWatch);
        }
    }
}

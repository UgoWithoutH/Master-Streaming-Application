using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Class
{
    [DataContract]
    public class OeuvreWatch : IEquatable<OeuvreWatch>
    {
        /// <summary>
        /// Date de création de cette OeuvreWatch
        /// </summary>
        [DataMember]
        public DateTime TimeAdd { get; set; }

        /// <summary>
        /// Oeuvre donnée dans le constructeur
        /// </summary>
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


        /// <summary>
        /// On considère que 2 OeuvreWatch sont égales si leur attribut Oeuvre.Titre est égal
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

        public override int GetHashCode()
        {
            return Oeuvre.Titre.GetHashCode();
        }
    }
}

using Swordfish.NET.Collections;
using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace Class

{
    [DataContract]
    public class Watchlist

    {
        [DataMember]
        public ObservableCollection<OeuvreWatch> OeuvresVisionnees { get; private set; }

        public Watchlist()

        {

            OeuvresVisionnees = new ObservableCollection<OeuvreWatch>();

        }

        public void AjouterOeuvre(Oeuvre o)

        {
            OeuvresVisionnees.Add(new OeuvreWatch(DateTime.Now, o));
        } 

        public void SupprimerOeuvre(Oeuvre o)

        {

            foreach (OeuvreWatch ow in OeuvresVisionnees)

                if (o.Titre.Equals(ow.Oeuvre.Titre))
                    OeuvresVisionnees.Remove(ow);
        }

    }

}


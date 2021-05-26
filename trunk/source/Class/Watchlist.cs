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

        public bool AjouterOeuvre(Oeuvre o)

        {

            int nbInWatchlistBefore = OeuvresVisionnees.Count;

            OeuvresVisionnees.Add(new OeuvreWatch(DateTime.Now,o));

            int nbInWatchlistAfter = OeuvresVisionnees.Count;

            return nbInWatchlistAfter > nbInWatchlistBefore;

        }

        public bool SupprimerOeuvre(Oeuvre o)

        {

            foreach (OeuvreWatch ow in OeuvresVisionnees)

                if (o.Titre.Equals(ow.Oeuvre.Titre))
                    return OeuvresVisionnees.Remove(ow);

            return false;

        }

    }

}


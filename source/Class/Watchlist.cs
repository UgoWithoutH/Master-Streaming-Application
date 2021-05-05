using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class Watchlist
    {
        private List<OeuvreWatch> OeuvresVisionnees { get; set; }

        public Watchlist()
        {
            OeuvresVisionnees = new List<OeuvreWatch>();
        }

        public bool AjouterOeuvre(Oeuvre o)
        {
            int nbInWatchlistBefore = OeuvresVisionnees.Count;
            OeuvresVisionnees.Add(new OeuvreWatch(DateTime.Now, o));
            int nbInWatchlistAfter = OeuvresVisionnees.Count;
            return nbInWatchlistAfter > nbInWatchlistBefore;
        }

        public bool SupprimerOeuvre(Oeuvre o)
        {
            foreach (OeuvreWatch ow in OeuvresVisionnees)
                if (ow.OriginalOeuvre.Equals(o))
                    return OeuvresVisionnees.Remove(ow);
            return false;
        }
    }
}

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
        /// <summary>
        /// Collection stockant les oeuvres déja visionnées du compte utilisateur
        /// </summary>
        [DataMember]
        public ObservableCollection<OeuvreWatch> OeuvresVisionnees { get; private set; }

        public Watchlist()

        {

            OeuvresVisionnees = new ObservableCollection<OeuvreWatch>();

        }

        /// <summary>
        /// Ajoute une Oeuvre à la collection OeuvresVisionnees
        /// </summary>
        /// <param name="o">Oeuvre voulant être ajoutée</param>
        public void AjouterOeuvre(Oeuvre o)

        {
            OeuvresVisionnees.Add(new OeuvreWatch(DateTime.Now, o));
        }

        /// <summary>
        /// Supprime une Oeuvre de la collection OeuvresVisionnees
        /// </summary>
        /// <param name="o">Oeuvre voulant être supprimée</param>
        public void SupprimerOeuvre(Oeuvre o)

        {
            OeuvresVisionnees.Remove(new OeuvreWatch(DateTime.Now, o));
        }

    }

}


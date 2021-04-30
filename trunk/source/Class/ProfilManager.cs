﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Class
{
    public class ProfilManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyname)
         => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));


        public ObservableCollection<Genre> ListGenres { get; private set; } = new ObservableCollection<Genre>() { new Genre("Humour"),
                                                                                                                  new Genre("Romance") };

        public string Nom { get; private set; }

        public Dictionary<Genre, HashSet<Oeuvre>> ListOeuvres { get; private set; }

        public LinkedList<Serie> listing;

        public Oeuvre OeuvreSelectionnée { get; set; }

        public void ModifierNomProfil(string nouveauNom)
        {
            Nom = nouveauNom;
        }

        public ProfilManager()
        {
            ListOeuvres = new Dictionary<Genre, HashSet<Oeuvre>>();
            listing = new LinkedList<Serie>();
        }

        public bool AjouterGenre(Genre g)
        {
            if (g == null) return false;

            if (!ListOeuvres.ContainsKey(g))
            {
                ListOeuvres.Add(g, null);
                return true;
            }
            else return false;
            
        }

        public bool SupprimerGenre(Genre g)
        {
            if (g == null) return false;

            if (ListOeuvres.ContainsKey(g))
            {
                ListOeuvres.Remove(g);
                return true;
            }
            else return false;
        }

        public bool AjouterOeuvre(Oeuvre o)
        {

            if (o is Serie) listing.AddFirst((Serie)o);

            bool res = false;

            if (o == null) return false;

            foreach (Genre g in o.TagsGenres)
            { 
                if (ListOeuvres.ContainsKey(g))
                {
                    ListOeuvres.TryGetValue(g, out HashSet<Oeuvre> value);

                    if (value == null)
                    {
                        ListOeuvres[g] = new HashSet<Oeuvre>();
                        ListOeuvres[g].Add(o);
                        res = true;
                    }
                    else if (!value.Contains(o))
                    {
                        ListOeuvres[g].Add(o);
                        res = true;
                    }
                }  
            }

            return res;
        }

        public bool SupprimerOeuvre(Oeuvre o)
        {
            if (o is Serie) listing.Remove((Serie)o);

            bool res = false;

            if (o == null) return false;

            foreach (Genre g in o.TagsGenres)
            {
                ListOeuvres.TryGetValue(g, out HashSet<Oeuvre> value);
                if (value.Contains(o))
                {
                    value.Remove(o);
                    res = true;
                }
            }

            return res;
        }
    }
}
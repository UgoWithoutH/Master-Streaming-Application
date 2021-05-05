using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class Auteur : IEquatable<Auteur>
    {
        public string Nom { get; private set; }

        public string Prenom { get; private set; }

        public Métier Profession { get; private set; }

        public Auteur(string nom, string prenom, Métier profession)
        {
            Nom = nom;
            Prenom = prenom;
            Profession = profession;
        }

        public override string ToString()
        {
            return $"Nom : {Nom} prénom : {Prenom} profession : {Profession}";
        }

        public bool Equals(Auteur other)
        {
            if (other == null) return false;
            return Nom.Equals(other.Nom) && Prenom.Equals(other.Prenom) && Profession.Equals(other.Profession);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false; // ou ReferenceEquals si == réécrit
            if (obj == this) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Auteur);
        }
    }
}

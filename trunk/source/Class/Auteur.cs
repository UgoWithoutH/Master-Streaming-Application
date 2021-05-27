using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Class
{
    [DataContract]
    public class Auteur : IEquatable<Auteur>, IDataErrorInfo
    {
        [DataMember]
        [Required]
        public string Nom { get; private set; } // passer public pour modif

        [DataMember]
        [Required]
        public string Prenom { get; private set; }

        [DataMember]
        [Required]
        public Métier Profession { get; private set; }

        public string Error { get; }

        /// <summary>
        /// Méthode qui permet de chercher tous les attributs et de valider ceux-ci 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {

                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                    GetType().GetProperty(columnName).GetValue(this)
                    , new ValidationContext(this)
                    {
                        MemberName = columnName
                    }
                    , validationResults))
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

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

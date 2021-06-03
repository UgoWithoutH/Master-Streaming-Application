using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Class
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, KnownType(typeof(Serie))]
    public abstract class Oeuvre : IEquatable<Oeuvre>, IComparable<Oeuvre>, IComparable, IDataErrorInfo, ICloneable
    {
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

        /// <summary>
        /// titre de l'Oeuvre
        /// </summary>
        [Required]
        [DataMember]
        public string Titre { get; set; }

        /// <summary>
        /// date de sortie de l'Oeuvre
        /// </summary>
        [Required]
        [DataMember(IsRequired = false, EmitDefaultValue = false)] //pour le bug du chargement de données  via JSON (DateTime.MinValue)
        public DateTime DateSortie { get; set; }

        /// <summary>
        /// note de l'Oeuvre. Si on veut récupérer la note et qu'elle est null, retourne 0 (note la plus basse).
        /// Si on veut setter la note à une valeur < 0 ou > 5, remplace cette valeur 0 ou 5 (respectivement min et max de la note).
        /// </summary>
        public int? Note {
            get
            {
                if (note == null) return 0;
                else return note;
            }
            set
            {
                if (value < 0) note = 0;
                else if (value > 5) note = 5;
                else note = value;
            }
        }
        [DataMember]
        private int? note;

        /// <summary>
        /// description de l'Oeuvre
        /// </summary>
        [Required]
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// chemin de l'image de l'Oeuvre depuis le dossier Master_Streaming
        /// </summary>
        [Required]
        [DataMember]
        public string ImageName { get; set; }

        /// <summary>
        /// List des Auteur ayant contribué à la création de l'Oeuvre
        /// </summary>
        [DataMember]
        public List<Auteur> ListAuteur { get; set; } = null;

        /// <summary>
        /// HashSet de Genre.
        /// Contient les Genre dont l'Oeuvre fait partie.
        /// </summary>
        [Required]
        [DataMember]
        public HashSet<Genre> TagsGenres { get; private set; }

        protected Oeuvre()
        {
            Titre = null;
            DateSortie = new DateTime();
            Description = null;
            note = null;
            ImageName = null;
            ListAuteur = new List<Auteur>();
            TagsGenres = new HashSet<Genre>();
        }

        protected Oeuvre(string titre, DateTime dateSortie, string description, int? note,string imageName, List<Auteur> listAuteurs, HashSet<Genre> tagsgenres)
        {
            Titre = titre;
            DateSortie = dateSortie;
            Description = description;
            Note = note;
            ImageName = imageName;
            TagsGenres = tagsgenres;
            ListAuteur = listAuteurs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>une copie exacte de cette Oeuvre</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return ToString(); //équivalant à this.ToString(), faisant référence au ToString de la classe dérivée
        }

        /// <summary>
        /// On considère que 2 Oeuvre sont égales si leurs attributs Titre sont égaux
        /// </summary>
        /// <param name="other">Oeuvre avec laquelle comparer</param>
        /// <returns>true si leurs attributs Titre sont égaux, false sinon</returns>
        public bool Equals(Oeuvre other)
        {
            return Titre.Equals(other.Titre);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != (obj.GetType())) return false;

            return Equals(obj as Oeuvre);
        }

        public override int GetHashCode()
        {
            return 590323563 + EqualityComparer<string>.Default.GetHashCode(Titre);
        }

        public int CompareTo(Oeuvre other)
        {
            return Titre.CompareTo(other.Titre);
        }

        int IComparable.CompareTo(object obj)
        {
            if(!(obj is Oeuvre))
            {
                throw new ArgumentException("Argument is not an Oeuvre","obj");
            }
            Oeuvre otheroeuvre = obj as Oeuvre;
            return this.CompareTo(otheroeuvre);
        }

        public static bool operator <(Oeuvre left, Oeuvre right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Oeuvre left, Oeuvre right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Oeuvre left, Oeuvre right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Oeuvre left, Oeuvre right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}

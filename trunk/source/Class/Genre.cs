using System;

namespace Class
{
    public class Genre : IEquatable<Genre>
    {
        public string Nom { 
            get => nom;
            private set => nom = value.ToUpper();
           }
        private string nom;

        public Genre (string nom)
        {
            Nom = nom;
        }

        public string getNom()
        {
            return Nom;
        }

        public override string ToString()
        {
            return Nom;
        }

        public bool Equals(Genre other)
        {
            return Nom.Equals(other.Nom);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Genre);
        }
    }
}

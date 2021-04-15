using System;

namespace Class
{
    public class Genre
    {
        public string Nom { get; private set; }

        public Genre (string nom)
        {
            Nom = nom;
        }

        public string getNom()
        {
            return Nom;
        }
    }
}

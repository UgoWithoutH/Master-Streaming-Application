using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
   public class Serie
    {
        public string Titre { get; private set; }

        public string ImageName { get; private set; }

        public Serie(string titre, string imageName)
        {
            Titre = titre;
            ImageName = imageName;
        }
    }
}

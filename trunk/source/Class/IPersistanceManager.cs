using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Class
{
    public interface IPersistanceManager
    {
        ObservableCollection<ProfilManager> ChargeDonnées();
        void SauvegardeDonnées(ObservableCollection<ProfilManager> ListProfils);
    }
}

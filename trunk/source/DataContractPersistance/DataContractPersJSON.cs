using Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace DataContractPersistance
{
    public class DataContractPersJSON : DataContractPers
    {
        public DataContractPersJSON()
        {
            RelativePath = "../../../../Master_Streaming/bin/Release/JSON";
            FileName = "Master_Streaming.json";
            Serializer = new DataContractJsonSerializer(typeof(ObservableCollection<ProfilManager>));
        }

        public override void SauvegardeDonnées(ObservableCollection<ProfilManager> ListProfils)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            using (Stream writer = File.Create(FileNameAndPath))
            {
                Serializer.WriteObject(writer, ListProfils);
            }
        }
    }
}


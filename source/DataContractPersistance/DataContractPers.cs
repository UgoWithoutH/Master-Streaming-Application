using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Class;

namespace DataContractPersistance
{
    public class DataContractPers : IPersistanceManager
    {
        public string FilePath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "../../../../Master_Streaming/bin/Debug/XML");

        public string FileName { get; set; } = "Master_Streaming.xml";

        string FileNameAndPath => Path.Combine(FilePath, FileName);

        ObservableCollection<ProfilManager> IPersistanceManager.ChargeDonnées()
        {
            if (!File.Exists(FileNameAndPath))
                throw new FileNotFoundException("Fichier de données manquant");

            ObservableCollection<ProfilManager> list_profils;

            var serializer = new DataContractSerializer(typeof(ObservableCollection<ProfilManager>));

            using (Stream s = File.OpenRead(FileNameAndPath))
            {
                list_profils = serializer.ReadObject(s) as ObservableCollection<ProfilManager>;
            }
            return list_profils;
        }

        void IPersistanceManager.SauvegardeDonnées(ObservableCollection<ProfilManager> ListProfils)
        {
            var serializer = new DataContractSerializer(typeof(ObservableCollection<ProfilManager>));

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            var settings = new XmlWriterSettings() { Indent = true };

            using(TextWriter tw = File.CreateText(FileNameAndPath))
            {
                using (XmlWriter writer = XmlWriter.Create(tw, settings))
                {
                    serializer.WriteObject(writer, ListProfils);
                }
            }
        }
    }
}

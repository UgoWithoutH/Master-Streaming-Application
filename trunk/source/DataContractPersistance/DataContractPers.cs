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
        public string FilePath => Path.Combine(Directory.GetCurrentDirectory(), RelativePath);

        public string RelativePath { get; set; } = "../../../../Master_Streaming/bin/Debug/XML";

        public string FileName { get; set; } = "Master_Streaming.xml";

        protected XmlObjectSerializer Serializer { get; set; } = new DataContractSerializer(typeof(ObservableCollection<ProfilManager>), new DataContractSerializerSettings() { PreserveObjectReferences = true });

        protected string FileNameAndPath => Path.Combine(FilePath, FileName);

        public virtual ObservableCollection<ProfilManager> ChargeDonnées()
        {
            if (!File.Exists(FileNameAndPath))
                throw new FileNotFoundException("Fichier de données manquant");

            ObservableCollection<ProfilManager> list_profils;

            using (Stream s = File.OpenRead(FileNameAndPath))
            {
                list_profils = Serializer.ReadObject(s) as ObservableCollection<ProfilManager>;
            }
            return list_profils;
        }

        public virtual void SauvegardeDonnées(ObservableCollection<ProfilManager> ListProfils)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            var settings = new XmlWriterSettings() { Indent = true };

            using(TextWriter tw = File.CreateText(FileNameAndPath))
            {
                using (XmlWriter writer = XmlWriter.Create(tw, settings))
                {
                    Serializer.WriteObject(writer, ListProfils);
                }
            }
        }
    }
}

using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace WpfTest
{
    public static class Repository
    {
        public static ObservableCollection<Diagnosis> LoadCollection(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("XML File: " + path + " not found");

            XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<Diagnosis>));
            using (TextReader reader = new StreamReader(path))
            {
                return Serializer.Deserialize(reader) as ObservableCollection<Diagnosis>;
            }
        }

        public static void SaveCollection(ObservableCollection<Diagnosis> collection, string path)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<Diagnosis>));
                Serializer.Serialize(writer, collection);
            }
        }
    }
}

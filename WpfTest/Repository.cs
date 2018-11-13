using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace WpfTest
{
    public class Repository
    {
        private readonly ObservableCollection<Diagnosis> _collection;
        private readonly string _path;

        public Repository(ObservableCollection<Diagnosis> collection, string path)
        {
            _path = path;
            _collection = collection;
        }

        public ObservableCollection<Diagnosis> LoadCollection()
        {
            if (!File.Exists(_path))
                throw new FileNotFoundException("XML File: " + _path + " not found");

            var serializer = new XmlSerializer(typeof(ObservableCollection<Diagnosis>));
            using (TextReader reader = new StreamReader(_path))
            {
                return serializer.Deserialize(reader) as ObservableCollection<Diagnosis>;
            }
        }

        public void SaveCollection(ObservableCollection<Diagnosis> collection)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<Diagnosis>));
            using (TextWriter writer = new StreamWriter(_path))
            {
                serializer.Serialize(writer, collection);
            }
        }
    }
}
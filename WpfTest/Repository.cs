using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace WpfTest
{
    public class Repository<T>
    {
        private readonly ObservableCollection<T> _collection;
        private readonly string _path;

        public Repository(ObservableCollection<T> collection, string path)
        {
            _path = path;
            _collection = collection;
        }

        public ObservableCollection<T> LoadCollection()
        {
            if (!File.Exists(_path))
                throw new FileNotFoundException("XML File: " + _path + " not found");

            var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
            using (TextReader reader = new StreamReader(_path))
            {
                return serializer.Deserialize(reader) as ObservableCollection<T>;
            }
        }

        public void SaveCollection(ObservableCollection<T> collection)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
            using (TextWriter writer = new StreamWriter(_path))
            {
                serializer.Serialize(writer, collection);
            }
        }
    }
}
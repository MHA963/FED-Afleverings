using Debt_Book.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Debt_Book.Data
{
    public class Repository
    {
        internal static ObservableCollection<Agent> ReadFile(string fileName)
        {
            //// Create an instance of the XmlSerializer class and specify the type of object to deserialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Agent>));
            TextReader reader = new StreamReader(fileName);
            var depters = (ObservableCollection<Agent>)serializer.Deserialize(reader);
            reader.Close();
            return depters;
        }
        internal static void SaveFile(string fileName, ObservableCollection<Agent> depters)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Agent>));
            TextWriter writer = new StreamWriter(fileName);
            // Serialize all the depters.
            serializer.Serialize(writer, depters);
            writer.Close();
        }
    }
}

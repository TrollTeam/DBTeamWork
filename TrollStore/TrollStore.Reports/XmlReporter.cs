using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrollStore.Reports
{
    public class XmlReporter<T>
    {
        private string outputFilePath;
        private string inputFilePath;
        private XmlSerializer serializer;

        public XmlReporter(string outputFilePath, string inputFilePath)
        {
            this.outputFilePath = outputFilePath;
            this.inputFilePath = inputFilePath;
            this.serializer = new XmlSerializer(typeof(List<T>));
        }

        public void GenerateReport(IEnumerable<T> collectionToSerialize)
        {
            using (var sw = new StreamWriter(this.outputFilePath))
            {
                serializer.Serialize(sw, collectionToSerialize);
            }
        }

        public IEnumerable<T> ReadData()
        {
            using (var sr = new StreamReader(this.inputFilePath))
            {
                return (List<T>)this.serializer.Deserialize(sr);
            }
        }
    }
}

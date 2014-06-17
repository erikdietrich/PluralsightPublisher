using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PluralsightPublisher.DataAccess
{
    public class BasicXmlDocument : IXmlDocument
    {

        public XDocument Load(string path)
        {
            return XDocument.Load(path);
        }

        public void Save(XElement root, string path)
        {
            root.Save(path);
        }
    }
}

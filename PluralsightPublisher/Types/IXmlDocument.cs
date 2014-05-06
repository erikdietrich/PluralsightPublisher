using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PluralsightPublisher.Types
{
    public interface IXmlDocument
    {
        XDocument Load(string path);

        void Save(XElement root, string path); 
    }
}

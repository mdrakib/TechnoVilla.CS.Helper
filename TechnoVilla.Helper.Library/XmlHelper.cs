using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TechnoVilla.Helper.Library
{
    public class XmlHelper
    {
        public void UpdateXml(string xmlPath, string xpath, string value)
        {
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException($"File '{xmlPath}' not found.");

            var doc = XDocument.Load(xmlPath);
            var element = doc.XPathEvaluate(xpath);
            if (element == null)
                throw new Exception("XPath element not found.");

            if (element is IEnumerable)
                foreach (var el in (element as IEnumerable))
                    UpdateElement(el, value);
            else UpdateElement(element, value);

            doc.Save(xmlPath);
        }

        private void UpdateElement(object element, string value)
        {
            if (element is XElement) (element as XElement).Value = value;
            if (element is XAttribute) (element as XAttribute).Value = value;
        }
    }
}

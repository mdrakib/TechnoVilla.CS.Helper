using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;

namespace TechnoVilla.Helper.Library.Tests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void UpdateXmlTest()
        {
            string xmlContent = @"<root>
    <connectionStrings>
        <add name=""ConnectionString"" connectionString=""blank"" />
    </connectionStrings>
</root>";

            string fileName = "test.xml";
            if (File.Exists(fileName)) File.Delete(fileName);

            File.WriteAllText(fileName, xmlContent);

            string xpath = "//connectionStrings/add/@connectionString";
            string value = "updated";

            XmlHelper helper = new XmlHelper();
            helper.UpdateXml(fileName, xpath, value);
            var element = XDocument.Load(fileName).XPathEvaluate(xpath);

            element = (element as IEnumerable<object>).FirstOrDefault();

            Assert.IsNotNull(element);            
            Assert.AreEqual(value, (element as XAttribute).Value);
        }
    }
}

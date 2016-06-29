using System;
using C = System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace TechnoVilla.Helper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new Exception("Please specify command. For help, type 'helper /help'");

                string command = args[0];
                
                switch(command)
                {
                    case "xml":
                        if (args.Length != 4)
                            throw new Exception("Missing required parameter. Please use 'Helper {filename} {xpath} {value}'");

                        string fileName = args[1];
                        string xpath = args[2];
                        string value = args[3];

                        if (!File.Exists(fileName))
                            throw new FileNotFoundException($"File '{fileName}' not found.");

                        var doc = XDocument.Load(fileName);
                        var element = doc.XPathSelectElement(xpath);
                        if (element == null)
                            throw new Exception("XPath element not found.");

                        element.Value = value;
                        doc.Save(fileName);

                        C.WriteLine($"Successfully updated file {fileName}");

                        break;
                    default:
                        throw new Exception($"Command '{command}' not found.");
                }

                Environment.Exit(0);
            }
            catch(Exception ex)
            {
                C.WriteLine("Error: " + ex.Message);
                Environment.Exit(-1);
            }
        }
    }
}

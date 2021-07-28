using System;
using System.Xml;

namespace Work_with_XPath
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var xDoc = new XmlDocument();

            xDoc.Load("users.xml");

            var xRoot = xDoc.DocumentElement;

            // выбор всех дочерних узлов
            //var childnodes = xRoot.SelectNodes("*");
            //foreach (XmlNode n in childnodes)
            //{
            //    Console.WriteLine(n.OuterXml);
            //}

            //var childnodes = xRoot.SelectNodes("user");
            //foreach (XmlNode n in childnodes)
            //{
            //    Console.WriteLine(n.SelectSingleNode("@name").Value);
            //}

            //var childnode = xRoot.SelectSingleNode("user[@name='Vitaly']");
            //if (childnode != null)
            //{
            //    Console.WriteLine(childnode.OuterXml);
            //}

            //var childnode = xRoot.SelectSingleNode("user[company='Microsoft']");
            //if (childnode != null)
            //{
            //    Console.WriteLine(childnode.OuterXml);
            //}

            var childnodes = xRoot.SelectNodes("//user/company");
            foreach (XmlNode n in childnodes)
            {
                Console.WriteLine(n.InnerText);
            }
        }
    }
}
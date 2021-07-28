using System;
using System.Xml;
using System.Collections.Generic;

namespace Work_with_System_dot_XML
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            OutputXml("users.xml");

            AddNewUserToXml(new User() {
                Name = "Alexey",
                Age = 11,
                Company = "Apple"
            }, "users.xml");

            RemoveFirstChildFromXml("users.xml");

            foreach (User user in GetUsersFromXml("users.xml"))
            {
                Console.WriteLine(user.ToString());
            }
        }

        private static void RemoveFirstChildFromXml(String filename)
        {
            var xmlDocument = new XmlDocument();

            xmlDocument.Load(filename);

            var xmlRoot = xmlDocument.DocumentElement;

            var firstChild = xmlRoot.FirstChild;

            xmlRoot.RemoveChild(firstChild);

            xmlDocument.Save(filename);
        }

        private static void AddNewUserToXml(User user, String filename)
        {
            var xmlDocument = new XmlDocument();

            xmlDocument.Load(filename);

            var userElement = xmlDocument.CreateElement("user");
            var userNameAttribute = xmlDocument.CreateAttribute("name");

            var ageElement = xmlDocument.CreateElement("age");
            var companyElement = xmlDocument.CreateElement("company");

            var nameText = xmlDocument.CreateTextNode(user.Name);
            var ageText = xmlDocument.CreateTextNode(user.Age.ToString());
            var companyText = xmlDocument.CreateTextNode(user.Company);

            userNameAttribute.AppendChild(nameText);
            ageElement.AppendChild(ageText);
            companyElement.AppendChild(companyText);

            userElement.Attributes.Append(userNameAttribute);
            userElement.AppendChild(companyElement);
            userElement.AppendChild(ageElement);

            xmlDocument.DocumentElement.AppendChild(userElement);

            xmlDocument.Save(filename);
        }

        private static List<User> GetUsersFromXml(String filename)
        {
            var users = new List<User>();

            var xmlDocument = new XmlDocument();

            xmlDocument.Load(filename);

            var xmlRoot = xmlDocument.DocumentElement;

            foreach (XmlNode xmlNode in xmlRoot)
            {
                var user = new User();

                var xmlAttribute = GetAttribute(xmlNode, "name");

                user.Name = xmlAttribute.Value;

                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    if (childNode.Name == "age")
                    {
                        user.Age = Int32.Parse(childNode.InnerText);
                    }

                    if (childNode.Name == "company")
                    {
                        user.Company = childNode.InnerText;
                    }
                }

                users.Add(user);
            }

            return users;
        }

        private static void OutputXml(String filename)
        {
            var xmlDocument = new XmlDocument();

            xmlDocument.Load(filename);

            var xmlRoot = xmlDocument.DocumentElement;

            foreach (XmlNode xmlNode in xmlRoot)
            {
                var xmlAttribute = GetAttribute(xmlNode, "name");

                Console.Write("Name: {1}{0}", Environment.NewLine, xmlAttribute.Value);

                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    if (childNode.Name == "age")
                    {
                        Console.Write("Age: {1}{0}", Environment.NewLine, childNode.InnerText);
                    }

                    if (childNode.Name == "company")
                    {
                        Console.Write("Company: {1}{0}", Environment.NewLine, childNode.InnerText);
                    }
                }

                Console.WriteLine();
            }
        }

        private static XmlNode GetAttribute(XmlNode xmlNode, String attributeName)
        {
            var xmlAttribute = default(XmlNode);

            if (xmlNode.Attributes.Count > 0)
            {
                xmlAttribute = xmlNode.Attributes.GetNamedItem(attributeName);
            }

            return xmlAttribute;
        }
    }
}
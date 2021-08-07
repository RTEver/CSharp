using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace LINQ_to_XML
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            foreach (Phone phone in GetPhones("phones.xml"))
            {
                Console.WriteLine("Name: {1}{0}Price: {2}{0}Company: {3}{0}{0}",
                    Environment.NewLine,
                    phone.Name,
                    phone.Price,
                    phone.Company);
            }

            Example_3();
        }

        private static void Example_1()
        {
            var xdoc = new XDocument();

            // создаем первый элемент
            var iphone6 = new XElement("phone");

            // создаем атрибут
            var iphoneNameAttr = new XAttribute("name", "iPhone 6");
            var iphoneCompanyElem = new XElement("company", "Apple");
            var iphonePriceElem = new XElement("price", "40000");

            // добавляем атрибут и элементы в первый элемент
            iphone6.Add(iphoneNameAttr);
            iphone6.Add(iphoneCompanyElem);
            iphone6.Add(iphonePriceElem);

            // создаем второй элемент
            var galaxys5 = new XElement("phone");

            var galaxysNameAttr = new XAttribute("name", "Samsung Galaxy S5");
            var galaxysCompanyElem = new XElement("company", "Samsung");
            var galaxysPriceElem = new XElement("price", "33000");

            galaxys5.Add(galaxysNameAttr);
            galaxys5.Add(galaxysCompanyElem);
            galaxys5.Add(galaxysPriceElem);

            // создаем корневой элемент
            var phones = new XElement("phones");

            // добавляем в корневой элемент
            phones.Add(iphone6);
            phones.Add(galaxys5);

            // добавляем корневой элемент в документ
            xdoc.Add(phones);

            //сохраняем документ
            xdoc.Save("phones.xml");
        }

        private static void Example_2()
        {
            new XDocument(new XElement("phones",
                new XElement("phone",
                    new XAttribute("name", "iPhone 6"),
                    new XElement("company", "Apple"),
                    new XElement("price", 40000)),
                new XElement("phone",
                    new XAttribute("name", "Samsung Galaxy S5"),
                    new XElement("company", "Samsung"),
                    new XElement("price", 33000)))).Save("phones.xml");
        }

        private static void Output(String filename)
        {
            var xdoc = XDocument.Load(filename);

            foreach (XElement phoneElement in xdoc.Element("phones").Elements("phone"))
            {
                var nameAttribute = phoneElement.Attribute("name");
                var companyElement = phoneElement.Element("company");
                var priceElement = phoneElement.Element("price");

                if (nameAttribute != null && companyElement != null && priceElement != null)
                {
                    Console.WriteLine($"Смартфон: {nameAttribute.Value}");
                    Console.WriteLine($"Компания: {companyElement.Value}");
                    Console.WriteLine($"Цена: {priceElement.Value}");
                }

                Console.WriteLine();
            }
        }

        private static IEnumerable<Phone> GetPhones(String filename)
        {
            var xDoc = XDocument.Load(filename);

            var phones = from phone in xDoc.Element("phones").Elements("phone")
                         where phone.Element("company").Value == "Samsung"
                         select new Phone()
                         {
                             Name = phone.Attribute("name").Value,
                             Price = Int32.Parse(phone.Element("price").Value),
                             Company = phone.Element("company").Value,
                         };

            return phones;
        }

        private static void Example_3()
        {
            var xdoc = XDocument.Load("phones.xml");

            var root = xdoc.Element("phones");

            foreach (XElement xe in root.Elements("phone").ToList())
            {
                // изменяем название и цену
                // если iphone - удаляем его
                if (xe.Attribute("name").Value == "Samsung Galaxy S5")
                {
                    xe.Attribute("name").Value = "Samsung Galaxy Note 4";

                    xe.Element("price").Value = "31000";
                }
                else if (xe.Attribute("name").Value == "iPhone 6")
                {
                    xe.Remove();
                }
            }

            // добавляем новый элемент
            root.Add(new XElement("phone",
                        new XAttribute("name", "Nokia Lumia 930"),
                        new XElement("company", "Nokia"),
                        new XElement("price", "19500")));

            xdoc.Save("phones1.xml");

            // выводим xml-документ на консоль
            Console.WriteLine(xdoc);
        }
    }
}
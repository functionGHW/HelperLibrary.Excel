using HelperLibrary.Excel;
using HelperLibrary.Excel.Configurations;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ExampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //var configLoader = new AttributeConfigurationLoader();
            var configLoader = new XmlConfigurationLoader("configs\\test.config.xml");
            var reader = new ExcelDataReader("testdata.xlsx", configLoader);
            try
            {
                var users = reader.ReadData<User>();

                foreach (var u in users)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", u.ID, u.Name, u.Age, u.Email, u.Birthday);
                }
            }
            catch (DataReaderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}

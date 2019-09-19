using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestEmployee
{
    public class TestEmployee
    {
        public static String input = "Cherry,Developer,220,2";

        public static void firstTest()
        {
            string mystr = "\0\0\0\0\0\0\0\0\0\0";
            Console.WriteLine("------{0}", mystr.Length);
            BinaryWriter writer = new BinaryWriter(File.Open(@"C:\Users\Cherry\Desktop\Q.txt", FileMode.OpenOrCreate));

            Employee emp = new Employee();
            Console.WriteLine(!String.IsNullOrEmpty("Cherry"));
            Employee.Parse("Cherry,developer,200,1").Write(writer);
            //Employee.Parse("Srikanthzerdrtft687trestrydtufyiftudryesrydtufyig,hr,202,3").Write(writer);

            String paddedName = "Cherry".PadRight(25, '\0');

            Console.Write("{0}{1}{2}", "lol", paddedName, "pol");
            Console.WriteLine();
            Console.Write("-----");
            string cherry = "\0t\0\0\0";
            string[] array = cherry.Split('\0');
            foreach (string name in array)
            {
                Console.Write(name);
            }

            Console.Write("##########");
            Console.WriteLine(array.Length);

        }


        public static void Essential()
        {
            //char[] employeeData = r.ReadChars(100);
            //string[] employeeElements = employeeData.ToString().Split('\0');

            //if (String.IsNullOrEmpty(employeeElements[0]))
            //    _name = null;
            //else
            //{
            //    _name = employeeElements[0];
            //}

            //if (String.IsNullOrEmpty(employeeElements[1]))
            //    _jobTitle = null;
            //else
            //{
            //    _jobTitle = employeeElements[1];
            //}

            //_salary = Convert.ToDecimal(employeeElements[2]);
            //_previousExperienceInYears = Convert.ToInt32(employeeElements[3]);
        }
        public static void Main(string[] args)
        {
            string message = "ImSrikanth";
            var output = message.PadRight(20, '#');
            Console.WriteLine("{0}{1}",output,"---");
            
            Console.WriteLine("Parse is Working Correctly ? : " + TestParse());
            Console.WriteLine("To String is Working Correctly ? : " + TestToString());
            Console.ReadLine();
        }

        private static bool TestParse()
        {

            Employee employee = Employee.Parse(input);

            var output = employee.ToString();

            return String.Equals(input, output);

        }

        private static bool TestToString()
        {
            
            Employee employee = Employee.Parse(input);
            var output = employee.ToString();
            return String.Equals(input, output);
        }
    }
}

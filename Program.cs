using CsvHelper;
using System.Globalization;

namespace CSV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"H:\Students_ - Students_.csv";
            var reader = new StreamReader(path);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Data>().ToList();

            var queryOne = from x in records
                           where x.gender == "Male" && x.university.Contains("Moscow")
                           select x;

            var queryTwo = from x in records
                           orderby x.GPA descending
                           select x;

            var queryThree = from x in records
                             where x.email.Contains("@mit.edu") || x.email.Contains("@berkley.edu")
                             select x;

            var queryFour = from x in records
                            where x.gender == "Female" && x.GPA > 4.0 && x.university == "American University of Science and Technology"
                            select x;

           foreach(var person in queryOne)
            {
                Console.WriteLine($"{person.first_name} {person.last_name} is {person.gender} and goes to {person.university}");
            }

           foreach(var person in queryTwo)
            {
                Console.WriteLine($"{person.first_name} {person.last_name} has a GPA of {person.GPA}");
            }

            Console.WriteLine($"The number of people using a domain @mit.edu or @berkley.edu is {queryThree.Count()}");

            foreach(var person in queryFour)
            {
                Console.WriteLine($"{person.first_name} {person.last_name} is {person.gender}, goes to {person.university} and has a GPA of {person.GPA}");
            }


        }
    }

    class Data
    {
        public int id { get; set; }             
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string university { get; set; }

        public double GPA { get; set; }
    }
}
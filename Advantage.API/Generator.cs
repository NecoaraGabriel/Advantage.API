using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvantageData.Models;

namespace AdvantageData.API
{
    public static class Generator
    {
        private static List<string> UniqueNames = new List<string>();
        public static string GenerateName()
        {
            Random rnd = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "y" };
            
            var Name = "";
            var LastName = "";
            var nameLength = rnd.Next(4, 8);
            var lastNameLength = rnd.Next(5, 9);

            var n = nameLength > lastNameLength ? nameLength : lastNameLength;
            var i = 0;

            while(i<n)
            {
                if( i < nameLength)
                {
                    Name += i % 2 == 0 ? vowels[rnd.Next(vowels.Length)] : consonants[rnd.Next(consonants.Length)];
                }
                if( i < lastNameLength)
                {
                    LastName += i % 2 == 0 ? vowels[rnd.Next(vowels.Length)] : consonants[rnd.Next(consonants.Length)];
                }
                i++;
            }

            LastName = LastName.Substring(0, 1).ToUpper() + LastName.Substring(1, lastNameLength-1);
            Name = Name.Substring(0, 1).ToUpper() + Name.Substring(1, nameLength-1);

            return $"{LastName} {Name}";
        }
        public static string GenerateNameUnique()
        {
            var userName = GenerateName();

            while (UniqueNames.Contains(userName))
            {
                userName = GenerateName();
            }
            UniqueNames.Add(userName);

            return userName;
        }


        public static string GenerateEmail(string Name)
        {
            List<string> emailDomains = new List<string>() { "yahoo.com", "hotmail.com", "gmail.com", "outlook.com" };
            Random rnd = new Random();

            Name = Name.Replace(' ', '.').ToLower();

            return $"{Name}@{emailDomains.ElementAt(rnd.Next(emailDomains.Count))}";
        }

        public static Order GenerateOrder(int id, Customer customer, DateTime startDt)
        {
            Random rnd = new Random();
            var maxDays = DateTime.Now - startDt;//use  larger interval
            DateTime placed = startDt.AddDays(rnd.Next(1,maxDays.Days));
            DateTime fulfilled = placed.AddDays(rnd.Next(1,7));
            var longFulfilled = DateTime.Now.Date < fulfilled.Date ? 0 : Utils.ToEpoch(fulfilled);

            return new Order
            {
                Id = id,
                Customer = customer,
                Amount = rnd.Next(10, 31),
                Placed = Utils.ToEpoch(placed),
                Fulfilled = longFulfilled,
                Status = longFulfilled == 0 ? "Ongoing" : "Complete"
            };
        }


    }
}

using System;
using System.Linq;

namespace nsu.timofeev.sharpLab.NameGenerator
{
    public class NameGenerator : INameGenerator
    {
        private WorldService _worldService;

        private bool CheckName(string name)
        {
            foreach (var worm in _worldService.Worms.ToList())
            {
                if (worm.Name.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }
        
        private String RandName()
        {
            string[] firstPart = { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian", "petro", "jorjo", "senjo", "alfredo", "letrejo"};
            string[] secondPart = { "abby", "abigail", "adele", "adrian", "aswer", "qojo", "saran", "bojo", "kambojo", "lejo", "raya", "vaya", "kaya", "peto", "lola"};
        
            Random random = new Random();
            return (string)firstPart.GetValue(random.Next(firstPart.Length)) + "_" + (string)secondPart.GetValue(random.Next(secondPart.Length));
        }
        
        public string Generate(WorldService worldService)
        {
            _worldService = worldService;
            var randName = RandName();
            while (!CheckName(randName))
            {
                randName = RandName();
            }

            return randName;
        }
    }
}
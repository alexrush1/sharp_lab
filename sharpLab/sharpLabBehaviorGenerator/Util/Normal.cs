using System;

namespace nsu.timofeev.sharpLab
{
    public class Normal
    {
        public static int NextNormal(Random r, double mu = 0, double sigma = 5)
        {

            var u1 = r.NextDouble();

            var u2 = r.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); 

            var randNormal = mu + sigma * randStdNormal;

            return (int)Math.Round(randNormal);

        }
    }
}
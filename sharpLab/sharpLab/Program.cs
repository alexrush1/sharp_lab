using System;
using System.IO;

namespace nsu.timofeev.sharpLab
{
    class Program
    {
        public static void Main(string[] args)
        {
            StreamWriter streamWriter = new StreamWriter("log.txt");
            World world = new World(streamWriter);
            world.AddWorm();
            world.Live();
            streamWriter.Close();
        }
    }
}
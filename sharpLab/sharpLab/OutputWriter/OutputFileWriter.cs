using System;
using System.IO;

namespace nsu.timofeev.sharpLab.OutputWriter
{
    public class OutputFileWriter : IOutputWriter
    {
        private StreamWriter _output;

        public OutputFileWriter(string path)
        {
            _output = new StreamWriter(path);
        }

        public void Log(WorldService worldService)
        {
            _output.Write("Worms: [");
            foreach (var worm in worldService.Worms)
            {
                _output.Write(" "+ worm.Name + "-" + worm.Health +" (" + worm.Position.X + ", " + worm.Position.Y + "), ");
            }
            _output.Write("], Food: ");
            foreach (var food in worldService.Foods)
            {
                _output.Write(" (" + food.Position.X + ", " + food.Position.Y + ") ");
            }
            _output.Write("\n");
        }

        public void Dispose()
        {
            _output.Dispose();
        }
    }
}
using System;
using System.IO;
using System.Linq;

namespace nsu.timofeev.sharpLab.OutputWriter
{
    public class OutputFileWriter : IOutputWriter
    {
        private string path;
        public StreamWriter _output;

        public OutputFileWriter(string path)
        {
           _output = new StreamWriter(path);
        }

        public void Log(WorldService worldService, int roundNumber)
        {
            _output.Write("Round " + roundNumber);
            _output.Write(" Worms: [");
            foreach (var worm in worldService.Worms.ToList())
            {
                _output.Write(" "+ worm.Name + "-" + worm.Health +" (" + worm.Position.X + ", " + worm.Position.Y + "), ");
            }
            _output.Write("], Food: ");
            foreach (var food in worldService.Foods.ToList())
            {
                _output.Write(" (" + food.Position.X + ", " + food.Position.Y + ") ");
            }
            _output.WriteLine();
            _output.Flush();
        }

        public void Dispose()
        {
            //_output.Dispose();
        }
    }
}
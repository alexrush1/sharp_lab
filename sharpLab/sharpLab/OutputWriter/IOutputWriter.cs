using System;

namespace nsu.timofeev.sharpLab.OutputWriter
{
    public interface IOutputWriter : IDisposable
    {
        void Log(WorldService worldService, int roundNumber);
    }
}
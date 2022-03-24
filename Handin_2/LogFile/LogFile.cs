using System;
using System.IO;

namespace Handin2
{

    public class LogFile
    {
        private readonly string _logFile = "logfile.txt"; // Navnet på systemets default log-fil

        public LogFile()
        {
            
        }
        
        public LogFile(string logFile)
        {
            _logFile = logFile;
        }
        
        public void LogDoorLocked(int id)
        {
            using (var writer = File.AppendText(_logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
            }
        }

        public void LogDoorUnlocked(int id)
        {
            using (var writer = File.AppendText(_logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            }
        }
    }
}
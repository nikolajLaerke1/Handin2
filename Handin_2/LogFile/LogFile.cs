namespace Handin2;

public static class LogFile
{
    private static string logFile = "logfile.txt"; // Navnet på systemets log-fil
    public static void LogDoorLocked(int id)
    {
        using (var writer = File.AppendText(logFile))
        {
            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
        }
    }

    public static void LogDoorUnlocked(int id)
    {
        using (var writer = File.AppendText(logFile))
        {
            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
        }
    }
}
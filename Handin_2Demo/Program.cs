using System;
using Handin2;
using Ladeskab;

class Program
{
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes
        LogFile logFile = new("logfile.txt");
        UsbChargerSimulator simulator = new();

        IDoor door = new Door();
        IDisplay display = new Display();
        IRfidReader rfidReader = new RfidReader();
        IChargeControl charger = new ChargeControl(simulator, display);
        StationControl stationControl = new StationControl(charger, door, display, rfidReader, logFile);
                
        bool finish = false;
        do
        {
            Console.WriteLine("[Simulator] Vælg en mulighed: (E)xit, (O)pen Door, (C)lose Door, " +
                              "(R)ead Tag, (T)ilslut, (D)isconnect, " +
                              "(S)tart Charge, (P)ause Charge: ");
            var input = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(input)) continue;

            switch (char.ToUpper(input[0]))
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.OnDoorOpen();
                    break;

                case 'C':
                    if (stationControl.IsDoorOpen())
                        door.OnDoorClose();
                    else
                        Console.WriteLine("Door is already closed");
                    break;

                case 'R':
                    Console.WriteLine("[Simulator] Indtast RFID id: ");
                    string idString = Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    rfidReader.OnRfidRead(id);
                    break;
                
                case 'T':
                    if (stationControl.IsDoorOpen())
                    {
                        simulator.SimulateConnected(true);
                        charger.Connected = true;
                    }
                    else
                        Console.WriteLine("Door is not open. Please open the door before charging");
                    break;
                
                case 'D':
                    if (stationControl.IsDoorOpen())
                    {
                        simulator.SimulateConnected(false);
                        charger.Connected = false;
                    }
                    else
                        Console.WriteLine("Door is not open. Please open the door before attempting to disconnect");
                    break;
                
                case 'S':
                    simulator.StartCharge();
                    break;
                
                case 'P':
                    simulator.StopCharge();
                    break;
                
                default:
                    break;
            }

        } while (!finish);
    }
}

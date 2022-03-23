using Handin2;
using Ladeskab;

class Program
{
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes
        IDoor door = new Door();
        IChargeControl charger = new ChargeControl();
        IDisplay display = new Display();
        IRfidReader rfidReader = new RfidReader();
        StationControl stationControl = new StationControl(charger, door, display, rfidReader);
        UsbChargerSimulator simulator = new();
                
        bool finish = false;
        do
        {
            string input;
            Console.WriteLine("Indtast E, O, C, R, 'T', 'D', 'S', 'P': ");
            input = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.OnDoorOpen();
                    break;

                case 'C':
                    door.OnDoorClose();
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    //rfidReader.OnRfidRead(id);
                    break;
                
                case 'T':
                    simulator.SimulateConnected(true);
                    break;
                
                case 'D':
                    simulator.SimulateConnected(false);
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

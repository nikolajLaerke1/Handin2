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
        StationControl stationControl = new StationControl(charger, door, display);
                
        bool finish = false;
        do
        {
            Console.WriteLine("Indtast E, O, C, R: ");
            var input = Console.ReadLine();
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
                    door.OnDoorClose();
                    break;

                case 'R':
                    Console.WriteLine("Indtast RFID id: ");
                    string? idString = Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    //rfidReader.OnRfidRead(id);
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}

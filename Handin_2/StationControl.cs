using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        }

        private LadeskabState _state = LadeskabState.Available;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private LogFile _logFile;
        
        public StationControl(
            IChargeControl charger,
            IDoor door,
            IDisplay display,
            IRfidReader rfid)
        {
            _charger = charger;
            _door = door;
            _display = display;
            door.DoorEvent += HandleDoorEvent;
            rfid.RfidEvent += RfidDetected;
            _display.UpdateInstructionsArea("Indlæs RFID");
            _logFile = new LogFile();
        }

        private void RfidDetected(object sender, RfidEventArgs e)
        {
            int id = e.Id;
            switch (_state)
            {
                case LadeskabState.Available:
                    SkabAvailable(id);
                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked: 
                    SkabLocked(id);
                    break;
            }
        }
        
        private void SkabLocked(int id)
        {
            if (id == _oldId)
            {
                _charger.StopCharge();
                _door.UnlockDoor();
                _logFile.LogDoorUnlocked(id);

                _display.UpdateInstructionsArea("Tag din telefon ud af skabet og luk skabet");
                _state = LadeskabState.Available;
            }
            else
            { 
                _display.UpdateInstructionsArea("Forkert RFID tag");
            }
        }

        private void SkabAvailable(int id)
        {
            if (_charger.Connected)
            {
                _door.LockDoor();
                _charger.StartCharge();
                _oldId = id;
                _logFile.LogDoorLocked(id);


                _display.UpdateInstructionsArea(
                    "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op");
                _state = LadeskabState.Locked;
            }
            else
                _display.UpdateInstructionsArea("Din telefon er ikke ordentlig tilsluttet. Prøv igen");
        }

        private void HandleDoorEvent(object sender, DoorEventArgs e)
        {
            //Do something
            if (e.NewState == "open")
            {
                _state = LadeskabState.DoorOpen;
                _display.UpdateInstructionsArea("Tilslut din telefon");
            }
            else if (e.NewState == "closed")
            {
                _display.UpdateInstructionsArea("Indlæs dit RFID");
            }
        }

        public bool IsDoorOpen()
        {
            return _state == LadeskabState.DoorOpen;
        }
    }
}

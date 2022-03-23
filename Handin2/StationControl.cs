﻿using System;
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
        };

        // Her mangler flere member variable
        private LadeskabState _state = LadeskabState.Available;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private IRfidReader _reader;

        

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
            _display.ShowRfidInstruction();
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object sender, RfidEventArgs e)
        {
            int id = e.Id;
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        LogFile.LogDoorLocked(id);
                        

                        _display.ShowCharging();
                        _state = LadeskabState.Locked;
                    }
                    else
                        _display.ShowConnectionError();

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        LogFile.LogDoorUnlocked(id);
                        
                        _display.ShowRemovePhone();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.ShowRfidError();
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void HandleDoorEvent(object Sender, DoorEventArgs e)
        {
            //Do something
            if (e.NewState == "open")
            {
                _display.ShowConnectPhone();
            }
            else if (e.NewState == "closed")
            {
                _display.ShowRfidInstruction();
            }
        }
        
    }
}

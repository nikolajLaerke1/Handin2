using System;

namespace Handin2
{

    public class RfidReader : IRfidReader
    {
        public event EventHandler<RfidEventArgs>? RfidEvent;

        public void OnRfidRead(int id)
        {
            RfidEvent?.Invoke(this, new RfidEventArgs(id));
        }
    }
}
﻿namespace Handin2;

public interface IChargeControl
{
    public bool Connected { get; set; }

    public void StartCharge();
    public void StopCharge();

    public class ConnectedEventArgs : EventArgs
    {
        public bool Connected { get; set; }
    }
}
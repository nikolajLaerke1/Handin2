namespace Handin2;

public class Door : IDoor
{
    public bool Locked { get; set; }
    public void LockDoor()
    {
        Console.WriteLine("[Door]: Locking door");
        Locked = true;
    }

    public void UnlockDoor()
    {
        Console.WriteLine("[Door]: Unlocking door");
        Locked = false;
    }

    public void OnDoorOpen()
    {
        OnDoorOpened(new DoorOpenedEventArgs() {NewState = "open"});
    }

    public void OnDoorClose()
    {
        //Lav event til station control
    }

    public event EventHandler<DoorOpenedEventArgs> DoorOpenedEvent;

    protected virtual void OnDoorOpened(DoorOpenedEventArgs e)
    {
        DoorOpenedEvent?.Invoke(this, e);
    }
}
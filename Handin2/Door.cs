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
        OnDoor(new DoorEventArgs() {NewState = "open"});
    }

    public void OnDoorClose()
    {
        OnDoor(new DoorEventArgs() {NewState = "closed"});
    }

    public event EventHandler<DoorEventArgs> DoorEvent;

    protected virtual void OnDoor(DoorEventArgs e)
    {
        DoorEvent?.Invoke(this, e);
    }
    
}
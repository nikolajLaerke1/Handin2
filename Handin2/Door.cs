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
        //Lav event til station control
    }

    public void OnDoorClose()
    {
        //Lav event til station control
    }
}
namespace Handin2;

public interface IRfidReader
{
    public event EventHandler<RfidEventArgs> RfidEvent;
    public void OnRfidRead(int id);
}

public class RfidEventArgs : EventArgs
{
    public int Id { get; }
        
    public RfidEventArgs(int id) => Id = id;
}
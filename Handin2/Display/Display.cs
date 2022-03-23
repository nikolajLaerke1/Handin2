namespace Handin2;

public class Display : IDisplay
{
    public Display() {}

    #region Instructions
    public void ShowConnectPhone() =>
        Console.WriteLine("[Display]: Tilslut telefon");
    public void ShowRfidInstruction() =>
        Console.WriteLine("[Display]: Indlæs RFID");
    public void ShowOccupied() =>
        Console.WriteLine("[Display]: Ladeskabet er optaget");
    public void ShowRemovePhone() =>
        Console.WriteLine("[Display]: Tag din telefon ud af skabet og luk døren");
    #endregion
    
    #region Errors
    public void ShowConnectionError() =>
        Console.WriteLine("[Display]: Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
    public void ShowRfidError() =>
        Console.WriteLine("[Display]: Forkert RFID tag");
    #endregion

    #region Charging
    public void ShowCharging() =>
        Console.WriteLine("[Display]: Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
    public void ShowFullyCharged() =>
        Console.WriteLine("[Display]: Telefonen er fuldt opladt");

    public void ShowChargeError() =>
        Console.WriteLine("[Display]: Der er sket en fejl, fjern telefon");

    #endregion
}



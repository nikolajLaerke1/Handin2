namespace Handin2;

public class Display : IDisplay
{
    private string _instructionArea = "";
    private string _statusArea = "";

    public Display() {}

    #region Instructions
    public void ShowConnectPhone()
    {
        _instructionArea = "Tilslut telefon";
        _statusArea = "";
    }
    
    public void ShowRfidInstruction()
    {
        _instructionArea = "Indlæs RFID";
        _statusArea = "";
    }
    
    public void ShowOccupied()
    {
        _instructionArea = "Ladeskabet er optaget";
        _statusArea = "";
    }

    public void ShowRemovePhone()
    {
        _instructionArea = "Tag din telefon ud af skabet og luk døren";
        _statusArea = "";
    }
    #endregion
    
    #region Errors
    public void ShowConnectionError()
    {
        _instructionArea = "Din telefon er ikke ordentlig tilsluttet. Prøv igen.";
        _statusArea = "";
    }

    public void ShowRfidError()
    {
        _instructionArea = "Forkert RFID tag";
        _statusArea = "";
    }
    #endregion

    #region Charging
    public void ShowCharging()
    {
        _instructionArea = "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.";
        _statusArea = "Telefonen lader...";
    }

    public void ShowFullyCharged()
    {
        _instructionArea = "";
        _statusArea = "Telefonen er fuldt opladt";
    }

    public void ShowChargeError()
    {
        _instructionArea = "";
        _statusArea = "Der er sket en fejl, fjern telefon";
    }
    #endregion
}



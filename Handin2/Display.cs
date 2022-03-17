namespace Handin2;

public class Display
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
    #endregion
    
    #region Errors
    public void ShowConnectionError()
    {
        _instructionArea = "Tilslutningsfejl";
        _statusArea = "";
    }

    public void ShowRfidError()
    {
        _instructionArea = "RFID fejl";
        _statusArea = "";
    }
    #endregion

    #region Charging
    public void ShowCharging()
    {
        _instructionArea = "";
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
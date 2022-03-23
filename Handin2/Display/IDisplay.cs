namespace Handin2;

public interface IDisplay
{
    public string ChargeArea { get; set; }
    public string InstructionsArea { get; set; }
    
    #region Instructions
    public void ShowConnectPhone();
    public void ShowRfidInstruction();
    public void ShowOccupied();
    public void ShowRemovePhone();
    #endregion
    
    #region Errors
    public void ShowConnectionError();
    public void ShowRfidError();
    public void ShowChargeError();
    #endregion

    #region Charging
    public void ShowCharging();
    #endregion

    public void UpdateDisplay(string InstructionsArea, string chargeArea);
    public void UpdateDisplay(string instructionsArea);
}
namespace Handin2;

public interface IDisplay
{
    #region Instructions
    public void ShowConnectPhone();
    public void ShowRfidInstruction();
    public void ShowOccupied();
    public void ShowRemovePhone();
    #endregion
    
    #region Errors
    public void ShowConnectionError();
    public void ShowRfidError();
    #endregion

    #region Charging
    public void ShowCharging();
    public void ShowFullyCharged();
    public void ShowChargeError();
    #endregion
}
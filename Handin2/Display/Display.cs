namespace Handin2;

public class Display : IDisplay
{
    public Display() {}
    
    public string ChargeArea { get; set; }
    public string InstructionsArea { get; set; }

    #region Instructions

    public void ShowConnectPhone()
    {
        InstructionsArea = "Tilslut din telefon";
        ChargeArea = "Afventer forbindelse til telefonen...";
        UpdateDisplay();
    }

    public void ShowRfidInstruction()
    {
        InstructionsArea = "Indlæs dit RFID";
        ChargeArea = "Afventer RFID godkendelse...";
        UpdateDisplay();
    }
    public void ShowOccupied()
    {
        InstructionsArea = "Ladeskabet er optaget";
        ChargeArea = "Oplader...";
        UpdateDisplay();
    }

    public void ShowRemovePhone()
    {
        InstructionsArea = "Tag din telefon ud af skabet og luk skabet";
        ChargeArea = "Fuldt opladet";
        UpdateDisplay();
    }
    #endregion
    
    #region Errors
    public void ShowConnectionError()
    {
        InstructionsArea = "Din telefon er ikke ordentlig tilsluttet. Prøv igen";
        ChargeArea = "Afventer forbindelse til telefonen...";
        UpdateDisplay();
    }

    public void ShowRfidError()
    {
        InstructionsArea = "Forkert RFID tag.";
        // Charge area isn't affected
        UpdateDisplay();
    }

    public void ShowChargeError()
    {
        InstructionsArea = "Der er sket en fejl, fjern telefonen";
        ChargeArea = "Opladerfejl";
        UpdateDisplay();
    }
    #endregion

    #region Charging

    public void ShowCharging()
    {
        InstructionsArea = "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.";
        ChargeArea = "Oplader...";
        UpdateDisplay();
    }
    #endregion

    private void UpdateDisplay()
    {
        Console.WriteLine($"Instruktioner: {InstructionsArea}");
        Console.WriteLine($"Opladningsstatus: {ChargeArea}");
    }
}



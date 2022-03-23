namespace Handin2;

public class Display : IDisplay
{
    public Display() {}
    
    public string ChargeArea { get; set; }
    public string InstructionsArea { get; set; }

    #region Instructions

    public void ShowConnectPhone() =>
        UpdateDisplay("Tilslut din telefon", "Afventer forbindelse til telefonen...");
    public void ShowRfidInstruction() =>
        UpdateDisplay("Indlæs dit RFID", "Afventer RFID godkendelse...");
    public void ShowOccupied() =>
        UpdateDisplay("Ladeskabet er optaget", "Oplader...");

    public void ShowRemovePhone() =>
        UpdateDisplay("Tag din telefon ud af skabet og luk skabet", "Fuldt opladet");
    #endregion
    
    #region Errors
    public void ShowConnectionError() =>
        UpdateDisplay("Din telefon er ikke ordentlig tilsluttet. Prøv igen", 
            "Afventer forbindelse til telefonen...");

    public void ShowRfidError()
    {
        // Since this error is only displayed when the door is locked,
        // the current charging state should remain unaffected
        UpdateDisplay("Forkert RFID tag");
    }

    public void ShowChargeError() =>
        UpdateDisplay("Der er sket en fejl, fjern telefonen", "Opladerfejl");
    #endregion

    #region Charging

    public void ShowCharging() =>
        UpdateDisplay("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op", 
            "Oplader...");
    #endregion

    public void UpdateDisplay(string instructionsArea, string chargeArea)
    {
        ChargeArea = chargeArea;
        UpdateDisplay(instructionsArea);
    }
    
    public void UpdateDisplay(string instructionsArea)
    {
        InstructionsArea = instructionsArea;
        
        Console.WriteLine($"Instruktioner: {InstructionsArea}");
        Console.WriteLine($"Opladningsstatus: {ChargeArea}");
    }
}



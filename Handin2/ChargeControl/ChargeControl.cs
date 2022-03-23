namespace Handin2;

public class ChargeControl : IChargeControl
{
    private const double MaxCurrent = 500.0; // mA
    private const double FullyChargedCurrent = 5.0; // mA
    
    public bool Connected { get; set; }
    private bool Charging { get; set; }
    private IUsbCharger _usbCharger;
    private IDisplay _display;
    
    public ChargeControl(IUsbCharger usbCharger, IDisplay display)
    {
        _usbCharger = usbCharger;
        _display = display;
        _usbCharger.CurrentValueEvent += HandleCurrentValueEvent;
    }
    
    public void StartCharge()
    {
        if (!Connected)
        {
            return;
        }

        Charging = true;
        _usbCharger.StartCharge();
    }

    public void StopCharge()
    {
        Charging = false;
     
        if (!Connected)
        {
            Console.WriteLine("[ChargeControl]: Phone is disconnected, charging stopped");
            return;
        }
        
        Console.WriteLine("[ChargeControl]: Charging stopped");

    }

    public void HandleCurrentValueEvent(object sender, CurrentEventArgs args)
    {
        if (args.Current == 0)
            return;

        if (args.Current is > 0 and < FullyChargedCurrent)
        {
            _display.UpdateChargeArea("Fuldt opladt");
            return;
        }

        if (args.Current is > FullyChargedCurrent and <= MaxCurrent)
        {
            _display.UpdateChargeArea("Telefon oplades...");
            return;
        }

        if (args.Current > MaxCurrent)
        {
            _display.UpdateChargeArea("Opladerfejl");
        }
    }
}
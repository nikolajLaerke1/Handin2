namespace Handin2;

public class ChargeControl : IChargeControl
{
    public bool Connected { get; set; }
    private bool Charging { get; set; }
    
    public void StartCharge()
    {
        if (!Connected)
        {
            Console.WriteLine("[ChargeControl]: Tried to charge, but phone was disconnected");
            return;
        }

        Charging = true;
        Console.WriteLine("[ChargeControl]: Charging started");
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
}
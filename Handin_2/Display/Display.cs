using System;

namespace Handin2
{

    public class Display : IDisplay
    {
        public string ChargeArea { get; set; } = "";
        public string InstructionsArea { get; set; } = "";

        public void UpdateChargeArea(string message)
        {
            ChargeArea = message;
            UpdateDisplay();
        }

        public void UpdateInstructionsArea(string message)
        {
            InstructionsArea = message;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            Console.WriteLine($"Instructions Area: {InstructionsArea}");
            Console.WriteLine($"Charge Area: {ChargeArea}");
        }
    }
}


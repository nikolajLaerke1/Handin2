using Handin2;
using NSubstitute;
using NUnit.Framework;

namespace UsbSimulator.Test;

[TestFixture]
public class DisplayTest
{
    private IDisplay _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new Display();
    }

    [Test]
    public void Display_ShowConnectPhone_ConnectPhoneDisplayed()
    {
        _uut.ShowConnectPhone();

        Assert.That(_uut.InstructionsArea, Is.EqualTo("Tilslut din telefon"));
        Assert.That(_uut.ChargeArea, Is.EqualTo("Afventer forbindelse til telefonen..."));
    }
    
    [Test]
    public void Display_ShowRfidInstruction_RfidDisplayed()
    {
        _uut.ShowRfidInstruction();

        Assert.That(_uut.InstructionsArea, Is.EqualTo("Indlæs dit RFID"));
        Assert.That(_uut.ChargeArea, Is.EqualTo("Afventer RFID godkendelse..."));
    }
    
    [Test]
    public void Display_ShowOccupied_OccupiedDisplayed()
    {
        _uut.ShowOccupied();

        Assert.That(_uut.InstructionsArea, Is.EqualTo("Ladeskabet er optaget"));
        Assert.That(_uut.ChargeArea, Is.EqualTo("Oplader..."));
    }
    
    [Test]
    public void Display_ShowRemovePhone_RemovePhoneDisplayed()
    {
        _uut.ShowRemovePhone();

        Assert.That(_uut.InstructionsArea, Is.EqualTo("Tag din telefon ud af skabet og luk skabet"));
        Assert.That(_uut.ChargeArea, Is.EqualTo("Fuldt opladet"));
    }
    
    [Test]
    public void Display_ShowConnectionError_ConnectionErrorDisplayed()
    {
        _uut.ShowConnectionError();

        Assert.That(_uut.InstructionsArea, Is.EqualTo("Din telefon er ikke ordentlig tilsluttet. Prøv igen"));
        Assert.That(_uut.ChargeArea, Is.EqualTo("Afventer forbindelse til telefonen..."));
    }
    
    [Test]
    public void Display_ShowRfidError_RfidErrorDisplayed()
    {
        _uut.ShowRfidError();

        Assert.That(_uut.InstructionsArea, Is.EqualTo("Forkert RFID tag"));
    }
    
    [Test]
    public void Display_ShowChargeError_ChargeErrorDisplayed()
    {
        _uut.ShowChargeError();

        Assert.That(_uut.InstructionsArea, Is.EqualTo("Der er sket en fejl, fjern telefonen"));
        Assert.That(_uut.ChargeArea, Is.EqualTo("Opladerfejl"));
    }
    
    [Test]
    public void Display_ShowCharging_ChargingDisplayed()
    {
        _uut.ShowCharging();

        Assert.That(_uut.InstructionsArea, Is.EqualTo("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op"));
        Assert.That(_uut.ChargeArea, Is.EqualTo("Oplader..."));
    }

    [Test]
    public void Display_UpdateDisplay_DisplayedUpdated()
    {
        _uut.UpdateDisplay("InstructionsArea Test", "ChargeArea Test");
        Assert.That(_uut.InstructionsArea, Is.EqualTo("InstructionsArea Test"));
    }
}
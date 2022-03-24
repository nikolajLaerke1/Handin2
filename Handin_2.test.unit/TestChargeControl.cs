using Handin2;
using NSubstitute;
using NUnit.Framework;

namespace Handin_2.test.unit;

[TestFixture]
public class TestChargeControl
{
    private IUsbCharger fakeCharger;
    private IDisplay fakeDisplay;
    private IChargeControl _uut;
    
    [SetUp]
    public void Setup()
    {
        fakeCharger = Substitute.For<IUsbCharger>();
        fakeDisplay = Substitute.For<IDisplay>();
        
        _uut = new ChargeControl(fakeCharger, fakeDisplay);
    }

    [TestCase(true, 1)]
    [TestCase(false, 0)]
    public void StartCharge_Connected_StartChargeCalled(bool connected, int startChargeCalled)
    {
        _uut.Connected = connected;

        _uut.StartCharge();
        
        fakeCharger.Received(startChargeCalled).StartCharge();
    }

    [TestCase(true, 1)]
    [TestCase(false, 0)]
    public void StopCharge_Connected_DisplayUpdated(bool connected, int startChargeCalled)
    {
        _uut.Connected = connected;

        _uut.StopCharge();
        
        fakeCharger.Received(startChargeCalled).StopCharge();
    }

    [TestCase(-1.0, true, false, "", 0)]
    [TestCase(0, true, false, "", 0)]
    [TestCase(0, false, false, "", 0)]
    [TestCase(1.0, false, true, "Fuldt opladt", 0)]
    [TestCase(1.0, true, true, "Fuldt opladt", 0)]
    [TestCase(5.0, false, true, "Fuldt opladt", 0)]
    [TestCase(5.0, true, true, "Fuldt opladt", 0)]
    [TestCase(5.1, false, true, "Telefon oplades...", 0)]
    [TestCase(5.1, true, true, "Telefon oplades...", 0)]
    [TestCase(500.0, false, true, "Telefon oplades...", 0)]
    [TestCase(500.0, true, true, "Telefon oplades...", 0)]
    [TestCase(501.0, false, true, "Opladerfejl", 1)]
    [TestCase(501.0, true, true, "Opladerfejl", 1)]
    public void HandleCurrentValueEvent_EventRaised_DisplayUpdated(
        double current,
        bool connected,
        bool expectedConnected,
        string chargeAreaMessage,
        int stoppedCharge)
    {
        _uut.Connected = connected;
        fakeCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = current});

        fakeDisplay.Received(1).UpdateChargeArea(chargeAreaMessage);
        fakeCharger.Received(stoppedCharge).StopCharge();
    }
}
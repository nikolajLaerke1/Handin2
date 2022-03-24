using Handin2;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Handin_2.test.unit;

[TestFixture]
public class TestStationControl
{
    private StationControl _uut;
    private IChargeControl fakeChargeControl;
    private IDoor fakeDoor;
    private IDisplay fakeDisplay;
    private IRfidReader fakeReader;
    private IUsbCharger fakeUsbCharger;
    private LogFile fakeLogger;

    [SetUp]
    public void Setup()
    {
        fakeUsbCharger = Substitute.For<IUsbCharger>();
        fakeDoor = Substitute.For<IDoor>();
        fakeDisplay = Substitute.For<IDisplay>();
        fakeReader = Substitute.For<IRfidReader>();
        fakeLogger = Substitute.For<LogFile>();
        fakeChargeControl = Substitute.For<ChargeControl>(fakeUsbCharger, fakeDisplay);

        _uut = new StationControl(fakeChargeControl, fakeDoor, fakeDisplay, fakeReader, fakeLogger);
    }

    [Test]
    public void RfidDetected_StateAvailableAndChargerConnected_SkabAvailableCalled()
    {
        const int id = 12;
        
        fakeChargeControl.Connected = true;
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = id});

        fakeDoor.Received(1).LockDoor();
        fakeChargeControl.Received(1).StartCharge();
        fakeLogger.Received(1).LogDoorLocked(id);
        fakeDisplay.Received(1).UpdateInstructionsArea(
            "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op");
    }
    
    [Test]
    public void RfidDetected_StateAvailableAndChargerDisconnected_SkabAvailableCalled()
    {
        const int id = 12;
        
        fakeChargeControl.Connected = false;
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = id});

        fakeDoor.Received(0).LockDoor();
        fakeChargeControl.Received(0).StartCharge();
        fakeLogger.Received(0).LogDoorLocked(id);
        fakeDisplay.Received(1).UpdateInstructionsArea(
            "Din telefon er ikke ordentlig tilsluttet. Prøv igen");
    }
    
    [Test]
    public void RfidDetected_StateLockedAndChargerConnected_SkabLockedCalled()
    {
        const int id = 12;
        
        fakeChargeControl.Connected = true;
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = id});

        fakeChargeControl.Received(1).StopCharge();
        fakeDoor.Received(1).LockDoor();
        fakeLogger.Received(1).LogDoorLocked(id);
        fakeDisplay.Received(1).UpdateInstructionsArea(
            "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op");
    }
}
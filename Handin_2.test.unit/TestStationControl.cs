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
    [TestCase(0)]
    public void RfidDetected_StateLockedAndChargerConnected_SkabLockedCalled(int id)
    {
        fakeChargeControl.Connected = true;
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = id});
        
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = id});

        fakeChargeControl.Received(1).StopCharge();
        fakeDoor.Received(1).UnlockDoor();
        fakeLogger.Received(1).LogDoorUnlocked(id);
        fakeDisplay.Received(1).UpdateInstructionsArea(
            "Tag din telefon ud af skabet og luk skabet");
    }
    
    [TestCase(0)]
    public void RfidDetected_StateLockedAndWrongRfid_SkabLockedCalled(int id)
    {
        fakeChargeControl.Connected = true;
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = id});
        
        // scanning a tag with a different id
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = ++id});

        fakeChargeControl.Received(0).StopCharge();
        fakeDoor.Received(0).UnlockDoor();
        fakeLogger.Received(0).LogDoorUnlocked(id);
        fakeDisplay.Received(1).UpdateInstructionsArea(
            "Forkert RFID tag");
    }

    [TestCase("open", "Tilslut din telefon")]
    [TestCase("closed", "Indlæs dit RFID")]
    public void DoorEvent_DoorStateChange_LadeSkabStateIsChanged(string state, string displayMessage)
    {
        fakeDoor.DoorEvent += Raise.EventWith(new DoorEventArgs{NewState = state});
        fakeDisplay.Received(1).UpdateInstructionsArea(displayMessage);
    }

    [Test]
    public void IsDoorOpenCalled_DoorIsClosed_FalseReturned()
    {
        fakeChargeControl.Connected = true;
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = 0});
        
        Assert.False(_uut.IsDoorOpen());
    }

    [Test]
    public void ReaderDetected_StateIsDoorOpen_NothingCalled()
    {
        fakeDoor.DoorEvent += Raise.EventWith(new DoorEventArgs{NewState = "open"});
        fakeReader.RfidEvent += Raise.EventWith(new RfidEventArgs{Id = 0});

        // Assert that SkabLocked was not called
        fakeChargeControl.DidNotReceive().StopCharge();
        fakeDoor.DidNotReceive().UnlockDoor();
        fakeLogger.DidNotReceive().LogDoorUnlocked(0);
        fakeDisplay.DidNotReceive().UpdateInstructionsArea(
            "Forkert RFID tag");
        
        // Assert that SkabAvailable was not called
        fakeDoor.DidNotReceive().LockDoor();
        fakeChargeControl.DidNotReceive().StartCharge();
        fakeLogger.DidNotReceive().LogDoorLocked(0);
        fakeDisplay.DidNotReceive().UpdateInstructionsArea(
            "Din telefon er ikke ordentlig tilsluttet. Prøv igen");
        
    }
}
    
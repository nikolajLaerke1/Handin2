using Handin2;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Handin_2.test.unit
{

    [TestFixture]
    public class TestStationControl
    {
        private StationControl _uut;
        private IChargeControl fakeChargeControl;
        private IDoor fakeDoor;
        private IDisplay fakeDisplay;
        private IRfidReader fakeReader;

        [SetUp]
        public void Setup()
        {

            fakeChargeControl = Substitute.For<ChargeControl>();
            fakeDoor = Substitute.For<Door>();
            fakeDisplay = Substitute.For<Display>();
            fakeReader = Substitute.For<RfidReader>();

            _uut = new StationControl(fakeChargeControl, fakeDoor, fakeDisplay, fakeReader);

        }
    }
}
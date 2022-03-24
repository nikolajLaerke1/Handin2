using System;
using NSubstitute;
using NUnit.Framework;

namespace Handin2.test.unit
{


    [TestFixture]
    public class TestRfidReader
    {
        private IRfidReader _uut;
        private RfidEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _uut = new RfidReader();
            _receivedEventArgs = null;

            _uut.RfidEvent +=
                (o, args) => { _receivedEventArgs = args; };
            

        }

        [Test]
        public void Rfid_EventRaisedWith1_EventRaisedWith1()
        {
            var wasCalled = false;


            _uut.RfidEvent += (_, _) => wasCalled = true;
            _uut.OnRfidRead(1);

            Assert.True(wasCalled);
            Assert.That(_receivedEventArgs.Id, Is.EqualTo(1));
        }
    }
}
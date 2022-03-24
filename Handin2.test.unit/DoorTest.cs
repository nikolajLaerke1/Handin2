using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Handin2.test.unit
{
    [TestFixture]
    public class DoorTest
    {
        private Door _uut;
        private DoorEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new Door();
            _uut.Locked = false;

            // Set up an event listener to check the event occurrence and event data
            _uut.DoorEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
        }


        [Test]
        public void OnDoorOpen_DoorOpened_EventFires()
        {
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void OnDoorClose_DoorClosed_EventFires()
        {
            _uut.OnDoorClose();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void OnDoorOpen_DoorOpened_CorrectNewStateReceived()
        {
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs.NewState, Is.EqualTo("open"));
        }

        [Test]
        public void OnDoorOpen_DoorClosed_CorrectNewStateReceived()
        {
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs.NewState, Is.EqualTo("closed"));
        }


    }

    
}

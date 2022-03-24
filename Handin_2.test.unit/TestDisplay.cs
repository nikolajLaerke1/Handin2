using NSubstitute;
using NUnit.Framework;

namespace Handin2.test.unit
{

    [TestFixture]
    public class DisplayTest
    {
        private Display _uut;

        [SetUp]
        public void Setup() => _uut = Substitute.For<Display>();

        [Test]
        public void UpdateChargeArea_Message_ChargeAreaIsUpdated()
        {
            const string chargeAreaMessage = "chargeAreaMessage";

            // Act
            _uut.UpdateChargeArea(chargeAreaMessage);

            // Assert
            _uut.Received(1).UpdateDisplay();

            // Make sure only charge area changed its display area
            Assert.That(_uut.ChargeArea, Is.EqualTo(chargeAreaMessage));
            Assert.That(_uut.InstructionsArea, Is.EqualTo(""));
        }

        [Test]
        public void UpdateInstructionsArea_Message_InstructionsAreaIsUpdated()
        {
            const string instructionsAreaMessage = "instructionsAreaMessage";

            // Act
            _uut.UpdateInstructionsArea(instructionsAreaMessage);

            // Assert
            _uut.Received(1).UpdateDisplay();

            // Make sure only the instructions area changed its display area
            Assert.That(_uut.InstructionsArea, Is.EqualTo(instructionsAreaMessage));
            Assert.That(_uut.ChargeArea, Is.EqualTo(""));
        }
    }
}
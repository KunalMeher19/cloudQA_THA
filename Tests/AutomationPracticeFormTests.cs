using NUnit.Framework;
using CloudQATests.Pages;

namespace CloudQATests.Tests
{
    [TestFixture]
    public class AutomationPracticeFormTests : BaseTest
    {
        private AutomationPracticeFormPage _page;

        [SetUp]
        public new void Setup()
        {
            // BaseTest.Setup() is called automatically by NUnit because of [SetUp] attribute inheritance,
            // but since we are hiding it with 'new', we might need to be careful.
            // Actually, NUnit calls SetUp on base classes first. 
            // However, to be safe and clean, let's just use the inherited Driver.
            // We don't need a separate [SetUp] here unless we do extra stuff.
            
            // Wait, NUnit runs Base [SetUp] then Derived [SetUp].
            // So Driver is already initialized.
            _page = new AutomationPracticeFormPage(Driver);
            _page.GoTo();
        }

        [Test]
        public void FirstName_ShouldBeEditable()
        {
            // Arrange
            string expectedName = "CloudQA Candidate";

            // Act
            _page.SetFirstName(expectedName);
            string actualName = _page.GetFirstName();

            // Assert
            Assert.That(actualName, Is.EqualTo(expectedName), "First Name field value mismatch.");
        }

        [Test]
        public void Email_ShouldBeEditable()
        {
            // Arrange
            string expectedEmail = "candidate@cloudqa.io";

            // Act
            _page.SetEmail(expectedEmail);
            string actualEmail = _page.GetEmail();

            // Assert
            Assert.That(actualEmail, Is.EqualTo(expectedEmail), "Email field value mismatch.");
        }

        [Test]
        public void Gender_ShouldBeSelectable()
        {
            // Act
            _page.SelectGender("Male");

            // Assert
            Assert.That(_page.IsGenderSelected("Male"), Is.True, "Male gender should be selected.");
            Assert.That(_page.IsGenderSelected("Female"), Is.False, "Female gender should not be selected.");
        }

        [Test]
        public void Mobile_ShouldBeEditable()
        {
            // Arrange
            string expectedMobile = "1234567890";

            // Act
            _page.SetMobile(expectedMobile);
            string actualMobile = _page.GetMobile();

            // Assert
            Assert.That(actualMobile, Is.EqualTo(expectedMobile), "Mobile field value mismatch.");
        }
    }
}

using OpenQA.Selenium;

namespace CloudQATests.Pages
{
    public class AutomationPracticeFormPage : BasePage
    {
        private const string PageUrl = "https://app.cloudqa.io/home/AutomationPracticeForm";

        public AutomationPracticeFormPage(IWebDriver driver) : base(driver)
        {
        }

        public void GoTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
        }

        // --- Robust Elements ---
        // We use the helper methods from BasePage which rely on Label Text.
        
        private IWebElement FirstNameInput => GetInputByLabel("First Name");
        private IWebElement LastNameInput => GetInputByLabel("Last Name");
        private IWebElement EmailInput => GetInputByLabel("Email");
        private IWebElement MobileInput => GetInputByLabel("Mobile #");
        
        // For Gender, the text "Male", "Female" appears after the radio inputs (not in labels)
        private IWebElement MaleGenderRadio => GetRadioByFollowingText("Male");
        private IWebElement FemaleGenderRadio => GetRadioByFollowingText("Female");

        // --- Actions ---

        public void SetFirstName(string firstName)
        {
            FirstNameInput.Clear();
            FirstNameInput.SendKeys(firstName);
        }

        public string GetFirstName() => FirstNameInput.GetAttribute("value");

        public void SetLastName(string lastName)
        {
            LastNameInput.Clear();
            LastNameInput.SendKeys(lastName);
        }

        public string GetLastName() => LastNameInput.GetAttribute("value");

        public void SetEmail(string email)
        {
            EmailInput.Clear();
            EmailInput.SendKeys(email);
        }

        public string GetEmail() => EmailInput.GetAttribute("value");

        public void SetMobile(string mobile)
        {
            MobileInput.Clear();
            MobileInput.SendKeys(mobile);
        }
        
        public string GetMobile() => MobileInput.GetAttribute("value");

        public void SelectGender(string gender)
        {
            if (gender.Equals("Male", System.StringComparison.OrdinalIgnoreCase))
            {
                if (!MaleGenderRadio.Selected) MaleGenderRadio.Click();
            }
            else if (gender.Equals("Female", System.StringComparison.OrdinalIgnoreCase))
            {
                if (!FemaleGenderRadio.Selected) FemaleGenderRadio.Click();
            }
        }

        public bool IsGenderSelected(string gender)
        {
             if (gender.Equals("Male", System.StringComparison.OrdinalIgnoreCase))
            {
                return MaleGenderRadio.Selected;
            }
            else if (gender.Equals("Female", System.StringComparison.OrdinalIgnoreCase))
            {
                return FemaleGenderRadio.Selected;
            }
            return false;
        }
    }
}

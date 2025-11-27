using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CloudQATests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            // Default explicit wait of 10 seconds
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Robust helper to find an input element associated with a specific label text.
        /// Uses XPath to find the label, then the following input.
        /// </summary>
        protected IWebElement GetInputByLabel(string labelText)
        {
            var xpath = $"//label[normalize-space()='{labelText}']/following::input[1]";
            return WaitUntilVisible(By.XPath(xpath));
        }

        /// <summary>
        /// Robust helper to find a radio button or checkbox associated with a label.
        /// Handles cases where the input might be immediately before or after the label.
        /// </summary>
        protected IWebElement GetToggleByLabel(string labelText)
        {
            // Checks both preceding and following siblings for the input
            var xpath = $"//label[normalize-space()='{labelText}']/preceding::input[1] | //label[normalize-space()='{labelText}']/following::input[1]";
            return WaitUntilVisible(By.XPath(xpath));
        }

        /// <summary>
        /// Robust helper to find a radio button by the text that immediately follows it.
        /// Useful when the text is not inside a label element.
        /// </summary>
        protected IWebElement GetRadioByFollowingText(string textValue)
        {
            // This finds an input where the normalized text value appears immediately after
            var xpath = $"//input[@type='radio'][following-sibling::text()[normalize-space()='{textValue}'][1]]";
            try 
            {
                return WaitUntilVisible(By.XPath(xpath));
            }
            catch
            {
                // Fallback: try using the lowercase version as id
                var idValue = textValue.ToLower();
                return WaitUntilVisible(By.XPath($"//input[@type='radio'][@id='{idValue}']"));
            }
        }

        /// <summary>
        /// Waits for an element to be visible and returns it.
        /// </summary>
        protected IWebElement WaitUntilVisible(By locator)
        {
            return Wait.Until(d => 
            {
                var element = d.FindElement(locator);
                return (element != null && element.Displayed) ? element : null;
            });
        }
    }
}

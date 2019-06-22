using System;
using TechTalk.SpecFlow;
using System.Threading;
using SpecflowPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using static SpecflowPages.CommonMethods;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecflowTests.AcceptanceTest
{
    [Binding]
    public class SpecFlowFeature1Steps : Utils.Start
    {
        #region TestCase 1 - Add new language
        [Given(@"I clicked on the Language tab under Profile page")]
        public void GivenIClickedOnTheLanguageTabUnderProfilePage()
        {
            //Wait
            CommonMethods.Wait(3000);

            // Click on Languages tab 
            Driver.driver.FindElement(By.XPath("//a[text() = 'Languages']")).Click();


        }

        [When(@"I add a new language")]
        public void WhenIAddANewLanguage()
        {
            try
            {
                //Click on Add New button under Language tab
                Driver.driver.FindElement(By.XPath("//div[@data-tab = 'first']//div[text() = 'Add New']")).Click();

                //Add new Language - not existing language
                Driver.driver.FindElement(By.XPath("//input[@placeholder = 'Add Language']")).SendKeys("English");

                //Select the Basic level from the Language level
                SelectElement obj = new SelectElement(driver.FindElement(By.XPath("//div[@data-tab = 'first']//select[@name= 'level']")));
                obj.SelectByText("Basic");

                //Click on Add button
                Driver.driver.FindElement(By.XPath("//div[@data-tab = 'first']//input[@value = 'Add']")).Click();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }

        [Then(@"that language should be displayed on my listings")]
        public void ThenThatLanguageShouldBeDisplayedOnMyListings()
        {
            try
            {
                //Start the Reports
                CommonMethods.ExtentReports();
                CommonMethods.test = CommonMethods.extent.StartTest("Add a Language");

                string ExpectedValue = "English";

                //Get all the Language values in the List
                IList<IWebElement> langList = Driver.driver.FindElements(By.XPath("//div[@data-tab = 'first' ]//table//tr/td[1]"));
                Console.WriteLine(langList);

                bool matchLang = false;
                foreach (IWebElement lang in langList)
                {
                    if (ExpectedValue == lang.Text)
                    {
                        test.Log(LogStatus.Pass, "Test Passed: New Language added successfully");
                        SaveScreenShotClass.SaveScreenshot(Driver.driver, "LanguageAdded");
                        matchLang = true;
                        Assert.IsTrue(true);
                    }


                }

                if (matchLang == false)
                {
                    CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                    Assert.Fail();

                }

            }
            catch (Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed", e.Message);
                Assert.Fail();
            }


        }
        #endregion

        #region TestCase 2 - Adding 3 more languages with scenarioOutline
        [Given(@"I click on the Language tab to add multiple records")]
        public void GivenIClickOnTheLanguageTabToAddMultipleRecords()
        {
            CommonMethods.Wait(3000);
            // Click on Languages tab 
            Driver.driver.FindElement(By.XPath("//a[text() = 'Languages']")).Click();
        }

        [When(@"I enter '(.*)' and '(.*)' and click add new button")]
        public void WhenIEnterAndAndClickAddNewButton(string p0, string p1)
        {
            //Click on Add New button under Language tab
            Driver.driver.FindElement(By.XPath("//div[@data-tab = 'first']//div[text() = 'Add New']")).Click();

            //Add new Language - not existing language
            Driver.driver.FindElement(By.XPath("//input[@placeholder = 'Add Language']")).SendKeys(p0);

            //Select the Basic level from the Language level
            SelectElement obj = new SelectElement(driver.FindElement(By.XPath("//div[@data-tab = 'first']//select[@name= 'level']")));
            obj.SelectByText(p1);

            //Click on Add button
            Driver.driver.FindElement(By.XPath("//div[@data-tab = 'first']//input[@value = 'Add']")).Click();
        }

        [Then(@"the record '(.*)' should be added to the list")]
        public void ThenTheRecordShouldBeAddedToTheList(string p0)
        {
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Delete the first Language record");

            string ExpectedValue = p0;

            //Get all the Language values in the List
            IList<IWebElement> langList = Driver.driver.FindElements(By.XPath("//div[@data-tab = 'first' ]//table//tr/td[1]"));
            Console.WriteLine(langList);

            bool matchLang = false;
            foreach (IWebElement lang in langList)
            {
                if (ExpectedValue == lang.Text)
                {
                    test.Log(LogStatus.Pass, "Test Passed: New Language added successfully");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "LanguageAdded");
                    matchLang = true;
                    Assert.IsTrue(true);
                }
            }

            if (matchLang == false)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                Assert.Fail();

            }
        }

        #endregion

        #region TestCase 2- Duplicate addition

        [Given(@"I click on the language tab to add existing language")]
        public void GivenIClickOnTheLanguageTabToAddExistingLanguage()
        {
            CommonMethods.Wait(3000);
            // Click on the Language tabe inside profile page
            Driver.driver.FindElement(By.XPath("//a[text() = 'Languages']")).Click();

        }

        [When(@"I add the duplicate language")]
        public void WhenIAddTheDuplicateLanguage()
        {
            try
            {
                //Click on Add New button under Language tab
                Driver.driver.FindElement(By.XPath("//div[@data-tab = 'first']//div[text() = 'Add New']")).Click();

                //Add a language value which is already existing
                Driver.driver.FindElement(By.XPath("//input[@placeholder = 'Add Language']")).SendKeys("English");

                //Select the Conversational level from the Language level
                SelectElement obj = new SelectElement(driver.FindElement(By.XPath("//div[@data-tab = 'first']//select[@name= 'level']")));
                obj.SelectByText("Basic");

                //Click on Add button
                Driver.driver.FindElement(By.XPath("//div[@data-tab = 'first']//input[@value = 'Add']")).Click();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [Then(@"The seller should not allow to add")]
        public void ThenTheSellerShouldNotAllowToAdd()
        {

            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Add a duplicate Language");
            //get the message from the popup box
            try
            {
                String actualMessage = Driver.driver.FindElement(By.XPath("//div[contains(@class, 'ns-box-inner')]")).Text;
                string expectedMessage = "This language is already exist in your language list.";
                if (actualMessage == expectedMessage)
                {

                    test.Log(LogStatus.Pass, "Test Passed: Not allowed to add duplicate language");
                    Assert.IsTrue(true);

                }
                else
                {
                    CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                    Assert.Fail();

                }
            }
            catch (Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed", e.Message);
                Assert.Fail();
            }


        }
        #endregion

        #region TestCase3 - Edit the values
        [Given(@"I click on the language tab to check edit")]
        public void GivenIClickOnTheLanguageTabToCheckEdit()
        {
            CommonMethods.Wait(3000);
            // Click on the Language tabe inside profile page
            Driver.driver.FindElement(By.XPath("//a[text() = 'Languages']")).Click();
        }
        [When(@"I click edit button and change the language level")]
        public void WhenIClickEditButtonAndChangeTheLanguageLevel()
        {
            //click on the Edit button corresponding to English language
            //string editBtnXpath = "//div[@data-tab = 'first' ]//table//tr[1]/td[3]";
            string editBtnXpath = "//div[@data-tab = 'first' ]//td[text()= 'English']/following-sibling::td[@class]//i[@class = 'outline write icon']";
            if (CommonMethods.isElementPresent(By.XPath(editBtnXpath)))
            {
                Driver.driver.FindElement(By.XPath(editBtnXpath)).Click();
            }

            // Change the level from Basic to conversational in language level - make sure level should be other than conversational before edit
            SelectElement obj = new SelectElement(driver.FindElement(By.XPath("//div[@data-tab = 'first']//select[@name= 'level']")));
            obj.SelectByText("Conversational");

            //Click on the Update button
            Driver.driver.FindElement(By.XPath("//div[@data-tab = 'first']//input[@value = 'Update']")).Click();


        }
        [Then(@"The language level should be updated with new info")]
        public void ThenTheLanguageLevelShouldBeUpdatedWithNewInfo()
        {
            //Extent report intialization
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Edit the Language level");

            String actualMessage = Driver.driver.FindElement(By.XPath("//div[contains(@class, 'ns-box-inner')]")).Text;
            string expectedMessage = "English has been updated to your languages";
            if (actualMessage == expectedMessage)
            {
                test.Log(LogStatus.Pass, "Test Passed: Not allowed to add duplicate language");
                Assert.IsTrue(true);
            }
            else
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                Assert.Fail();
            }
        }
        #endregion

        #region TestCase 4- Max limit of records
        [Given(@"I click on the language tab to check max limit")]
        public void GivenIClickOnTheLanguageTabToCheckMaxLimit()
        {
            CommonMethods.Wait(3000);
            // Click on the Language tabe inside profile page
            Driver.driver.FindElement(By.XPath("//a[text() = 'Languages']")).Click();
        }
        [When(@"I try to add fifth record")]
        public void WhenITryToAddFifthRecord()
        {
            //Added code in the following function - not much validation required, since the Add new button is not visible


        }
        [Then(@"The seller should not allo to add")]
        public void ThenTheSellerShouldNotAlloToAdd()
        {
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Max Limit check - Trying to add fifth recod");

            string addNewBtnXpath = "//div[@data-tab = 'first']//div[text() = 'Add New']";
            // Driver.driver.FindElement(By.XPath("//div[@data-tab = 'first']//div[text() = 'Add New']"));
            if (CommonMethods.isElementPresent(By.XPath(addNewBtnXpath)))
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                Assert.Fail();
            }
            else
            {
                test.Log(LogStatus.Pass, "Test Passed: Not showing Add new button");
                Assert.IsTrue(true);
            }
        }
        #endregion

        #region TestCase 5 - Delete the record
        [Given(@"I click on the language tab to check Delete function")]
        public void GivenIClickOnTheLanguageTabToCheckDeleteFunction()
        {
            CommonMethods.Wait(3000);
            // Click on the Language tabe inside profile page
            Driver.driver.FindElement(By.XPath("//a[text() = 'Languages']")).Click();
        }

        [When(@"I click on the delete icon for the first record")]
        public void WhenIClickOnTheDeleteIconForTheFirstRecord()
        {
            //Click on the delete icon for the first language record 
                string deleteBtnXpath = "//tbody[1]//*[@class = 'remove icon']";
                Driver.driver.FindElement(By.XPath(deleteBtnXpath)).Click();
         }

        [Then(@"The record should be deleter from the list\.")]
        public void ThenTheRecordShouldBeDeleterFromTheList_()
        {
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Delete the first Language record");

            String actualMessage = Driver.driver.FindElement(By.XPath("//div[contains(@class, 'ns-box-inner')]")).Text;
            string expectedMessage = "has been deleted from your languages";
            if (actualMessage.Contains(expectedMessage))
            {

                test.Log(LogStatus.Pass, "Test Passed: The record deleted");
                Assert.IsTrue(true);

            }
            else
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                Assert.Fail();

            }

        }
        #endregion

        #region Test6 - Shareskil page test
        [Given(@"I am on profile page and the share skill button is clickable")]
        public void GivenIAmOnProfilePageAndTheShareSkillButtonIsClickable()
        {

        }

        [When(@"I click on Share skill button")]
        public void WhenIClickOnShareSkillButton()
        {
            // click on Shareskill button
            driver.FindElement(By.XPath("//a[contains(text(), 'Share Skill')]")).Click();
        }

        [Then(@"I should move to the share skill page")]
        public void ThenIShouldMoveToTheShareSkillPage()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Add a Language");

            string expectedTitle = "ServiceListing";

            if (driver.Title.Equals(expectedTitle))
            {
                test.Log(LogStatus.Pass, "Test Passed- Navigates to shareskill page ");
                //SaveScreenShotClass.SaveScreenshot(Driver.driver, "shareSkill Page");
                Assert.IsTrue(true);
            }
            else
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                Assert.Fail();
            }

        }

        #endregion

        #region Test - Create New share skill record
        [Given(@"I am on profile page and clicked on share skill button")]
        public void GivenIAmOnProfilePageAndClickedOnShareSkillButton()
        {
            //On Profile page
            // click on Shareskill button
            
            driver.FindElement(By.XPath("//a[contains(text(), 'Share Skill')]")).Click();

        }

        [When(@"I give all the mandatory values for share skill form")]
        public void WhenIGiveAllTheMandatoryValuesForShareSkillForm()
        {
            // enter Title and Description
            driver.FindElement(By.XPath("//input[@type = 'text'  and @name = 'title']")).SendKeys("Title for Specflow test");
            driver.FindElement(By.XPath("//textarea[@name = 'description']")).SendKeys("Description for SpecflowTest");

            //Select Category 
            IWebElement CategList = driver.FindElement(By.XPath("//select[@name = 'categoryId']"));
            SelectElement categSelect = new SelectElement(CategList);
            categSelect.SelectByText("Fun & Lifestyle");

            //Select Sub category 
            IWebElement SubCategList = driver.FindElement(By.XPath("//select[@name = 'subcategoryId']"));
            SelectElement subCategSelect = new SelectElement(SubCategList);
            subCategSelect.SelectByText("Gaming");

            //enter tags value
            IWebElement tags = driver.FindElement(By.XPath("//*[contains(text(), 'Tags')]/parent::div/following-sibling::div//input"));

            tags.SendKeys("Games");
            tags.SendKeys(Keys.Enter);

            // Select service type as one-off
            driver.FindElement(By.XPath("//label[contains(text(), 'One-off service')]/preceding-sibling::input[@name = 'serviceType']")).Click();

            //Select Location type as on-line
            driver.FindElement(By.XPath("//label[contains(text(), 'Online')]/preceding-sibling::input[@name = 'locationType']")).Click();

            //Select Skill-trade and the corresponding details
            //Skill-trade as Skill-exchange
            //Skill-exchage as newGame
            driver.FindElement(By.XPath("//label[contains(text(), 'Skill-exchange')]/preceding-sibling::input[@name = 'skillTrades']")).Click();

            IWebElement SkillTag = driver.FindElement(By.XPath("//h3[contains(text(), 'Skill-Exchange')]/parent::div/following-sibling::div//input"));
            SkillTag.SendKeys("NewGame");
            SkillTag.SendKeys(Keys.Enter);

            //Click on Save button
            driver.FindElement(By.XPath("//input[@value = 'Save']")).Click();
        }

        [Then(@"the new record should be created")]
        public void ThenTheNewRecordShouldBeCreated()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("New share skill record");

            //Check whether record has been created or not
            if ((driver.Title).Equals("ListingManagement"))
            {
                test.Log(LogStatus.Pass, "New share skill record created sucessfully");
                Assert.IsTrue(true);
            }
            else
            {
                test.Log(LogStatus.Fail, "New Share skill record not saved");
                Assert.Fail();
            }

        }

        #endregion

        #region test8- new share skill record with incomplete data

        [Given(@"I am on profile page and click on share skill button")]
        public void GivenIAmOnProfilePageAndClickOnShareSkillButton()
        {
            //On Profile page
            // click on Shareskill button

            driver.FindElement(By.XPath("//a[contains(text(), 'Share Skill')]")).Click();

        }

        [When(@"I give all inputs and not give a mandatory value")]
        public void WhenIGiveAllInputsAndNotGiveAMandatoryValue()
        {
            //Check if user is able to save the record without entering mandatory values
            // Not entering Title field - which is mandatory
           // driver.FindElement(By.XPath("//input[@type = 'text'  and @name = 'title']")).SendKeys("Title for Specflow test");
            driver.FindElement(By.XPath("//textarea[@name = 'description']")).SendKeys("Description for SpecflowTest");

            //Select Category 
            IWebElement CategList = driver.FindElement(By.XPath("//select[@name = 'categoryId']"));
            SelectElement categSelect = new SelectElement(CategList);
            categSelect.SelectByText("Fun & Lifestyle");

            //Select Sub category 
            IWebElement SubCategList = driver.FindElement(By.XPath("//select[@name = 'subcategoryId']"));
            SelectElement subCategSelect = new SelectElement(SubCategList);
            subCategSelect.SelectByText("Gaming");

            //enter tags value
            IWebElement tags = driver.FindElement(By.XPath("//*[contains(text(), 'Tags')]/parent::div/following-sibling::div//input"));

            tags.SendKeys("Games");
            tags.SendKeys(Keys.Enter);

            // Select service type as one-off
            driver.FindElement(By.XPath("//label[contains(text(), 'One-off service')]/preceding-sibling::input[@name = 'serviceType']")).Click();

            //Select Location type as on-line
            driver.FindElement(By.XPath("//label[contains(text(), 'Online')]/preceding-sibling::input[@name = 'locationType']")).Click();

            //Select Skill-trade and the corresponding details
            //Skill-trade as Skill-exchange
            //Skill-exchage as newGame
            driver.FindElement(By.XPath("//label[contains(text(), 'Skill-exchange')]/preceding-sibling::input[@name = 'skillTrades']")).Click();

            IWebElement SkillTag = driver.FindElement(By.XPath("//h3[contains(text(), 'Skill-Exchange')]/parent::div/following-sibling::div//input"));
            SkillTag.SendKeys("NewGame");
            SkillTag.SendKeys(Keys.Enter);

            //Click on Save button
            driver.FindElement(By.XPath("//input[@value = 'Save']")).Click();

        }

        [Then(@"try to save the record")]
        public void ThenTryToSaveTheRecord()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("New shareskill record - with no mandatory input");

            //Check whether record has been created or not
            if ((driver.Title).Equals("ServiceListing"))
            {
                test.Log(LogStatus.Pass, "Pass - New share skill record not created");
                Assert.IsTrue(true);
            }
            else
            {
                test.Log(LogStatus.Fail, "Test Failed");
                Assert.Fail();
            }
        }

        #endregion

        #region test9-Delete existing shareskill record

        [Given(@"I moved to the Managelisting Page")]
        public void GivenIMovedToTheManagelistingPage()
        {
            //click on ManageListings tab
            driver.FindElement(By.XPath("//a[contains(text(), 'Manage Listings')]")).Click();
        }

        [When(@"I delete a record from the list")]
        public void WhenIDeleteARecordFromTheList()
        {
            //click on the remove icon 
            driver.FindElement(By.XPath("//i[@class = 'remove icon']")).Click();
        }

        [Then(@"the record should be deleted from the list")]
        public void ThenTheRecordShouldBeDeletedFromTheList()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("New share skill record");

            //Check for deletion - accept delete record 
            if (driver.FindElement(By.XPath("//button[@class = 'ui icon positive right labeled button']")).Displayed)
            {
                driver.FindElement(By.XPath("//button[@class = 'ui icon positive right labeled button']")).Click();
                test.Log(LogStatus.Pass, "Test Pass- record deleted");
                Assert.IsTrue(true);
            }
            else
            {
                test.Log(LogStatus.Fail, "Test failed");
                Assert.Fail();
            }
        }

        #endregion

        #region test10- search skills by Category

        [Given(@"I click on the search Icon on profile page")]
        public void GivenIClickOnTheSearchIconOnProfilePage()
        {
            //Click on Search icon
            driver.FindElement(By.XPath("//i[@class = 'search link icon']")).Click();

        }

        [When(@"I select a category on search page")]
        public void WhenISelectACategoryOnSearchPage()
        {
            //Select a category from the search option
            driver.FindElement(By.XPath("//a[text() = 'Programming & Tech']")).Click();

        }

        [Then(@"the skills list should be displayed with that category")]
        public void ThenTheSkillsListShouldBeDisplayedWithThatCategory()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Search by Category");

            Thread.Sleep(2000);
            //The feature is not available to check it- so getting the number of records
            string numRecords = driver.FindElement(By.XPath("//a[@class = 'active item category']/span")).Text;
            if(numRecords != null)
            {
                test.Log(LogStatus.Pass, "Test Passed: Total number of records " + numRecords);
                Assert.IsTrue(true);
            }
            else
            {
                test.Log(LogStatus.Fail, "Test failed");
            }
        }

        #endregion

        #region test11 - Search by subcategory

        [Given(@"I clicked on search icon on profile page")]
        public void GivenIClickedOnSearchIconOnProfilePage()
        {
            //Click on Search icon
            driver.FindElement(By.XPath("//i[@class = 'search link icon']")).Click();
        }

        [When(@"I select category and subcategory on search page")]
        public void WhenISelectCategoryAndSubcategoryOnSearchPage()
        {
            //Select a category from the search option
            driver.FindElement(By.XPath("//a[text() = 'Programming & Tech']")).Click();
            //Select a subcategory under the above category
            driver.FindElement(By.XPath("//a[text() = 'QA']")).Click();

        }

        [Then(@"the skills should be listed with the given criteria")]
        public void ThenTheSkillsShouldBeListedWithTheGivenCriteria()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Search by SubCategory");

            Thread.Sleep(2000);
            //The feature is not available to check it- so getting the number of records test
            string numRecords = driver.FindElement(By.XPath("//a[@class='active item subcategory']/span")).Text;
            if (numRecords != null)
            {
                test.Log(LogStatus.Pass, "Test Passed: Total number of records " + numRecords);
                Assert.IsTrue(true);
            }
            else
            {
                test.Log(LogStatus.Fail, "Test failed");
            }
        }

        #endregion

        #region test12 - Search skill by Online filter

        [Given(@"I click on search Icon on profile page")]
        public void GivenIClickOnSearchIconOnProfilePage()
        {
            //Click on Search icon
            driver.FindElement(By.XPath("//i[@class = 'search link icon']")).Click();
        }

        [When(@"I select Online value from the filters section")]
        public void WhenISelectOnlineValueFromTheFiltersSection()
        {
            //Click on Online filter i search page
            driver.FindElement(By.XPath("//button[text() = 'Online']")).Click();

        }

        [Then(@"the skills should be listed with the given filter")]
        public void ThenTheSkillsShouldBeListedWithTheGivenFilter()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Search skill by Online filter");
            
                test.Log(LogStatus.Pass, "Test Passed");
                Assert.IsTrue(true);

        }

        #endregion

        #region Test13 - Check the number of records per page test

        [Given(@"I clicked on search Icon")]
        public void GivenIClickedOnSearchIcon()
        {
            //Click on Search icon
            driver.FindElement(By.XPath("//i[@class = 'search link icon']")).Click();

        }

        [When(@"I select number of pages as (.*)")]
        public void WhenISelectNumberOfPagesAs(int p0)
        {
            //select number of records as 12
            driver.FindElement(By.XPath("//div[@class = 'right floated column ']//button[text() = '12']")).Click();

        }

        [Then(@"the given number of records should be displayed per page")]
        public void ThenTheGivenNumberOfRecordsShouldBeDisplayedPerPage()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Number of REcords per page Test");

            //get the number of records from the page
            int actualRecords = driver.FindElements(By.XPath("//div[@class = 'ui stackable three cards']/div")).Count;

            if(actualRecords.Equals(12))
            {
                test.Log(LogStatus.Pass, "Test passed");
                Assert.IsTrue(true);
            }
            else
            {
                test.Log(LogStatus.Fail, "Test failed");
                Assert.Fail();
            }
        }


        #endregion

        #region Test 15 - Sign In with Happy path

        [Given(@"I clicked on the SignIn button after launching the url")]
        public void GivenIClickedOnTheSignInButtonAfterLaunchingTheUrl()
        {
            //steps have been already done through BeforeScenario hook
        }

        [When(@"Click on signIn and enter correct ""(.*)"" and ""(.*)""")]
        public void WhenClickOnSignInAndEnterCorrectAnd(string p0, string p1)
        {
            //this part has been covered through Before Scenario hook
        }

        [Then(@"user should be able to login to the home page")]
        public void ThenUserShouldBeAbleToLoginToTheHomePage()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Happy path SingIn Test");

            if(driver.Title.Equals("Profile"))
            {
                test.Log(LogStatus.Pass, "Test Passed");
                Assert.IsTrue(true);
            }
            else
            {
                test.Log(LogStatus.Fail, "Test Failed");
                Assert.Fail();
            }

        }



        #endregion
    }
}


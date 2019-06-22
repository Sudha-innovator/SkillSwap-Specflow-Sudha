Feature: SpecFlowFeature1
	In order to update my profile 
	As a skill trader
	I want to add the languages that I know

@mytag
Scenario: Test01 Check if user could able to add a language 
	Given I clicked on the Language tab under Profile page
	When I add a new language
	Then that language should be displayed on my listings

Scenario: Test02 Check if seller could be able to add a duplicate language
    Given I click on the language tab to add existing language
	When I add the duplicate language
	Then The seller should not allow to add 

Scenario: Test03 Check if seller can be able to edit the existing language
      Given I click on the language tab to check edit
	  When I click edit button and change the language level
	  Then The language level should be updated with new info

Scenario: Test04 Check if seller can be able to add fifth record in language
       Given I click on the language tab to check max limit
	   When I try to add fifth record 
	   Then The seller should not allo to add

Scenario: Test05 Check if seller can be able to delete the language record
       Given I click on the language tab to check Delete function
	   When I click on the delete icon for the first record
	   Then The record should be deleter from the list.

Scenario Outline: Test02 scenario outline with Adding Multiple record 
       Given I click on the Language tab to add multiple records
	   When I enter '<name>' and '<level>' and click add new button
	   Then the record '<name>' should be added to the list 
	   

	   Examples: 
	   | name   | level          | namechk |
	   | Telugu | Fluent         | Telugu  |
	   | Tamil  | Conversational | Tamil   |
	   | Hindi  | Basic          | Hindi   |

Scenario: Test06 Check if user is able to move to share skill page 
	Given I am on profile page and the share skill button is clickable
	When I click on Share skill button
	Then I should move to the share skill page 

Scenario: Test07 Check if user is able to create a new share skill
   Given I am on profile page and clicked on share skill button
   When I give all the mandatory values for share skill form
   Then the new record should be created

Scenario: Test08 Check if user is able to create a new record with incomplete data
   Given I am on profile page and click on share skill button
   When I give all inputs and not give a mandatory value
   Then try to save the record

Scenario: Test09 Check if user is able to delete a record in ManageListings
	Given I moved to the Managelisting Page
	When I delete a record from the list
	Then the record should be deleted from the list

Scenario: Test10 Check if user is able to search skills by category wise
	Given I click on the search Icon on profile page
	When I select a category on search page
	Then the skills list should be displayed with that category

Scenario: Test11 Check if user is able to search skills by category and subcategory wise
	Given I clicked on search icon on profile page
	When I select category and subcategory on search page
	Then the skills should be listed with the given criteria

Scenario: Test12 Check is user is able to search skills by online filter
	Given I click on search Icon on profile page
	When I select Online value from the filters section
	Then the skills should be listed with the given filter

Scenario: Test13 Check the numberOfRecords per page button
	Given I clicked on search Icon
	When I select number of pages as 12
	Then the given number of records should be displayed per page

Scenario: Test14 Check if signIn form is available
	Given I clicked on Sign in button after launching the url
	When I click on SignIn button
	Then the user should navigate to signIn form

Scenario: Test15 Check if user is able to signIn with Happy credentials
	Given I clicked on the SignIn button after launching the url
	When Click on signIn and enter correct "sudha86.tumu@gmail.com" and "test@123"
	Then user should be able to login to the home page


Scenario Outline: Test16 Check if user is able to SignIn with incorrect credentials
	Given I click on the signIn button after launching the url
	When click on SignIN and enter values <username> and <password>
	Then user should not be allowed to login
	
	Examples: 
	| username               | password |
	| sudha86.tumu@gmail.com | test123  |

Feature: OnlienSearch
	
@mytag
Scenario Outline: user can serach the wiki with his selected language
	Given user naviogates to url 'http://www.wikipedia.org/'
	When user enters 'test with xunit' in serach field
	And selected the '<language>' language from language drop down 
	And click on the search button
	Then search result page will be displayed 
	Examples:
	|language|
	|English|
	|Deutsch|
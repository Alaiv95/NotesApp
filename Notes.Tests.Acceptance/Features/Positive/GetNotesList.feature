Feature: GetNotesList

Get list of notes from /note endpoint

@SmokeTest
Scenario: User hits /note endpoint to get list of notes
	Given The URL for the list of notes is "/note"
	When I send request to this URL
	Then I should get list of notes that contains following data
	| id                                   | title   |
	| 70c3e832-9b93-42bc-bfcc-354426ae0681 | dasdasd |
	| 59459251-7cea-4091-a7c2-6d80be198925 | dasdasd |
	| 027b540b-5145-4c95-93d0-eb38f380f097 | test    |

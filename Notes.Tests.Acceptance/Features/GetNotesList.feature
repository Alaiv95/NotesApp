Feature: GetNotesList

Get list of notes from /note endpoint

@SmokeTest
Scenario: User hits /note endpoint to get list of notes
	Given The URL for the list of notes is "/note"
	When I send request to this URL
	Then I should get list of notes that contains following data
	| id                                   | title   |
	| e3ed53f4-9a69-42b6-8028-d27066f1a9e7 | dasdasd |
	| 71319891-b7ad-49c3-b8c8-43d9e940db2f | test    |
	| 12eb04a0-8f6a-4e80-94d2-b0c1adfa8d06 | test    |

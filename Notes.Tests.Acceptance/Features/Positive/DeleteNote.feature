Feature: DeleteNote

Deleting existing note using /note/{id} endpoint

@SmokeTest
Scenario: User hits /note/{id} endpoint to delete note
	Given The URL to delete the note is "/note/{id}"
	When I send request to this URL with created note id
	Then I should get success status code
	And Created note should not exist in database

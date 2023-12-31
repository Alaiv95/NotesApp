Feature: UpdateNote

Updating existing note data using /note/{id} endpoint

@SmokeTest
Scenario: User hits /note/{id} endpoint to update existing note
	Given Already created note and its id with Url "/note"
	Given The URL to update the note is "/note"
	When I send request to this URL with existing note id and following values
		| Title    | Details    |
		| newTitle | newDetails |
	Then Updated note should contains new values
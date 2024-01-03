Feature: DeleteNoteNegative

Attempts to delete non existing note or with wrong userId from DELETE /note/{id} endpoint

@Negative
Scenario: User attempts to delete non existing note from /note/{id} endpoint
	Given The URL to delete note is "/note/{id}"
	When I send request to this URL with following id "0add75c2-815c-4b4a-83a1-f79d8eb63217"
	Then  I should get response with error status code

@Negative
Scenario: User attempts to delete note of another user from /note/{id} endpoint
	Given The URL to delete note is "/note/{id}"
	When I send request to this URL with following id "70c3e832-9b93-42bc-bfcc-354426ae0681"
	Then  I should get response with error status code
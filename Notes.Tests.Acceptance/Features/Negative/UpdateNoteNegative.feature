Feature: UpdateNoteNegative

Attempts to update non existing note or with wrong userId from PUT /note/{id} endpoint

@Negative
Scenario: User attempts to update non existing note from /note/{id} endpoint
	Given The URL to update note is "/note/{id}"
	When I send request to this URL with following data
		| Id									| Title   | Details   |
		|  0add75c2-815c-4b4a-83a1-f79d8eb63217 | newTitle | newDetails |
	Then  I should get error status code from response

@Negative
Scenario: User attempts to update note of another user from /note/{id} endpoint
	Given The URL to update note is "/note/{id}"
	When I send request to this URL with following data
		| Id									| Title   | Details   |
		|  70c3e832-9b93-42bc-bfcc-354426ae0681 | newTitle | newDetails |
	Then  I should get error status code from response

@Negative
Scenario: User attempts to update note with empty Title field from /note/{id} endpoint
	Given The URL to update note is "/note/{id}"
	When I send request to this URL with following data
		| Title   | Details   |
		|		   | newDetails |
	Then  I should get error status code from response
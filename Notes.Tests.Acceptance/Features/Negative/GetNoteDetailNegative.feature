Feature: GetNoteDetailNegative

Non existing Note detail info from /note/{id} endpoint

@Negative
Scenario: User hit /note/{id} endpoint to get non existing note detail info
	Given The URL for the notes details is "/note/{id}"
	When I send request to this URL with following note id "b2b68020-96c8-4211-9d3c-f78dfd30aed5"
	Then I should get error status code response

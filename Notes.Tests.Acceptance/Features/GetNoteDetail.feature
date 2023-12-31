Feature: GetNoteDetail

Note detail info from /note/{id} endpoint

@SmokeTest
Scenario: User hit /note/{id} endpoint to get note detail info
	Given The URL for the notes detail is "/note/{id}"
	When I send request to this URL with this id "8b073196-31c7-483c-b5bb-36aa21133985"
	Then I should get following note detail
	| id                                   | title | details      | creationDate                | editDate |
	| 8b073196-31c7-483c-b5bb-36aa21133985 | test  | test details | 2023-12-31T21:17:25.3724528 |          |

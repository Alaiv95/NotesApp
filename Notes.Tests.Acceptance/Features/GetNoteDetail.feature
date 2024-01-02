Feature: GetNoteDetail

Note detail info from /note/{id} endpoint

@SmokeTest
Scenario: User hit /note/{id} endpoint to get note detail info
	Given The URL for the notes detail is "/note/{id}"
	When I send request to this URL with this id "70c3e832-9b93-42bc-bfcc-354426ae0681"
	Then I should get following note detail
	| id                                   | title	  | details      | creationDate                | editDate |
	| 70c3e832-9b93-42bc-bfcc-354426ae0681 | dasdasd  | test details | 2024-01-02T21:53:42.6357505 |          |

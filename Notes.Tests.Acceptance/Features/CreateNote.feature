Feature: CreateNote

Creation of new note with /note endpoint

@SmokeTest
Scenario: User hits /note endpoint with valid data to create new note
	Given The URL to create note is "/note"
	When I send request to this endpoint with given values
		| Title | Details      |
		| test  | test details |
	Then I can get created note from database with id from response
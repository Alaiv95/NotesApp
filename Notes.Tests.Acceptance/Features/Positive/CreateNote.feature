Feature: CreateNote

Creation of new note with /note endpoint

@SmokeTest
Scenario: User hits /note endpoint with valid data to create new note
	Given The URL to create note is "/note"
	When I send request to this endpoint with given values
		| Title | Details      |
		| test  | test details |
	Then I can get created note from database with id from response

Scenario: User hits /note endpoint with max length Title to create new note
	Given The URL to create note is "/note"
	When I send request to this endpoint with given values
		| Title | Details      |
		| YwguHVmZptUHHyZGqtNevGxrgANLMfGfFnqjtuXAmwjYnDazQLgzuirCLArfuURqCcutfbHeMXPUDWCXQUVUvNzkXEcTNiApZGhHJPRLTEMFmvLTPewCVfJDbtjvAWNkRrGUeKPPrzCVLJJFxxZVHp  | test  |
	Then I can get created note from database with id from response

Scenario: User hits /note endpoint with min length data to create new note
	Given The URL to create note is "/note"
	When I send request to this endpoint with given values
		| Title | Details |
		| T		| Y		  |
	Then I can get created note from database with id from response
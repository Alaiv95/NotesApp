Feature: CreateNote with InvalidData

Attempts to create Note with invalid data from POST /note endpoint

@Negative
Scenario: User attempts to create Note with empty title at /note endpoint
	Given The URL to create new note is "/note"
	When I send request to this url with empty Title
		| Title | Details      |
		|	    | test details |
	Then I should get error status response

@Negative
Scenario: User attempts to create Note with more then max len title at /note endpoint
	Given The URL to create new note is "/note"
	When I send request to this url with empty Title
		| Title | Details      |
		| vPbXiWqfYrQRLuKyALFUWSqXVqCmrvqYrYcZGpPCguHpPbbrSYqZvYriAxJbMmvZpuHCgmWmeRnqMhUnYJxfvYemifLWqNhNbnQfpwjxmKVvqBGgJzuENUqMvfHVaMYxJTPLGBuYCpiiXtwefHAyEYr   | test details |
	Then I should get error status response

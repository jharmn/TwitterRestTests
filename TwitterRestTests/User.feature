Feature: Twitter user lookup

@Positive @Get @User
Scenario: Show twitter user
	Given I access the resource url "/1/users/show.json?screen_name=jasonh_n_austin&include_entities=true"
	When I retrieve the results
	Then the status code should be 200
	And it should have the field "name" containing the value "Jason Harmon"
	And it should have the field "id" containing the value "57005215"

@Negative @Get @User
Scenario: Invalid twitter user
	Given I access the resource url "/1/users/show.json?screen_name=jasonh_n_portland&include_entities=true"
	When I retrieve the results
	Then the status code should be 404
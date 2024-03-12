Feature: CRUD

Scenario: CRUD
	When Create and add to database new author
		| Name          | Login         | Email           |
		| NewTestAuthor | IqaAutomation | tester@a1qa.com |
	Then Created author 'is' exist in database
	And Property 'Name' of created author in database 'is' equal to 'NewTestAuthor'
	And Property 'Login' of created author in database 'is' equal to 'IqaAutomation'
	And Property 'Email' of created author in database 'is' equal to 'tester@a1qa.com'
	When Update property 'Login' of created author in database as a 'New qa Login'
	Then Property 'Login' of created author in database 'is' equal to 'New qa Login'
	When Delete created author from database
	Then Created author 'is not' exist in database

Feature: CRUD

Scenario: CRUD
	When Create and add to database new author
		| Name          | Login         | Email           |
		| NewTestAuthor | IqaAutomation | tester@a1qa.com |
	Then Created author 'is' exist in database
		And Property 'name' of created author in database 'is' equal to 'NewTestAuthor'
		And Property 'login' of created author in database 'is' equal to 'IqaAutomation'
		And Property 'email' of created author in database 'is' equal to 'tester@a1qa.com'
	When Update property 'login' of created author in database as a 'New_qa_Login'
	Then Property 'login' of created author in database 'is' equal to 'New_qa_Login'
	When Delete created author from database
	Then Created author 'is not' exist in database

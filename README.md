# TMS
This is Task Management System's Api (like TFS)
1. before start project, run TMS.sql file, it will create tables and add admin user into the table(userName : admin, password : admin), 
	also it will add permissions.

2. postman collections use 2 variables, which are declared on collection level:
	1. url - written address of service
	2. token - when user logins, jwt token is returned, you have to write this token to collection's token varaible, after this, it will be in every request 
		automatically.

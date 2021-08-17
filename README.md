# Introduction
A simple snowboard shop E-commerce web application with asp.net, MVC (Model-View-Controller), along with SQL database integration. The web application utilizes Microsoft’s ASP.NET and MVC template and was developed with Microsoft’s Visual Studio 2019 and database connections to a Microsoft Azure server.

# Database (Azure/SQL)
The snowboard shop utilizes the Entity Framework to connect the web application to an SQL Azure database. Your database should connect upon building the web application as the server allows for public endpoint connections. However, to connect your own database you need to adjust the connection string within the JSON file “appsettings.JSON”. Then simply running the “Update-database” command in the package manager console will allow you to connect your own database to the web application. Shown below is the connection string that connects to my own Azure database. **The project will not compile and will run into errors if a functioning database connection is not set here.**

# Account Validation and Security
When a user registers an account they are sent an email that contains a link to confirm their account and adjusts the Boolean value of “EmailConfirmed” within the user table. This security measure allows for ensuring a user’s email is real and their own and if the user forgets their password they are also sent an email with a link to allow them to change the password.
Continually, the role and user ID’s and passwords were generated with a hash value for their specific ID’s as an added security measure. Most built in log-in and account management scripts come from JSON and validation scripts such as “jquery.validate.min.js” and various other JSON libraries.


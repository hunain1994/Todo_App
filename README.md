# Todo-App

Description

A simple todo application with n-tier architecture created in dotnet core 3.1, dotnet core entityframework and code first approach.

Layers included are
1. Data Layer
2. Service Layer
3. Api Layer
4. UI layer

How to run
1. Create a database in sqlserver
2. Goto appsettings.json file of Api project and update server connection string for database according to your sql server
3. Write command on package manager console update-database
4. After database is migrated, set multiple startup projects (if not already configured) with Api and Web as startup projects
5. Port for Api project is (http :5000)
6. Port for Web project are (https :5002) (http :5003)

Run Project


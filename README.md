# Book Store Web API

This project is an implementation of Book Store which was developed with the **.NET 5** framework.

It basically provides **CRUD** operations for entities and a basic authentication mechanism for the endpoints. 

As a database, an in memory database has been used with default data which are loaded when WebApi runs. 

Also, inital test data which are loaded when a test runs have been used in the Tests project.

All unit tests have been done with the **xUnit** tool. Also, **FluentValidation** library has been to do unit tests.

It runs on port 5000 and 5001 *(this port is used by swagger, namely for test purposes. So, to use api, please use port 5000 instead of 5001)*. <br /><br />

# Overview of Endpoints

![Swagger](https://github.com/recep-yildirim/BookStoreWebApi/blob/master/Images/swagger.png)
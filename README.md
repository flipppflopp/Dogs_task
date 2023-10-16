#Dog API application

##General

This is a application which allows you to add new dogs in database, view existing dogs and sorting list of requested dogs.

#Structure

###DB
Solution contains 4 projects, `DB` contains database context and models of database. 
Database performed with **EF** using code-first. Models located in `DB.Models` directory, 
also we have directory `DB.Models.Validations` which contains `DogValidation` class,
this class is declaring an attribute which needed validate invalid JSON in API.

###Dogs

This project contains API for our application, here we have controller `DogsController`, 
in this controller we have API in which we are handling requests, requests handling using MediatR.
In `appsettings.json` and `Startup.cs` we are using **AspNetCoreRateLimit** to handle situations 
when we have to many requests per time.

###Services

In **Services** we have directory named `Features` in features we have realisation of Mediator pattern,
each directory inside if `Features` contains commands and commands handlers which is performing request 
processing. Directories `Interfaces` and `Services` is needed to realise Repository pattern, in `Interfaces`
we have interfaces of our services, and using it to open our repository for outer usage. In `Services` we have
concrete realisation of our repositories.

###Tests

This project contains unit tests to check our repository. It`s developed using XUnit.

##Using

You can use this application using `curl` command in basic Windows terminal.

Getting dogs example:
````
//Getting dogs sorted by fields
curl -X GET "https://localhost:7151/dogs?attribute=tail_length&order=asc"
curl -X GET "https://localhost:7151/dogs?attribute=weight&order=desc"

//Getting dogs using pagination
curl -X GET "https://localhost:7151/dogs?pageNumber=3&pageSize=10"
curl -X GET "https://localhost:7151/dogs?pageNumber=4&pageSize=7"

//Getting dogs using both features
curl -X GET "https://localhost:7151/dogs?attribute=tail_length&order=asc&pageNumber=3&pageSize=10"
curl -X GET "https://localhost:7151/dogs?attribute=weight&order=desc&pageNumber=4&pageSize=7"
````

Adding new dof example:
````
curl -X POST https://localhost:7151/dog -H "Content-Type: application/json" -d "{\"name\": \"Doggy2\", \"color\": \"red\", \"tail_length\": 173, \"weight\": 33}"
````

You also allowed to use Postman to test API.
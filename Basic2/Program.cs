//We need to add new namespace
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//I want to create a list that contains Persons instance
List<Person> people = new List<Person>() 
{
    new Person (1, "Nasir", 33),
    new Person (2, "Joe", 25),
    new Person (3, "Jane", 19)
};

app.MapGet("/", () => "Hello World!");

//Get all people
app.MapGet("/people", () => 
{
    return Results.Ok(people);
});

//Add a new POST
app.MapPost("/addperson", ([FromForm] string Name, [FromForm] int Age) => 
{
    if (string.IsNullOrEmpty(Name) || Age == 0)
    {
        return Results.BadRequest("Name or age cannot be empty or zero");
    }

    //TERNARY OPERATION - short version of if-else
    int lastIndex = people.Any() ? people.Max(p => p.Id)+1 : 1;

    //Finally I can create my new person
    Person newPerson = new Person(lastIndex, Name, Age);

    //Add it to the list
    people.Add(newPerson);
    return Results.Ok($"New person with name {newPerson.name} added.");


}).DisableAntiforgery();

app.Run();

record Person(int Id, string name, int age);

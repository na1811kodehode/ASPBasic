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

//Find person
app.MapPost("/findperson", ([FromForm] int Id) => 
{
    var existPerson = people.FirstOrDefault(person => person.Id == Id); //boolean result - basically if 'this' person ID is equal to my input ID...

    //Let us check
    if (existPerson == null)
    {
        return Results.NotFound(new {Message = $"Person with {Id} does not exist."});
    }
    //If person found then return
    return Results.Json(new {Id = existPerson.Id, Name = existPerson.name, Age = existPerson.age});

}).DisableAntiforgery();


//Update person
app.MapPut("/updateperson", ([FromForm] int Id, [FromForm] string updatedName, [FromForm] int updatedAge) =>
{
    var existPerson = people.FirstOrDefault(person => person.Id == Id);

    if (existPerson == null)
    {
        return Results.NotFound(new {Message = $"Person with {Id} does not exist."});
    }
    //Step 1: Create a new person since we  use record :(
    var updatedPerson = existPerson with {name = updatedName, age = updatedAge};

    //Step 2: Get index
    var index = people.FindIndex (p => p.Id == Id);

    //And replace
    people[index] = updatedPerson;

    return Results.Ok($"Person with {Id} is updated!");
});

//Delete person

app.Run();

record Person(int Id, string name, int age);

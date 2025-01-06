var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var random = new Random();

//Let us create a string array
string[] names = {"Alice", "Bob", "Charlie", "Diana", "Ethan"};

app.MapGet("/", () => "Hello World!");

//GET, POST, PUT, DELETE

//This is text return
app.MapGet("/gettext", ()=> 
{
    string myName = "Nasir";

    return Results.Text($"My name is {myName}");

});


//Create a JSON return
app.MapGet("/getjson", () => 
{
    string myName = "Nasir";
    int myAge = 33;
    string myCity = "Oslo";

    return Results.Json(new {Name = myName, Age = myAge, City = myCity});

});

//Create another JSON example
app.MapGet("mypet", () =>
{
    string species = "Snake";
    int age = 5;
    double length = 9.34;
    string color = "Brown";
    string type = "Anaconda";

    return Results.Json(new {Species = species, Age = age, Length = length, Color = color, Type = type});

});


app.MapGet("/people", () => 
{
    return Results.Ok(names);

});

//Select 3rd person (index2)
app.MapGet("selectperson", () =>
{
    return Results.Text(names[2]);
    
});


app.MapGet("getselectedperson/{indexNumber}", (int indexNumber) =>
{
    int indexSize = names.Length-1;

    if (indexNumber > indexSize || indexNumber < 0) {
        return Results.BadRequest($"Max index size is {indexSize}");
    }
    else {
        string selectedName = names[indexNumber];
        return Results.Ok($"You have selected person {selectedName}");
    }
});

//Now we also need to remove the error when the user input a string or char inside parameter
app.MapGet("/geterrorhandling/{indexNumberInput}", (string indexNumberInput) =>
{
    //Try to parse to create an indexNumber
    if (!int.TryParse(indexNumberInput, out int indexNumber))
    {
        return Results.BadRequest($"Enter an integer number");
    }

    //We get index size
    int indexSize = names.Length;

    //Check
    if (indexSize < 0 || indexNumber >= indexSize)
    {
        return Results.BadRequest($"Index size is {indexSize}");
    }

    Console.WriteLine(indexNumberInput);

    string selectedName = names[indexNumber];
    return Results.Ok($"You have selected person {selectedName}");
}


);

//Pick a random person from array 
app.MapGet("/randomperson", () =>
{
    int randomIndex = random.Next(names.Length);
    string randomPerson = names[randomIndex];

    return Results.Text(randomPerson);

});

/* ------- POST ----------- */

//Search a name inside a string array
app.MapGet("/findperson/{personName}", (string personName) =>
{
    //Search 
    if (!names.Contains(personName)) {
        //Person not found
        return Results.BadRequest($"Person with name {personName} does not exist!");
    }
    //Person found
    return Results.Ok($"Person with name {personName} exists.");

});

app.MapGet("/calculate/{operatorType}/{number1}/{number2}", (string operatorType, double number1, double number2) => 
{
    double result;
    switch (operatorType)
    {
        case "addition": //addition
        result = number1 + number2;
        break;

        case "subtraction": //subtraction
        result = number1 - number2;
        break;

        case "division": //division
        if (number2 == 0) 
            return Results.BadRequest("Number 2 cannot be zero");
        result = number1 / number2;
        break;

        case "multiplication": //multiplication
        result = number1 * number2;
        break;

        default:
        return Results.BadRequest("Invalid input");
    }

    return Results.Ok($"{number1} {operatorType} {number2} = {result}");

    //Dictionary <string char>


});

app.Run();

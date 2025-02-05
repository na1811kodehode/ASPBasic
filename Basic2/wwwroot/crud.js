//Client side scripts
function getAllPeople() {
    console.log("It is to get all people script");
    //We will use fetch method to get data from the backend
    
    fetch("http://localhost:5035/people", {
        method: "GET",
    })
    .then((response) => {
        if (!response.ok) {
            throw new Error("Failed fetch from backend")
        }
        return response.json()
    })
    .then((data) => {
        //To see if we can fetch data from backend
        console.log(data);
        //Our logic will come here
        //We insert data to our table
        //Grab table body
        const peopleList = document.getElementById("peopleList");
        //Whenever we create a new person, we need a new (clean) table
        peopleList.innerHTML = "";

        data.forEach((person) => {
            const row = document.createElement("tr");
            //Create cell
            const idCell = document.createElement("td");
            idCell.textContent = person.id;
            row.appendChild(idCell);

            //Person name
            const nameCell = document.createElement("td");
            nameCell.textContent = person.name;
            row.appendChild(nameCell);

            //Person age
            const ageCell = document.createElement("td");
            ageCell.textContent = person.age;
            row.appendChild(ageCell);

            //Add it to tbody
            peopleList.appendChild(row);
        });

    })
    .catch((error) => {
        console.error("error", error);
    })
} //End of getAllPeople

function addNewPerson() {
    console.log("It is to add a new person script");

    let name = document.querySelector("#name").value;
    let age = parseInt(document.querySelector("#age").value);

    //Debug if I could get name and age
    console.log(name, age);

    //We get data from form
    //We create a virtual form
    const formData = new FormData();
    formData.append("Name", name);
    formData.append("Age", age);

    //Fetch
    fetch("http://localhost:5035/addperson", {
        method: "POST",
        body: formData,
    })
    .then((response) => {
        if (!response.ok) {
            throw new Error("Error sending data to server");
        }
        return response.json();
    })
    .then ((data) => {
        console.log(data);
        //My logic
        //Update table
        getAllPeople();
        //Reset form
        document.querySelector("#name").value = "";
        document.querySelector("#age").value = "";
    })
    .catch((error) => {
        console.error("error", error);
    });
}

function findPerson() {
    console.log("It is to find a person script");
    let Id = parseInt(document.getElementById("findId").value);

    if (isNaN(Id)) {
        alert("Enter an integer");
        return;
    }
    const formData = new FormData();
    formData.append("Id", Id);

    //Fetch
    fetch("http://localhost:5035/findperson", {
        method: "POST",
        body: formData,
    })
    .then((response) => {
        if (!response.ok) {
            throw new Error("Error failed fetch data from server")
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);
        //My logic
        alert(`Person found: ${data.name}, ${data.age}`);
    })
    .catch((error) => {
        console.error("error", error);
    });
}

function updatePerson() {
    console.log("It is to update a person script");

    let id = parseInt(document.querySelector("#updateId").value);
    let name = document.querySelector("#updateName").value;
    let age = parseInt(document.querySelector("#updateAge").value);

    if (id == "") {
        alert('"ID" cannot be empty.');
        return;
    }
    else if (name == "") {
        alert('"Name" cannot be empty.');
        return;
    }
    else if (age == "") {
        alert('"Age" cannot be empty.');
        return;
    }

    //Test if the input works
    console.log(id, name, age);

    //We get data from virtual form
    const formData = new FormData();
    formData.append("Id", id);
    formData.append("updatedName", name);
    formData.append("updatedAge", age);

    //Fetch
    fetch("http://localhost:5035/updateperson", {
        method: "PUT",
        body: formData,
    })
    .then((response) => {
        if (!response.ok) {
            throw new Error("Failed to fetch data from server")
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);

        //Update table
        getAllPeople();

        //Reset form
        document.querySelector("#updateId").value = "";
        document.querySelector("#updateName").value = "";
        document.querySelector("#updateAge").value = "";
    })
    .catch((error) => {
        console.error("error", error);
    });
}

function deletePerson() {
    console.log("It is to delete a person script");

    let id = parseInt(document.querySelector("#deleteId").value);

    //Test if the input works
    console.log(id);

    const formData = new FormData();
    formData.append("Id", id);
    var idValue = formData.get("Id");

    fetch("http://localhost:5035/deleteperson", {
        method: "DELETE",
        body: formData,
    })
    .then((response) => {
        if (!response.ok) {
            throw new Error("Failed to fetch data from server")
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);

        //Update table
        getAllPeople();
        alert(`User with ID ${idValue} successfully deleted.`);
    })
    .catch((error) => {
        console.error("error", error);
    });
}
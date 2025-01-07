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
    })
    .catch((error) => {
        console.error("error", error);
    })
} //End of getAllPeople

function addNewPerson() {
    console.log("It is to add a new person script");
}

function findPerson() {
    console.log("It is to find a person script");
}

function updatePerson() {
    console.log("It is to update a person script");
}

function deletePerson() {
    console.log("It is to delete a person script");
}
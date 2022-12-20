let tracks = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:63408/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TrackCreated", (user, message) => {
        getdata();
    });

    connection.on("TrackDeleted", (user, message) => {
        getdata();
    });

    connection.on("TrackUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:63408/track')
        .then(x => x.json())
        .then(y => {
            tracks = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    tracks.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.trackId +
        "</td><td>" + t.namePlace +
        "</td><td>" + t.length +
        "</td><td>" + `<button type="button" onclick="Delete(${t.trackId})">Delete</button>` + `<button type="button" onclick="Update(${t.trackId})">Update</button>`
            + "</td></tr>";
    });
}

function Update(id) {
    let name = document.getElementById('trackname').value;
    let length = document.getElementById('length').value;

    fetch('http://localhost:63408/track/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { TrackId: id, NamePlace: name, Length: length })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function Delete(id) {
    fetch('http://localhost:63408/track/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ TrackId: id })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function Create() {
    let name = document.getElementById('trackname').value;
    let length = document.getElementById('length').value;
    fetch('http://localhost:63408/track/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { NamePlace: name, Length: length })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function ChangeToIndex() {
    location.replace("index.html")
}
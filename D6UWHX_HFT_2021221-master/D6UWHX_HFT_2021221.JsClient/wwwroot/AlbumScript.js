let albums = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:63408/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("AlbumCreated", (user, message) => {
        getdata();
    });

    connection.on("AlbumDeleted", (user, message) => {
        getdata();
    });

    connection.on("AlbumUpdated", (user, message) => {
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
    await fetch('http://localhost:63408/album/')
        .then(x => x.json())
        .then(y => {
            albums = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    albums.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.albumID + "</td><td>"
            + t.title + "</td>" + "<td>" +
            `<button type="button" onclick="Delete(${t.albumID})">Delete</button>` + `<button type="button" onclick="Update(${t.albumID})">Update</button>`
            + "</td></tr>";
    });
}

function Update(id) {
    let title = document.getElementById('Album_Title').value;
    fetch('http://localhost:63408/album/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { AlbumID: id, Title: title})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function Delete(id) {
    fetch('http://localhost:63408/album/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ AlbumID: id })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function Create() {
    let title = document.getElementById('Album_Title').value;
    fetch('http://localhost:63408/album/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Title: title })
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
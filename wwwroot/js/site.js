const uri = '/Status';
// FETCH REQUESTS
function getStatuses(timeframe) {
    fetch(uri + `/${timeframe}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            displayStatuses(data);
        })
        .catch(error => console.log(error));
}

function postStatus() {
    const statusType = document.getElementById('statusTypes').value;
    const timestamp = new Date(document.getElementById('Timestamp').value);
    console.log(typeof timestamp);
    var minutesLogged = document.getElementById('MinutesLogged').value;
    minutesLogged == '' ? minutesLogged = 0 : null;
    var deviationFromTarget = document.getElementById('DeviationFromTarget').value;
    deviationFromTarget == '' ? deviationFromTarget = 0 : null;

    const Status = {
        
            'StatusType': statusType,
            'Timestamp': timestamp.toISOString(),
            'MinutesLogged': minutesLogged,
            'DeviationFromTarget': deviationFromTarget
    }
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Status)
    })
        .then(res => res.json())
        .then(JSONres => {
            console.log(JSONres);
            getStatuses('Daily');
        })
        .catch(error => console.error('Unable to add item', error));
    return;
}

// UTILITY FUNCTIONS
function displayStatuses(data) {
    console.log(data);
    let newBreakBody = document.createElement("tbody");
    newBreakBody.id = 'BreakBody';
    let newLunchBody = document.createElement("tbody");
    newLunchBody.id = 'LunchBody';
    let newMeetingBody = document.createElement("tbody");
    newMeetingBody.id = 'MeetingBody';

    const BreakBody = document.getElementById("BreakBody");
    const LunchBody = document.getElementById("LunchBody");
    const MeetingBody = document.getElementById("MeetingBody");

    let BodiesArray = [[BreakBody, newBreakBody], [LunchBody, newLunchBody], [MeetingBody, newMeetingBody]];

    data.forEach(status => {
        switch (status.statusType) {
            case 'Break':
                addTableEntry(newBreakBody, status);
            case 'Lunch':
                addTableEntry(newLunchBody, status);
            case 'Meeting':
                addTableEntry(newMeetingBody, status);
        }
    });

    BodiesArray.forEach(tup => {
        tup[0].parentNode.replaceChild(tup[1], tup[0]);
    });
}

function addTableEntry(tbody, status) {
    tbody.insertRow(-1);
    let count = 0;
    let c1 = tbody.lastChild.insertCell(0);
    c1.hidden = true;
    let c2 = tbody.lastChild.insertCell(1);
    let c3 = tbody.lastChild.insertCell(2);
    let c4 = tbody.lastChild.insertCell(3);

    c1.innerText = status.id;
    c2.innerText = status.timestamp;
    c3.innerText = status.minutesLogged;
    c4.innerText = status.deviationFromTarget;
}

function replaceTableEntries(tbody, newTbody) {
    tbody.parentNode.replaceChild(newTbody, tbody);
}

function addStatus(statusType) {
    let minutesLogged;
    let deviationFromTarget;
    let intervalID;

    switch (statusType) {
        case ('Break'):
            intervalId = setInterval();
    }
}

function startTimer(statusType) {
    let date = new Date(0);
    switch (statusType) {
        case 'Break':
            date.setHours(0, 30, 0, 0);
            break;
        case 'Lunch':
            date.setHours(1, 0, 0, 0);
            break;
        case 'Meeting':
            date.setHours(2, 0, 0, 0);
            break;
    }
    
    let time = date.toTimeString().slice(0, 8);
    let timer = document.getElementById('timer');
    let intervalIDDiv = document.getElementById('intervalID');
    timer.innerHTML = time;
    intervalID = setInterval(advanceTimer, 1000, statusType);
    intervalIDDiv.innerHTML = intervalID;
}
function advanceTimer(statusType) {
    let timer = document.getElementById('timer');
    let currentTime = timer.innerHTML;
    let hours = parseInt(currentTime.slice(0, 2));
    let minutes = parseInt(currentTime.slice(3, 5));
    let seconds = parseInt(currentTime.slice(6));
    // const currentTimeMilliseconds = ((hours * 60 + minutes) * 60 + seconds) * 1000;
    let newTime = new Date(0);
    newTime.setHours(hours, minutes, seconds - 1, 0);
    timer.innerHTML = newTime.toTimeString().slice(0, 8);
}
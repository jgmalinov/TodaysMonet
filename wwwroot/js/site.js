const uri = '/Status';
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


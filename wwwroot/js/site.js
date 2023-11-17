const uri = '/Status';
function getStatuses(timeframe) {
    fetch(uri + `/${timeframe}`)
        .then(response => response.json())
        .then(data => DisplayStatuses(data))
        .catch(error => console.log(error));
}

function postStatus() {
    const statusType = document.getElementById('statusTypes').value;
    const timestamp = document.getElementById('Timestamp').value;
    const minutesLogged = document.getElementById('MinutesLogged').value;
    const deviationFromTarget = document.getElementById('DeviationFromTarget').value;

    const status = {
        'StatusType': statusType,
        'Timestamp': timestamp,
        'MinutesLogged': minutesLogged,
        'DeviationFromTarget': deviationFromTarget
    }
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(status)
    })
        .then(res => res.json())
        .then(JSONres => getStatuses())
        .catch(error => console.error('Unable to add item', error));
    return;
}

function displayStatuses(data) {
    console.log(data);
    let newBreakBody = document.createElement("tbody");
    let newLunchBody = document.createElement("tbody");
    let newMeetingBody = document.createElement("tbody");

    const BreakBody = document.GetElementById("BreakBody");
    const LunchBody = document.GetElementById("LunchBody");
    const MeetingBody = document.GetElementById("MeetingBody");

    let BodiesArray = [(BreakBody, newBreakBody), (LunchBody, newLunchBody), (MeetingBody, newMeetingBody)];

    for (let status in data) {
        switch (status.StatusType) {
            case 'Break':
                addTableEntry(newBreakBody, status);
            case 'Lunch':
                addTableEntry(newLunchBody, status);
            case 'Meeting':
                addTableEntry(newMeetingBody, status);
        }
    }

    for (let tup in BodiesArray) {
        tup[0].parentNode.replaceChild(tup[1], tup[0]);
    }
}

function addTableEntry(tbody, status) {
    tbody.insertRow(-1);
    let count = 0;
    let c1 = tbody.insertCell(0);
    let c2 = tbody.insertCell(1);
    let c3 = tbody.insertCell(2);
    let c4 = tbody.insertCell(4);

    c1.innerText = status.id;
    c2.innerText = status.Timestamp;
    c3.innerText = status.MinutesLogged;
    c4.innerText = status.DeviationFromTarget;
}

function replaceTableEntries(tbody, newTbody) {
    tbody.parentNode.replaceChild(newTbody, tbody);
}


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

function postStatus(status) {
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

function submitStatus(statusType) {
    const Status = {

        'StatusType': statusType,
        'Timestamp': timestamp.toISOString(),
        'MinutesLogged': minutesLogged,
        'DeviationFromTarget': deviationFromTarget
    }
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
    c1.hidden = true;
    let c2 = tbody.lastChild.insertCell(1);
    let c3 = tbody.lastChild.insertCell(2);
    let c4 = tbody.lastChild.insertCell(3);

    c1.innerText = status.id;
    c2.innerText = status.timestamp;
    c3.innerText = status.minutesLogged;
    c4.innerText = status.deviationFromTarget;
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
    let timer = document.getElementById('timer');
    let intervalIDDiv = document.getElementById('intervalID');

    if (timer.innerHTML == "") {
        let date = new Date(0);
        switch (statusType) {
            case 'Break':
                date.setHours(0, 0, 15, 0);
                break;
            case 'Lunch':
                date.setHours(1, 0, 0, 0);
                break;
            case 'Meeting':
                date.setHours(2, 0, 0, 0);
                break;
        }

        let time = date.toTimeString().slice(0, 8);
        timer.innerHTML = time;
    }

    intervalID = setInterval(advanceTimer, 1000);
    intervalIDDiv.innerHTML = intervalID;
}
function advanceTimer() {
    let timer = document.getElementById('timer');
    let [overdue, hours, minutes, seconds] = getFormattedTime(timer);
    const currentTimeMilliseconds = convertToMilliseconds(hours, minutes, seconds);
    const shouldAddOrSubtract = overdue || currentTimeMilliseconds == 0;
    let secondsModifier, newTimeString;

    if (shouldAddOrSubtract) {
        secondsModifier = 1;
        newTimeString = '-';
    } else {
        secondsModifier = -1;
        newTimeString = '';
    }

    let newTime = new Date(0);
    newTime.setHours(hours, minutes, seconds + secondsModifier, 0);


    newTimeString += newTime.toTimeString().slice(0, 8);
    timer.innerHTML = newTimeString;
}

function pauseInterval() {
    const intervalID = document.getElementById('intervalID');
    if (intervalID.innerHTML != "") {
        clearInterval(parseInt(intervalID.innerHTML));
        intervalID.innerHTML = "";
    } else {
        startTimer();
    }
}

function getFormattedTime(timer) {
    let currentTime = timer.innerHTML;
    let hours, minutes, seconds;
    const overdue = currentTime[0] == '-';
    if (overdue) {
        hours = parseInt(currentTime.slice(1, 3));
        minutes = parseInt(currentTime.slice(4, 6));
        seconds = parseInt(currentTime.slice(7));
        timer.style.color = 'red';

    } else {
        hours = parseInt(currentTime.slice(0, 2));
        minutes = parseInt(currentTime.slice(3, 5));
        seconds = parseInt(currentTime.slice(6));
        timer.style.color = 'black';
    }

    return [overdue, hours, minutes, seconds];
}

function convertToMilliseconds(hours, minutes, seconds) {
    return ((hours * 60 + minutes) * 60 + seconds) * 1000;
}
const uri = '/Status';
function getStatuses(timeframe) {
    fetch(uri + `/${timeframe}`)
        .then(response => response.json())
        .then(data => DisplayStatuses(data))
        .catch(error => console.log(error));
}

function displayStatuses(data) {
    console.log(data);
    const BreakTable = document.GetElementById("BreakTable");
    const LunchTable = document.GetElementById("LunchTable");
    const MeetingTable = document.GetElementById("MeetingTable");

    for (let status in data) {
        switch (status.StatusType) {
            case 'Break':
                addTableEntry(BreakTable, status);
            case 'Lunch':
                addTableEntry(LunchTable, status);
            case 'Meeting':
                addTableEntry(MeetingTable, status);
        }
    }
}

function addTableEntry(table, status) {
    table.insertRow(-1);
    let count = 0;
    let c1 = table.insertCell(0);
    let c2 = table.insertCell(1);
    let c3 = table.insertCell(2);
    let c4 = table.insertCell(4);

    c1.innerText = status.id;
    c2.innerText = status.Timestamp;
    c3.innerText = status.MinutesLogged;
    c4.innerText = status.DeviationFromTarget;
}


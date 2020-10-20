var isLogOutInProgess = false;
function CheckSession(message) {
    $.ajax
    ({
        url: "/" + kendo.culture().name + '/Generic/CheckSession',
        type: 'Get',
        datatype: 'application/json',
        contentType: 'application/json',
        success: function (result) {
            if (result.SessionActive == false) {
                logoutOnSessionExpire(message);
            }
        },
        error: function (e) {
        },
    });
}

function setUpTimeoutErrorCode(message) {
    $.ajaxSetup({
        statusCode: {
            306: function () {
                logoutOnSessionExpire(message);
            }
        }
    });
}

$(window).on('storage', message_receive);

// use local storage for messaging. Set message in local storage and clear it right away
// This is a safe way how to communicate with other tabs while not leaving any traces
function message_broadcast(message) {
    localStorage.setItem('Damacon_Mutui_Logout', JSON.stringify(message));
    localStorage.removeItem('Damacon_Mutui_Logout');
}

// receive message
function message_receive(ev) {
    if (ev.originalEvent.key != 'Damacon_Mutui_Logout') return; // ignore other keys
    var message = JSON.parse(ev.originalEvent.newValue);
    if (!message) return; // ignore empty msg or msg reset

    // here you act on messages.
    // you can send objects like { 'command': 'doit', 'data': 'abcd' }
    if (message.command == 'Logout') {
        clearInterval(window.checkSessionIntervalId);
        window.location = "/" + kendo.culture().name + '/Account/Login';
    }
}

function logoutOnSessionExpire(message)
{
    if(isLogOutInProgess == false){
        isLogOutInProgess = true;
        clearInterval(window.checkSessionIntervalId);
        if (!alert(message)) {
            message_broadcast({ command: "Logout" });
            window.location = "/" + kendo.culture().name + '/Account/Login';
        }
    }
}

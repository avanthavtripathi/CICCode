

function pop(windownames) {
    var windowname = windownames.split('@@@')[0];
    document.getElementById("ctl00_MainConHolder_HdnActivityId").value = windownames.split('@@@')[1];
    document.getElementById("ctl00_MainConHolder_LblActivity").innerHTML = windownames.split('@@@')[2];
    document.getElementById("ctl00_MainConHolder_HdnComplaint_No").value = windownames.split('@@@')[3];
    toggle(windowname);
}

function toggle(div_id) 
{
    var el = document.getElementById(div_id);
    if (el.style.display == 'none') { el.style.display = 'block'; }
    else { el.style.display = 'none'; }
}

function hide(div) 
{
    document.getElementById(div).style.display = 'none';
    return false;
}

document.onkeydown = function(evt) 
{
    evt = evt || window.event;
    if (evt.keyCode == 27) {
        hide('popUpDiv');
    }
};
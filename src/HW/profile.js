function setDisp(id, disp) {
    document.getElementById(id).style.display = (disp ? '' : 'none');
}
function chkEmail() {
    document.getElementById('Staremail').src = 'img/star' + (eval('document.forms[0].email.value.length > 5 && document.forms[0].email.value.indexOf(\'@\') > 0 && document.forms[0].email.value.lastIndexOf(\'.\') > document.forms[0].email.value.indexOf(\'@\')+1') ? 'OK' : '') + '.gif';
}
function chkChk(id) {
    document.getElementById('Star' + id).src = 'img/star' + (eval('document.forms[0].' + id + '.checked') ? 'OK' : '') + '.gif';
}
function chkDdl(id) {
    document.getElementById('Star' + id).src = 'img/star' + (eval('document.forms[0].' + id + '[document.forms[0].' + id + '.selectedIndex].value != \'\'') ? 'OK' : '') + '.gif';
}
function getDdl(id) {
    if (document.getElementById(id.substr(1).replace('D', '').replace('M', '').replace('Y', '')).style.display == 'none') {
        return;
    }
    else {
        return eval('document.forms[0].' + id + '[document.forms[0].' + id + '.selectedIndex].value');
    }
}
function getRad(id) {
    if (document.getElementById(id.substr(1)).style.display != 'none') {
        for (i = 0; i < eval('document.forms[0].' + id + '.length'); i++)
            if (eval('document.forms[0].' + id + '[' + i + '].checked'))
                return eval('document.forms[0].' + id + '[' + i + '].value');
    }
    return;
}
function isDate(id) {
    try {
        var d = parseInt(getDdl(id + 'D'));
        var m = parseInt(getDdl(id + 'M'));
        var y = parseInt(getDdl(id + 'Y'));
        if (d == 0 || m == 0 || y == 0) {
            return false;
        }
        else {
            m = m - 1;
            var dt = new Date(y, m, d);
            return d == dt.getDate() && m == dt.getMonth() && y == dt.getFullYear() && dt < (new Date());
        }
    }
    catch (ex) {
        return false;
    }
}
function chkDate(id) {
    document.getElementById('Star' + id).src = 'img/star' + (isDate(id) ? 'OK' : '') + '.gif';
}
function chkTxt(id, len) {
    document.getElementById('Star' + id).src = 'img/star' + (eval('document.forms[0].' + id + '.value.length >= ' + len) ? 'OK' : '') + '.gif';
}
function chkRad(id) {
    checked = false;
    for (i = 0; i < eval('document.forms[0].' + id + '.length'); i++)
        if (eval('document.forms[0].' + id + '[' + i + '].checked'))
            checked = true;
    document.getElementById('Star' + id).src = 'img/star' + (checked ? 'OK' : '') + '.gif';
}
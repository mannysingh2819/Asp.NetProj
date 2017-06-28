
function changeColor(id) {
 document.getElementById(id).style.color = "##FFFFFF"; // forecolor
 document.getElementById(id).style.backgroundColor = "#E9967A"; // backcolor
}

function copytoclipbrd(elementid) {
     clipboardData.setData('Text', document.getElementById(elementid).value);
}

function textCounter(elementid, counterID, maxLen) {
    cnt = document.getElementById(counterID);
    if (elementid.value.length > maxLen) {
        elementid.value = elementid.value.substring(0, maxLen);
    }
    cnt.innerHTML = maxLen - elementid.value.length +' remaining';
}
function chkblanktxt() {
    var txtval = document.getElementById('txtnotes');
    if (txtval.valueOf == '')
    {
        alert('Enter some Notes');
        return false;
    }
    
        return true;
   }


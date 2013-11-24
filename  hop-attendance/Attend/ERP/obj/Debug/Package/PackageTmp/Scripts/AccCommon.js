/*Common*/
function showPopup(containerId, coverBackground) {
    $('.popup').css("display", "none");
    $('#' + containerId).css("display", "block");
    $('#' + containerId).center();


    if ($('#' + containerId).offset().top > 200) {
        $('#' + containerId).css("top", 50);
    }
}

function closePopup(containerId) {
    $('#Popup').hide();
}

var webRoot = '/ACC/';

/*Common*/


function showBudgetPopup(ID) {
    if (ID == undefined) {
        ID = '0';
    }
    $.ajaxSetup({ cache: false });
    $.get(webRoot + 'Budget/BudgetSetupPopup', { Id: ID }, function (data) {
        $('#PopupContainer').html(data);
        showPopup('Popup', false);
    });
//    alert(ID);
}
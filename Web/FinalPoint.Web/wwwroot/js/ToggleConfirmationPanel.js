
function toggleConfirmation(mainBtnId, confirmPanelId, parcelId) {
    if (!parcelId) {
        parcelId = '';
    }

    if (!$("#" + mainBtnId + parcelId).is(":hidden")) {
        console.log("not hidden");

        $("#" + mainBtnId + parcelId).hide();
        $("#" + confirmPanelId + parcelId).show();
    }
    else {
        $("#" + mainBtnId + parcelId).show();
        $("#" + confirmPanelId + parcelId).hide();
    }
}
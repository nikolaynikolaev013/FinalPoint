
function toggleConfirmation(parcelId) {
    if (!parcelId) {
        parcelId = '';
    }
    if (!$("#hideBtn" + parcelId).is(":hidden")) {

        $("#hideBtn" + parcelId).hide();
        $("#confirmPanel" + parcelId).show();
    }
    else {
        $("#hideBtn" + parcelId).show();
        $("#confirmPanel" + parcelId).hide();
    }
}
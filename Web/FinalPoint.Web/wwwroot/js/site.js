$("#confirmDeleteDiv").hide();

function togglePanel(isClicked) {
    if (isClicked) {
        $("#delBtn").hide();
        $("#confirmDeleteDiv").show();
    } else {
        $("#delBtn").show();
        $("#confirmDeleteDiv").hide();
    }
}
let protocolId = $("#parcelsTable").attr("data-protocolId");

function closeProtocol(protocolId) {
    $.ajax({
        method: "Put",
        url: "api/Protocol/" + protocolId,
    success: function (res) {
        if (res) {
            $("#protocolMainResponseMessage #message").text("Протокол №" + protocolId + " беше приключен успешно!");
            $("#protocolMainResponseMessage").addClass("alert-success");
            $("#protocolMainResponseMessage").removeClass("alert-warning");
            $("#parcelInsert").hide();
            $("#hideBtn").hide();
            $("#confirmPanel").hide();
            $("#responseMessage")?.hide();
            $(".importIdIcon").hide();
        }
    }
});
        }

$("#submitBtn").click(function () {
    let parcelId = $("#parcelIdInput").val();
    let isCheck = $('input[type=radio][name=addRemoveRadioBtns]:checked').attr('id') == "check" ? true : false;

    $.ajax({
        method: "GET",
        url: "/CheckedParcelResult/" + parcelId + "/" + protocolId + "/" + isCheck,
            success: function (res) {
                $("#responseMessage").html(res);

                $.ajax({
                    method: "GET",
                    url: "/ReloadParcelsTable/" + protocolId,
                success: function (res) {
                    $("#parcelsTable").html(res);


                    $.ajax({
                        method: "GET",
                        url: "api/Protocol/" + protocolId,
                    success: function (res) {
                        $("#numberOfCheckedParcels").html(res);
                        let inputFieldEl = document.querySelector("#parcelIdInput");
                        inputFieldEl.value = "";
                        inputFieldEl.focus();
                    }
                });
                        }
                    });
                }
            });
        });

$('input[type=radio][name=addRemoveRadioBtns]').change(function () {
    if (this.id == 'remove') {
        $("#submitBtn").text("Премахване");
    }
    else if (this.id == 'check') {
        $("#submitBtn").text("Добавяне");
    }
});

$(document).keypress(function (event) {
    let keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        $("#submitBtn").click();
    }
});


//initial load
$("#confirmPanel").hide();

$.ajax({
    method: "GET",
    url: "api/Protocol/" + protocolId,
success: function (res) {
    $("#numberOfCheckedParcels").html(res);
}
        });
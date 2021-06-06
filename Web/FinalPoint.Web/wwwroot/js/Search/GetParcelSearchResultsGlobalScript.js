﻿$("#search").click(function () {
    GetParcelResults()
});


function GetParcelResults() {
    let parcelId = CheckNullUndefined($("#parcelId").val()) ? $("#parcelId").val() : 0;
    let firstName = CheckNullUndefined($("#recipentFirstName").val()) ? $("#recipentFirstName").val() : 0;
    let lastName = CheckNullUndefined($("#recipentLastName").val()) ? $("#recipentLastName").val() : 0;
    let phoneNumber = CheckNullUndefined($("#recipentPhoneNumber").val()) ? $("#recipentPhoneNumber").val() : 0;

    $.ajax({
        method: "GET",
        url: "/CheckParcel/" + parcelId + "/" + firstName + "/" + lastName + "/" + phoneNumber + "/false",
        success: function (res) {
            $("#parcelSearchResult").html(res);

            $(".confirmDelete").hide();
        }
    });
    function CheckNullUndefined(value) {
        return !(typeof value == 'string' && !value.trim() || typeof value == 'undefined' || value === null);
    }
}
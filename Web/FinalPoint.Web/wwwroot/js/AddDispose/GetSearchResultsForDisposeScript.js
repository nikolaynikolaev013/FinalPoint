$("#search").click(function () {
    GetParcelResults()
});


function GetParcelResults() {
    let parcelId = CheckNullUndefined($("#parcelId").val()) ? $("#parcelId").val() : 0;
    let firstName = CheckNullUndefined($("#recipentFirstName").val()) ? $("#recipentFirstName").val() : 0;
    let lastName = CheckNullUndefined($("#recipentLastName").val()) ? $("#recipentLastName").val() : 0;
    let phoneNumber = CheckNullUndefined($("#recipentPhoneNumber").val()) ? $("#recipentPhoneNumber").val() : 0;

    $.ajax({
        method: "GET",
        url: "/CheckParcel/" + parcelId + "/" + firstName + "/" + lastName + "/" + phoneNumber  + "/true",
        success: function (res) {
            $("#parcelSearchResult").html(res);

            let regex = /[0-9]+/;
            var items = jQuery('div[id^=appendBtn_]');

            Array.prototype.forEach.call(items, child => {
                let btn = document.querySelector("#disposeBtn_" + regex.exec(child.id)[0]);
                child.appendChild(btn);
            });

            $(".confirmDelete").hide();
        }
    });
    function CheckNullUndefined(value) {
        return !(typeof value == 'string' && !value.trim() || typeof value == 'undefined' || value === null);
    }
}
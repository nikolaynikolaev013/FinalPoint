function disposeParcel(parcelId) {
            $.ajax({
                method: "DELETE",
                url: "/ParcelApi/" + parcelId,
                success: function (res) {
                    $("#parcelCard_" + parcelId).addClass("animate__backOutRight").delay(500).hide(0);
                    setTimeout(GetParcelResults, 500);
                }
            });
        }

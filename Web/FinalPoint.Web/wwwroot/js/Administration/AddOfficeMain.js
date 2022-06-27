
let isAddingNewCity = false;

function addNewCity() {
    isAddingNewCity = !isAddingNewCity;

    if (isAddingNewCity) {
        $("#currCity").hide();
        $("#addCity").show();
    } else {
        $("#currCity").show();
        $("#addCity").hide();
    }
}

$("#OfficeType").change(function () {
    var isOffice = $(this).children("option:selected").val();
    //Enum 10 is office
    if (isOffice == 10) {
        $("#responsibleSortingCenter").show();
    } else {
        $("#responsibleSortingCenter").hide();
    }
});



//Initial loader
        var isOffice = $("#OfficeType").children("option:selected").val();
//Enum 10 is office
if (isOffice == 10) {
    $("#responsibleSortingCenter").show();
} else {
    $("#responsibleSortingCenter").hide();
}

$("#addCity").hide();
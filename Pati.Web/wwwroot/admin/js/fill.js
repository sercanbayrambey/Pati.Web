$(document).ready(function () {

    if ($(".pet-current-species").length && $(".pet-current-genus").length) {
        $(".genus-select").val($(".pet-current-genus").val());
        $(".species-select").val($(".pet-current-species").val());
    }

    if ($(".current-city").length && $(".current-county").length) {
        console.log("??");
        $(".city-select").val($(".current-city").val());
        $(".county-select").val($(".county-city").val());
    }


})
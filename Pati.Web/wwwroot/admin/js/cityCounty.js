$(document).ready(function () {

    if ($("form select.city-select").length && $("form select.county-select").length) {
        GetCities();
    }



});

let isDefaultValuesFilled = false;

function GetCities() {
    console.log("?");
    $.post("/CityCounty/GetCities")
        .done(function (response) {
            FillTheCitySelect(response);
        });
}

function FillTheCitySelect(response) {
    $.each(response, function (index, item) {
        $('form select.city-select').append(`<option value="${item.cityId}"> 
                                       ${item.cityName} 
                                  </option>`);
      
    });
    if ($(".current-city").length && $(".current-county").length) {
        $(".city-select").val($(".current-city").val());
        GetCounties($(".current-city").val());
    }
}

$('form select.city-select').change(function () {
    var selectedCityId = $(this).find("option:selected").val();
    if (selectedCityId <= 0 || selectedCityId > 81)
        return;

    GetCounties(selectedCityId);

})

function GetCounties(cityId) {
    $.post("/CityCounty/GetCounties", { cityId: cityId })
        .done(function (response) {
            FillTheCountySelect(response);
        });
}   


function FillTheCountySelect(response) {
    $('form select.county-select').find('option').remove().end().append('<option value="0">Seçiniz</option>');
    $.each(response, function (index, item) {
        $('form select.county-select').append(`<option value="${item.countyId}"> 
                                       ${item.countyName} 
                                  </option>`);
  
    });
    if ($(".current-city").length && $(".current-county").length && !isDefaultValuesFilled) {
        $(".county-select").val($(".current-county").val());
        isDefaultValuesFilled = true;
    }
}
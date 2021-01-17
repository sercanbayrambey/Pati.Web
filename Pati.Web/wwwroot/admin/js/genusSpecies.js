$(document).ready(function () {
    if ($("form select.genus-select").length && $("form select.species-select").length) {
        GetGenusses();
    }

});

function GetGenusses() {
    $.post("/admin/Genus/GetGenusses")
        .done(function (response) {
            FillTheGenusSelect(response);
        });
}

function FillTheGenusSelect(response) {
    $.each(response, function (index, item) {
        $('form select.genus-select').append(`<option value="${item.genusId}"> 
                                       ${item.genusName} 
                                  </option>`);
    });
}

$('form select.genus-select').change(function () {
    var selectedGenusId = parseInt($(this).find("option:selected").val());

    GetSpecies(selectedGenusId);
});

function GetSpecies(genusId) {
    $.post("/admin/Species/GetSpecies", { genusId: genusId })
        .done(function (response) {
            console.log(response);
            FillTheSpeciesSelect(response);
        });
}


function FillTheSpeciesSelect(response) {
    $('form select.species-select').find('option').remove().end().append('<option value="0">Seçiniz</option>');
    $.each(response, function (index, item) {
        $('form select.species-select').append(`<option value="${item.speciesId}"> 
                                       ${item.speciesName} 
                                  </option>`);
    });
}


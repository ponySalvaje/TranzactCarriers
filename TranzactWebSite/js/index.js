const url = 'https://localhost:44373/Carrier';

function checkIfGetPremiumButtonEnabled() {
    var shouldBeEnabled = false;

    const birthDate = $("#dateOfBirth").val();
    const age = $("#age").val();

    if (birthDate.trim().length > 0 && age.trim().length > 0) shouldBeEnabled = true;

    if (shouldBeEnabled) enableGetPremiumButton();
    else disableGetPremiumButton();
}

function enableGetPremiumButton() {
    $("#getCarriersButton").removeAttr('disabled')
}

function disableGetPremiumButton() {
    $("#getCarriersButton").attr("disabled", true);
}

function getCarriers() {

    const birthDate = $("#dateOfBirth").val();
    const state = $("#state").val();
    const age = $("#age").val();
    const plan = $("#plan").val();

    getCarriersFromAPI(birthDate, state, age, plan)
        .then(data => {
            if (data.length > 0) {
                populateCarrierEntries(data);
            } else {
                showNoCarriers();
            }
        }).catch(error => {
            showError();
        });
}

function populateAge() {
    const birthDate = $("#dateOfBirth").val();
    var ageInMilliseconds = new Date() - new Date(birthDate);
    const age = Math.floor(ageInMilliseconds / 1000 / 60 / 60 / 24 / 365);
    $("#age").val(age);
}

function populateCarrierEntries(data) {
    let element = '';
    data.forEach(function (carrier) {
        element = element + '<div class="row">' +
            '<div class="col-sm-3">' + carrier.carrier + '</div>' +
            '<div class="col-sm-3">' + parseFloat(carrier.premium).toFixed(2).toString() + '</div>' +
            '<div class="col-sm-3">' + calculateAnuallyAmount(carrier.premium) + '</div>' +
            '<div class="col-sm-3">' + calculateMonthlyAmount(carrier.premium) + '</div>' +
            '</div>';
    });
    $("#carrierEntries").html(element);
}

function recalculatePremiumsPeriod() {
    if (!$("#getCarriersButton").attr('disabled')) getCarriers();
}

function calculateAnuallyAmount(premium) {
    const period = $("#period").val();
    switch (period) {
        case 'Monthly': return (parseFloat(premium) * 12).toFixed(2).toString();
        case 'Quaterly': return (parseFloat(premium) * 4).toFixed(2).toString();
        case 'Semi-Anually': return (parseFloat(premium) * 2).toFixed(2).toString();
        case 'Anually': return (parseFloat(premium) * 1).toFixed(2).toString();
        default: break;
    }
}

function calculateMonthlyAmount(premium) {
    const period = $("#period").val();
    switch (period) {
        case 'Monthly': return (parseFloat(premium)).toFixed(2).toString();
        case 'Quaterly': return (parseFloat(premium) / 3).toFixed(2).toString();
        case 'Semi-Anually': return (parseFloat(premium) / 6).toFixed(2).toString();
        case 'Anually': return (parseFloat(premium) / 12).toFixed(2).toString();
        default: break;
    }
}

function showError() {
    $("#carrierEntries").html('<p>We could not find any results. Please review your requirements and retry</p>');
}

function showNoCarriers() {
    $("#carrierEntries").html('<p>We could not find any results. Please review your requirements and retry</p>');
}

async function getCarriersFromAPI(birthDate, state, age, plan) {
    const getCarriersURL = url + '?DateOfBirth=' + birthDate + '&State=' + state + '&Age=' + age + '&Plan=' + plan;
    const response = await fetch(getCarriersURL,
        {
            method: 'GET',
            mode: 'cors',
            redirect: 'follow',
            referrerPolicy: 'no-referrer',
        });
    let carriers = await response.json();
    return carriers;
}
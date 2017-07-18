(function () {
    'use strict';

    var passengersDropdown = document.getElementById('PassengersCount'),
        priceDisplay = document.getElementsByClassName('currency')[0],
        currencySymbol = moneyToFloat(priceDisplay.innerHTML).symbol,
        originalPrice = moneyToFloat(priceDisplay.innerHTML).value;

    passengersDropdown.addEventListener('change', function (el) {
        priceDisplay.innerHTML = currencySymbol + ' ' + parseFloat(parseFloat(originalPrice) * parseInt(passengersDropdown.value)).toFixed(3);        
    });

    function moneyToFloat(money) {
        if (typeof money !== 'string') {
            throw new Error('Money should be string');
        }

        var currencySymbol = money.substr(0, 2);
        var value = money.substr(2, money.length - 1);

        return {
            symbol: currencySymbol,
            value: value
        };
    }
}());
$(document).ready(function() {
    $('#GovernorateId').on('change', function () {
        var GovernorateId = $(this).val();
        var cityList = $('#CityId');
        cityList.empty();
        cityList.append();
        if (GovernorateId !== '') {
            $.ajax({
                url: '/deliveries/GetCities?GovernorateId=' + GovernorateId,
                success: function (cities) {
                    cityList.append($('<option></option>'))
                    $.each(cities, function (i, city) {
                        cityList.append($('<option></option>').attr('value', city.value).text(city.text));
                    });
                },
                error: function () {
                    alert('Something went wrong!');
                }
            });
        }
    });
});
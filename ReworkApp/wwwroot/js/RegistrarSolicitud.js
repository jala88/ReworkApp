document.addEventListener("DOMContentLoaded", function () {

    var tarjetaDropdown = document.getElementById("TarjetaId");


    tarjetaDropdown.addEventListener("change", function () {
        CargarPartesTarjetas(tarjetaDropdown.value);
    });

    function CargarPartesTarjetas(id_tarjeta) {

        $.ajax({
            type: "GET",
            url: '/Admin/ViewBagPartesTarjetasId',
            dataType: "json",
            data: { "id_tarjeta": id_tarjeta },
            success: function (response) {
 
                var partesDropdown = document.getElementById("ParteTarjetaId");
                partesDropdown.innerHTML = "";

                response.forEach(function (item) {
                    var option = document.createElement("option");
                    option.value = item.Value;
                    option.text = item.Text;
                    partesDropdown.add(option);
                });
            },
            error: function () {
                alert("Error al cargar las partes de la tarjeta.");
            }
        });
    }
});


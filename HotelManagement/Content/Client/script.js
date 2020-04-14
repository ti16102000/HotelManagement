$(document).ready(function () {
    //Number room
    var number = $("#selectCate option:selected").attr('data-room');
    $('#numberRoom').html(number + " room(s)");
    $('#inputNumber').attr("max", number);
    $("#selectCate").change(function () {
        var number = $(this).find("option:selected").attr('data-room');
        $('#numberRoom').html(number + " room(s)");
        $('#inputNumber').attr("max", number);
    });

    //Date in/out

    $('#dataIn').change(function () {
        var currDay = new Date();
        var startDay = new Date($('#dateIn').val());
        if (startDay < currDay) {
            $('#errorDateIn').css('display', 'block');
            $('.btnBooking').prop("disabled", true);
        } else {
            $('#errorDateIn').css('display', 'none');
            $('.btnBooking').prop("disabled", false);
        }
    })
    $('#dateOut').change(function () {
        var startDay = new Date($('#dateIn').val());
        var endDay = new Date($('#dateOut').val());
        var millisecondsPerDay = 1000 * 60 * 60 * 24;
        var millisBetween = endDay.getTime() - startDay.getTime();
        var days = millisBetween / millisecondsPerDay;

        if (days < 0) {
            $('#errorDateOut').css('display', 'block');
            $('.btnBooking').prop("disabled", true);
        } else {
            $('#errorDateOut').css('display', 'none');
            $('.btnBooking').prop("disabled", false);
            if (days == 0) {
                $('#countDate').val(1);
            } else {
                $('#countDate').val(Math.round(days));
            }
        }
    })

    //Room
    var checkNumber = parseInt($('#checkNumber').text());
    $.each($('.itemRoom'), function (index, value) {
        if (index < checkNumber) {
            $(value).addClass("active");
        }
    });
    $('.itemRoom').click(function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
        } else {
            $(this).addClass("active");
        }
    })
    $('#btnRoom').click(function () {
        var numberRoom = parseInt($('#checkNumber').text());
        var numberRoomBook = $('.active').length;
        var token = $('#TokenBooking').val();
        var IDBook = parseInt($('#IDBooking').val());
        var NameHisBook = "Check in đủ số lượng phòng đã đặt";
        var arrRoom = [];
        if (numberRoomBook > numberRoom || numberRoomBook==0) {
            alert("Error!");
        } else {
            $.each($('.active'), function (index, value) {
                arrRoom.push(parseInt($(value).attr('data-idRoom')));
            });
            $.each(arrRoom, function (index, value) {
                var IDRoom = value;
                $.ajax({
                    url: '/Booking/TakeRoom/',
                    type: 'POST',
                    dataType: 'json',
                    data: { IDBook, IDRoom },
                    success: function (data) {

                    }
                    //error: function (xhr, textStatus, errorThrown) {
                    //    console.log('Error in Operation');
                    //}
                });
            });

            window.open('http://localhost:53561/Booking/CheckIn?idBooking=' + IDBook + '&tokenBook=' + token + '&number=' + numberRoomBook, '_self', false);
        }
    })

    //Order
    $('#menuService>div').click(function () {
        $('#menuService>div').removeClass("menuActive");
        $(this).addClass("menuActive");
        var id = parseInt($(this).attr('data-idcate'));
        var token = $('#tokenOrd').val();
        $.post("/Order/AjaxService/", { idCate: id }).done(function (data) {
            var dt = jQuery.parseJSON(data);
            $('#tableService tbody').html('');
            dt.forEach(function (i) {
                var row = '<tr>' +
                    '<td>' + i.NameService + '</td>' +
                    '<td>' + i.PriceService + '</td>' +
                    '<td> <a href="/Order/OrderService?token=' + token + '&idSer=' + i.IDService + '">Order</a></td>' +
                    '</tr>';
                $('#tableService tbody').append(row);
            });
        });
    });

})
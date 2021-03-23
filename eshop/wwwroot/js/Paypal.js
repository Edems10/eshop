paypal.Buttons({
    style: {
        color: 'blue',
        shape: 'pill'
    },
    createOrder: function (data, actions) {
        //var model =@Html.Raw(Json.Encode(Model));
        //var ses = sessionStorage.getItem("TotalPrice");
        //var username = '<%= Session["TotalPrice"] %>';
        //alert(username);
        var dollar;
        var a = document.getElementById('total_price').innerText;
        a = a.substring(0, a.indexOf('.'));
        a = a.replace(",", "");
        dollar = Math.floor(parseInt(a) / 21).toString();
        
        //alert(a);
        return actions.order.create({
            purchase_units: [{
                amount: {
                    value: dollar
                }
            }]
        });
    },
    onApprove: function (data, actions) {
        return actions.order.capture().then(function (details) {
            var a = true;
            console.log(details)
            window.location.replace("https://localhost:44348/Paypal/Succes")
        })
    },
    onCancel: function (data) {
        window.location.replace("https://localhost:44348/Paypal/Reject")                         
    }

}).render('.paypal-payment-button');


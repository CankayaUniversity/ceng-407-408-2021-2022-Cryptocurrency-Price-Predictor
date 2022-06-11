<?php 

require "db_baglanti.php";

if(!isset($_SESSION['durum']))
{
    header("Location: login.php");
    exit;
}

?>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" >
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css"/>
    <script type="text/javascript" src="https://s3.tradingview.com/tv.js"></script>
    <link rel="stylesheet" href="css/my_style.css">
    <title>CRYPTOCAST</title>
</head>
<body >
    <div class="container mt-2">
        <nav class="navbar navbar-expand-lg bg-light">
            <div class="container-fluid">
                <a class="navbar-brand" href="index.php">
                    <b>CRYPTOCAST</b>
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
                        <!--
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="#">Home</a>
                        </li>
                        -->
                        <li class="nav-item">
                            <a class="btn btn-outline-danger" aria-current="page" href="islemler.php?durum=logout">
                                <i class="fas fa-sign-out"></i> Logout
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
        <div class="card mt-5">
            <div class="card-body">
                <div class="tradingview-widget-container">
                    <div id="tradingview"></div>
                </div>
            </div>
        </div>

        <div class="card mt-5">
            <div class="card-header">
                <h3>Predictions</h3>
            </div>
            <div class="card-body" id="prediction" style="font-weight: bold;font-size:20px">
            </div>
        </div>
    </div>
    <!-- JavaScript Bundle with Popper -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/js/bootstrap.bundle.min.js" ></script>
    <script>
        new TradingView.widget(
        {
            "width": 1200,
            "height": 500,
            "symbol": "COINBASE:BTCUSD",
            "interval": "D",
            "timezone": "Etc/UTC",
            "theme": "Light",
            "style": "1",
            "locale": "en",
            "toolbar_bg": "#f1f3f6",
            "enable_publishing": false,
            "allow_symbol_change": true,
            "container_id": "tradingview"
        });

    

        var requestOptions = {
            method: 'GET',
            redirect: 'follow'
        };

        fetch("http://95.70.201.54/berke/api/modelPrediction/GetModelPrediction", requestOptions)
        .then(response => response.json())
        .then(result => {
            const data = result.ReturnObject;
            console.log(data)

            if(data.Prediction == 0)
            {
                $("#prediction").html(`
                    <div class="alert alert-secondary" role="alert" style="display:flex">
                        <i class="fa-solid fa-arrows-left-right"></i>
                        <span style="margin-left:5px;">Predicted Stability</span>
                    </div>
                `);
            }
            else if (data.Prediction == 1)
            {
                $("#prediction").html(`
                    <div class="alert alert-success" role="alert" style="display:flex">
                        <i class="fa-solid fa-arrow-up"></i> 
                        <span style="margin-left:5px;">Predicted Increase</span>
                    </div>
                `);
            }
            else 
            {
                $("#prediction").html(`
                    <div class="alert alert-danger" role="alert" style="display:flex">
                        <i class="fa-solid fa-arrow-down"></i> 
                        <span style="margin-left:5px;">Predicted Decrease</span>
                    </div>
                `);
            }
        })
        .catch(error => {
            console.log('error', error);
            
            $("#prediction").html(`
                <div class="alert alert-danger" role="alert" style="display:flex">
                    <i class="fa-solid fa-xmark fa-2x"></i> 
                    <span style="margin-left:5px;">Could not be predicted</span>
                </div>
            `);

        
        });
    </script>
</body>
</html>
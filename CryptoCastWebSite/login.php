<?php 
session_start();
if(isset($_SESSION['durum']))
{
    header("Location: index.php");
    exit;
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Sign In </title>

    <!-- Font Icon -->
    <link rel="stylesheet" href="fonts/material-icon/css/material-design-iconic-font.min.css">

    <!-- Main css -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/my_style.css">
</head>
<body >

    <div class="main">

        <section class="signup">
            <div class="container">
                
                <div class="signup-content">
                
                    <form method="POST" id="signup-form" class="signup-form" action="islemler.php">
                        <h2 class="form-title">CRYPTOCAST</h2>
                        <?php if(isset($_GET['durum']) && $_GET['durum'] == "false"){ ?>
                            <div class="alert alert-danger" role="alert" style="margin-bottom:15px;text-align:center;font-weight:bold;color:red">
                                You entered wrong. Try again
                            </div>
                        <?php } ?>

                        <div class="form-group">
                            <input type="text" class="form-input" name="kullanici_adi" id="name" placeholder="Username or Email Address" required/>
                        </div>
                        <div class="form-group">
                        
                        <div class="form-group">
                            <input type="password" class="form-input" name="sifre" id="password" placeholder="Password" required/>
                            <span toggle="#password" class="zmdi zmdi-eye field-icon toggle-password"></span>
                        </div>
                        
                        <div class="text-right p-t-13 p-b-23" style="text-align:right;">
                            <span class="txt1">
                        
                            </span>
        
                            <a href="forgot-password.php"  style="color:DodgerBlue;"  class="txt2">
                                Forgot Password?
                            </a>
                        </div>
                        
                        <div class="form-group">
                            <input type="submit" name="giris" id="submit" class="form-submit" value="Log in" style="cursor:pointer"/>
                        </div>
						
					    <div class="flex-col-c p-t-170 p-b-40" style="text-align:right">
                            <span class="txt1 p-b-9">
                                Don’t have an account?
                            </span>
                            <a href="register.php" style="color:DodgerBlue;" class="txt3">
                                Sign in
                            </a>
                        </div>
                    </form>
                    
                </div>
            </div>
        </section>

    </div>

    <!-- JS -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="js/main.js"></script>
</body>
</html>
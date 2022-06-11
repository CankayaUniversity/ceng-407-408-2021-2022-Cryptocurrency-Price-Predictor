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
    <link rel="stylesheet" href="fonts/material-icon/css/material-design-iconic-font.min.css">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" >
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/my_style.css">
    <title>Sign Up</title>
</head>
<body>

    <div class="main">
        <section class="signup">
            <div class="container">
                <div class="signup-content">
                    <form method="POST" id="signup-form" class="signup-form" action="islemler.php">
                        <h2 class="form-title">Create account</h2>
                        <?php if(isset($_GET['durum']) && $_GET['durum'] == "false"){ ?>
                            <div class="alert alert-danger" role="alert" style="margin-bottom:15px;text-align:center;font-weight:bold;color:red">
                                Your registration could not be performed. Try again.
                            </div>
                        <?php } ?>

                        <?php if(isset($_GET['mesaj'])){ ?>
                            <div class="alert alert-danger" role="alert" style="margin-bottom:15px;text-align:center;font-weight:bold;color:red">
                                <?php echo $_GET['mesaj']; ?>
                            </div>
                        <?php } ?>

                        <div class="form-group">
                            <input type="text" class="form-input" name="kullanici_adi" id="name" 
                            placeholder="Username" required value="<?php echo isset($_GET['kullanici_adi']) ? $_GET['kullanici_adi']: '' ?>"/>
                        </div>
                        <div class="form-group">
                            <input type="email" class="form-input" name="email" id="email" placeholder="Email Address" required
                            value="<?php echo isset($_GET['email']) ? $_GET['email']: '' ?>"
                            />
                        </div>
                        <div class="form-group">
                            <input type="password" class="form-input" name="sifre" id="password" placeholder="Password" required/>
                            <span toggle="#password" class="zmdi zmdi-eye field-icon toggle-password"></span>
                        </div>
                        <div class="form-group">
                            <input type="password" class="form-input" name="sifre_tekrar" id="re_password" placeholder="Repeat your password" required/>
                        </div>

                        <div class="form-group">
                            <label for="bilgilendirme_durum">I want daily notification mail.</label>
                            <br>
                            <select value="evet" name="bilgilendirme_durum" id="bilgilendirme_durum" style="margin-top:7px;width:100%;height:40px;padding-left:10px;border:1px solid #ebebeb;border-radius:3px">
                                <option value="evet">Yes</option>
                                <option value="hayır">No</option>
                            </select>
                        </div>
                        
                        <div class="form-group">
                            <input type="submit" name="kayit_ol" id="submit" class="form-submit" value="Sign up" style="cursor:pointer"/>
                        </div>

                       
                    </form>

                    <div style="text-align:center">
                        <a href="login.php" style="color:DodgerBlue;" class="txt3">
                            Login 
                        </a>
                    </div>
                    
                </div>
            </div>
        </section>

    </div>

    <!-- JS -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="js/main.js"></script>
</body>
</html>
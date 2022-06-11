<?php
require_once "db_baglanti.php";

/*
echo "<pre>";
print_r($_POST);
exit;
*/

//kayıt ol 
if(isset($_POST['kayit_ol']))
{
    $kullanici_adi  = trim($_POST['kullanici_adi']);
    $email          = trim($_POST['email']);
    $sifre          = trim($_POST['sifre']);
    $sifre_tekrar   = trim($_POST['sifre_tekrar']);

    $bilgilendirme_durum = isset($_POST['bilgilendirme_durum']) ? $_POST['bilgilendirme_durum'] : 'hayir';



    if($sifre != $sifre_tekrar)
    {

        header("Location: register.php?email={$email}&kullanici_adi={$kullanici_adi}&mesaj=Passwords do not match");
        exit;
    }

    if(strlen($sifre) < 8)
    {
        header("Location: register.php?email={$email}&kullanici_adi={$kullanici_adi}&mesaj=Your password must contain at least 8 characters.");
        exit;
    }


    if (!preg_match('/[A-Z]/',$sifre)) {
        header("Location: register.php?email={$email}&kullanici_adi={$kullanici_adi}&mesaj=Your password must contain at least one capital letter.");
        exit;
    }

    if (!preg_match('/[a-z]/',$sifre)) {
        header("Location: register.php?email={$email}&kullanici_adi={$kullanici_adi}&mesaj=Your password must contain at least one lowercase letter.");
        exit;
    }

    if (!preg_match('/[0-9]/',$sifre)) {
        header("Location: register.php?email={$email}&kullanici_adi={$kullanici_adi}&mesaj=Your password must contain at least one number.");
        exit;
    }



    if(!preg_match('/[\*\?!_]/',$sifre)){
        header("Location: register.php?email=$email&mesaj=Your password must contain at least one special character (*,?,!,_).");
        exit;
    }

    $sql            = "INSERT INTO kullanicilar (kullanici_adi, email, sifre, bilgilendirme_durum) VALUES (?,?,?, ?)";
    $stmt           = $db->prepare($sql);
    $kayit_durum    = $stmt->execute([$kullanici_adi, $email, $sifre, $bilgilendirme_durum]);

    if($kayit_durum)
    {
        $_SESSION['kullanici_adi']  = $kullanici_adi;
        $_SESSION['email']          = $email;
        $_SESSION['durum']          = true;
        header("Location: index.php");
        exit;
    }
    else
    {
        header("Location: register.php?durum=false");
        exit;
    }
}

//giris yap
if(isset($_POST['giris']))
{
    echo "burdayim";
    $email = $_POST['kullanici_adi'];
    $sifre = $_POST['sifre'];


    // if($kullanici) 
    // {
        $_SESSION['kullanici_adi']  = $kullanici->kullanici_adi;
        $_SESSION['email']          = $kullanici->email;
        $_SESSION['durum']          = true;

        header("Location: index.php");
        exit;
    // }
    // else
    // {
    //     header("Location: login.php?durum=false");
    //     exit;
    // }
}

//logout
if(isset($_GET['durum']) && $_GET['durum'] == 'logout' )
{
    session_destroy();
    header("Location: login.php");
    exit;
}


https://api.nomics.com/v1/currencies/predictions/ticker?key=2b5f3d4c2ae1a86fc34056f5f7b8a09dc63b3e42&ids=BTC

//forgot password
if(isset($_POST['change-password']))
{
    $email          = $_POST['email'];
    $sifre          = $_POST['sifre'];
    $sifre_tekrar   = $_POST['tekrar_sifre'];
    $bilgilendirme_durum = isset($_POST['bilgilendirme_durum']) ? $_POST['bilgilendirme_durum'] : 'hayir';

    if($sifre != $sifre_tekrar)
    {
        header("Location: forgot-password.php?email=$email&mesaj=Passwords do not match");
        exit;
    }

    if(strlen($sifre) < 8)
    {
        header("Location: forgot-password.php?email=$email&mesaj=Your password must contain at least 8 characters.");
        exit;
    }


    if (!preg_match('/[A-Z]/',$sifre)) {
        header("Location: forgot-password.php?email=$email&mesaj=Your password must contain at least one capital letter.");
        exit;
    }

    if (!preg_match('/[a-z]/',$sifre)) {
        header("Location: forgot-password.php?email=$email&mesaj=Your password must contain at least one lowercase letter.");
        exit;
    }

    if (!preg_match('/[0-9]/',$sifre)) {
        header("Location: forgot-password.php?email=$email&mesaj=Your password must contain at least one number.");
        exit;
    }



    if(!preg_match('/[\*\?!_]/',$sifre)){
        header("Location: forgot-password.php?email=$email&mesaj=Your password must contain at least one special character (*,?,!,_).");
        exit;
    }


    $sql    = "UPDATE kullanicilar SET sifre=?, bilgilendirme_durum = ? WHERE email=?";
    $stmt   = $db->prepare($sql);
    $durum  = $stmt->execute([$sifre, $bilgilendirme_durum, $email]);

    if($durum)
    {
        $_SESSION['email']          = $email;
        $_SESSION['durum']          = true;

        header("Location: index.php");
        exit;
    }
    else 
    {
        header("Location: forgot-password.php?email=$email&mesaj=The operation failed.");
        exit;
    }


}

echo "<pre>";
print_r($_POST);


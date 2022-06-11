<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" >
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css"/>
    <link rel="stylesheet" href="css/my_style.css">
    <title>Forgot Password</title>
</head>
<body>
    <div class="container mt-5">
        <div class="card">
            <div class="card-header">
                <h2>New Password</h2>
            </div>
            <div class="card-body">
                <?php if(isset($_GET['mesaj'])){ ?>
                    <div class="alert alert-danger" role="alert" style="margin-bottom:15px;text-align:center;font-weight:bold;color:red">
                        <?php echo $_GET['mesaj']; ?>
                    </div>
                <?php } ?>
                <form action="islemler.php" method="POST">
                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon1">
                            <i class="fa-solid fa-envelope-circle-check"></i>
                        </span>
                        <input type="text" name="email" class="form-control form-control-lg" placeholder="Email Address" 
                            value="<?php echo isset($_GET['email']) ? $_GET['email'] : ''; ?>" required>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon1">
                            <i class="fa-solid fa-lock"></i>
                        </span>
                        <input type="password" name="sifre" class="form-control form-control-lg" placeholder="New Password" minlength="8" required>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon1">
                            <i class="fa-solid fa-lock"></i>
                        </span>
                        <input type="password" name="tekrar_sifre" class="form-control form-control-lg" placeholder="New Password Again" minlength="8" required>
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-text">
                            <input class="form-check-input mt-0" type="checkbox" value="evet" name="bilgilendirme_durum">
                        </div>
                        <input type="text" class="form-control form-control-lg" disabled value="I want to receive daily notification mail.">
                    </div>

                    <div class="d-grid">
                        <button type="submit" class="btn btn-outline-info btn-lg" name="change-password">
                            <i class="fa-solid fa-paper-plane"></i> Change Password
                        </button>
                        <div style="text-align:center" class="mt-2">
                            <a href="login.php" class="btn btn-outline-primary">
                                <i class="fa-solid fa-right-to-bracket"></i> Login
                            </a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
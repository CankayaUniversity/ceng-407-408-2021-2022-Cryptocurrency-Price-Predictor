package com.example.cryptocastapp.Activities;

import static android.content.ContentValues.TAG;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.NotificationCompat;

import android.app.Activity;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.cryptocastapp.Entity.CoinResult;
import com.example.cryptocastapp.Entity.PredictionResult;
import com.example.cryptocastapp.R;
import com.google.gson.Gson;

import java.io.IOException;
import java.util.Arrays;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class MainActivity extends Activity {

    private String cryptoName;

    private EditText crypto_name_input_edit_text;
    private Button show_typed_crypto;
    private Button show_all_crypto;

    private boolean isValidCoinName = true;
    private TextView invalid_coin_name_text_view;

    private Button button_notify;
    private static final String PRIMARY_CHANNEL_ID = "primary_notification_channel";
    private NotificationManager mNotifyManager;
    private static final int NOTIFICATION_ID = 0;

    private Button try_network_button;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);



        crypto_name_input_edit_text = findViewById(R.id.crypto_name_input_edit_text);
        show_typed_crypto = findViewById(R.id.show_typed_crypto);
        show_all_crypto = findViewById(R.id.show_all_crypto);

        try_network_button = findViewById(R.id.try_network_button);

        invalid_coin_name_text_view = findViewById(R.id.invalid_coin_name_text_view);

        cryptoName = GetCryptoNameFromLocalDataSource();
        crypto_name_input_edit_text.setText(cryptoName);

        button_notify = findViewById(R.id.notify);

        show_typed_crypto.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SaveCryptoNameToLocalDataSource(crypto_name_input_edit_text.getText().toString());
                NavigateSpecificCryptoActivty();

            }
        });

        show_all_crypto.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                NavigateCryptoListActivty();
            }
        });

        try_network_button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                NavigateTryNetworkActivty();
            }
        });

        button_notify.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sendNotification();
            }
        });

        createNotificationChannel();
    }

    public void sendNotification()
    {
        NotificationCompat.Builder notifyBuilder = getNotificationBuilder();
        mNotifyManager.notify(NOTIFICATION_ID, notifyBuilder.build());
    }

    private NotificationCompat.Builder getNotificationBuilder()
    {
        NotificationCompat.Builder notifyBuilder = new NotificationCompat.Builder(this, PRIMARY_CHANNEL_ID)
                .setContentTitle("Your Predict Is Ready!!")
                .setContentText("We Predict a Increasing on BTC")
                .setSmallIcon(R.drawable.ic_android);
        return notifyBuilder;
    }

    public void createNotificationChannel()
    {
        mNotifyManager = (NotificationManager)
                getSystemService(NOTIFICATION_SERVICE);
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.O)
        {
            // Create a NotificationChannel
            NotificationChannel notificationChannel = new NotificationChannel(PRIMARY_CHANNEL_ID,
                    "Mascot Notification", NotificationManager
                    .IMPORTANCE_HIGH);

            notificationChannel.enableLights(true);
            notificationChannel.setLightColor(Color.RED);
            notificationChannel.enableVibration(true);
            notificationChannel.setDescription("Notification from Mascot");
            mNotifyManager.createNotificationChannel(notificationChannel);
        }
    }

    private void SaveCryptoNameToLocalDataSource(String cryptoName)
    {
        this.cryptoName = cryptoName;

        String CONST_DATA = "CRYPTO_NAME";
        SharedPreferences preferences = this.getSharedPreferences(CONST_DATA, getApplicationContext().MODE_PRIVATE);
        SharedPreferences.Editor editor = preferences.edit();
        editor.putString(CONST_DATA,String.valueOf(cryptoName));
        editor.apply();
    }

    private String GetCryptoNameFromLocalDataSource()
    {
        String result;
        String CONST_DATA = "CRYPTO_NAME";
        SharedPreferences preferences = this.getSharedPreferences(CONST_DATA, getApplicationContext().MODE_PRIVATE);
        result = preferences.getString(CONST_DATA, "");

        return result;
    }

    private void NavigateSpecificCryptoActivty()
    {
        Intent specificCryptoIntent = new Intent(MainActivity.this, CryptoListActivity.class);
        specificCryptoIntent.putExtra("crypto_name", cryptoName);
        startActivity(specificCryptoIntent);
    }

    private void NavigateCryptoListActivty()
    {
        Intent cryptoListIntent = new Intent(MainActivity.this, CryptoListActivity.class);
        startActivity(cryptoListIntent);
    }

    private void NavigateTryNetworkActivty()
    {
        Intent tryNetworkItemIntent = new Intent(MainActivity.this, TryNetworkActivity.class);
        startActivity(tryNetworkItemIntent);
    }
}
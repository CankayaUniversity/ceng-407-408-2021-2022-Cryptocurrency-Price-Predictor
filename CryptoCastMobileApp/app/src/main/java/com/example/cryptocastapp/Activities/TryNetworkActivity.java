package com.example.cryptocastapp.Activities;

import static android.content.ContentValues.TAG;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;

import com.example.cryptocastapp.Entity.CoinResult;
import com.example.cryptocastapp.Entity.PredictionResult;
import com.example.cryptocastapp.Interfaces.RetrofitAPI;
import com.example.cryptocastapp.R;
import com.google.gson.Gson;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;


public class TryNetworkActivity extends AppCompatActivity {

    private TextView date_text_view;
    private TextView prediction_text_view;

    public int responseCode;
    public static String responseString = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_try_network);

        date_text_view = findViewById(R.id.date_text_view);
        prediction_text_view = findViewById(R.id.prediction_text_view);

        date_text_view.setText("Connection Failed");
        prediction_text_view.setText("Connection Failed");


        GetPredictionDataFromNetwork();
    }


    public void GetPredictionDataFromNetwork()
    {
/*
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("95.70.201.54/berke/api/weatherforecast")
                .build();
        RetrofitAPI retrofitAPI = retrofit.create(RetrofitAPI.class);
        retrofitAPI.getPosts().enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response)
            {
                response.toString();
            }
            @Override
            public void onFailure(Call<ResponseBody> call, Throwable t) {

            }
        });
 */





                OkHttpClient client = new OkHttpClient().newBuilder()
                .build();
        Request request = new Request.Builder()
                .url("http://95.70.201.54/berke/api/weatherforecast")
                .method("GET", null)
                .build();
        client.newCall(request).enqueue(new Callback() {
            @Override
            public void onFailure(@NonNull Call call, @NonNull IOException e) {
                TryNetworkActivity.this.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        date_text_view.setText("Request Failed");
                        prediction_text_view.setText("Request Failed");
                    }
                });
                Log.d(TAG, "onFailure");
            }

            @Override
            public void onResponse(@NonNull Call call, @NonNull Response response) throws IOException {
                if(response.isSuccessful())
                {
                    final String responseBody = response.body().string();
                    PredictionResult predictionResult = new Gson().fromJson(responseBody, PredictionResult.class);

                    TryNetworkActivity.this.runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            date_text_view.setText(predictionResult.getDate());
                            prediction_text_view.setText(predictionResult.getPrediction());
                        }
                    });
                }
            }
        });

    }
}
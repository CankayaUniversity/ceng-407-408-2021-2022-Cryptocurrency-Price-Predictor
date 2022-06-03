package com.example.cryptocastapp.Activities;

import static android.content.ContentValues.TAG;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import android.util.Log;

import com.example.cryptocastapp.Adapter.CoinResultAdapter;
import com.example.cryptocastapp.Entity.CoinResult;
import com.example.cryptocastapp.Entity.PredictionResult;
import com.example.cryptocastapp.R;
import com.google.gson.Gson;

import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class CryptoListActivity extends AppCompatActivity {

    private String cryptoRequestList;

    private RecyclerView coin_list_recyclerview;

    private boolean isUp;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_crypto_list);

        coin_list_recyclerview = findViewById(R.id.coin_list_recyclerview);

        Bundle bundle = getIntent().getExtras();

        cryptoRequestList = "";

        if(bundle != null)//List cryptoName
        {
            cryptoRequestList = bundle.getString("crypto_name");
            GetCoinDataFromNetwork(cryptoRequestList);
        }
        else//List saved cryptos
        {
            cryptoRequestList = getString(R.string.coin_list_for_network_request);
            GetCoinDataFromNetwork(cryptoRequestList);
        }

    }

    private void GetCoinDataFromNetwork(String cryptoRequestList)
    {
        String requestUrl = "https://rest.coinapi.io/v1/assets?filter_asset_id="+ cryptoRequestList +"&apikey=899A9A95-A469-4CE7-BEB1-AD5EB2172DE7";

        OkHttpClient client = new OkHttpClient().newBuilder()
                .build();
        Request request = new Request.Builder()
                //.url("https://rest.coinapi.io/v1/assets?filter_asset_id=btc&apikey=899A9A95-A469-4CE7-BEB1-AD5EB2172DE7")
                .url(requestUrl)
                .method("GET", null)
                .build();
        client.newCall(request).enqueue(new Callback() {
            @Override
            public void onFailure(@NonNull Call call, @NonNull IOException e) {
                Log.d(TAG, "onFailure");
            }

            @Override
            public void onResponse(@NonNull Call call, @NonNull Response response) throws IOException {
                if(response.isSuccessful())
                {
                    final String responseBody = response.body().string();
                    CoinResult[] coinResult = new Gson().fromJson(responseBody, CoinResult[].class);

                    CryptoListActivity.this.runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            setAdapterRecyclerView(Arrays.asList(coinResult), isUp);
                        }
                    });

                    Log.d(TAG,"onResponse");
                }
            }
        });
    }

    private void setAdapterRecyclerView(List<CoinResult> resultList, boolean isUp)
    {
        CoinResultAdapter adapter = new CoinResultAdapter(resultList, isUp);
        RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(getApplicationContext());
        coin_list_recyclerview.setLayoutManager(mLayoutManager);
        coin_list_recyclerview.setAdapter(adapter);
        //weather_list_recyclerview
        // adapter
    }

}
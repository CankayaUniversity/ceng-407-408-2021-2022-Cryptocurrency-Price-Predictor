package com.example.cryptocastapp.Activities;

import static android.content.ContentValues.TAG;

import android.graphics.Color;
import android.graphics.Paint;
import android.os.Bundle;
import android.util.Log;
import android.view.WindowManager;
import android.widget.SeekBar;
import android.widget.SeekBar.OnSeekBarChangeListener;
import android.widget.TextView;

//import com.github.mikephil.charting.charts.CandleStickChart;
import androidx.annotation.NonNull;

import com.example.cryptocastapp.Entity.ChartResult;
import com.example.cryptocastapp.Entity.CoinResult;
import com.example.cryptocastapp.R;
import com.example.cryptocastapp.charts.CandleStickChart;
//import com.github.mikephil.charting.components.XAxis;
import com.example.cryptocastapp.components.XAxis;
//import com.github.mikephil.charting.components.XAxis.XAxisPosition;
import com.example.cryptocastapp.components.XAxis.XAxisPosition;
//import com.github.mikephil.charting.components.YAxis;
import com.example.cryptocastapp.components.YAxis;
//import com.github.mikephil.charting.components.YAxis.AxisDependency;
import com.example.cryptocastapp.components.YAxis.AxisDependency;
//import com.github.mikephil.charting.data.CandleData;
import com.example.cryptocastapp.data.CandleData;
//import com.github.mikephil.charting.data.CandleDataSet;
import com.example.cryptocastapp.data.CandleDataSet;
//import com.github.mikephil.charting.data.CandleEntry;
import com.example.cryptocastapp.data.CandleEntry;
//import com.github.mikephil.charting.interfaces.datasets.ICandleDataSet;
//import com.github.mikephil.charting.interfaces.datasets.IDataSet;
//import com.xxmassdeveloper.mpchartexample.notimportant.DemoBase;
import com.example.cryptocastapp.notimportant.DemoBase;
import com.google.gson.Gson;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class
CandleStickChartActivity extends DemoBase implements OnSeekBarChangeListener {

    private CandleStickChart chart;
    private SeekBar seekBarX, seekBarY;
    private TextView tvX, tvY;

    private String cryptoName;

    private ChartResult[] coinResult;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_candle_stick_chart);

        setTitle("CandleStickChartActivity");


        Bundle bundle = getIntent().getExtras();
        if(bundle != null)
        {
            cryptoName = bundle.getString("coinName");
        }
        else
        {
            cryptoName = "btc";
        }

        coinResult = GetChartDataFromNetwork(cryptoName);


        tvX = findViewById(R.id.tvXMax);
        tvY = findViewById(R.id.tvYMax);

        seekBarX = findViewById(R.id.seekBar1);
        seekBarX.setOnSeekBarChangeListener(this);

        seekBarY = findViewById(R.id.seekBar2);
        seekBarY.setOnSeekBarChangeListener(this);

        chart = findViewById(R.id.chart1);
        chart.setBackgroundColor(Color.WHITE);

        chart.getDescription().setEnabled(false);

        // if more than 60 entries are displayed in the chart, no values will be
        // drawn
        chart.setMaxVisibleValueCount(60);

        // scaling can now only be done on x- and y-axis separately
        chart.setPinchZoom(false);

        chart.setDrawGridBackground(false);

        XAxis xAxis = chart.getXAxis();
        xAxis.setPosition(XAxisPosition.BOTTOM);
        xAxis.setDrawGridLines(false);

        YAxis leftAxis = chart.getAxisLeft();
//        leftAxis.setEnabled(false);
        leftAxis.setLabelCount(7, false);
        leftAxis.setDrawGridLines(false);
        leftAxis.setDrawAxisLine(false);

        YAxis rightAxis = chart.getAxisRight();
        rightAxis.setEnabled(false);
//        rightAxis.setStartAtZero(false);

        // setting data
        seekBarX.setProgress(40);
        seekBarY.setProgress(100);

        chart.getLegend().setEnabled(false);

    }

    private ChartResult[] GetChartDataFromNetwork(String cryptoName)
    {
        String requestUrl = "https://rest.coinapi.io/v1/ohlcv/BITSTAMP_SPOT_" + cryptoName +"_USD/latest?period_id=1DAY&apikey=899A9A95-A469-4CE7-BEB1-AD5EB2172DE7";

        OkHttpClient client = new OkHttpClient().newBuilder()
                .build();
        MediaType mediaType = MediaType.parse("text/plain");
        RequestBody body = RequestBody.create(mediaType, "");
        Request request = new Request.Builder()
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
                    coinResult = new Gson().fromJson(responseBody, ChartResult[].class);

                    CandleStickChartActivity.this.runOnUiThread(new Runnable() {
                        @Override
                        public void run() {

                        }
                    });

                    Log.d(TAG,"onResponse");
                }
            }
        });

        return coinResult;
    }


    @Override
    public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {

        progress = (seekBarX.getProgress());

        tvX.setText(String.valueOf(progress));
        tvY.setText(String.valueOf(seekBarY.getProgress()));

        chart.resetTracking();

        ArrayList<CandleEntry> values = new ArrayList<>();


        for(int i = 0; i < progress; i++)
        {
            values.add(new CandleEntry(
                    i,
                    coinResult[progress - i].getPriceHigh(),
                    coinResult[progress - i].getPriceLow(),
                    coinResult[progress - i].getPriceOpen(),
                    coinResult[progress - i].getPriceClose(),
                    getResources().getDrawable(R.drawable.star)
                    ));
        }



/*

        for (int i = 0; i < progress; i++) {
            float multi = (seekBarY.getProgress() + 1);
            float val = (float) (Math.random() * 40) + multi;

            float high = (float) (Math.random() * 9) + 8f;
            float low = (float) (Math.random() * 9) + 8f;

            float open = (float) (Math.random() * 6) + 1f;
            float close = (float) (Math.random() * 6) + 1f;

            boolean even = i % 2 == 0;

            values.add(new CandleEntry(
                    i, val + high,
                    val - low,
                    even ? val + open : val - open,
                    even ? val - close : val + close,
                    getResources().getDrawable(R.drawable.star)
            ));
        }
 */



        CandleDataSet set1 = new CandleDataSet(values, "Data Set");

        set1.setDrawIcons(false);
        set1.setAxisDependency(AxisDependency.LEFT);
//        set1.setColor(Color.rgb(80, 80, 80));
        set1.setShadowColor(Color.DKGRAY);
        set1.setShadowWidth(0.7f);
        set1.setDecreasingColor(Color.RED);
        set1.setDecreasingPaintStyle(Paint.Style.FILL);
        set1.setIncreasingColor(Color.rgb(122, 242, 84));
        set1.setIncreasingPaintStyle(Paint.Style.FILL);
        set1.setNeutralColor(Color.BLUE);
        //set1.setHighlightLineWidth(1f);

        CandleData data = new CandleData(set1);

        chart.setData(data);
        chart.invalidate();
    }

  /*
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.candle, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {
            case R.id.viewGithub: {
                Intent i = new Intent(Intent.ACTION_VIEW);
                i.setData(Uri.parse("https://github.com/PhilJay/MPAndroidChart/blob/master/MPChartExample/src/com/xxmassdeveloper/mpchartexample/CandleStickChartActivity.java"));
                startActivity(i);
                break;
            }
            case R.id.actionToggleValues: {
                for (IDataSet set : chart.getData().getDataSets())
                    set.setDrawValues(!set.isDrawValuesEnabled());

                chart.invalidate();
                break;
            }
            case R.id.actionToggleIcons: {
                for (IDataSet set : chart.getData().getDataSets())
                    set.setDrawIcons(!set.isDrawIconsEnabled());

                chart.invalidate();
                break;
            }
            case R.id.actionToggleHighlight: {
                if(chart.getData() != null) {
                    chart.getData().setHighlightEnabled(!chart.getData().isHighlightEnabled());
                    chart.invalidate();
                }
                break;
            }
            case R.id.actionTogglePinch: {
                if (chart.isPinchZoomEnabled())
                    chart.setPinchZoom(false);
                else
                    chart.setPinchZoom(true);

                chart.invalidate();
                break;
            }
            case R.id.actionToggleAutoScaleMinMax: {
                chart.setAutoScaleMinMaxEnabled(!chart.isAutoScaleMinMaxEnabled());
                chart.notifyDataSetChanged();
                break;
            }
            case R.id.actionToggleMakeShadowSameColorAsCandle: {
                for (ICandleDataSet set : chart.getData().getDataSets()) {
                    ((CandleDataSet) set).setShadowColorSameAsCandle(!set.getShadowColorSameAsCandle());
                }

                chart.invalidate();
                break;
            }
            case R.id.animateX: {
                chart.animateX(2000);
                break;
            }
            case R.id.animateY: {
                chart.animateY(2000);
                break;
            }
            case R.id.animateXY: {
                chart.animateXY(2000, 2000);
                break;
            }
            case R.id.actionSave: {
                if (ContextCompat.checkSelfPermission(this, Manifest.permission.WRITE_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED) {
                    saveToGallery();
                } else {
                    requestStoragePermission(chart);
                }
                break;
            }
        }
        return true;
    }
   */

    @Override
    protected void saveToGallery() {
        saveToGallery(chart, "CandleStickChartActivity");
    }

    @Override
    public void onStartTrackingTouch(SeekBar seekBar) {}

    @Override
    public void onStopTrackingTouch(SeekBar seekBar) {}
}

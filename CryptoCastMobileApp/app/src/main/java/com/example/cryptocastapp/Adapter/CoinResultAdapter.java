package com.example.cryptocastapp.Adapter;

import static android.content.ContentValues.TAG;

import android.content.Intent;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.cryptocastapp.Activities.CandleStickChartActivity;
import com.example.cryptocastapp.Activities.CryptoListActivity;
import com.example.cryptocastapp.Activities.MainActivity;
import com.example.cryptocastapp.Entity.CoinResult;
import com.example.cryptocastapp.Entity.PredictionResult;
import com.example.cryptocastapp.R;
import com.google.gson.Gson;

import java.io.IOException;
import java.util.List;
import java.util.Random;
import java.util.concurrent.ThreadLocalRandom;

public class CoinResultAdapter extends RecyclerView.Adapter<CoinResultAdapter.ViewHolder>
{
    private List<CoinResult> coinResultList;

    boolean isUp;

    public CoinResultAdapter(List<CoinResult> coinResultList, boolean isUp)
    {
        this.isUp = isUp;
        this.coinResultList = coinResultList;
    }

    @Override
    public ViewHolder onCreateViewHolder(ViewGroup viewGroup, int viewType) {
        View view = LayoutInflater.from(viewGroup.getContext())
                .inflate(R.layout.coin_list_item, viewGroup, false);
        ViewHolder holder = new ViewHolder(view);
        return holder;
    }

    @Override
    public void onBindViewHolder(ViewHolder holder, final int position) {
        CoinResult result = coinResultList.get(position);

        holder.coin_id_textview.setText(result.getAssetId());

        holder.price_textview.setText("Price: " + result.getPriceUsd().toString() + " USD");

        String dateString = "Date: " + result.getDataTradeEnd();
        dateString = dateString.replace("T", " Time:");
        dateString = dateString.substring(0,dateString.length() - 9);
        holder.date_textview.setText(dateString);



        if(/*new Random().nextInt(2) == 0*/ isUp)
        {
            holder.predict_imageview.setImageResource(R.mipmap.green_up_arrow_icon);
        }
        else
        {
            holder.predict_imageview.setImageResource(R.mipmap.red_down_arrow_icon);
        }


        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent chartIntent = new Intent(v.getContext(), CandleStickChartActivity.class);
                chartIntent.putExtra("coinName",result.getAssetId());
                v.getContext().startActivity(chartIntent);
            }
        });

    }

    @Override
    public int getItemCount()
    {
        return coinResultList.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder {
        private TextView coin_id_textview;
        private TextView date_textview;
        private TextView price_textview;

        private ImageView predict_imageview;

        public ViewHolder(View v) {
            super(v);

            coin_id_textview = v.findViewById(R.id.coin_id_textview);
            date_textview = v.findViewById(R.id.date_textview);
            price_textview = v.findViewById(R.id.price_textview);
            predict_imageview = v.findViewById(R.id.predict_imageview);

        }
    }




}

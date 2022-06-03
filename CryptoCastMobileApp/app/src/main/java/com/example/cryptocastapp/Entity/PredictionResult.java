package com.example.cryptocastapp.Entity;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class PredictionResult {

    @SerializedName("date")
    @Expose
    private String date;
    @SerializedName("prediction")
    @Expose
    private Integer prediction;

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public Integer getPrediction() {
        return prediction;
    }

    public void setPrediction(Integer prediction) {
        this.prediction = prediction;
    }

}
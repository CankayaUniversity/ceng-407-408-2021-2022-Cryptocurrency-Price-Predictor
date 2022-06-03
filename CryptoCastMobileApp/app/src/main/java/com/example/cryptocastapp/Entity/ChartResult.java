package com.example.cryptocastapp.Entity;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class ChartResult {

    @SerializedName("time_period_start")
    @Expose
    private String timePeriodStart;
    @SerializedName("time_period_end")
    @Expose
    private String timePeriodEnd;
    @SerializedName("time_open")
    @Expose
    private String timeOpen;
    @SerializedName("time_close")
    @Expose
    private String timeClose;
    @SerializedName("price_open")
    @Expose
    private float priceOpen;
    @SerializedName("price_high")
    @Expose
    private float priceHigh;
    @SerializedName("price_low")
    @Expose
    private float priceLow;
    @SerializedName("price_close")
    @Expose
    private float priceClose;
    @SerializedName("volume_traded")
    @Expose
    private Double volumeTraded;
    @SerializedName("trades_count")
    @Expose
    private Integer tradesCount;

    public String getTimePeriodStart() {
        return timePeriodStart;
    }

    public void setTimePeriodStart(String timePeriodStart) {
        this.timePeriodStart = timePeriodStart;
    }

    public String getTimePeriodEnd() {
        return timePeriodEnd;
    }

    public void setTimePeriodEnd(String timePeriodEnd) {
        this.timePeriodEnd = timePeriodEnd;
    }

    public String getTimeOpen() {
        return timeOpen;
    }

    public void setTimeOpen(String timeOpen) {
        this.timeOpen = timeOpen;
    }

    public String getTimeClose() {
        return timeClose;
    }

    public void setTimeClose(String timeClose) {
        this.timeClose = timeClose;
    }

    public float getPriceOpen() {
        return priceOpen;
    }

    public void setPriceOpen(float priceOpen) {
        this.priceOpen = priceOpen;
    }

    public float getPriceHigh() {
        return priceHigh;
    }

    public void setPriceHigh(float priceHigh) {
        this.priceHigh = priceHigh;
    }

    public float getPriceLow() {
        return priceLow;
    }

    public void setPriceLow(float priceLow) {
        this.priceLow = priceLow;
    }

    public float getPriceClose() {
        return priceClose;
    }

    public void setPriceClose(float priceClose) {
        this.priceClose = priceClose;
    }

    public Double getVolumeTraded() {
        return volumeTraded;
    }

    public void setVolumeTraded(Double volumeTraded) {
        this.volumeTraded = volumeTraded;
    }

    public Integer getTradesCount() {
        return tradesCount;
    }

    public void setTradesCount(Integer tradesCount) {
        this.tradesCount = tradesCount;
    }

}
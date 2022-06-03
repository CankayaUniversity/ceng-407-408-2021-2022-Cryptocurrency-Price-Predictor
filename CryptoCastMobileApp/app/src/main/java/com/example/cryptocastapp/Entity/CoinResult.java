package com.example.cryptocastapp.Entity;


import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class CoinResult {

    @SerializedName("asset_id")
    @Expose
    private String assetId;
    @SerializedName("name")
    @Expose
    private String name;
    @SerializedName("type_is_crypto")
    @Expose
    private Integer typeIsCrypto;
    @SerializedName("data_quote_start")
    @Expose
    private String dataQuoteStart;
    @SerializedName("data_quote_end")
    @Expose
    private String dataQuoteEnd;
    @SerializedName("data_orderbook_start")
    @Expose
    private String dataOrderbookStart;
    @SerializedName("data_orderbook_end")
    @Expose
    private String dataOrderbookEnd;
    @SerializedName("data_trade_start")
    @Expose
    private String dataTradeStart;
    @SerializedName("data_trade_end")
    @Expose
    private String dataTradeEnd;
    @SerializedName("data_symbols_count")
    @Expose
    private Integer dataSymbolsCount;
    @SerializedName("volume_1hrs_usd")
    @Expose
    private Double volume1hrsUsd;
    @SerializedName("volume_1day_usd")
    @Expose
    private Double volume1dayUsd;
    @SerializedName("volume_1mth_usd")
    @Expose
    private Double volume1mthUsd;
    @SerializedName("price_usd")
    @Expose
    private Double priceUsd;
    @SerializedName("id_icon")
    @Expose
    private String idIcon;
    @SerializedName("data_start")
    @Expose
    private String dataStart;
    @SerializedName("data_end")
    @Expose
    private String dataEnd;

    public CoinResult() {

    }

    public String getAssetId() {
        return assetId;
    }

    public void setAssetId(String assetId) {
        this.assetId = assetId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Integer getTypeIsCrypto() {
        return typeIsCrypto;
    }

    public void setTypeIsCrypto(Integer typeIsCrypto) {
        this.typeIsCrypto = typeIsCrypto;
    }

    public String getDataQuoteStart() {
        return dataQuoteStart;
    }

    public void setDataQuoteStart(String dataQuoteStart) {
        this.dataQuoteStart = dataQuoteStart;
    }

    public String getDataQuoteEnd() {
        return dataQuoteEnd;
    }

    public void setDataQuoteEnd(String dataQuoteEnd) {
        this.dataQuoteEnd = dataQuoteEnd;
    }

    public String getDataOrderbookStart() {
        return dataOrderbookStart;
    }

    public void setDataOrderbookStart(String dataOrderbookStart) {
        this.dataOrderbookStart = dataOrderbookStart;
    }

    public String getDataOrderbookEnd() {
        return dataOrderbookEnd;
    }

    public void setDataOrderbookEnd(String dataOrderbookEnd) {
        this.dataOrderbookEnd = dataOrderbookEnd;
    }

    public String getDataTradeStart() {
        return dataTradeStart;
    }

    public void setDataTradeStart(String dataTradeStart) {
        this.dataTradeStart = dataTradeStart;
    }

    public String getDataTradeEnd() {
        return dataTradeEnd;
    }

    public void setDataTradeEnd(String dataTradeEnd) {
        this.dataTradeEnd = dataTradeEnd;
    }

    public Integer getDataSymbolsCount() {
        return dataSymbolsCount;
    }

    public void setDataSymbolsCount(Integer dataSymbolsCount) {
        this.dataSymbolsCount = dataSymbolsCount;
    }

    public Double getVolume1hrsUsd() {
        return volume1hrsUsd;
    }

    public void setVolume1hrsUsd(Double volume1hrsUsd) {
        this.volume1hrsUsd = volume1hrsUsd;
    }

    public Double getVolume1dayUsd() {
        return volume1dayUsd;
    }

    public void setVolume1dayUsd(Double volume1dayUsd) {
        this.volume1dayUsd = volume1dayUsd;
    }

    public Double getVolume1mthUsd() {
        return volume1mthUsd;
    }

    public void setVolume1mthUsd(Double volume1mthUsd) {
        this.volume1mthUsd = volume1mthUsd;
    }

    public Double getPriceUsd() {
        return priceUsd;
    }

    public void setPriceUsd(Double priceUsd) {
        this.priceUsd = priceUsd;
    }

    public String getIdIcon() {
        return idIcon;
    }

    public void setIdIcon(String idIcon) {
        this.idIcon = idIcon;
    }

    public String getDataStart() {
        return dataStart;
    }

    public void setDataStart(String dataStart) {
        this.dataStart = dataStart;
    }

    public String getDataEnd() {
        return dataEnd;
    }

    public void setDataEnd(String dataEnd) {
        this.dataEnd = dataEnd;
    }

}
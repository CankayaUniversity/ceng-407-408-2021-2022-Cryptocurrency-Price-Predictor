package com.example.cryptocastapp.interfaces.dataprovider;


//import com.github.mikephil.charting.data.BarData;
import com.example.cryptocastapp.data.BarData;

public interface BarDataProvider extends BarLineScatterCandleBubbleDataProvider {

    BarData getBarData();
    boolean isDrawBarShadowEnabled();
    boolean isDrawValueAboveBarEnabled();
    boolean isHighlightFullBarEnabled();
}

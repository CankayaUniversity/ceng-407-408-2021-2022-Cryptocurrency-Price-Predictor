package com.example.cryptocastapp.interfaces.dataprovider;


//import com.github.mikephil.charting.data.CandleData;
import com.example.cryptocastapp.data.CandleData;
public interface CandleDataProvider extends BarLineScatterCandleBubbleDataProvider {

    CandleData getCandleData();
}


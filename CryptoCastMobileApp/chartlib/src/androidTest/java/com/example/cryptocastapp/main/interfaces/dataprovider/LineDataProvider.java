package com.example.cryptocastapp.interfaces.dataprovider;

//import com.github.mikephil.charting.components.YAxis;
import com.example.cryptocastapp.components.YAxis;
//import com.github.mikephil.charting.data.LineData;
import com.example.cryptocastapp.data.LineData;

public interface LineDataProvider extends BarLineScatterCandleBubbleDataProvider {

    LineData getLineData();

    YAxis getAxis(YAxis.AxisDependency dependency);
}


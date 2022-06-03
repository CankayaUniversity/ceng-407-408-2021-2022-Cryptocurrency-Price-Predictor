package com.example.cryptocastapp.interfaces.dataprovider;

//import com.github.mikephil.charting.components.YAxis.AxisDependency;
import com.example.cryptocastapp.components.YAxis.AxisDependency;
//import com.github.mikephil.charting.data.BarLineScatterCandleBubbleData;
import com.example.cryptocastapp.data.BarLineScatterCandleBubbleData;
//import com.github.mikephil.charting.utils.Transformer;
import com.example.cryptocastapp.utils.Transformer;

public interface BarLineScatterCandleBubbleDataProvider extends ChartInterface {

    Transformer getTransformer(AxisDependency axis);
    boolean isInverted(AxisDependency axis);

    float getLowestVisibleX();
    float getHighestVisibleX();

    BarLineScatterCandleBubbleData getData();
}


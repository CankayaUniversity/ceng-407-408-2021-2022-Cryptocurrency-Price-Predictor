package com.example.cryptocastapp.Interfaces;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.GET;

public interface RetrofitAPI {
    @GET("/posts")
    Call<ResponseBody> getPosts();
}

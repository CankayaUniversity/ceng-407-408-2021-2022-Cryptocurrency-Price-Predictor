# Machine Learning Libraries
from sklearn import model_selection
from sklearn import svm

import yfinance as yf
# Adaboost Classifier
from sklearn.ensemble import AdaBoostClassifier
from sklearn.model_selection import train_test_split
from sklearn.metrics import classification_report, confusion_matrix
from sklearn.model_selection import cross_val_score
from sklearn import datasets
from sklearn import metrics
from datetime import datetime
import talib
import os
import numpy

end_date = datetime.today().strftime('%Y-%m-%d')

df = yf.download("BTC-USD", start="2015-11-20", end=end_date, interval = "1d")
        # fetch data by interval (including intraday if period < 60 days)
        # valid intervals: 1m,2m,5m,15m,30m,60m,90m,1h,1d,5d,1wk,1mo,3mo
        # (optional, default is '1d')
        #interval = "1m",


# Condition label shall have three values as [1,0,-1] which stands for increase-stable-decrease values respectively
cond= [1] # Initial value is considered to be stable therefore 0
for i in range(1, len(df['Close'])):
  if (df['Close'][i]-df['Close'][i-1])>0:
    cond.append(1)
  elif (df['Close'][i]-df['Close'][i-1])==0:
    cond.append(0)
  elif (df['Close'][i]-df['Close'][i-1])<0:
    cond.append(-1)

df['Condition'] = cond # Assign the list as a dataframe column


# Trending Indicators
df['SMA20'] = talib.SMA(df['Close'], timeperiod=20)
df['EMA20'] = talib.EMA(df['Close'], timeperiod=20)
df['ADX'] = talib.ADX(df['High'], df['Low'], df['Close'])

# Remove NaN values caused by technical indicators
df_ind=df[:][27:]

# Window Size 100
# Sliding window
windowStart = 0
windowSize = 101
overallAccuracy = 0
exception_count = 0

#print(range(len(df_ind['Close'])-1-windowSize))

print(range(len(df['Close'])-windowSize+1))
for i in range(len(df['Close'])-windowSize+1):
  # Slicing window values
  df_window = df[:][windowStart:(windowStart + windowSize)]
  X = df_window.drop(['Condition'], axis=1)
  y = df_window['Condition']
  
  # Train Test Split
  X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.0001, random_state=0, shuffle=False)
  try:
    # Create adaboost classifer object
    abc = AdaBoostClassifier()#(n_estimators=50, learning_rate=1)
    # Train Adaboost Classifer
    model = abc.fit(X_train, y_train.ravel())

    #Predict the response for test dataset
    y_pred = model.predict(X_test)

    overallAccuracy+=(metrics.accuracy_score(y_test, y_pred))

   
  except:
    exception_count += 1
    
  windowStart+=1

#print(str(exception_count) + " Exceptions occured.") 
overallAccuracy= overallAccuracy/(len(df['Close'])-windowSize)
print("Overall accuracy is " + str(round(100*overallAccuracy)) + "%.")
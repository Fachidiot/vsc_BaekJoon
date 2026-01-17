# 라이브러리 호출
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import Dense
from google.colab import files
import numpy as np
import os

# 데이터 준비 (github에서 직접 clone받아오기.)
if os.path.exists("./data"):
    print("data 폴더가 이미 존재합니다. git clone을 건너뜁니다.")
else:
    print("data의 git clone을 실행합니다.")
#   !git clone https://github.com/taehojo/data.git

git_DataSet = np.loadtxt("./data/ThoraricSurgery3.csv", delimiter=",")

gX = git_DataSet[:, 0:16]    # 입력값 (0 ~ 15열, 환자 정보)
gY = git_DataSet[:, 16]      # 출력값 (16열, 생존 여부)

print(git_DataSet)
# 데이터 준비 (local 데이터 upload)
if os.path.exists('ThoraricSurgery3.csv'):
    print(f"ThoraricSurgery3.csv 파일이 이미 존재합니다. 업로드를 건너뜁니다.")
    data = 'ThoraricSurgery3.csv'
else:
    print(f"파일을 업로드합니다.")
    data = files.upload()
    data = list(data.keys())[0]

local_DataSet = np.loadtxt(data, delimiter=",")
lX = local_DataSet[:0:16]   # 입력값
lY = local_DataSet[:, 16]    # 출력값

print(local_DataSet)

# 딥러닝 모델 생성
model = Sequential()
model.add(Dense(30, input_dim=16, activation='relu'))   # 은닉층
model.add(Dense(10, activation='relu'))
model.add(Dense(1, activation='sigmoid'))               # 출력층

# 모델 실행 (학습)
model.compile(loss='binary_crossentropy',
              optimizer='adam', metrics=['accuracy'])
history = model.fit(gX, gY, epochs=5, batch_size=16)

# 모델 활용
data_to_predict = [2, 2.44, 0.96, 2, 0, 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 73]
data_array = np.array(data_to_predict)
reshaped_data = data_array.reshape(1, -1)

print("모델에 입력될 데이터의 모양:", reshaped_data.shape)
prediction = model.predict(reshaped_data)

print("\n예측 결과:", prediction)

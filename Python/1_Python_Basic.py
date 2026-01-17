import matplotlib.pyplot as plt
import numpy as np

# [변수]
a = "명지전문대학"
b = "김학동"
print(a + b)

# [배열]
narray = np.array([1, 3, 5, 7, 9])
narray.shape
print(len(narray))  # narray배열의 길이 : 5
print(narray[-1] + narray[-4])  # (-1인덱스)9 + (-4인덱스)3 = 12
print(narray[1:5])  # narray의 인덱스1부터 4까지의 값 : 3,5,7,9

darray = np.array([[1, 3, 5, 7, 9], [0, 2, 4, 6, 8]])
darray.shape  # shape함수 : darray를 보여줘라.
print(darray.reshape(5, 2))  # reshape : 배열의 구조를 바꾸겠다. 2,5 -> 5,2
print(darray.reshape(10,))  # reshape : 5,2 -> 10,

zero = np.zeros((2, 5))  # zero : 모든값이 0인 2,5 배열 생성
print(zero)
ones = np.ones(10,)  # ones : 모든값이 1인 10, 배열 생성
print(ones)

# [랜덤값]
r = np.random.rand(1000)
plt.hist(r)
plt.grid()  # plt

r = np.random.normal(0, 1, 500)
plt.hist(r)
plt.grid()

r = np.random.randint(0, 100, 10)
for n in r:
    print(n)
print("")
for n in r:
    print("김학동")

r = range(10)
list(r)

r = range(1, 10)
for i in r:
    if (i % 2 == 0):
        print(i, "은 짝수입니다.")
    else:
        print(i, "은 홀수입니다.")


def number_check(num):
    if num % 2 == 0:
        print("짝수")
    else:
        print("홀수")


number_check(13)

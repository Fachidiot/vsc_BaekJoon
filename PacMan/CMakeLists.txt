# CMake에게 프로젝트를 어떻게 빌드해야 하는지 알려주는 설명서 파일

# CMake 최소 요구 버전
# Minimum required version of CMake
cmake_minimum_required(VERSION 3.10)

# 프로젝트 이름 설정
# Set the project name
project(CPP_Project)

# C++ 17 표준 사용
# Use the C++17 standard
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# 실행 파일 생성
# Create the executable
# add_executable(main main.cpp)
add_executable(main main.cpp Map.cpp Player.cpp Ghost.cpp ConsoleUtils.cpp)

# 터미널 명령어
# mkdir build
# cmake ..
# make
# ./lobby_server
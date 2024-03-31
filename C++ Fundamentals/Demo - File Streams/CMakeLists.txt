cmake_minimum_required(VERSION 3.27)
project(Demo_File_Streams)

set(CMAKE_CXX_STANDARD 17)

add_executable(read_binary_content read_binary_content.cpp)
add_executable(read_content read_content.cpp)
add_executable(split_file split_file.cpp)
add_executable(write_content write_content.cpp)

# Configure the OpenSSL package. I have installed version 3.1.1 using `chocolatey`.
set(OPENSSL_ROOT_DIR "C:/Program Files/OpenSSL-Win64")
find_package(OpenSSL)

if (OpenSSL_FOUND)
    add_executable(calculate_hash calculate_hash.cpp)
    target_include_directories(calculate_hash PRIVATE ${OPENSSL_INCLUDE_DIR})
    target_link_libraries(calculate_hash ${OPENSSL_LIBRARIES})
endif ()

# Copy the files folder to the build directory
file(COPY files DESTINATION ${CMAKE_BINARY_DIR})
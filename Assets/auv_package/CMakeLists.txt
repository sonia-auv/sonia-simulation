cmake_minimum_required(VERSION 2.8.3)
project(auv_package)

find_package(catkin REQUIRED COMPONENTS
  rospy
  ros_tcp_endpoint
  message_generation
  unity_robotics_demo_msgs
  geometry_msgs
  sensor_msgs
  std_msgs
)

#add_message_files(DIRECTORY msg)

#add_service_files(DIRECTORY srv)

generate_messages(
  DEPENDENCIES
  unity_robotics_demo_msgs
)

catkin_package(CATKIN_DEPENDS
    ros_tcp_endpoint
    unity_robotics_demo_msgs
    message_runtime)


#############
## Testing ##
#############

## Add gtest based cpp test target and link libraries
# catkin_add_gtest(${PROJECT_NAME}-test test/test_robotics_demo.cpp)
# if(TARGET ${PROJECT_NAME}-test)
#   target_link_libraries(${PROJECT_NAME}-test ${PROJECT_NAME})
# endif()

## Add folders to be run by python nosetests
# catkin_add_nosetests(test)

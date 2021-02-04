# sonia_simulation
How to start the control in Unity:

0. Open Unity project
1. Compile WS:
2. source/devel/setup.bash
3. Start a roscore
4. Open new terminal + $ rosparam load ~/catkin_ws/src/sonia-simulation/Config/params.yaml
5. Source terminal $ source/devel/setup.bash
6. cd ~/catkin_ws/src/sonia-simulation/Assets/auv_package/Scripts
7. execute server_endpoint $ ./server_endpoint.py


(Optionnal)
8. Send message of type PosRot on /PosRot $ cd ~/catkin_ws/src/sonia-simulation/Assets/Auv_package/Scripts & ./server_endpoint.py

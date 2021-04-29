#!/usr/bin/env python

import rospy

from ros_tcp_endpoint import TcpServer, RosPublisher, RosSubscriber, RosService
from auv_package.msg import *
from geometry_msgs.msg import Pose
from sensor_msgs.msg import Image

def main():
    ros_node_name = rospy.get_param("/TCP_NODE_NAME", 'TCPServer')
    buffer_size = rospy.get_param("/TCP_BUFFER_SIZE", 1024)
    connections = rospy.get_param("/TCP_CONNECTIONS", 10)
    tcp_server = TcpServer(ros_node_name, buffer_size, connections)
    rospy.init_node(ros_node_name, anonymous=True)
    
    tcp_server.start({
        'pos_rot': RosSubscriber('pos_rot', Pose, tcp_server),
        'initial_condition': RosPublisher('initial_condition', Pose, queue_size=10),
        # 'sensor_msgs/Front_camera_simulation': RosPublisher('sensor_msgs/Front_camera_simulation', CamData, queue_size=10),
        # 'sensor_msgs/Bottom_camera_simulation': RosPublisher('sensor_msgs/Bottom_camera_simulation', CamData, queue_size=10),
        'front_simulation': RosPublisher('front_simulation', Image, queue_size=10)
    })
    
    rospy.spin()


if __name__ == "__main__":
    main()

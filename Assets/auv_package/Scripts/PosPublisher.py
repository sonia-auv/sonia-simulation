#!/usr/bin/env python

# import random
# import rospy
# from auv_package.msg import PosRot

# TOPIC_NAME = 'pos_rot'
# NODE_NAME = 'pos_publisher'


# def post_pos():
#     pub = rospy.Publisher(TOPIC_NAME, PosRot, queue_size=10)
#     rospy.init_node(NODE_NAME, anonymous=True)
#     rate = rospy.Rate(10)  # 10hz
#     pos_x = 0
#     while not rospy.is_shutdown():
        
#         pos_x = pos_x + 0.01
#         pos_y = 2
#         pos_z = 3
#         rot_x = 0
#         rot_y = 0
#         rot_z = 0
#         rot_w = 1
#         pos_rot = PosRot(pos_x,pos_y,pos_z,0,0,0,1)
#         pub.publish(pos_rot)
#         rate.sleep()


# if __name__ == '__main__':
#     try:
#         post_pos()
#     except rospy.ROSInterruptException:
#         pass
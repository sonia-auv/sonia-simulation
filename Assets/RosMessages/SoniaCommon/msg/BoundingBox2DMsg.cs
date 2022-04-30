//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class BoundingBox2DMsg : Message
    {
        public const string k_RosMessageName = "sonia_common/BoundingBox2D";
        public override string RosMessageName => k_RosMessageName;

        //  A 2D bounding box that can be rotated about its center.
        //  All dimensions are in pixels, but represented using floating-point
        //    values to allow sub-pixel precision. If an exact pixel crop is required
        //    for a rotated bounding box, it can be calculated using Bresenham's line
        //    algorithm.
        //  The 2D position (in pixels) and orientation of the bounding box center.
        public Geometry.Pose2DMsg center;
        //  The size (in pixels) of the bounding box surrounding the object relative
        //    to the pose of its center.
        public double size_x;
        public double size_y;

        public BoundingBox2DMsg()
        {
            this.center = new Geometry.Pose2DMsg();
            this.size_x = 0.0;
            this.size_y = 0.0;
        }

        public BoundingBox2DMsg(Geometry.Pose2DMsg center, double size_x, double size_y)
        {
            this.center = center;
            this.size_x = size_x;
            this.size_y = size_y;
        }

        public static BoundingBox2DMsg Deserialize(MessageDeserializer deserializer) => new BoundingBox2DMsg(deserializer);

        private BoundingBox2DMsg(MessageDeserializer deserializer)
        {
            this.center = Geometry.Pose2DMsg.Deserialize(deserializer);
            deserializer.Read(out this.size_x);
            deserializer.Read(out this.size_y);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.center);
            serializer.Write(this.size_x);
            serializer.Write(this.size_y);
        }

        public override string ToString()
        {
            return "BoundingBox2DMsg: " +
            "\ncenter: " + center.ToString() +
            "\nsize_x: " + size_x.ToString() +
            "\nsize_y: " + size_y.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
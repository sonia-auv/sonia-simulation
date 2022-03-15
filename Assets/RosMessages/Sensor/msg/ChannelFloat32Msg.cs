//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Sensor
{
    [Serializable]
    public class ChannelFloat32Msg : Message
    {
        public const string k_RosMessageName = "sensor_msgs/ChannelFloat32";
        public override string RosMessageName => k_RosMessageName;

        //  This message is used by the PointCloud message to hold optional data
        //  associated with each point in the cloud. The length of the values
        //  array should be the same as the length of the points array in the
        //  PointCloud, and each value should be associated with the corresponding
        //  point.
        //  Channel names in existing practice include:
        //    "u", "v" - row and column (respectively) in the left stereo image.
        //               This is opposite to usual conventions but remains for
        //               historical reasons. The newer PointCloud2 message has no
        //               such problem.
        //    "rgb" - For point clouds produced by color stereo cameras. uint8
        //            (R,G,B) values packed into the least significant 24 bits,
        //            in order.
        //    "intensity" - laser or pixel intensity.
        //    "distance"
        //  The channel name should give semantics of the channel (e.g.
        //  "intensity" instead of "value").
        public string name;
        //  The values array should be 1-1 with the elements of the associated
        //  PointCloud.
        public float[] values;

        public ChannelFloat32Msg()
        {
            this.name = "";
            this.values = new float[0];
        }

        public ChannelFloat32Msg(string name, float[] values)
        {
            this.name = name;
            this.values = values;
        }

        public static ChannelFloat32Msg Deserialize(MessageDeserializer deserializer) => new ChannelFloat32Msg(deserializer);

        private ChannelFloat32Msg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.name);
            deserializer.Read(out this.values, sizeof(float), deserializer.ReadLength());
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.name);
            serializer.WriteLength(this.values);
            serializer.Write(this.values);
        }

        public override string ToString()
        {
            return "ChannelFloat32Msg: " +
            "\nname: " + name.ToString() +
            "\nvalues: " + System.String.Join(", ", values.ToList());
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

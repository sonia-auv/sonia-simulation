//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Std
{
    [Serializable]
    public class Int32MultiArrayMsg : Message
    {
        public const string k_RosMessageName = "std_msgs/Int32MultiArray";
        public override string RosMessageName => k_RosMessageName;

        //  Please look at the MultiArrayLayout message definition for
        //  documentation on all multiarrays.
        public MultiArrayLayoutMsg layout;
        //  specification of data layout
        public int[] data;
        //  array of data

        public Int32MultiArrayMsg()
        {
            this.layout = new MultiArrayLayoutMsg();
            this.data = new int[0];
        }

        public Int32MultiArrayMsg(MultiArrayLayoutMsg layout, int[] data)
        {
            this.layout = layout;
            this.data = data;
        }

        public static Int32MultiArrayMsg Deserialize(MessageDeserializer deserializer) => new Int32MultiArrayMsg(deserializer);

        private Int32MultiArrayMsg(MessageDeserializer deserializer)
        {
            this.layout = MultiArrayLayoutMsg.Deserialize(deserializer);
            deserializer.Read(out this.data, sizeof(int), deserializer.ReadLength());
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.layout);
            serializer.WriteLength(this.data);
            serializer.Write(this.data);
        }

        public override string ToString()
        {
            return "Int32MultiArrayMsg: " +
            "\nlayout: " + layout.ToString() +
            "\ndata: " + System.String.Join(", ", data.ToList());
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

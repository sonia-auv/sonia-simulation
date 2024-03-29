//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class AddOffsetRequest : Message
    {
        public const string k_RosMessageName = "sonia_common/AddOffset";
        public override string RosMessageName => k_RosMessageName;

        public double add_offset;

        public AddOffsetRequest()
        {
            this.add_offset = 0.0;
        }

        public AddOffsetRequest(double add_offset)
        {
            this.add_offset = add_offset;
        }

        public static AddOffsetRequest Deserialize(MessageDeserializer deserializer) => new AddOffsetRequest(deserializer);

        private AddOffsetRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.add_offset);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.add_offset);
        }

        public override string ToString()
        {
            return "AddOffsetRequest: " +
            "\nadd_offset: " + add_offset.ToString();
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

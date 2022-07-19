//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class ObjectiveResetRequest : Message
    {
        public const string k_RosMessageName = "sonia_common/ObjectiveReset";
        public override string RosMessageName => k_RosMessageName;

        public const sbyte ALL = -1;
        public const sbyte BUOY = 0;
        public const sbyte FENCE = 1;
        public const sbyte PINGER = 2;
        public sbyte objectiveType;

        public ObjectiveResetRequest()
        {
            this.objectiveType = 0;
        }

        public ObjectiveResetRequest(sbyte objectiveType)
        {
            this.objectiveType = objectiveType;
        }

        public static ObjectiveResetRequest Deserialize(MessageDeserializer deserializer) => new ObjectiveResetRequest(deserializer);

        private ObjectiveResetRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.objectiveType);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.objectiveType);
        }

        public override string ToString()
        {
            return "ObjectiveResetRequest: " +
            "\nobjectiveType: " + objectiveType.ToString();
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
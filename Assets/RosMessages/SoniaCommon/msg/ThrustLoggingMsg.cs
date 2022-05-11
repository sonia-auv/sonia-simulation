//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class ThrustLoggingMsg : Message
    {
        public const string k_RosMessageName = "sonia_common/ThrustLogging";
        public override string RosMessageName => k_RosMessageName;

        public float[] thrust_axe;
        public float[] thrust_thruster;

        public ThrustLoggingMsg()
        {
            this.thrust_axe = new float[0];
            this.thrust_thruster = new float[0];
        }

        public ThrustLoggingMsg(float[] thrust_axe, float[] thrust_thruster)
        {
            this.thrust_axe = thrust_axe;
            this.thrust_thruster = thrust_thruster;
        }

        public static ThrustLoggingMsg Deserialize(MessageDeserializer deserializer) => new ThrustLoggingMsg(deserializer);

        private ThrustLoggingMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.thrust_axe, sizeof(float), deserializer.ReadLength());
            deserializer.Read(out this.thrust_thruster, sizeof(float), deserializer.ReadLength());
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.WriteLength(this.thrust_axe);
            serializer.Write(this.thrust_axe);
            serializer.WriteLength(this.thrust_thruster);
            serializer.Write(this.thrust_thruster);
        }

        public override string ToString()
        {
            return "ThrustLoggingMsg: " +
            "\nthrust_axe: " + System.String.Join(", ", thrust_axe.ToList()) +
            "\nthrust_thruster: " + System.String.Join(", ", thrust_thruster.ToList());
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

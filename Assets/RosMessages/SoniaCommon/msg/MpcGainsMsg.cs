//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class MpcGainsMsg : Message
    {
        public const string k_RosMessageName = "sonia_common/MpcGains";
        public override string RosMessageName => k_RosMessageName;

        public double[] OV;
        public double[] MV;
        public double[] MVR;
        public double max_thrust;
        public double min_thrust;

        public MpcGainsMsg()
        {
            this.OV = new double[0];
            this.MV = new double[0];
            this.MVR = new double[0];
            this.max_thrust = 0.0;
            this.min_thrust = 0.0;
        }

        public MpcGainsMsg(double[] OV, double[] MV, double[] MVR, double max_thrust, double min_thrust)
        {
            this.OV = OV;
            this.MV = MV;
            this.MVR = MVR;
            this.max_thrust = max_thrust;
            this.min_thrust = min_thrust;
        }

        public static MpcGainsMsg Deserialize(MessageDeserializer deserializer) => new MpcGainsMsg(deserializer);

        private MpcGainsMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.OV, sizeof(double), deserializer.ReadLength());
            deserializer.Read(out this.MV, sizeof(double), deserializer.ReadLength());
            deserializer.Read(out this.MVR, sizeof(double), deserializer.ReadLength());
            deserializer.Read(out this.max_thrust);
            deserializer.Read(out this.min_thrust);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.WriteLength(this.OV);
            serializer.Write(this.OV);
            serializer.WriteLength(this.MV);
            serializer.Write(this.MV);
            serializer.WriteLength(this.MVR);
            serializer.Write(this.MVR);
            serializer.Write(this.max_thrust);
            serializer.Write(this.min_thrust);
        }

        public override string ToString()
        {
            return "MpcGainsMsg: " +
            "\nOV: " + System.String.Join(", ", OV.ToList()) +
            "\nMV: " + System.String.Join(", ", MV.ToList()) +
            "\nMVR: " + System.String.Join(", ", MVR.ToList()) +
            "\nmax_thrust: " + max_thrust.ToString() +
            "\nmin_thrust: " + min_thrust.ToString();
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

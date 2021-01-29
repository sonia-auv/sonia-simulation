//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using RosMessageGeneration;

namespace RosMessageTypes.Std
{
    public class String : Message
    {
        public const string RosMessageName = "std_msgs/String";

        public string data;

        public String()
        {
            this.data = "";
        }

        public String(string data)
        {
            this.data = data;
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();
            listOfSerializations.Add(SerializeString(this.data));

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {
            var dataStringBytesLength = DeserializeLength(data, offset);
            offset += 4;
            this.data = DeserializeString(data, offset, dataStringBytesLength);
            offset += dataStringBytesLength;

            return offset;
        }

        public override string ToString()
        {
            return "String: " +
            "\ndata: " + data.ToString();
        }
    }
}

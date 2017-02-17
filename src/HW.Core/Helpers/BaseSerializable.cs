using System;
using System.IO;
using System.Xml.Serialization;

namespace HW.Core.Helpers
{
	public class BaseSerializable<T>
	{
		public BaseSerializable()
		{
		}
		
		public static T Deserialize(string filename)
		{
			return Deserialize(new StreamReader(filename));
		}
		
		public static T Deserialize(TextReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			T type = (T)serializer.Deserialize(reader);
			reader.Close();
			return type;
		}
	}
}

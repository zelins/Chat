using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utils
{
    public static class ObjectExtensions
    {
        public static byte[] SerializeToByteArray(this object obj)
        {
            var formatter = new BinaryFormatter();

            byte[] bytes;

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);

                bytes = stream.ToArray();
            }

            return bytes;
        }

        public static object DeserializeToObject(this byte[] bytes)
        {
            var formatter = new BinaryFormatter();

            var stream = new MemoryStream(bytes);

            return formatter.Deserialize(stream);
        }
    }
}
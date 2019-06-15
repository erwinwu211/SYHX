using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class CloneExtensions
{
    public static T Clone<T>(this T target)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            binaryFormatter.Serialize(stream, target);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
}
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JB.Helpers
{
    static public class GenericExtensions
    {

        /// <summary>
        /// Make a Deep copy of an object as opposed to a shallow copy which would
        /// only copy references.
        /// Use it on any type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}

using System.IO;
using System.Reflection;
using System.Text;

namespace DB_Example.Utils
{
    public class FileReader
    {
        public static string ReadAllText(string relativePath)
        {
            var sb = new StringBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(relativePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                sb.Append(reader.ReadToEnd());
            }
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIDs
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FileInfo filePath = new FileInfo(ConfigurationManager.AppSettings["filepath"]);
                ParseIds(filePath);
            }
            catch(Exception ex)
            {
                string fileName = "Error".AppendTimeStamp();
                File.WriteAllText($"{Environment.CurrentDirectory}\\{fileName}", $"{ex.Message}\n{ex.StackTrace}");
            }
        }
        public static void ParseIds(FileInfo filePath)
        {
            string fileName = filePath.FullName;
            StringBuilder stringBuilder = new StringBuilder();
            using(StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    stringBuilder.AppendLine($"'{line}',");
                }
            }
            File.WriteAllText($"{filePath.DirectoryName}\\{filePath.Name.AppendTimeStamp()}",stringBuilder.ToString());   
        }
        
    }
    public static class MyExtensions
    {
        public static string AppendTimeStamp(this string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("_yyyyMMdd_HHmm"),
                Path.GetExtension(fileName)
                );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;


namespace ConsoleApp1
{
    class Program
    {
        public static object AppName { get; private set; }

        static void Main(string[] args)
        {
            var aTagList = CreateATagList(Environment.CurrentDirectory);
            var html = $"<html><head></head><body>{aTagList}</body></html>";
            using (FileStream fs = new FileStream("index.html", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine(html);

                }

            }
            System.Diagnostics.Process.Start("index.html");
        }
        private static string CreateATagList(string path)
        {
            string[] files = Directory.GetFiles(path);
            var sb = new StringBuilder();
            List<string> filesOrdered = new List<string>();

            var exeName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            foreach (string f in files)
            {
                var filenameOnly = Path.GetFileName(f);

                if (filenameOnly == "index.html" || filenameOnly.StartsWith(exeName)) continue;
                filesOrdered.Add(filenameOnly);

            }
            filesOrdered.Sort();
            foreach (var filenameOnly in filesOrdered)
            {
                sb.Append($"<a download='{filenameOnly}' href='{filenameOnly}'>{filenameOnly}</a></br>");
            }
            return sb.ToString();
        }
    }
}

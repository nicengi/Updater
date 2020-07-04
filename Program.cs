using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Nicengi.Update
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
            args = new string[]
            {
                "-SoftwareName:Test Soft",
                "-SoftwareGuid:3C96E550-3F80-407B-B8B2-97CFFF91CE4E",
                "-Ver:0.1.0.0",
                "-Args:{3}",
                //"-SilentMode",
            };
#endif
            Dictionary<string, string> arguments = new Dictionary<string, string>()
            {
                { "UpdateUrl", Properties.Settings.Default.UpdateUrl },
                { "ClassName", null },
                { "Pid" , "0" },
                { "Args", null },
            };
            foreach (string arg in args)
            {
                if (arg[0] == '-' || arg[0] == '/')
                {
                    int index = arg.IndexOf(":");
                    string key = arg.Substring(1, index == -1 ? arg.Length - 1 : index - 1);
                    string value = arg.Substring(index + 1, arg.Length - index - 1);

                    if (arguments.ContainsKey(key))
                        arguments.Remove(key);
                    arguments.Add(key, value);
                }
            }
            if (!arguments.ContainsKey("SoftwareName") || !arguments.ContainsKey("SoftwareGuid") || !arguments.ContainsKey("Ver"))
            {
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Updater updater = new Updater(new UpdaterArgs()
            {
                UpdateUrl = arguments["UpdateUrl"],
                SoftwareName = arguments["SoftwareName"],
                SoftwareGuid = arguments["SoftwareGuid"],
                CurrentVersion = arguments["Ver"],
                Arguments = arguments["Args"],
                ClassName = arguments["ClassName"],
                ProcessId = int.Parse(arguments["Pid"]),
                SilentMode = arguments.ContainsKey("SilentMode") || arguments.ContainsKey("sm"),
            });
            updater.Start();
            updater.Dispose();
        }
#if DEBUG
        public static void Test()
        {
            UpdateInfo info = new UpdateInfo()
            {
                SoftwareGuid = "3C96E550-3F80-407B-B8B2-97CFFF91CE4E",
                Description = "This is a test update package.",
                Version = "2.0.0.0",
                Location = @"http://localhost/update/3C96E550-3F80-407B-B8B2-97CFFF91CE4E/update.exe",
            };

            XmlSerialize("UpdateInfo.xml", info);
        }

        public static void XmlSerialize<T>(string path, T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (StreamWriter stream = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                serializer.Serialize(stream, obj, namespaces);
            }
        }
#endif
    }
}

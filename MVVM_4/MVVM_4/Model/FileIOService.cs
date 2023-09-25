using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_4.Model
{
    internal class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path) => PATH = path;

        public T Deserialize<T>()
        {
            if (!File.Exists(PATH))
                File.WriteAllText(PATH, "");
           return JsonConvert.DeserializeObject<T>(File.ReadAllText(PATH));
        }
        public void Serialize<T>(T tasks)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string json = JsonConvert.SerializeObject(tasks);
                writer.WriteLine(json);
            }
        }
    }
}

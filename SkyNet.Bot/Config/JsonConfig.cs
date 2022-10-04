using System.IO;
using Newtonsoft.Json;

namespace SkyNet.Bot.Config
{
    public class JsonConfig
    {
        private readonly string _configDirectory = Path.Combine(Directory.GetCurrentDirectory(), "configs");
        private readonly string _filePath;
        private readonly string _fileName;

        public bool HasConfig
        {
            get
            {
                return File.Exists(this._filePath);
            }
        }

        public T LoadConfig<T>()
        {
            string contents = File.ReadAllText(this._filePath);
            return JsonConvert.DeserializeObject<T>(contents);
        }

        public void SaveConfig(object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(this._filePath, json);
        }
        public JsonConfig(string fileName)
        {
            this._fileName = fileName;
            this._filePath = Path.Combine(this._configDirectory, _fileName);

            if (!Directory.Exists(this._configDirectory))
            {
                Directory.CreateDirectory(this._configDirectory);
            }
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PlaywrightFramework.Utils
{
    public static class TestDataHelper
    {
        private static readonly string DataDirectory =
            Path.Combine(Directory.GetCurrentDirectory(), "src", "TestData");

        /// <summary>Load a JSON file and return as JObject</summary>
        public static JObject LoadJson(string fileName)
        {
            var filePath = Path.Combine(DataDirectory, fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Test data file not found: {filePath}");

            var json = File.ReadAllText(filePath);
            return JObject.Parse(json);
        }

        /// <summary>Load and deserialize JSON file to a typed object</summary>
        public static T Load<T>(string fileName)
        {
            var filePath = Path.Combine(DataDirectory, fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Test data file not found: {filePath}");

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json)
                ?? throw new InvalidOperationException($"Failed to deserialize {fileName}");
        }

        /// <summary>Get a specific section of a JSON file</summary>
        public static T GetSection<T>(string fileName, string sectionName)
        {
            var jObject = LoadJson(fileName);
            var section = jObject[sectionName]
                ?? throw new KeyNotFoundException($"Section '{sectionName}' not found in {fileName}");

            return section.ToObject<T>()
                ?? throw new InvalidOperationException($"Failed to parse section '{sectionName}'");
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTemplate.BotConfigs;
internal class JsonHandler
{
    public static string Token { get; set; }
    public static string Prefix { get; set; }

    public static async Task ReadJsonAsync()
    {
        using (StreamReader reader = new StreamReader("config.json"))
        {
            string json = await reader.ReadToEndAsync();
            JsonStructure data = JsonConvert.DeserializeObject<JsonStructure>(json);

            Prefix = data.Prefix;
            Token = data.Token;
        }
    }
    public static async Task WriteJsonAsync(JsonStructure data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        using (StreamWriter writer = new StreamWriter("config.json"))
        {
            await writer.WriteAsync(json);
        }
    }
}

internal sealed class JsonStructure
{
    public string Token { get; set; }
    public string Prefix { get; set; }
}

using Newtonsoft.Json.Linq;

string _connectionString;
using (StreamReader r = new StreamReader("Config.json"))
{
    _connectionString = JObject.Parse(r.ReadToEnd())["ConnectionStrings"]["DefaultConnection"].Value<string>();
}
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace PlaywrightTests;

public static class Schemas{
    public static JSchema auth = JSchema.Parse(File.ReadAllText("./../../../Schemas/AuthSchema.json"));
    public static JSchema patch = JSchema.Parse(File.ReadAllText("./../../../Schemas/PatchSchema.json"));
}


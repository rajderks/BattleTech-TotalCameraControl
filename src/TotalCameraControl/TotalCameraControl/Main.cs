using Harmony;

namespace TotalCameraControl
{
    public static class TotalCameraControl
    {
        internal static string ModDirectory;
        internal static Settings GlobalSettings;

        public static void Init(string directory, string settingsJSON)
        {
            ModDirectory = directory;

            Logger.Reset();

            Settings settings = new Settings();
            try
            {
                settings = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(settingsJSON);
            }
            catch (System.Exception e)
            {
                Logger.Error(e);
            }

            GlobalSettings = settings;
           
            var harmony = HarmonyInstance.Create("io.github.rajderks.TotalCameraControl");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}

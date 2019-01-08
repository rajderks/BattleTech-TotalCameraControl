using Harmony;

namespace TotalCameraControl
{
    public static class TotalCameraControl
    {
        public static void Init(string directory, string settingsJSON)
        {
            var harmony = HarmonyInstance.Create("io.github.rajderks.TotalCameraControl");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}

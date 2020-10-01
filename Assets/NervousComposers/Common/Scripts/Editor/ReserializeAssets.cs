using UnityEditor;

namespace Jusw85.Common
{
    /// <summary>
    /// https://forum.unity.com/threads/performance-improvements-to-text-serialization-in-2019-3.661993/#post-5072351
    /// https://docs.unity3d.com/ScriptReference/AssetDatabase.ForceReserializeAssets.html
    /// Used mainly to regenerate .meta files for VCS after upgrading Unity version, to reduce cluttering deliberate
    /// file changes with changes from the Unity upgrade 
    /// </summary>
    public static class ReserializeAssets
    {
        [MenuItem("Tools/Nervous Composers/Reserialize Assets", false, 120)]
        private static void DoReserializeAssets()
        {
            AssetDatabase.ForceReserializeAssets();
        }
    }
}
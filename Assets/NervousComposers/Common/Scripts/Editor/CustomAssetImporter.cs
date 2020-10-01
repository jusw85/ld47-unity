using System;
using UnityEditor;
using UnityEngine;

namespace Jusw85.Common
{
//    public class CustomAssetImporter : AssetPostprocessor
//    {
//        [Obsolete("Use Presets and PresetManager in 2018.1")]
//        private void OnPreprocessTexture()
//        {
//            var importer = (TextureImporter) assetImporter;
//
//            TextureImporterSettings settings = new TextureImporterSettings();
//            importer.ReadTextureSettings(settings);
//            settings.textureType = TextureImporterType.Sprite;
//            settings.spritePixelsPerUnit = 64f;
//            settings.filterMode = FilterMode.Point;
//            importer.SetTextureSettings(settings);
//
//            TextureImporterPlatformSettings platformSettings = importer.GetDefaultPlatformTextureSettings();
//            platformSettings.textureCompression = TextureImporterCompression.Uncompressed;
//            importer.SetPlatformTextureSettings(platformSettings);
//        }
//
//        private void OnPostprocessTexture(Texture2D texture)
//        {
//        }
//
//        private void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
//        {
//        }
//    }
}
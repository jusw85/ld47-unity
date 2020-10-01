using SuperTiled2Unity.Editor;
using UnityEngine;
using UnityEngine.Tilemaps;

//[CustomTiledImporter]
//public class TiledImporter : ICustomTiledImporter

namespace Jusw85.ST2U
{
    public class CustomST2UImporter : CustomTmxImporter
    {
        private const float SkinWidth = 0.0f;

//
        public void CustomizePrefab(GameObject prefab)
        {
//        FixRendererMaterial(prefab);
//        AddSpikeController(prefab);
        }

        private void DisableTilemapRenderer(Transform t)
        {
            if (t != null)
            {
                t.GetComponent<TilemapRenderer>().enabled = false;
            }
        }

        public override void TmxAssetImported(TmxAssetImportedArgs args)
        {
            Debug.Log("Applying import settings");
            var a = args.ImportedSuperMap;
            DisableTilemapRenderer(a.transform.Find("Grid/Solid"));
            DisableTilemapRenderer(a.transform.Find("Grid/Spikes"));

//        Transform transform = a.transform.Find("Grid/Spikes/Collision_Default");
//        if (transform != null)
//        {
//            PolygonCollider2D[] polygonColliders = transform.GetComponent<CompositeCollider2D>()
//                .GetComponentsInChildren<PolygonCollider2D>();
//            foreach (PolygonCollider2D polygonCollider in polygonColliders)
//            {
//                Vector2[] points = polygonCollider.GetPath(0);
//                if (points.Length == 4)
//                {
//                    // assume no rotation
//                    Vector2 max, min;
//                    max = min = points[0];
//                    foreach (Vector2 point in points)
//                    {
//                        max = Vector2.Max(point, max);
//                        min = Vector2.Min(point, min);
//                    }
//
//                    points[0] = new Vector2(min.x + SkinWidth, min.y + SkinWidth);
//                    points[1] = new Vector2(min.x + SkinWidth, max.y - SkinWidth);
//                    points[2] = new Vector2(max.x - SkinWidth, max.y - SkinWidth);
//                    points[3] = new Vector2(max.x - SkinWidth, min.y + SkinWidth);
//                }
//
//                polygonCollider.SetPath(0, points);
//            }
//        }
        }

//    private void FixRendererMaterial(GameObject prefab)
//    {
//        
//        string[] xs = {"Background", "Background2", "Background3"};
//        foreach (var x in xs)
//        {
//            Transform transform = prefab.transform.Find(x);
//            if (transform != null)
//            {
//                foreach (MeshRenderer ms in transform.GetComponentsInChildren<MeshRenderer>())
//                {
//                    ms.sharedMaterial.shader = Shader.Find("Sprites/Diffuse");
//                }
//                
//            }
//        }
//    }
//
//    private void AddSpikeController(GameObject prefab)
//    {
//        Transform transform = prefab.transform.Find("Spikes/Collision");
//        if (transform != null)
//        {
//            transform.gameObject.AddComponent<SpikesController>();
////            PolygonCollider2D polygonCollider = transform.GetComponent<PolygonCollider2D>();
////            if (polygonCollider != null) {
////                for (int i = 0; i < polygonCollider.pathCount; i++) {
////                    Vector2[] points = polygonCollider.GetPath(i);
////                    if (points.Length == 4) { // assume no rotation
////                        Vector2 max, min;
////                        max = min = points[0];
////                        foreach (Vector2 point in points) {
////                            max = Vector2.Max(point, max);
////                            min = Vector2.Min(point, min);
////                        }
////                        points[0] = new Vector2(min.x + SkinWidth, min.y + SkinWidth);
////                        points[1] = new Vector2(min.x + SkinWidth, max.y - SkinWidth);
////                        points[2] = new Vector2(max.x - SkinWidth, max.y - SkinWidth);
////                        points[3] = new Vector2(max.x - SkinWidth, min.y + SkinWidth);
////                    }
////                    polygonCollider.SetPath(i, points);
////                }
////            }
//        }
//    }
//
////
////    //private Vector2[] expandBoxBounds(float v) {
////    //}
////
//    public void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties)
//    {
////        //printDictionary(customProperties);
//        if (customProperties.ContainsKey("type"))
//        {
//            var spawnType = customProperties["type"];
////            Debug.Log(spawnType);
//            if (spawnType != null)
//            {
//                var spawnedObject = SpawnGeneric(spawnType, gameObject);
//                if (spawnType.Equals("LevelTransition"))
//                {
//                    var level = customProperties["level"];
//                    spawnedObject.GetComponent<LevelTransition>().nextSceneName = level;
//                }
//            }
//        }
//    }
//
//    public GameObject SpawnGeneric(string spawnType, GameObject parent)
//    {
//        var prefabPath = "Assets/Project/Prefabs/" + spawnType + ".prefab";
//        var spawn = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
//
//        GameObject spawnInstance = (GameObject) GameObject.Instantiate(spawn);
//        spawnInstance.name = spawn.name;
//
//        var collider = parent.GetComponentInChildren<Collider2D>();
//        Vector3 offset;
//        if (collider != null)
//        {
//            offset = collider.bounds.center - parent.transform.position;
//            collider.enabled = false;
//        }
//        else
//        {
//            var child = parent.transform.GetChild(0);
//            offset = child.localPosition;
//            Object.DestroyImmediate(child.gameObject);
//        }
//
//        spawnInstance.transform.parent = parent.transform;
//        spawnInstance.transform.localPosition = offset;
//        return spawnInstance;
//    }
//
//    private void printDictionary(IDictionary<string, string> dictionary)
//    {
//        foreach (KeyValuePair<string, string> kvp in dictionary)
//        {
//            Debug.Log(kvp.Key + ": " + kvp.Value);
//        }
//    }
    }
}
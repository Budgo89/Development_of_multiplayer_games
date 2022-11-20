using UnityEngine;

namespace Tool
{
    internal static class ResourcesLoader
    {
        public static Sprite LoadSprite(ResourcePath path) =>
            LoadObject<Sprite>(path);

        public static GameObject LoadPrefab(ResourcePath path) =>
            LoadObject<GameObject>(path);
            
        public static TextAsset LoadTextAsset(ResourcePath path) =>
            LoadObject<TextAsset>(path);

        public static TObject LoadObject<TObject>(ResourcePath path) where TObject : Object =>
            Resources.Load<TObject >(path.PathResource);
    }
}

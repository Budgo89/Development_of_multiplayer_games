using UnityEngine;

namespace View
{
    public class AudioBackgroundView : MonoBehaviour
    {
        [Header("Teg")] 
        [SerializeField] private string _tag;

        public void Awake()
        {
            GameObject obj = GameObject.FindWithTag(this._tag);
            if (obj != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.gameObject.tag = this._tag;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}

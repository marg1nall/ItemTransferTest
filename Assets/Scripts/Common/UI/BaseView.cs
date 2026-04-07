using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{

    public abstract class BaseView : MonoBehaviour
    {
        protected Image Image;
        
        protected virtual void Awake()
        {
            Image = GetComponent<Image>();
        }
        
        public void SetColor(Color color)
        {
            if (Image != null)
            {
                Image.color = color;
            }
        }
    }
}

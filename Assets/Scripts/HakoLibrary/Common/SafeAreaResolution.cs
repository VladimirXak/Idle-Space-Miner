using UnityEngine;

namespace HakoLibrary.Common
{
    public class SafeAreaResolution : MonoBehaviour
    {
        private void Awake()
        {
            float offset = Screen.height - Screen.safeArea.height;

            if (offset == 0)
                return;

            RectTransform rectTransform = GetComponent<RectTransform>();

            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, rectTransform.offsetMax.y - offset);
        }
    }
}

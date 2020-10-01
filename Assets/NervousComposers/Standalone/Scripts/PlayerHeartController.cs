using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jusw85.Common
{
    public class PlayerHeartController : MonoBehaviour
    {
        public Sprite fullHeart;
        public Sprite halfHeart;
        public Sprite emptyHeart;
        public int maxHp = 5;

        private GameObject heartPanel;
        private readonly List<Image> heartsList = new List<Image>();

        private void Start()
        {
            var maxHearts = (maxHp + 1) / 2;
            for (int i = 0, offset = 10; i < maxHearts; i++, offset += 70)
            {
                GameObject obj = new GameObject();
                Image heart = obj.AddComponent<Image>();
                heart.name = "Heart";
                heart.transform.SetParent(transform);
                
                RectTransform rt = heart.gameObject.GetComponent<RectTransform>();
                rt.pivot = new Vector2(0, 1);
                rt.anchorMin = new Vector2(0, 1);
                rt.anchorMax = new Vector2(0, 1);
                rt.anchoredPosition = new Vector2(offset, -10);

                heartsList.Add(heart);
            }

            ChangeHp(maxHp);

            foreach (Image img in heartsList)
            {
                img.SetNativeSize();
            }
        }

        public void ChangeHp(int hp)
        {
            hp = Mathf.Clamp(hp, 0, maxHp);
            foreach (Image heart in heartsList)
            {
                if (hp <= 0)
                {
                    heart.sprite = emptyHeart;
                }
                else if (hp <= 1)
                {
                    heart.sprite = halfHeart;
                }
                else
                {
                    heart.sprite = fullHeart;
                }

                hp -= 2;
            }
        }
    }
}
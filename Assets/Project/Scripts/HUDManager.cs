using Jusw85.Common;
using Prime31.ZestKit;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text youwin;
    [SerializeField] private Text youlose;

    public void SetHealth(int health)
    {
        healthText.text = "Crystal Health: " + health;
    }

    public void SetTime(int time)
    {
        timeText.text = "Time: " + time;
    }

    public void Win()
    {
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            IntTween tween = new IntTween();
            tween.initialize(new FontSizeTarget(youwin), 80, 1f);
            tween.start();
        }, 4f));
    }
    
    public void Lose()
    {
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            IntTween tween = new IntTween();
            tween.initialize(new FontSizeTarget(youlose), 80, 1f);
            tween.start();
        }, 4f));
    }

    class FontSizeTarget : AbstractTweenTarget<Text, int>
    {
        public override void setTweenedValue(int value)
        {
            _target.fontSize = value;
        }

        public override int getTweenedValue()
        {
            return _target.fontSize;
        }

        public FontSizeTarget(Text text)
        {
            _target = text;
        }
    }
}
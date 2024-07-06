using DG.Tweening;
using UnityEngine;

public class PaperParabolaAnimation : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    public void StartAnimation()
    {
        DOTween.To(setter: value =>
        {
            transform.position = Parabola(startPosition.position, endPosition.position, 120, value);
        }, startValue: 0, endValue: 1, duration: 2f)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        float Func(float x) => 4 * (-height * x * x + height * x);

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t));
    }
}

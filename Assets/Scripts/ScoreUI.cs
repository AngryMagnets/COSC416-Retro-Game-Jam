using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI current
                          , toUpdate;
    [SerializeField] 
    Transform container;

    [SerializeField]
    private Ease AnimationCurve;

    [SerializeField]
    private float duration;

    private float containerInitPosition
                , moveAmount;

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("0");
        toUpdate.SetText("0");
        containerInitPosition = container.localPosition.y;
        moveAmount = current.rectTransform.rect.height;

    }

    void UpdateScore (int score)
    {
        toUpdate.SetText($"{score}");
        container.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(AnimationCurve);
        StartCoroutine(ResetContainer(score));
    }

    private IEnumerator ResetContainer (int score) 
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{score}");
        Vector3 localPosition = container.localPosition;
        container.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);
    }
}

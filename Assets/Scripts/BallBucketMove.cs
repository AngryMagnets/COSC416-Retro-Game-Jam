using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BallBucketMove : MonoBehaviour
{
    [SerializeField] private float leftPosition, rightPosition, duration;
    [SerializeField] private Ease movementCurve;

    private void Start()
    {
        StartCoroutine(movement());
    }

    private IEnumerator movement()
    {
        while (true)
        {
            transform.DOLocalMoveX(leftPosition, duration).SetEase(movementCurve);
            yield return new WaitForSeconds(duration);
            transform.DOLocalMoveX(rightPosition, duration).SetEase(movementCurve);
            yield return new WaitForSeconds(duration);
        }
    }
}

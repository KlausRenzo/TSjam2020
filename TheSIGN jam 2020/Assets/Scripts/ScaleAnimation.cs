using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 maxScaleAmount;
    [SerializeField] private Vector3 minScaleAmount;

    [SerializeField] private float animSpeed;
    [SerializeField] private Ease ease;

    private IEnumerator Start()
    {
        while (true)
        {
            GoBig();
            yield return new WaitForSeconds(animSpeed);
            GoSmall();
            yield return new WaitForSeconds(animSpeed);
        }
    }

    private void GoBig()
    {
        transform.DOScale(maxScaleAmount, animSpeed).SetEase(ease);
    }

    private void GoSmall()
    {
        transform.DOScale(minScaleAmount, animSpeed).SetEase(ease);
    }

    
}

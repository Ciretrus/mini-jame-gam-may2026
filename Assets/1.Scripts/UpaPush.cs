using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpaPush : MonoBehaviour
{
    [SerializeField] private Vector2 m_offset = new Vector2(0,0.1f);
    [SerializeField] private float m_tweenTime = 0.2f;
    [SerializeField] private Transform m_upaUp;
    private bool m_active = false;

    private void OnMouseUp()
    {
        if (!m_active)
        {
            m_active = true;
            m_upaUp.DOMove((Vector2)m_upaUp.position + m_offset, m_tweenTime).SetLoops(4, LoopType.Yoyo)
                .SetEase(Ease.InOutSine).OnComplete(() => m_active = false);
        }
    }

}

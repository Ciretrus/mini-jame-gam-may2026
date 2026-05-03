using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreGlow : MonoBehaviour
{
    [SerializeField] private Color m_brightness = new Color(0.3f,0.3f,0.3f,1f);
    void Start()
    {
        TextMeshProUGUI score = GetComponent<TextMeshProUGUI>();
        score.fontMaterial.DOColor(m_brightness, "_OutlineColor", 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }


}

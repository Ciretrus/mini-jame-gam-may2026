using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButtons : MonoBehaviour
{
    [SerializeField] private Microwave m_microwave;
    [SerializeField] private GameObject m_light;
    [SerializeField] private float m_rotateSpeed = 10f;
    [SerializeField] private float m_timeSpeed = 1f;
    [SerializeField] private Color m_colorHue = Color.white;
    [SerializeField] private AudioSource m_audioSource;
    private SpriteRenderer m_sprite;
    private Color m_startColor;

    private void Awake()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_startColor = m_sprite.color;
    }

    private void OnMouseDown()
    {
        if (m_microwave.m_isClosed)
        {
            m_audioSource.Play();
        }
    }
    private void OnMouseDrag()
    {
        BananaSettings bananaSettings = m_microwave.GetBananaSettings();
        //Debug.Log(m_microwave.m_isClosed);
        if (m_microwave.m_isClosed)
        {
            m_sprite.color = m_colorHue;
            m_light.SetActive(true);
            if (bananaSettings != null)
            {
                Vector3 rotVector = new Vector3(0, 0, m_rotateSpeed * Time.deltaTime);
                bananaSettings.transform.Rotate(rotVector);

                float freshness = bananaSettings.GetFreshness();

                bananaSettings.ChangeFreshness(freshness + m_timeSpeed * Time.deltaTime);
                Debug.Log("freshness" + freshness);
            }
        }
        
    }
    private void OnMouseUp()
    {
        m_audioSource.Stop();
        m_light.SetActive(false);
        m_sprite.color = m_startColor;
        
    }
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float m_timer = 120f;
    [SerializeField] private Camera m_camera;
    [SerializeField] private float m_punishment = 0.5f;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioSource m_clockSound;
    private float m_timeSpeed = 1f;
    private bool m_isDrag = false;
    private bool m_isStoped = true;
    public event Action OnStoped;
    
    private float m_currentAngle = 360f;



    public void Update()
    {
        if (!m_isDrag && !m_isStoped)
        {
            float rotationStep = (360f / m_timer) * m_timeSpeed * Time.deltaTime;

            m_currentAngle -= rotationStep;

            if (m_currentAngle <= 0f)
            {
                m_currentAngle = 0f;
                m_isStoped = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                OnStoped?.Invoke();
                m_audioSource.Play();
                m_clockSound.Pause();
                return;
            }

            transform.rotation = Quaternion.Euler(0, 0, m_currentAngle);
        }
        else if (m_isDrag)
        {
            float rawZ = transform.eulerAngles.z;

            if (rawZ <= 1f)
            {
                m_currentAngle = 360f;
            }
            else
            {
                m_currentAngle = rawZ;
            }
        }
    }


    void OnMouseDown()
    {
        m_isDrag = true;
        m_clockSound.Pause();
    }
    private void OnMouseDrag()
    {
        if (!m_isStoped)
        {
            Vector3 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, 0.15f);

            transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
        }
    }
    private void OnMouseUp()
    {
        m_clockSound.UnPause();
        m_isDrag = false;
        m_timeSpeed += m_punishment;
        m_clockSound.pitch = 1f + m_timeSpeed/5f;
    }

    public void Restart()
    {
        m_clockSound.Play();
        m_isStoped = false;
        m_currentAngle = 360;
        transform.rotation = Quaternion.Euler(0, 0, m_currentAngle);
        m_timeSpeed = 1f;
        m_clockSound.pitch = m_timeSpeed;

    }


}

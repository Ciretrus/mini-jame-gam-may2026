using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChangeDoor : MonoBehaviour
{
    [SerializeField] private GameObject m_doorDisable;
    [SerializeField] private GameObject m_doorEnable;
    [SerializeField] private Color m_colorHue = Color.white;
    [SerializeField] private bool m_isOpen = true;
    public event Action<bool> DoorChanged;
    private SpriteRenderer m_sprite;
    private Color m_startColor;
    

    private void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_startColor = m_sprite.color;
    }

    private void OnMouseUp()
    {
        m_sprite.color = m_startColor;
        //Debug.Log("Door closed");
        DoorChanged?.Invoke(m_isOpen);
        m_doorEnable.SetActive(true);
        m_doorDisable.SetActive(false);
        
    }

    private void OnMouseDown()
    {

        //Debug.Log("selectButton");
        m_sprite.color = m_colorHue;

    }
}

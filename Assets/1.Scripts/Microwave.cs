using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Microwave : MonoBehaviour
{
    public bool m_isFull;
    public bool m_isClosed;
    private BananaSettings m_bananaSettings;
    private BananaMove m_bananaMove;
    [SerializeField] private ChangeDoor m_changeDoorEvent1;
    [SerializeField] private ChangeDoor m_changeDoorEvent2;

    private void OnEnable()
    {
        m_changeDoorEvent1.DoorChanged += BananaCanMove;
        m_changeDoorEvent2.DoorChanged += BananaCanMove;
    }

    private void OnDisable()
    {
        m_changeDoorEvent1.DoorChanged -= BananaCanMove;
        m_changeDoorEvent2.DoorChanged -= BananaCanMove;
    }
    public void BananaCanMove(bool isClosed)
    {
        m_isClosed = isClosed;
        if (m_bananaMove != null)
        {
            m_bananaMove.canMove = !isClosed;
            
        }
    }

    public void SetBanana(BananaMove bananaMove, BananaSettings bananaSettings)
    {
        //
        if (bananaSettings != null && m_bananaSettings != bananaSettings)
        {
            m_bananaSettings = bananaSettings;
            m_bananaSettings.OnDestroyed += BananaDied;
            m_bananaMove = bananaMove;
        }
    }
    private void BananaDied(BananaSettings bananaSettings)
    {
        m_isFull = false;
        bananaSettings.OnDestroyed -= BananaDied;
    }
    public BananaSettings GetBananaSettings()
    {
        return m_bananaSettings;
    }
    public BananaMove GetBananaMove()
    {
        return m_bananaMove;
    }



}

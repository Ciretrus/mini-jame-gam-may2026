using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class BananaMove : MonoBehaviour
{
    public float m_followSpeed = 10f;
    public Camera m_camera;
    public Transform m_innerMicrowavePosition;
    public float m_timeTween = 0.5f;
    //private HingeJoint2D m_joint;
    private Rigidbody2D m_rb;
    private bool m_isInMicrowave;
    public bool canMove;
    public event Action InMicrowave;
    void Start()
    {
        canMove = true;
        //m_joint = GetComponent<HingeJoint2D>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDrag()
    {
        if (canMove)
        {
            m_rb.bodyType = RigidbodyType2D.Dynamic;
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - m_rb.position;
            m_rb.velocity = direction * m_followSpeed;
            
        }
        
        //m_joint.enabled = true;
        //m_joint.connectedAnchor = mousePos;
        //.anchor = transform.InverseTransformPoint(mousePos);

        
    }
    private void OnMouseUp()
    {
        //m_joint.enabled = false;
        if (m_isInMicrowave)
        {
            m_rb.bodyType = RigidbodyType2D.Static;
            Vector3 newPos = m_innerMicrowavePosition.position;
            transform.DOMove(new Vector3(newPos.x,newPos.y,transform.position.z), m_timeTween);
            InMicrowave?.Invoke();
        }
        else
        {
            m_rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InnerMicrowave"))
        {
            //other.gameObject.GetComponent<>;
            m_isInMicrowave = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InnerMicrowave"))
        {
            Debug.Log("exit MW");
            m_isInMicrowave = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tramp : MonoBehaviour
{
    public AudioSource m_AudioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Good"))
        {
            m_AudioSource.Play();
        }
    }
}

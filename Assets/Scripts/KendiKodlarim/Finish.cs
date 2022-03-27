using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public ParticleSystem[] efekt;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(efektOynat());
            Debug.Log("A");
        }
    }

    IEnumerator efektOynat()
    {

        for (int i = 0; i < efekt.Length; i++)
        {
            efekt[i].Play();
            yield return new WaitForSeconds(.35f);
        }
    }
}

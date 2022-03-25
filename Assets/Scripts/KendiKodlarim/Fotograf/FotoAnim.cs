using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotoAnim : MonoBehaviour
{
    [Header("FotografIcin")]
    private FotografController fotografController;


    void Start()
    {
        fotografController = GameObject.FindObjectOfType<FotografController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResimEkle()
    {
        StartCoroutine(Geciktir());
    }

    IEnumerator Geciktir()
    {
        yield return new WaitForSeconds(.02f);
        fotografController.FotografYerineKoy();
        transform.parent.transform.gameObject.SetActive(false);
    }
}

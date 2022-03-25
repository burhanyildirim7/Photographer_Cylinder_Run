using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterPaketiMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Header("Durumlar")]
    public bool kosuDurumu;

    private WaitForSeconds beklemeSuresi0 = new WaitForSeconds(.1f);
    private WaitForSeconds beklemeSuresi1 = new WaitForSeconds(1f);

    void Start()
    {
        OyunBasiAyarlari();

    }

    public void OyunBasiAyarlari()
    {
        kosuDurumu = false;

        StartCoroutine(OyunBasladiMiKontrol());
    }



    void FixedUpdate()
    {
        if (kosuDurumu)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
    }

    IEnumerator OyunBasladiMiKontrol()
    {
        while(!kosuDurumu)
        {
            if(GameController.instance.isContinue)
            {
                kosuDurumu = true;
            }
            yield return beklemeSuresi0;
        }
    }

    IEnumerator SaniyelikDurdur()
    {
        kosuDurumu = false;
        yield return beklemeSuresi1;
        kosuDurumu = true;
    }
}

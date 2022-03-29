using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public float donusHizi = 100;
    private float rotasyonKatSayi;
    private float currentPositionX;
    private float lastPositionX;

    public bool haraketEdebilir;

    void Start()
    {
        donusHizi = GameObject.FindObjectOfType<GameController>().platformDonusHizi;
        currentPositionX = 0;
        lastPositionX = 0;

        haraketEdebilir = true;
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.touchCount > 0 && haraketEdebilir)
         {
             Touch touch = Input.GetTouch(0);
             if (Input.GetTouch(0).phase == TouchPhase.Moved)
             {
                 transform.Rotate(Vector3.forward * touch.deltaPosition.x * Time.deltaTime * -20);
             }
         }
         */
        if (Input.GetMouseButtonDown(0) && haraketEdebilir)
        {
            currentPositionX = Input.mousePosition.x;
            lastPositionX = Input.mousePosition.x;
            rotasyonKatSayi = lastPositionX - currentPositionX;
        }

        if (Input.GetMouseButton(0) && haraketEdebilir)
        {
            currentPositionX = Input.mousePosition.x;
            rotasyonKatSayi = lastPositionX - currentPositionX;
            lastPositionX = Input.mousePosition.x;
            transform.Rotate(Vector3.forward * rotasyonKatSayi * Time.deltaTime * donusHizi);
        }

        if (Input.GetMouseButtonUp(0) && haraketEdebilir)
        {
            currentPositionX = 0;
            lastPositionX = 0;
            rotasyonKatSayi = 0;
        }
    }

    public void HaraketiDurdur()
    {
        haraketEdebilir = false;
        StartCoroutine(PlatformDonusuGeciktir());
    }

    private IEnumerator PlatformDonusuGeciktir()
    {
        yield return new WaitForSeconds(1);
        haraketEdebilir = true;
    }
}

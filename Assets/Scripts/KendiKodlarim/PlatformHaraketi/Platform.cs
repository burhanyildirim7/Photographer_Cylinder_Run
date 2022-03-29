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

    void Start()
    {
        donusHizi = GameObject.FindObjectOfType<GameController>().platformDonusHizi;
        currentPositionX = 0;
        lastPositionX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.touchCount > 0)
         {
             Touch touch = Input.GetTouch(0);
             if (Input.GetTouch(0).phase == TouchPhase.Moved)
             {
                 transform.Rotate(Vector3.forward * touch.deltaPosition.x * Time.deltaTime * -20);
             }
         }*/

        if (Input.GetMouseButtonDown(0))
        {
            currentPositionX = Input.mousePosition.x;
            lastPositionX = Input.mousePosition.x;
            rotasyonKatSayi = lastPositionX - currentPositionX;
        }

        if (Input.GetMouseButton(0))
        {
            currentPositionX = Input.mousePosition.x;
            rotasyonKatSayi = lastPositionX - currentPositionX;
            lastPositionX = Input.mousePosition.x;
            transform.Rotate(Vector3.forward * rotasyonKatSayi * Time.deltaTime * donusHizi);
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentPositionX = 0;
            lastPositionX = 0;
            rotasyonKatSayi = 0;
        }
    }
}

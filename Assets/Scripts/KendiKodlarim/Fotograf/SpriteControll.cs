using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControll : MonoBehaviour
{

    Vector2 hedef;
    Animation anim;

    void Awake()
    {
        anim = GetComponent<Animation>();
        hedef = transform.position;
    }


    public void SpriteAyarlari()
    {
        transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        StartCoroutine(SpriteKonumaGonder());
    }

    IEnumerator SpriteKonumaGonder()
    {
        anim.Play("Sprite");
        yield return new WaitForSeconds(.25f);

        while (Vector2.Distance(transform.position, hedef) >= .2f)
        {
            transform.position = Vector2.Lerp(transform.position, hedef, Time.deltaTime * 25);
            yield return null;
        }

    }


}

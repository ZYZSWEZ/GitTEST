using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeArea : MonoBehaviour
{
    // Start is called before the first frame update

    //public Vector3 pos;
    //public float size;
    //public Color color;
    //private CameraCtroller cc; 

    //private void Start()
    //{
    //    cc = Camera.main.GetComponent<CameraCtroller>();
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.name == "Gris")
    //    {

    //        if (pos != Vector3.zero)
    //        {
    //            cc.SetPos(pos);
    //        }

    //        if (size != 0)
    //        {
    //            cc.SetSize(size);
    //        }

    //        if (color != Color.clear)
    //        {
    //            cc.SetColor(color);
    //        }

    //    } 
    //}


    public Vector3 pos;
    public float size;
    public Color color;


    private CameraCtroller cc;

    private void Start()
    {
        cc = Camera.main.GetComponent<CameraCtroller>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gris")
        {
            if (pos != Vector3.zero)
            {
                cc.SetPos(pos);
            }
            if (size != 0)
            {
                cc.SetSize(size);
            }
            if (color != Color.clear)
            {
                cc.SetColor(color);
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Gris")
        {
            cc.SetPos(new Vector3(8.98f, 5.8f, 20f));
            cc.SetSize(5);
        }

    }




}

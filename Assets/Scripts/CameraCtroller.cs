using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtroller : MonoBehaviour
{
    // Start is called before the first frame update

    //private Vector3 targetPos;
    //private bool startPosLerp;
    //private float targetSize;
    //private bool startSizeLerp;
    //private Color targetColor;
    //private bool startColorLerp;

    //private float lerpSpeed;

    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void LateUpdate()
    //{

    //    //位置插值
    //    if (startPosLerp)
    //    {
    //        if (Vector3.Distance(transform.localPosition, targetPos) > 0.1f)
    //        {
    //            transform.localPosition= Vector3.Lerp(transform.localPosition,targetPos,lerpSpeed*Time.deltaTime);
    //        }
    //        else 
    //        {
    //            startPosLerp = false;
    //        }

    //    }

    //    //尺寸插值
    //    if (startSizeLerp)
    //    {
    //        if (Mathf.Abs(Camera.main.orthographicSize - targetSize) > 0.01f)
    //        {
    //            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, lerpSpeed * Time.deltaTime);
    //        }
    //        else
    //        {
    //            startSizeLerp = false;
    //        }

    //    }
    //    //颜色
    //    if (startColorLerp)
    //    {

    //        if (!Color.Equals(Camera.main.backgroundColor , targetColor) )
    //        {
    //            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, targetColor, lerpSpeed * Time.deltaTime);
    //        }
    //        else
    //        {
    //            startColorLerp = false;
    //        }

    //    }
    //}

    //public void SetPos(Vector3 pos)
    //{
    //    startPosLerp = true;
    //    targetPos = pos;
    //}

    //public void SetSize(float size)
    //{
    //    startSizeLerp = true;
    //    targetSize = size;
    //}

    //public void SetColor(Color color)
    //{
    //    startColorLerp = true;
    //    targetColor = color;
    //}

    private Vector3 targetPos;
    private bool startPosLerp;
    private float targetSize;
    private bool startSizeLerp;
    private Color targetColor;
    private bool startColorLerp;

    private float lerpSpeed;

    void LateUpdate()
    {
        if (startPosLerp)
        {
            if (Vector3.Distance(transform.localPosition, targetPos) > 0.1f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition,targetPos,lerpSpeed*Time.deltaTime);
            }
            else
            {
                startPosLerp = false;
            }
        }

        if (startSizeLerp)
        {
            if (Mathf.Abs(Camera.main.orthographicSize - targetSize) > 0.01f)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, lerpSpeed * Time.deltaTime);
            }
            else
            {
                startSizeLerp = false;
            }
        }


        if (startColorLerp)
        {
            if (!Color.Equals(Camera.main.backgroundColor , targetColor) )
            {
                Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, targetColor, lerpSpeed * Time.deltaTime);
            }
            else
            {
                startColorLerp = false;
            }
        }
    }




    public void SetPos(Vector3 pos)
    {
        startPosLerp = true;
        targetPos = pos; 
    }

    public void SetSize(float size)
    {
        startSizeLerp = true;
        targetSize = size;
    }

    public void SetColor(Color color)
    {
        startColorLerp = true;
        targetColor = color;
    }

}

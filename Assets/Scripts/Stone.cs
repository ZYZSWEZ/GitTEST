using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public GameObject tearsGo;

    private Transform[] roadsTrans;

    private int tearNum;
    [HideInInspector]
    public int stopTearNum;
    private GameObject grisGO;
    private bool startEndScript;
    private AudioSource audioSource;
    private AudioClip audioClip;


    void Start()
    {
        tearsGo = Resources.Load<GameObject>("Prefabs/Tear");
        Transform pointTrans = transform.Find("Points");
        roadsTrans = new Transform[pointTrans.childCount];
        for (int i = 0; i < roadsTrans.Length; i++)
        {
            roadsTrans[i] = pointTrans.GetChild(i);             
        }
        //foreach (var item in roadsTrans)
        //{
        //    Debug.Log(item);
        //}
        grisGO = GameObject.Find("Gris");
        Invoke("StartCreatingTears",4);
        audioClip = Resources.Load<AudioClip>(@"Gris/Audioclips/BG2");
        audioSource = GameObject.Find("Evn").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (tearNum >= 5)
        {
            CancelInvoke();
            tearNum = 0;
        }
        if (stopTearNum >= 5&&!startEndScript)
        {
            EndScriptOneSet();
            startEndScript = true;
        }
        if (startEndScript)
        {
            //需要渐变处理的内容

            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, 
                new Vector3(-13.4f, 6.6f, 2),1*Time.deltaTime);
            audioSource.volume -= Time.deltaTime*0.3f;
            if (Vector3.Distance(Camera.main.transform.localPosition, new Vector3(-13.4f, 6.6f, 2))<=0.01f)
            {
                //移动结束

                audioSource.clip = audioClip;
                audioSource.volume = 1;
                audioSource.Play();
                audioSource.loop = true;
                Destroy(this);
            }
        }
    }

    private void StartCreatingTears()
    {
        InvokeRepeating("CreateTear", 0, 2);
    }

    private void CreateTear() 
    {
        
        GameObject go =  Instantiate(tearsGo, roadsTrans[0].position, Quaternion.identity);
        Tear tear = go.GetComponent<Tear>();
        tear.roadTrans = roadsTrans;
        tear.finalIndex = roadsTrans.Length - 1-tearNum;
        tearNum++;
        tear.stone = this;
    }

    private void EndScriptOneSet()
    {
        grisGO.AddComponent<Gris>();
        grisGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Camera.main.transform.SetParent(grisGO.transform);
        //Camera.main.transform.localPosition = new Vector3(-13.4f, 6.6f, 2);
        audioSource = GameObject.Find("Evn").GetComponent<AudioSource>();

    }

}

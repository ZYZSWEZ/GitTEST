using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPlace : MonoBehaviour
{
    

    private Transform grisTrans;
    private AudioClip audioClipToNextLevel;
    private AudioClip audioClipNormal;
    private AudioClip audioClipJudge;
    private AudioSource audioSource;
    private AsyncOperation ao;

    void Start()
    {
       

        grisTrans = GameObject.Find("Gris").transform;
        audioClipToNextLevel = Resources.Load<AudioClip>("Gris/Audioclips/BG4");
        audioClipJudge = Resources.Load<AudioClip>("Gris/Audioclips/BG3");
        audioClipNormal = Resources.Load<AudioClip>("Gris/Audioclips/BG2");
        audioSource = GameObject.Find("Evn").GetComponent<AudioSource>();
    }

   
    void Update()
    {
        
    }


 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gris")
        {
            if (audioSource.isPlaying)
            {
                if (JudgeTearNumEnough())
                {
                    if (audioSource.clip.name != audioClipToNextLevel.name)
                    {
                        StartCoroutine(ToNextLevel());
                        //Invoke("PlayNormalClip", 24);
                    }
                }
                else
                {
                    if (audioSource.clip.name != audioClipJudge.name)
                    {
                        audioSource.clip = audioClipJudge;
                        audioSource.loop = false;
                        audioSource.Play();
                        Invoke("PlayNormalClip", 30);
                    }
                }
            }


        }

    }


    //判断当前是否通关
    private bool JudgeTearNumEnough()
    {
        for (int i = 0; i < grisTrans.childCount-1 ; i++)
        {
            if (grisTrans.GetChild(i).childCount <= 0)
            {
                //有空位置代表眼泪没有收集完毕
                return false;
            }
        }
        //已经没有空位置，通关
        return true;
    }

    private void PlayNormalClip()
    {
        audioSource.clip = audioClipNormal;

        audioSource.Play();

    }


    IEnumerator ToNextLevel()
    {
        //异步加载第二关场景
        ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;
        while (ao.progress < 0.9f)//等待场景加载完成
        {
            yield return null;
        }

        //玩家失去对Gris的控制权
        Destroy(grisTrans.GetComponent<Gris>());
        ToNextLevelScript toNextLevel = grisTrans.gameObject.AddComponent<ToNextLevelScript>();
        toNextLevel.SetRigidBodyType(RigidbodyType2D.Kinematic);
        //人物切换剧情动画状态1
        //人物升空 切换到剧情动画状态2
        
        toNextLevel.StartMove(new Vector3(311,3,0));


        yield return new WaitForSeconds(2);//等待人物升空


        //生成小花
        Instantiate(Resources.Load<GameObject>("Prefabs/Red"));

        //GameObject red = GameObject.Instantiate(Resources.Load("Prefabs/Red")) as GameObject;
        //red.transform.position = new Vector3(311, 3, 0);
        //yield return null;



        //播放音乐
        audioSource.clip = audioClipToNextLevel;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(1);//等待小花移动并消失
        //播放切换场景前的特效，并拉远摄像机视角
        yield return new WaitForSeconds(10);
        //人物下落 311 -2.2
        toNextLevel.StartMove(new Vector3(311, -3, 0));
        yield return new WaitForSeconds(2);
        //切换到场景2
        ao.allowSceneActivation = true;

    }


}

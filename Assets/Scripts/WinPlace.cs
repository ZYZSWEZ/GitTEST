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


    //�жϵ�ǰ�Ƿ�ͨ��
    private bool JudgeTearNumEnough()
    {
        for (int i = 0; i < grisTrans.childCount-1 ; i++)
        {
            if (grisTrans.GetChild(i).childCount <= 0)
            {
                //�п�λ�ô�������û���ռ����
                return false;
            }
        }
        //�Ѿ�û�п�λ�ã�ͨ��
        return true;
    }

    private void PlayNormalClip()
    {
        audioSource.clip = audioClipNormal;

        audioSource.Play();

    }


    IEnumerator ToNextLevel()
    {
        //�첽���صڶ��س���
        ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;
        while (ao.progress < 0.9f)//�ȴ������������
        {
            yield return null;
        }

        //���ʧȥ��Gris�Ŀ���Ȩ
        Destroy(grisTrans.GetComponent<Gris>());
        ToNextLevelScript toNextLevel = grisTrans.gameObject.AddComponent<ToNextLevelScript>();
        toNextLevel.SetRigidBodyType(RigidbodyType2D.Kinematic);
        //�����л����鶯��״̬1
        //�������� �л������鶯��״̬2
        
        toNextLevel.StartMove(new Vector3(311,3,0));


        yield return new WaitForSeconds(2);//�ȴ���������


        //����С��
        Instantiate(Resources.Load<GameObject>("Prefabs/Red"));

        //GameObject red = GameObject.Instantiate(Resources.Load("Prefabs/Red")) as GameObject;
        //red.transform.position = new Vector3(311, 3, 0);
        //yield return null;



        //��������
        audioSource.clip = audioClipToNextLevel;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(1);//�ȴ�С���ƶ�����ʧ
        //�����л�����ǰ����Ч������Զ������ӽ�
        yield return new WaitForSeconds(10);
        //�������� 311 -2.2
        toNextLevel.StartMove(new Vector3(311, -3, 0));
        yield return new WaitForSeconds(2);
        //�л�������2
        ao.allowSceneActivation = true;

    }


}

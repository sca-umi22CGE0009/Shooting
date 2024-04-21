using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private bool isPlayerOne;
    public int onePushCount;
    public float longPushTime;
    [SerializeField] private float interval;
    [SerializeField] private GameObject[] bullet = new GameObject[3];

    [SerializeField] private float bulletSpeed;

    [SerializeField]
    AudioClip audioClip;

    public static float bs;

    private bool[] bulletShot = new bool[3];

    public float intervalCount;

    private AudioSource audioSource;

    public GameObject[] Bullets
    {   
        get { return bullet; }
        set { bullet = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("SE").GetComponent<AudioSource>();
        onePushCount = 0;
        longPushTime = 0.0f;
        intervalCount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        bs = bulletSpeed;
        switch (game_stat)
        {
            case GameStat.PLAY:
                {
                    if (TimeScoreCounter.timeCountState != TimeScoreCounter.TimeCountState.STOP)
                    {
                        intervalCount += Time.deltaTime;
                        if (Input.GetKeyDown(KeyCode.Space) && intervalCount >= interval)
                        {
                            onePushCount += 1;
                            intervalCount = 0.0f;
                            bulletShot[0] = true;
                            if (onePushCount % 3 != 0) StartCoroutine(ShotBulletOne());
                            else ShotBulletTwo();

                        }

                        if (Input.GetKey(KeyCode.Space))
                        {
                            longPushTime += Time.deltaTime;
                            if (longPushTime >= 0)
                            {
                                if (!bulletShot[0])
                                {
                                    StartCoroutine(ShotBulletOne());
                                    bulletShot[0] = true;
                                }
                            }
                            if (longPushTime >= interval * 1)
                            {
                                if (!bulletShot[1])
                                {
                                    StartCoroutine(ShotBulletOne());
                                    bulletShot[1] = true;
                                }
                            }
                            if (longPushTime >= interval * 2)
                            {
                                if (!bulletShot[2])
                                {
                                    ShotBulletThree();
                                    bulletShot[2] = true;
                                }
                            }
                            if (longPushTime >= interval * 3)
                            {
                                for (int i = 0; i < bulletShot.Length; ++i)
                                {
                                    bulletShot[i] = false;
                                }
                                longPushTime = 0;
                            }
                        }
                        else
                        {
                            if (intervalCount >= interval)
                            {
                                for (int i = 0; i < bulletShot.Length; ++i)
                                {
                                    bulletShot[i] = false;
                                }
                                longPushTime = 0;
                            }
                        }
                    }
                }
                break;
        }

    }

    IEnumerator ShotBulletOne()
    {
        if(UIManager.isEnergyFunnel)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.2f);
                audioSource.PlayOneShot(audioClip);

                Instantiate(bullet[0], PlayerController.pos + new Vector3(-10, 30, 0), Quaternion.identity, transform);
                Instantiate(bullet[0], PlayerController.pos + new Vector3(10, 30, 0), Quaternion.identity, transform);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.2f);
                audioSource.PlayOneShot(audioClip);

                Instantiate(bullet[0], PlayerController.pos + new Vector3(0, 30, 0), Quaternion.identity, transform);
            }
        }
    }

    private void ShotBulletTwo()
    {
        if(UIManager.isEnergyFunnel)
        {
            audioSource.PlayOneShot(audioClip);

            Instantiate(bullet[1], PlayerController.pos + new Vector3(-10, 30, 0), Quaternion.identity, transform);
            Instantiate(bullet[1], PlayerController.pos + new Vector3(10, 30, 0), Quaternion.identity, transform);
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
            Instantiate(bullet[1], PlayerController.pos + new Vector3(0, 30, 0), Quaternion.identity, transform);
        }
    }

    private void ShotBulletThree()
    {
        if(UIManager.isEnergyFunnel)
        {
            audioSource.PlayOneShot(audioClip);

            Instantiate(bullet[2], PlayerController.pos + new Vector3(-10, 30, 0), Quaternion.identity, transform);
            Instantiate(bullet[2], PlayerController.pos + new Vector3(10, 30, 0), Quaternion.identity, transform);
        }
        else
        {
            audioSource.PlayOneShot(audioClip);

            Instantiate(bullet[2], PlayerController.pos + new Vector3(0, 30, 0), Quaternion.identity, transform);
        }
    }
}

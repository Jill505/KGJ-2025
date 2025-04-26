using NUnit.Framework;
using System.Collections;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
    static public bool IsGameGoing = true;
    static public bool AllowEle = true;

    [Header("Game System")]
    public int playerStudentIndex;


    public PlayerControl playerControl;

    [Header("Fight System")]
    public GameObject FightTarget;
    public GameObject FightCanvas;

    public Image fightFill;

    public GameObject linerPrefab;
    public GameObject liner;

    public float fightScaleConstX = 1f;
    public float fightScaleConstY = 0.2f;

    float clickCal = 0;

    float studentStr = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartFightCoroutine(Eleable eObject)
    {
        StartCoroutine(fightCoroutine(eObject));
    }

    IEnumerator fightCoroutine(Eleable eObject)
    {
        AllowEle = false;
        bool succ = false;
        playerControl.moveable = false;

        if (eObject.students != null)
        {
            eObject.students.moveable = false;
        }

        clickCal = 0;
        int targetClickNum = Random.Range(6, 12);

        Vector2 pos = (playerControl.transform.position + eObject.transform.position) / 2;

        Vector2 dirVector = eObject.transform.position - playerControl.transform.position;
        float dir = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;

        float dist = Vector2.Distance(playerControl.transform.position, eObject.gameObject.transform.position);

        liner = Instantiate(linerPrefab, pos, Quaternion.Euler(0,0,dir));
        liner.transform.localScale = new Vector3(fightScaleConstX *dist, fightScaleConstY, 0);


        //open canvas?
        FightCanvas.SetActive(true);

        bool t = true;
        float timeCal = 0;


        if (eObject.students != null)
        {
            eObject.students.Strength = studentStr;
        }


        while (t)
        {
            clickCal -= studentStr * Time.deltaTime;


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                clickCal++;
            }

            fightFill.fillAmount = (float)clickCal / (float)targetClickNum;

            if (clickCal >= targetClickNum)
            {
                t = false;
                succ = true;
            }

            yield return null;
        }

        yield return null;
        if (succ)
        {
            Debug.Log("���\�A�֥hť<<�ū�����>>�I");
            //playerControl.GetOneSoul();
            //Del Target;
            eObject.makeFadeAndDel();
        }
        else
        {

        }

        //open
        playerControl.moveable = true;
        FightCanvas.SetActive(false);
        Destroy(liner);
        AllowEle = true;
    }
}

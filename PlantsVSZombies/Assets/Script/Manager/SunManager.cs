using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public static SunManager Instance { get; private set; }

   
    [SerializeField]
    private int sunPoint;
    public int SunPoint
    { 
        get { return sunPoint; }
    }
    public TextMeshProUGUI sunPointText;
    private Vector3 SunPointTextPosition;
    public float produceTime;
    private float produceTimer;
    public GameObject sunPrefab;

    private bool isStarProduce = false;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateSunPointText();
        CalcSunPointTextPosition();
        StarProduce();
    }

    private void Update()
    {
        if (isStarProduce) 
        { 
         ProduceSun();
        }

    }
    public void StarProduce()
    {
        isStarProduce = true;


    }



    private void UpdateSunPointText()  //显示阳光
    {
        sunPointText.text = sunPoint.ToString();
    }

    public void SubSun(int point) //消耗阳光
    { 
        sunPoint -= point;
        UpdateSunPointText();
    }


    public void AddSun(int point) //增加阳光
    {
        sunPoint += point;
        UpdateSunPointText();
    }

    public Vector3 GetSunPointTextPosition() 
    {
        return SunPointTextPosition;
    }
    public void CalcSunPointTextPosition()
    {
      Vector3 position =  Camera.main.ScreenToWorldPoint(sunPointText.transform.position);
      position.z = 0;
      SunPointTextPosition = position;
    }

    void ProduceSun() 
    {
        produceTimer += Time.deltaTime;
        if (produceTimer >  produceTime) 
        {
            produceTimer = 0;
            Vector3 position = new Vector3(Random.Range(-6,6.5f),6.2f,-1);
            GameObject go = GameObject.Instantiate(sunPrefab,position,Quaternion.identity);
            go.transform.position = position;
            position.y = Random.Range(-4,4f);
            go.GetComponent<Sun>().LinearTo(position);
        }
    }




}

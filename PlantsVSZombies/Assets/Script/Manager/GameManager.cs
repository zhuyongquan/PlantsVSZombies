using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public PrepareUI prepareUI;
    public CardListUI cardListUI;
    public FailUI failUI;
    public WinUI winUI;



    private bool isGameEnd = false;



    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        GameStart();


    }



     void GameStart()
    {
        Vector3 currentPosition = Camera.main.transform.position;//摄像机的当前位置
        Camera.main.transform.DOPath(
            new Vector3[] { currentPosition, new Vector3(5,0,-10), currentPosition },
            6 ,PathType.Linear).OnComplete(OnCameraMoveComplete);
    }
    public void GameEndFail()//游戏结束处理  失败
    {
        if (isGameEnd == true) return;
        isGameEnd = true;
        failUI.Show();
        ZombieManager.Instance.Pause();
        cardListUI.DisableCardList();
        SunManager.Instance.StarProduce();
        AudioManager.instance.PlayClip(Config.lose_music);
    }

    public void GameEndSucces()//游戏结束处理  胜利
    {
        if (isGameEnd == true) return;
        isGameEnd = true;
        winUI.Show();
        cardListUI.DisableCardList();
        AudioManager.instance.PlayClip(Config.win_music);

    }


    void OnCameraMoveComplete()
    {
        prepareUI.Show(OnPrepreUIComplete);


    }

    private void OnPrepreUIComplete()
    {
        SunManager.Instance.StarProduce();
        ZombieManager.Instance.StartSpawn();
        cardListUI.ShowCardList();
        AudioManager.instance.PlayBGM(Config.bgm1);
    }







}

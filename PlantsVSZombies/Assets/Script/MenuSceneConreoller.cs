using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSceneConreoller : MonoBehaviour
{
    public GameObject inputPanelGo;
    public TMP_InputField nameinputField;//获得用户输入
    public TextMeshProUGUI nametext;

    private void Start()
    {
        UpdataNameUI();
    }


    public void OnChangeButtonClick() 
    {
        string name = PlayerPrefs.GetString("Name", "");
        nameinputField.text = name;
        inputPanelGo.SetActive(true);//调出名字输入框
        AudioManager.instance.PlayClip(Config.btn_click);
    }




    public void OnSubmitButtonClick()
    {

        PlayerPrefs.SetString("Name", nameinputField.text);
        inputPanelGo.SetActive(false);//隐藏面板
        UpdataNameUI();
    }

    void UpdataNameUI()
    {
       string name =  PlayerPrefs.GetString("Name","-");
        nametext.text = name;   
    }
    public void OnAdventureButtonClick() 
    {

        AudioManager.instance.PlayClip(Config.btn_click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);//加载下一个场景
    }




}

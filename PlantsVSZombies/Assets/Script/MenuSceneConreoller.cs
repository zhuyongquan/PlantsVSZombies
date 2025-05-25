using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSceneConreoller : MonoBehaviour
{
    public GameObject inputPanelGo;
    public TMP_InputField nameinputField;//����û�����
    public TextMeshProUGUI nametext;

    private void Start()
    {
        UpdataNameUI();
    }


    public void OnChangeButtonClick() 
    {
        string name = PlayerPrefs.GetString("Name", "");
        nameinputField.text = name;
        inputPanelGo.SetActive(true);//�������������
        AudioManager.instance.PlayClip(Config.btn_click);
    }




    public void OnSubmitButtonClick()
    {

        PlayerPrefs.SetString("Name", nameinputField.text);
        inputPanelGo.SetActive(false);//�������
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);//������һ������
    }




}

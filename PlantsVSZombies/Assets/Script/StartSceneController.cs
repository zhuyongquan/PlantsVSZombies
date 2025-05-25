using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void OnStartButtonClick()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//加载现有场景的下一个场景
    }



}

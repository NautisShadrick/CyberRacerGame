using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private GameObject music;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(music);
        SceneManager.LoadScene("Play");
    }
}

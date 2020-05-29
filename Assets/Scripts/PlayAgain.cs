using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgain : MonoBehaviour
{
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text="Final Score:\n"+GameManager.Instance.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Replay() {
        SceneManager.LoadScene("GameScreen",LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public bool timerStartet = false;

    private float gameTime;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private Image fadeImage;
    private float fadeSpeed = 0.3f;

    private float alpha = 0.0f;

    [SerializeField]
    private PhotonView photonView;
    
    void FadeOut()
    {
        alpha += fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        fadeImage.color = new Color(1, 1, 1, alpha);

        if (alpha >= 1)
        {
            timerStartet = false;
            SwitchToWaitingRoomScene();
        }
    }

    void FadeIn()
    {
        alpha -= fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        timerText.alpha = 0;

        fadeImage.color = new Color(1, 1, 1, alpha);
    }

    // Update is called once per frame
    void Update() {
        if (timerStartet)
        {
            if (PhotonNetwork.isMasterClient)
            {
                gameTime -= Time.deltaTime;


                if (gameTime <= 0)
                {
                    gameTime = 0;
                }
                
                photonView.RPC("UpdateTimer", PhotonTargets.All, gameTime);
            }

            if (gameTime == 0)
            {
                FadeOut();
            }
        }
        else if (alpha > 0)
        {
            FadeIn();
        }
    }

    public void SwitchToWaitingRoomScene()
    {
        SceneManager.LoadScene(1);
    }

    [PunRPC]
    public void UpdateTimer(float newGameTime)
    {
        if (!PhotonNetwork.isMasterClient)
        {
            gameTime = newGameTime;
        }
        timerText.text = string.Format("{0:00}:{1:00}", (int)gameTime / 60, (int)gameTime % 60);
    }

    public void StartTimer(float gameTimeForThisLevel){
        gameTime = gameTimeForThisLevel;
        timerText.alpha = 1;
        timerStartet = true;
    }
}

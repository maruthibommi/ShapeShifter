using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
public class GamePlayManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject player;
    public GameObject movementSlider;
    public GameObject sensSlider;
    public GameObject obstacleSlider;
    public GameObject tileManager;
    public GameObject playerScaleSlider;
    public GameObject obstacleScaleSlider;
    public GameObject slidervalue;

    void Start()//gets the player preferences values of in game debug menu//
    {
        movementSlider.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("playerSpeed",1f);
        sensSlider.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("playerSens",1f);
        obstacleSlider.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("obsCount",1f);
        playerScaleSlider.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("playerScale",1f);
        obstacleScaleSlider.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("obsScale",1f);
        slidervalue.GetComponent<Scrollbar>().value = 1f;

    }
    
    void Update()//updates the player preferences values of in game debug menu//
    {
        obstacleSlider.transform.GetChild(0).transform.GetComponent<TMPro.TextMeshProUGUI>().text =" no. of  obstacles  " + obstacleSlider.transform.GetComponent<Slider>().value.ToString();
        movementSlider.transform.GetChild(0).transform.GetComponent<TMPro.TextMeshProUGUI>().text =" speed x" + movementSlider.transform.GetComponent<Slider>().value.ToString();
        sensSlider.transform.GetChild(0).transform.GetComponent<TMPro.TextMeshProUGUI>().text =" sensitivity x" + sensSlider.transform.GetComponent<Slider>().value.ToString();
        playerScaleSlider.transform.GetChild(0).transform.GetComponent<TMPro.TextMeshProUGUI>().text =" Player scale x" + playerScaleSlider.transform.GetComponent<Slider>().value.ToString();
        obstacleScaleSlider.transform.GetChild(0).transform.GetComponent<TMPro.TextMeshProUGUI>().text =" Obstacle scale x" + obstacleScaleSlider.transform.GetComponent<Slider>().value.ToString();
        

        tileManager.transform.GetComponent<TileManager>().obstacleCount = (int) obstacleSlider.transform.GetComponent<Slider>().value;
        player.transform.GetComponent<PlayerMovement>().moveSpeed =0.1f * movementSlider.transform.GetComponent<Slider>().value;
        player.transform.GetComponent<PlayerMovement>().playerSensitivity = 0.01f * sensSlider.transform.GetComponent<Slider>().value;
        player.transform.localScale = new Vector3(playerScaleSlider.transform.GetComponent<Slider>().value,playerScaleSlider.transform.GetComponent<Slider>().value,playerScaleSlider.transform.GetComponent<Slider>().value);
        tileManager.transform.GetComponent<TileManager>().obstacleScale = obstacleScaleSlider.transform.GetComponent<Slider>().value;

        PlayerPrefs.SetFloat("playerSpeed",movementSlider.transform.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("playerSens",sensSlider.transform.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("obsCount",obstacleSlider.transform.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("playerScale",playerScaleSlider.transform.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("obsScale",obstacleScaleSlider.transform.GetComponent<Slider>().value);
        

    }
    public void continueAd(GameObject GameoverPanel)
    {
        player.transform.GetComponent<PlayerMovement>().isAlive = true;
        GameoverPanel.SetActive(false);
    }
    public void restart()
    {
        optionsPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
    public void exit()
    {
        Application.Quit();
    }
    public void OptionsPanel(GameObject go)
    {
        optionsPanel.SetActive(true);
        player.transform.GetComponent<PlayerMovement>().isAlive = false;
        go.GetComponent<Button>().interactable = false;
    }
    public void CloseOptionsPanel(GameObject go)
    {
        optionsPanel.SetActive(false);
        player.transform.GetComponent<PlayerMovement>().isAlive = true;
        go.GetComponent<Button>().interactable = true;
        slidervalue.GetComponent<Scrollbar>().value = 1f;
    }
}

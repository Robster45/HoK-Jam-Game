using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine.UI;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{

    
    private float playerhealth = PlayerData.Instance.GetHealth;
    //public Canvas canvas;
    public GameObject HUDpanel;
    public GameObject hurtpanel;
    public GameObject pausepanel;
    public GameObject checkpanel;

    public GameObject healthbar;
    public GameObject staminabar;

    public Button resumeButton;
    public Button quitButton;
    public Button yes;
    public Button no;
    //private GameObject canvas;
    

    // Start is called before the first frame update
    void Start()
    {

        //panel = GameObject.Find("HurtOverlay");


        HUDpanel.SetActive(true);
        hurtpanel.SetActive(false);
        pausepanel.SetActive(false);
        checkpanel.SetActive(false);

        Button resume = resumeButton.GetComponent<Button>();
        resume.onClick.AddListener(PauseGame);

        Button quit = quitButton.GetComponent<Button>();
        quit.onClick.AddListener(DoubleCheck);

        Button YES = yes.GetComponent<Button>();

        Button NO = no.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckDamage();


        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            PauseGame();
        }
    }


    void CheckDamage()
    {
        while (playerhealth == playerhealth--)
        {
            Damage();  
        }
    }


    void Damage()
    {
        if(hurtpanel.activeSelf == false)
        {
            hurtpanel.SetActive(true);
        }
        if(hurtpanel.activeSelf == true)
        {
            hurtpanel.SetActive(false);
        }
    }

    void PauseGame()
    {
        
        if(pausepanel.activeSelf == false)
        {
            pausepanel.SetActive(true);
            HUDpanel.SetActive(false);
        }
        if(pausepanel.activeSelf == true)
        {
            pausepanel.SetActive(false);
            HUDpanel.SetActive(true);
        }
        
    }

    void DoubleCheck()
    {
        yes.onClick.AddListener(Application.Quit);
        no.onClick.AddListener(NoButton);

        if (checkpanel.activeSelf == false)
        {
            checkpanel.SetActive(true);
        }
    }



    void NoButton()
    {
        if(checkpanel.activeSelf == true)
        {
            checkpanel.SetActive(false);
        }
    }


}

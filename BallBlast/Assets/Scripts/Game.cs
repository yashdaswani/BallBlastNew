using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Game : MonoBehaviour
{
	#region Singleton class: Game
	public float score = 0;
    public float Highscore;
	public static Game Instance;
	public TMP_Text scoreText;
	public TMP_Text HighscoreText;
	public TMP_Text GameOverPanelscoreText;
	public TMP_Text GameOverPanelHighscoreText;
    public GameObject PausePanel;
    public static float CoinAmount;
    private static float CoinSpeedPurchaseValue;
    public static float IncreaseCoinValue;
    public static float CoinValue;
    public TMP_Text CoinText;
    public TMP_Text CoinPurchaseText;
    public TMP_Text MissileSpeedText;
    public TMP_Text IncreaseCoinValueText;
    public TMP_Text IncreaseCoinValueTextButton;
    public static int MissileSpeed;
    public bool isGameover = false;
    public static bool isGamePlay = false;
    public GameObject ShopPanel;


    public GameObject SettingPanel;
    public Image sfxImage;
    public Image musicImage;
    public Sprite sfxmuteSprite;
    public Sprite sfxSprite;
    public static bool isSFXmute = false;
    public static bool isMusicmute = false;
    public Sprite musicmuteSprite;
    public Sprite musicSprite;

    public static float MissileDamage;
    public static float missileDamageCoinPurchaseValue;
    public TMP_Text missileDamageCoinPurchaseValueText;
    public TMP_Text MissileDamageText;

    public GameObject ReferralPanel;
    public TMP_InputField referralcode;

    public GameObject LoginPanel;
    public static int isUserLogin  = 0;

    [Header(" Audio")]
    public AudioSource ButtonAudio;
    public AudioSource CoinPickAudio;
    public AudioSource CoinRegAudio;

    public AudioMixer audioMixer;

    [Header("Missile")]
    public TMP_Text missileSpeed;
    public TMP_Text missilePower;

    public GameObject SpinPanel;
    public GameObject GamingSettingPanel;

    void Awake()
	{
		Instance = this;
		screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        //PlayerPrefs.SetFloat("Coin",1000);

        
        

    }

    private void Start()
    {
        isUserLogin = PlayerPrefs.GetInt("isUserLoggedIn", 0);
        if (isUserLogin == 0)
        {
            LoginPanel.SetActive(true);
            if(isUserLogin == 1)
            {
                LoginPanel.SetActive(false);
            }
        }

        Highscore = PlayerPrefs.GetFloat("highScore");
        CoinSpeedPurchaseValue = PlayerPrefs.GetFloat("CoinSpeedPurchaseValue", 5);
        IncreaseCoinValue = PlayerPrefs.GetFloat("IncreaseCoinValue", 1);
        CoinPurchaseText.text = CoinSpeedPurchaseValue.ToString();
        IncreaseCoinValueText.text = IncreaseCoinValue.ToString();
        UpdateCoinUI();

        MissileSpeed = PlayerPrefs.GetInt("MissileSpeed", 20);
        MissileSpeedText.text = "Missile Speed : " + MissileSpeed.ToString();

        CoinValue = PlayerPrefs.GetFloat("CoinValue", 1);

        IncreaseCoinValue = PlayerPrefs.GetFloat("IncreaseCoinValue");
        IncreaseCoinValueTextButton.text = IncreaseCoinValue.ToString();

        missileDamageCoinPurchaseValue = PlayerPrefs.GetFloat("missileDamageCoinPurchaseValue",5);
        missileDamageCoinPurchaseValueText.text = missileDamageCoinPurchaseValue.ToString();
        MissileDamage = PlayerPrefs.GetFloat("MissileDamage", 1);
        MissileDamageText.text ="+" + MissileDamage.ToString() + " DMG";
        
       

    }



    private void Update()
    {
		
        if(isGameover == true)
        {
            Time.timeScale = 0;
            gameOver();
            isGamePlay=false;
        }
         
        if(isGamePlay == true)
        {
            UpdateScoreText();
            missileSpeed.text = "MissileSpeed : " + MissileSpeed.ToString();
            missilePower.text = "MissilePower : " + PlayerPrefs.GetFloat("MissileDamage").ToString();
        }
        
        UpdateCoinUI();

    }

    #endregion

    [HideInInspector] public float screenWidth;
    public GameObject gameOverPanel;
    public  static bool  changeGravitytoZero = false;
    public AudioSource GameOverSound;
    


    public void UpdateScoreText()
    {
		scoreText.text = "Score : " + score.ToString();
        if(score > Highscore)
        {
            PlayerPrefs.SetFloat("highScore",score);
            Highscore = PlayerPrefs.GetFloat("highScore");
        }
            HighscoreText.text = "HighScore : " + Highscore.ToString(); 
        
       
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isGameover = false;
        PausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        isGameover=false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
       // GoogleAdMobController.instance.ShowInterstitialAd();
        Time.timeScale = 1;
        isGamePlay = true;
        SceneManager.LoadScene(1);
    }

    public void SettingButton()
    {
        SettingPanel.SetActive(true);
    }

    public void SettingClosePanbel()
    {
        SettingPanel.SetActive(false);
    }

    public void sfxtomutebutton()
    {
        isSFXmute = !isSFXmute;
        if(isSFXmute)
        {
            sfxImage.sprite = sfxmuteSprite;
            GameOverSound.volume = 0f;
        }
        else
        {
            sfxImage.sprite = sfxSprite;
            GameOverSound.volume = 1f;

        }
    }



    public void musictomuteButton()
    {
        isMusicmute = !isMusicmute;
        if (isMusicmute)
        {
           // PlayerPrefs.SetFloat("volume", 1);
            musicImage.sprite = musicmuteSprite;
        }
        else
        {
           // PlayerPrefs.SetFloat("volume", 0);
            musicImage.sprite = musicSprite;

        }
    }



    public void panelremove()
    {
        PausePanel.SetActive(false);
    }

    public void gameOver()
    {

        gameOverPanel.SetActive(true);
        GameOverPanelscoreText.text = "Score : " + PlayerPrefs.GetFloat("score").ToString();
        GameOverPanelHighscoreText.text = "HighScore : " +  PlayerPrefs.GetFloat("highScore");
    }

    public void Spin()
    {
        SpinPanel.SetActive(true);
    }


    
    public void CloseSpin()
    {
        SpinPanel.SetActive(false);
    }


    public void OpenGaming_Setting()
    {
        GamingSettingPanel.SetActive(true);
    }
    
    public void CloseGaming_Setting()
    {
        GamingSettingPanel.SetActive(false);
    }

    public void MakeGravityZero()
    {
        Physics.gravity = Vector3.zero;
    }

    public bool CheckCoinSpeed()
    {
        CoinSpeedPurchaseValue = PlayerPrefs.GetFloat("CoinSpeedPurchaseValue");
        CoinAmount = PlayerPrefs.GetFloat("Coin");
        if(CoinAmount >= CoinSpeedPurchaseValue)
        {
            return true;
        }
        else
            return false;
    }

    public void IncreaseMissileSpeed()
    {
        if(CheckCoinSpeed())
        {
            MissileSpeed = MissileSpeed + 1;
            PlayerPrefs.SetInt("MissileSpeed", MissileSpeed);
            MissileSpeedText.text ="Missile Speed : " + MissileSpeed.ToString();
            CoinAmount = CoinAmount - PlayerPrefs.GetFloat("CoinSpeedPurchaseValue");
            CoinRegAudio.Play();
            PlayerPrefs.SetFloat("Coin" , CoinAmount);
            CoinSpeedPurchaseValue = PlayerPrefs.GetFloat("CoinSpeedPurchaseValue");
            CoinSpeedPurchaseValue += 5;
            UpdatePurchaseCoinValue();
        
        }
        else
        {
            Debug.Log("Not Enough COin");
        }
        
    }


    public void IncreaseCoinValueMethod()
    {
        IncreaseCoinValue = PlayerPrefs.GetFloat("IncreaseCoinValue");
        CoinAmount = PlayerPrefs.GetFloat("Coin");

        if(CoinAmount >= IncreaseCoinValue)
        {
            CoinValue = PlayerPrefs.GetFloat("CoinValue");
            CoinValue = CoinValue + 1;
            PlayerPrefs.SetFloat("CoinValue", CoinValue);
            CoinAmount = CoinAmount - PlayerPrefs.GetFloat("IncreaseCoinValue");
            CoinRegAudio.Play();
            PlayerPrefs.SetFloat("Coin", CoinAmount); 
            IncreaseCoinValue = PlayerPrefs.GetFloat("IncreaseCoinValue");
            IncreaseCoinValue += 5;
            IncreaseCoinValueTextButton.text = IncreaseCoinValue.ToString();
            PlayerPrefs.SetFloat("IncreaseCoinValue", IncreaseCoinValue);
            IncreaseCoinValueText.text = ((IncreaseCoinValue * 10)/CoinAmount).ToString();
            
        }
    }


    public void IncreaseMissileDamage()
    {
        missileDamageCoinPurchaseValue = PlayerPrefs.GetFloat("missileDamageCoinPurchaseValue");
        CoinAmount = PlayerPrefs.GetFloat("Coin");

        if(CoinAmount >= missileDamageCoinPurchaseValue)
        {
            MissileDamage = PlayerPrefs.GetFloat("MissileDamage");
            MissileDamage = MissileDamage + 1;
            PlayerPrefs.SetFloat("MissileDamage", MissileDamage);
            MissileDamageText.text = "+" + MissileDamage.ToString() + " DMG";
            CoinAmount = CoinAmount - PlayerPrefs.GetFloat("missileDamageCoinPurchaseValue");
            CoinRegAudio.Play();
            PlayerPrefs.SetFloat("Coin", CoinAmount);
            missileDamageCoinPurchaseValue = PlayerPrefs.GetFloat("missileDamageCoinPurchaseValue");
            missileDamageCoinPurchaseValue = missileDamageCoinPurchaseValue + 5;
            missileDamageCoinPurchaseValueText.text = missileDamageCoinPurchaseValue.ToString();
            PlayerPrefs.SetFloat("missileDamageCoinPurchaseValue", missileDamageCoinPurchaseValue);
        }
    }


    public void UpdateCoinUI()
    {
        CoinAmount = PlayerPrefs.GetFloat("Coin",100);
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            CoinText.text = "Total Coin : " + CoinAmount.ToString();
        }
        else
        {
            CoinText.text = CoinAmount.ToString();
        }
        

    }

    public void UpdatePurchaseCoinValue()
    {
        PlayerPrefs.SetFloat("CoinSpeedPurchaseValue", CoinSpeedPurchaseValue);
        CoinPurchaseText.text = "-" + CoinSpeedPurchaseValue.ToString();
    }


    public void OpenShopPanel()
    {
        ShopPanel.SetActive(true);
    }
    public void CloseShopPanel()
    {
        ShopPanel.SetActive(false);
    }

    public void CloseReferral()
    {
        ReferralPanel.SetActive(false);
    }
    public void OpenReferral()
    {
        ReferralPanel.SetActive(true);
    }




    public void SetVOlume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void PlayButtonAUdio()
    {
        ButtonAudio.Play();
    }









}

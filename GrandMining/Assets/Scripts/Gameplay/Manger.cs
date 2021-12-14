using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Notifications.Android;

public class Manger : MonoBehaviour
{
    public AndroidNotificationChannel defaultNotificationChannel;
    private int identifier;
    public static event Action DigCall = delegate { };
    public static event Action DigPhase = delegate { };
    public static event Action ShowPopUp = delegate { };
    public TextMeshPro DigText;
    public TextMeshProUGUI CashText;
    public TextMeshProUGUI PuzzleGoalText;
    public TextMeshProUGUI WinningStreakText;
    public TextMeshProUGUI MoveAmountText;
    public TextMeshProUGUI GemText;
    public TextMeshProUGUI digCapacityText;
    public TextMeshProUGUI DigAmountText;
    public TextMeshProUGUI WinningStreakDisplay;

    public TextMeshProUGUI itemGoal;
    public TextMeshProUGUI ItemCollected;
    public TextMeshProUGUI rewardAmount;

    public LeanTweenType inType;

    Vector3 starFlowPosition = new Vector3(0f, 10.7f, -3.1f);//(0f, -5.3f, -2f);
    Vector3 starFlowRotation = new Vector3(-90f, 0f, 0f);

    public Slider slider;
    public Slider FreeGemBar;

    //public static bool inMatch3 = false;

    public static int firstStreakgoal = 55;
    public static int secondStreakgoal = 75;
    public static int thirdStreakgoal = 95;
    public static int fourthStreakgoal = 115;
    public static int fifthStreakgoal = 135;

    public static int freeGemTarget1 = 5;
    public static int freeGemTarget2 = 15;
    public static int freeGemTarget3 = 25;

    public static bool digRiward = false;
    public static bool collectRiward = false;
    //public static bool NewPlayer = true;

    public static int DigCapacity;
    public static int ExtraDigs;
    //public Text DiamondText, GoldText, IronText, RockText;

    string shareURL;

    //int followingStreak = DataStore.data.winstreakAmount + 1;

    private bool turnOn = true;
    public static bool puzzleDone = false;
    public float SetCashTime = 2.1f;

    public GameObject puzzleBoard, PuzzleUiPanel, slotPOPdown, MenuPanelParent, MenuPanel, StoreParentPanel, StorePanel, watchPanel, sliderPanel,
        pickAxeUpBut, shareForGem, winningAnimation, creditPanelParent, creditPanel, soundOffIcon, soundOnIcon;

    public GameObject RockNeedIMG;
    public GameObject IronNeedIMG;
    public GameObject GoldNeedIMG;
    public GameObject DiamondNeedIMG;
    public GameObject rewardGemAmount;

    public Button DigButt;
    public Slider winningBar;
    public Slider winningBarDisplay;


    private float initialCash;
    private float desireCash;
    private float currentCash;

    public static int puzzlegoalAmount;
    public static int moveAmount = 20;

    [SerializeField]
    private SlotMachine slotMachine;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "GM_Android",
            Name = "Grand Mining",
            Description = "For user's attentions",
            Importance = Importance.Default,
        };
        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);

        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Play and collect your digs!",
            Text = "We have some digs ready for you, Play and Collect them!",
            LargeIcon = "lg_icon",
            FireTime = System.DateTime.Now.AddSeconds(10800),
        };

        identifier = AndroidNotificationCenter.SendNotification(notification, "GM_Android");

        AndroidNotificationCenter.NotificationReceivedCallback receivedNotificationHandler = delegate (AndroidNotificationIntentData data)
        {
            var msg = "Notification received : " + data.Id + "\n";
            msg += "\n Notification received: ";
            msg += "\n .Title: " + data.Notification.Title;
            msg += "\n .Body: " + data.Notification.Text;
            msg += "\n .Channel: " + data.Channel;
            Debug.Log(msg);
        };
        AndroidNotificationCenter.OnNotificationReceived += receivedNotificationHandler;

        var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

        if(notificationIntentData != null)
        {
            Debug.Log("App was not opened with notification!");
        }


        if (Application.platform == RuntimePlatform.Android)
            shareURL = "https://play.google.com/store/apps/details?id=com.nobingames.grandmining";
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            print("Ifone");
        //checkNewPlayer();
        SoundCheck();
        creditPanel.transform.localScale = new Vector3(0, 0, 0);
        checkStreakHack();
        winningBar.maxValue = 5;
        winningBarDisplay.maxValue = 5;
        slider.maxValue = Manger.DigCapacity;
        checkMaxValue();
        creditPanelParent.SetActive(false);
        MenuPanelParent.SetActive(false);
        currentCash = DataStore.data.cashAmount;
        //DataStore.data.GetStatistics();
        //DigText = GetComponent<TextMeshPro>();
        //DigText.text = "100 digs";
    }
    /*void checkNewPlayer()
    {
        if(DataStore.data.oldPlayer == 0)
        {
            NewPlayer = true;
            DataStore.data.digAmount = 35;
            DataStore.data.gemAmount = 100;
            DataStore.data.cashAmount = 0;
            DataStore.data.oldPlayer = 1;
        }
        else
        {
            NewPlayer = false;
        }
    }*/
    void checkStreakHack()
    {
        if(DataStore.data.isInGame == 1)
        {
            DataStore.data.winstreakAmount = 0;
            DataStore.data.isInGame = 0;
            SetData();
        }
    }

    void checkMaxValue()
    {
        if (DataStore.data.freeGemState == 0)
        {
            rewardAmount.text = 100.ToString();
            FreeGemBar.maxValue = freeGemTarget1;
        }
        else if (DataStore.data.freeGemState == 1)
        {
            rewardAmount.text = 200.ToString();
            FreeGemBar.maxValue = freeGemTarget2;
        }
        else if (DataStore.data.freeGemState == 2)
        {
            rewardAmount.text = 300.ToString();
            FreeGemBar.maxValue = freeGemTarget3;
        }
        else if (DataStore.data.freeGemState == 3)
        {
            rewardAmount.text = 500.ToString();
            FreeGemBar.maxValue = freeGemTarget1;
        }
        else if (DataStore.data.freeGemState == 4)
        {
            rewardAmount.text = 600.ToString();
            FreeGemBar.maxValue = freeGemTarget2;
        }
        else if (DataStore.data.freeGemState == 5)
        {
            rewardAmount.text = 700.ToString();
            FreeGemBar.maxValue = freeGemTarget3;
        }
        else if (DataStore.data.freeGemState == 6)
        {
            rewardAmount.text = 1000.ToString();
            FreeGemBar.maxValue = freeGemTarget1;
        }
        else if (DataStore.data.freeGemState == 7)
        {
            rewardAmount.text = 1100.ToString();
            FreeGemBar.maxValue = freeGemTarget2;
        }
        else if (DataStore.data.freeGemState == 8)
        {
            rewardAmount.text = 1200.ToString();
            FreeGemBar.maxValue = freeGemTarget3;
        }
        else if (DataStore.data.freeGemState == 9)
        {
            rewardAmount.text = 1500.ToString();
            FreeGemBar.maxValue = freeGemTarget1;
        }
        else if (DataStore.data.freeGemState == 10)
        {
            rewardAmount.text = 1600.ToString();
            FreeGemBar.maxValue = freeGemTarget2;
        }
        else if (DataStore.data.freeGemState == 11)
        {
            rewardAmount.text = 1800.ToString();
            FreeGemBar.maxValue = freeGemTarget3;
        }

    }
    void checkBarValue()
    {
        if (DataStore.data.freeGemState <= 2)
            FreeGemBar.value = DataStore.data.rockGathered;
        else if (DataStore.data.freeGemState > 2 && DataStore.data.freeGemState <= 5)
            FreeGemBar.value = DataStore.data.ironGathered;
        else if (DataStore.data.freeGemState > 5 && DataStore.data.freeGemState <= 8)
            FreeGemBar.value = DataStore.data.goldGathered;
        else if (DataStore.data.freeGemState > 8 && DataStore.data.freeGemState <= 11)
            FreeGemBar.value = DataStore.data.diamondGathered;
    }

    void gemRewardedAnimation()
    {
        LeanTween.scale(rewardGemAmount, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setEase(inType);
        LeanTween.scale(rewardGemAmount, new Vector3(1f, 1f, 1f), 0.5f).setDelay(0.5f).setEase(inType);
    }

    void FreeGemUpdate()
    {
        if (DataStore.data.freeGemState == 0 && DataStore.data.rockGathered == freeGemTarget1)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 100;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 1 && DataStore.data.rockGathered == freeGemTarget2)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 200;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 2 && DataStore.data.rockGathered == freeGemTarget3)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 300;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 3 && DataStore.data.ironGathered == freeGemTarget1)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 500;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 4 && DataStore.data.ironGathered == freeGemTarget2)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 600;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 5 && DataStore.data.ironGathered == freeGemTarget3)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 700;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 6 && DataStore.data.goldGathered == freeGemTarget1)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 1000;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 7 && DataStore.data.goldGathered == freeGemTarget2)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 1100;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 8 && DataStore.data.goldGathered == freeGemTarget3)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 1200;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 9 && DataStore.data.diamondGathered == freeGemTarget1)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 1500;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 10 && DataStore.data.diamondGathered == freeGemTarget2)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 1600;
            gemRewardedAnimation();
        }
        else if (DataStore.data.freeGemState == 11 && DataStore.data.diamondGathered == freeGemTarget3)
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 1800;
            gemRewardedAnimation();
        }
    }
    void GemStateUpdate()
    {
        if (DataStore.data.freeGemState == 0 && DataStore.data.rockGathered == freeGemTarget1)
        {
            DataStore.data.freeGemState = 1;
            DataStore.data.rockGathered = 0;
        }
        else if (DataStore.data.freeGemState == 1 && DataStore.data.rockGathered == freeGemTarget2)
        {
            DataStore.data.freeGemState = 2;
            DataStore.data.rockGathered = 0;
        }
        else if (DataStore.data.freeGemState == 2 && DataStore.data.rockGathered == freeGemTarget3)
        {
            DataStore.data.freeGemState = 3;
            DataStore.data.rockGathered = 0;
        }
        else if (DataStore.data.freeGemState == 3 && DataStore.data.ironGathered == freeGemTarget1)
        {
            DataStore.data.freeGemState = 4;
            DataStore.data.ironGathered = 0;
        }
        else if (DataStore.data.freeGemState == 4 && DataStore.data.ironGathered == freeGemTarget2)
        {
            DataStore.data.freeGemState = 5;
            DataStore.data.ironGathered = 0;
        }
        else if (DataStore.data.freeGemState == 5 && DataStore.data.ironGathered == freeGemTarget3)
        {
            DataStore.data.freeGemState = 6;
            DataStore.data.ironGathered = 0;
        }
        else if (DataStore.data.freeGemState == 6 && DataStore.data.goldGathered == freeGemTarget1)
        {
            DataStore.data.freeGemState = 7;
            DataStore.data.goldGathered = 0;
        }
        else if (DataStore.data.freeGemState == 7 && DataStore.data.goldGathered == freeGemTarget2)
        {
            DataStore.data.freeGemState = 8;
            DataStore.data.goldGathered = 0;
        }
        else if (DataStore.data.freeGemState == 8 && DataStore.data.goldGathered == freeGemTarget3)
        {
            DataStore.data.freeGemState = 9;
            DataStore.data.goldGathered = 0;
        }
        else if (DataStore.data.freeGemState == 9 && DataStore.data.diamondGathered == freeGemTarget1)
        {
            DataStore.data.freeGemState = 10;
            DataStore.data.diamondGathered = 0;
        }
        else if (DataStore.data.freeGemState == 10 && DataStore.data.diamondGathered == freeGemTarget2)
        {
            DataStore.data.freeGemState = 11;
            DataStore.data.diamondGathered = 0;
        }
        else if (DataStore.data.freeGemState == 11 && DataStore.data.diamondGathered == freeGemTarget3)
        {
            DataStore.data.freeGemState = 0;
            DataStore.data.diamondGathered = 0;
        }
    }
    void checkRockState()
    {
        if(DataStore.data.freeGemState == 0)
        {
            if (DataStore.data.rockGathered < freeGemTarget1)
                DataStore.data.rockGathered = DataStore.data.rockGathered + 1;              
        }
        else if (DataStore.data.freeGemState == 1)
        {
            if (DataStore.data.rockGathered < freeGemTarget2)
                DataStore.data.rockGathered = DataStore.data.rockGathered + 1;
        }
        else if (DataStore.data.freeGemState == 2)
        {
            if (DataStore.data.rockGathered < freeGemTarget3)
                DataStore.data.rockGathered = DataStore.data.rockGathered + 1;
        }
        
    }
    void checkIronState()
    {
        if (DataStore.data.freeGemState == 3)
        {
            if (DataStore.data.ironGathered < freeGemTarget1)
                DataStore.data.ironGathered = DataStore.data.ironGathered + 1;
        }
        else if (DataStore.data.freeGemState == 4)
        {
            if (DataStore.data.ironGathered < freeGemTarget2)
                DataStore.data.ironGathered = DataStore.data.ironGathered + 1;
        }
        else if (DataStore.data.freeGemState == 5)
        {
            if (DataStore.data.ironGathered < freeGemTarget3)
                DataStore.data.ironGathered = DataStore.data.ironGathered + 1;
        }
    }
    void checkGoldState()
    {
        if (DataStore.data.freeGemState == 6)
        {
            if (DataStore.data.goldGathered < freeGemTarget1)
                DataStore.data.goldGathered = DataStore.data.goldGathered + 1;
        }
        else if (DataStore.data.freeGemState == 7)
        {
            if (DataStore.data.goldGathered < freeGemTarget2)
                DataStore.data.goldGathered = DataStore.data.goldGathered + 1;
        }
        else if (DataStore.data.freeGemState == 8)
        {
            if (DataStore.data.goldGathered < freeGemTarget3)
                DataStore.data.goldGathered = DataStore.data.goldGathered + 1;
        }
    }
    void checkDiamondState()
    {
        if (DataStore.data.freeGemState == 9)
        {
            if (DataStore.data.diamondGathered < freeGemTarget1)
                DataStore.data.diamondGathered = DataStore.data.diamondGathered + 1;
        }
        else if (DataStore.data.freeGemState == 10)
        {
            if (DataStore.data.diamondGathered < freeGemTarget2)
                DataStore.data.diamondGathered = DataStore.data.diamondGathered + 1;
        }
        else if (DataStore.data.freeGemState == 11)
        {
            if (DataStore.data.diamondGathered < freeGemTarget3)
                DataStore.data.diamondGathered = DataStore.data.diamondGathered + 1;
        }
    }

    void displayFreeGemState()
    {
        if(DataStore.data.freeGemState <= 2)
        {
            RockNeedIMG.SetActive(true);
            IronNeedIMG.SetActive(false);
            GoldNeedIMG.SetActive(false);
            DiamondNeedIMG.SetActive(false);
            ItemCollected.text = DataStore.data.rockGathered.ToString();
            if (DataStore.data.freeGemState == 0)
                itemGoal.text = freeGemTarget1.ToString();
            else if (DataStore.data.freeGemState == 1)
                itemGoal.text = freeGemTarget2.ToString();
            else
                itemGoal.text = freeGemTarget3.ToString();
        }
        else if (DataStore.data.freeGemState > 2 && DataStore.data.freeGemState <= 5)
        {
            RockNeedIMG.SetActive(false);
            IronNeedIMG.SetActive(true);
            GoldNeedIMG.SetActive(false);
            DiamondNeedIMG.SetActive(false);
            ItemCollected.text = DataStore.data.ironGathered.ToString();
            if (DataStore.data.freeGemState == 3)
                itemGoal.text = freeGemTarget1.ToString();
            else if (DataStore.data.freeGemState == 4)
                itemGoal.text = freeGemTarget2.ToString();
            else
                itemGoal.text = freeGemTarget3.ToString();
        }
        else if (DataStore.data.freeGemState > 5 && DataStore.data.freeGemState <= 8)
        {
            RockNeedIMG.SetActive(false);
            IronNeedIMG.SetActive(false);
            GoldNeedIMG.SetActive(true);
            DiamondNeedIMG.SetActive(false);
            ItemCollected.text = DataStore.data.goldGathered.ToString();
            if (DataStore.data.freeGemState == 6)
                itemGoal.text = freeGemTarget1.ToString();
            else if (DataStore.data.freeGemState == 7)
                itemGoal.text = freeGemTarget2.ToString();
            else
                itemGoal.text = freeGemTarget3.ToString();
        }
        else if (DataStore.data.freeGemState > 8 && DataStore.data.freeGemState <= 11)
        {
            RockNeedIMG.SetActive(false);
            IronNeedIMG.SetActive(false);
            GoldNeedIMG.SetActive(false);
            DiamondNeedIMG.SetActive(true);
            ItemCollected.text = DataStore.data.diamondGathered.ToString();
            if (DataStore.data.freeGemState == 9)
                itemGoal.text = freeGemTarget1.ToString();
            else if (DataStore.data.freeGemState == 10)
                itemGoal.text = freeGemTarget2.ToString();
            else
                itemGoal.text = freeGemTarget3.ToString();
        }
    }

    public void startSpinning()
    {
        if (slotMachine.SlotMachineStopped && DataStore.data.digAmount > 0)
        {
            soundManager.playSound("dig");
            DigCall();
        }
    }
    public void SetData()
    {
        DataStore.dataSet = false;
        DataStore.data.setData();
    }
    /*public void SetCash(float value)
    {
        initialCash = currentCash;
        desireCash = value;
    }*/
    void SoundCheck()
    {
        if (DataStore.data.isSoundOff == 0)
        {
            AudioListener.volume = 1.0f;
            soundOffIcon.SetActive(false);
            soundOnIcon.SetActive(true);
        }
        else
        {
            AudioListener.volume = 0.0f;
            soundOffIcon.SetActive(true);
            soundOnIcon.SetActive(false);
        }
    }
    public void soundButtonClick()
    {
        if (DataStore.data.isSoundOff == 0)
        {
            AudioListener.volume = 0.0f;
            soundOffIcon.SetActive(true);
            soundOnIcon.SetActive(false);
            DataStore.data.isSoundOff = 1;
            SetData();
        }
        else
        {
            AudioListener.volume = 1.0f;
            soundOffIcon.SetActive(false);
            soundOnIcon.SetActive(true);
            DataStore.data.isSoundOff = 0;
            SetData();
        }
    }
    public void menuPop()
    {
        MenuPanelParent.SetActive(true);
        LeanTween.move(MenuPanel.GetComponent<RectTransform>(), new Vector3(-121, 0, 0), 25 * Time.deltaTime).setDelay(0.2f).setEase(inType);
    }
    public void menuClose()
    {
        soundManager.playSound("button");
        LeanTween.move(MenuPanel.GetComponent<RectTransform>(), new Vector3(223, 0, 0), 25 * Time.deltaTime).setEase(inType);
        StartCoroutine("closeMenu");
    }
    private IEnumerator closeMenu()
    {
        yield return new WaitForSeconds(0.3f);
        MenuPanelParent.SetActive(false);
    }
    public void openCreditPanel()
    {
        soundManager.playSound("button");
        menuClose();
        creditPanelParent.SetActive(true);
        LeanTween.scale(creditPanel, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.3f).setEase(inType);
    }
    public void closeCreditPanel()
    {
        creditPanel.transform.localScale = new Vector3(0, 0, 0);
        creditPanelParent.SetActive(false);
    }
    public void openOnlyStorePanel()
    {
        soundManager.playSound("button");
        menuClose();
        StoreParentPanel.SetActive(true);
        LeanTween.scale(StorePanel, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.3f).setEase(inType);
    }
    public void openStorePanel()
    {
        soundManager.playSound("button");
        menuClose();
        StoreParentPanel.SetActive(true);
        LeanTween.scale(StorePanel, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.3f).setEase(inType);
    }
    public void openAdPanel()
    {
        soundManager.playSound("button");
        menuClose();
        StoreParentPanel.SetActive(true);
        LeanTween.scale(watchPanel, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.3f).setEase(inType);
    }
    public void addCash()
    {
        currentCash = DataStore.data.cashAmount;
        initialCash = currentCash;
        if(slotMachine.stoppedSlot == "Rock")
        {
            desireCash = currentCash + 0;// 50;
            checkRockState();
        }
        else if(slotMachine.stoppedSlot == "Iron")
        {
            desireCash = currentCash + 0;// 100;
            checkIronState();
        }
        else if (slotMachine.stoppedSlot == "Gold")
        {
            desireCash = currentCash + 0;// 200;
            checkGoldState();
        }
        else if(slotMachine.stoppedSlot == "Diamond")
        {
            desireCash = currentCash + 0;//500;
            checkDiamondState();
        }
        else if(slotMachine.stoppedSlot == "Puzzle")
        {
            desireCash = currentCash + 0;
        }
        DataStore.data.cashAmount = (int)desireCash;
        SetData();
    }
    private IEnumerator waitForTheDeadGem()
    {
        //DigButt.interactable = false;
        //stoppedSlot = "";
        //SetData();
        float timeInt = 2.7f;
        yield return new WaitForSeconds(timeInt);
        puzzleBoard.transform.localScale = new Vector3(0, 0, 0);
        PuzzleUiPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(pickAxeUpBut, new Vector3(1, 1, 1), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(slotPOPdown, new Vector3(1, 1, 1), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(sliderPanel, new Vector3(1, 1, 1), 30 * Time.deltaTime).setEase(inType);
        float timeint = 1 * Time.deltaTime;
        yield return new WaitForSeconds(timeint);
        DigButt.transform.localScale = new Vector3(1, 1, 1);
        float timein = 2;
        yield return new WaitForSeconds(timein);
        MovePieces.EndCheck = false;
        Match3.change = false;
        //Manger.puzzlegoalAmount = 50;
        Match3.diamondCount = 0;
        Manger.moveAmount = 20;
        if (DataStore.data.winstreakAmount == 0)
            puzzlegoalAmount = firstStreakgoal;
        else if (DataStore.data.winstreakAmount == 1)
            puzzlegoalAmount = secondStreakgoal;
        else if (DataStore.data.winstreakAmount == 2)
            puzzlegoalAmount = thirdStreakgoal;
        else if (DataStore.data.winstreakAmount == 3)
            puzzlegoalAmount = fourthStreakgoal;
        else if (DataStore.data.winstreakAmount == 4)
            puzzlegoalAmount = fifthStreakgoal;
        if (collectRiward == true)
        {
            digRiward = true;
            collectRiward = false;
        }
        DigButt.interactable = true;
        slotMachine.stoppedSlot = "";
        SlotMachine.endPuzzle = true;
    }

    public void backtoSlotmatchineAnimation()
    {
        StartCoroutine("waitForTheDeadGem");
    }

    /*public void ShareOnFacebook()
    {
        FB.ShareLink(
        new Uri(shareURL), 
        callback: ShareCallback
        );
    }*/

    private void ShareCallback(IShareResult result)
    {
        if (result.Cancelled || !String.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink Error: " + result.Error);
        }
        else if (!String.IsNullOrEmpty(result.PostId))
        {
            // Print post identifier of the shared content
            Debug.Log(result.PostId);
        }
        else
        {
            // Share succeeded without postID
            Debug.Log("ShareLink success!");
            DataStore.data.gemAmount = DataStore.data.gemAmount + 20;
            shareForGem.transform.localScale = new Vector3(0, 0, 0);
            SetData();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //if (DataStore.data.digAmount <= 0)
        //DigButt.interactable = false;

        if (puzzleDone == true)
        {
            if(DataStore.data.winstreakAmount > 0 && DataStore.data.winstreakAmount < 5)
                Instantiate(winningAnimation, starFlowPosition, transform.rotation * Quaternion.Euler(starFlowRotation));
            //inMatch3 = false;
            DataStore.data.isInGame = 0;
            SetData();
            puzzleDone = false;
            StartCoroutine("waitForTheDeadGem");
        }
        slider.maxValue = DigCapacity;
        checkMaxValue();
        PuzzleGoalText.text = puzzlegoalAmount.ToString();
        GemText.text = DataStore.data.gemAmount.ToString(); 
        CashText.text = DataStore.data.cashAmount.ToString();
        WinningStreakDisplay.text = DataStore.data.winstreakAmount.ToString();
        WinningStreakText.text = DataStore.data.winstreakAmount.ToString();
        winningBarDisplay.value = DataStore.data.winstreakAmount;
        winningBar.value = DataStore.data.winstreakAmount;
        MoveAmountText.text = moveAmount.ToString();
        //DiamondText.text = DataStore.data.diamondAmount.ToString();
        //GoldText.text = DataStore.data.goldAmount.ToString();
        //IronText.text = DataStore.data.ironAmount.ToString();
        //RockText.text = DataStore.data.rockAmount.ToString();
        if (DataStore.data.digAmount <= DigCapacity)
            DigAmountText.text = DataStore.data.digAmount.ToString();
        else
            DigAmountText.text = DigCapacity.ToString();
        digCapacityText.text = DigCapacity.ToString();
        if(DataStore.data.digAmount >= DigCapacity)
            DigText.text = "+" + ExtraDigs.ToString() + " digs";
        slider.value = DataStore.data.digAmount;
        checkBarValue();
        //addCash();
        if (slotMachine.showBuyingOtion == true)
        {
            DigPhase();
            //ShowPopUp();
            slotMachine.showBuyingOtion = false;
        }
        if(slotMachine.StoppedRotate == true)
        {
            addCash();
            FreeGemUpdate();
            slotMachine.StoppedRotate = false;
        }

        if (currentCash != desireCash)
        {
            if (initialCash < desireCash)
            {
                currentCash += (SetCashTime * Time.deltaTime) * (desireCash - initialCash);
                if (currentCash >= desireCash)
                    currentCash = (int)desireCash;
                    //SetData();
            }
            else
            {
                currentCash -= (SetCashTime * Time.deltaTime) * (initialCash - desireCash);
                if (currentCash <= desireCash)
                    currentCash = (int)desireCash;
                    //SetData();
            }
            CashText.text = currentCash.ToString("0");
            //DataStore.data.cashAmount = (int)currentCash;
            //SetData();

        }
        GemStateUpdate();
        displayFreeGemState();


    }
    /*static void Quit()
    {
        if (inMatch3)
            DataStore.data.winstreakAmount = 0;
        DataStore.data.setData();
        Debug.Log("Quitting the Player");
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.quitting += Quit;
    }*/

}

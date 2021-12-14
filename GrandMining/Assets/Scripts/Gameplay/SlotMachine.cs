using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public GameObject DiamondPop, GoldPop, IronPop, RockPop, PuzzlePop, CashPop, cashFlow, slotPOPdown, DigButtonPopDown, PuzzleUiPanel,
        puzzleBoard, StorePanel, StoreParentPanel, watchPanel, upgradeDisplay, upgradePanel, sliderPanel, pickAxeUpBut, disablePanel, RewardShowPanel, RewardDisplay, DigRiwardsDisplay,
        LoadingPanel;
    public LeanTweenType inType;
    public LeanTweenType outType;
    public LeanTweenType CashAnimationType;
    public LeanTweenType rollStopAnimation;

    bool NewPlayer = true;
    public static bool FirstDataRecieve = false;

    public static bool endPuzzle = false;

    public GameObject gemBar;

    private int randomValue;
    private int totalNumOfItems;
    private int NewDigAmount;
    private int numberOfRock, numberOfIron, numberOfGold, numberOfDiamond;
    public static int numberOfPuzzle, minTurn, meanTurn, maxTurn;
    private int rockAmount = 0;
    private int ironAmount = 0;
    private int goldAmount = 0;
    private int puzzleAmount = 0;
    private int diamondAmount = 0;
    public static int turn = 0;

    private float RockPosition = -8.6f;
    private float PuzzlePosition = -4.6f;
    private float DiamondPosition = -0.6000004f;
    private float IronPosition = 3.4f;
    private float GoldPosition = 7.4f;

    public static bool digAmountChanged = false;
    public static bool winStreakChange = true;

    Vector3 Position = new Vector3(-2f, 1, -1f);
    Vector3 CashFlowPosition = new Vector3(0f, -1f, -2f);//(0f, -5.3f, -2f);
    Vector3 CashFlowRotation = new Vector3(-90f, 0f, 0f);

    private float timeInterval;

    private Manger manager;
    private Match3 match3;

    public bool SlotMachineStopped;
    public bool showBuyingOtion;
    public bool StoppedRotate;

    public string stoppedSlot;
    public TextMeshProUGUI playerID;

    public Button DigButt;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 75;
        //Debug.Log("Its false");
        /*checkNewPlayer();
        if (NewPlayer == false)
        {
            Debug.Log("Its false");
            StartCoroutine("digFinishAnimation");
        }
        else
        {
            disablePanel.SetActive(false);
            //Debug.Log("Its false");
        }*/
        puzzleBoard.transform.localScale = new Vector3(0, 0, 0);
        PuzzlePop.transform.localScale = new Vector3(0, 0, 0);
        RockPop.transform.localScale = new Vector3(0, 0, 0);
        IronPop.transform.localScale = new Vector3(0, 0, 0);
        GoldPop.transform.localScale = new Vector3(0, 0, 0);
        DiamondPop.transform.localScale = new Vector3(0, 0, 0);
        PuzzleUiPanel.transform.localScale = new Vector3(0, 0, 0);
        StorePanel.transform.localScale = new Vector3(0, 0, 0);
        watchPanel.transform.localScale = new Vector3(0, 0, 0);
        upgradeDisplay.transform.localScale = new Vector3(0, 0, 0);
        DigRiwardsDisplay.transform.localScale = new Vector3(0, 0, 0);
        StoreParentPanel.SetActive(false);
        upgradePanel.SetActive(false);


        streakBasedGoal();
        if (DataStore.data.winstreakAmount == 5)
            DataStore.data.winstreakAmount = 0;

        SlotMachineStopped = true;
        showBuyingOtion = false;
        StoppedRotate = false;
        Manger.DigCall += Rotate;
    }
    void checkNewPlayer()
    {   
        if(DataStore.dataRecieve == true)
        {
            if (DataStore.data.oldPlayer == 0)
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
        }
    }

    public void OnClose()
    {
        LeanTween.scale(PuzzlePop, new Vector3(0, 0, 0), 0.15f).setEase(outType);
        LeanTween.scale(RockPop, new Vector3(0, 0, 0), 0.15f).setEase(outType);
        LeanTween.scale(DiamondPop, new Vector3(0, 0, 0), 0.15f).setEase(outType);
        LeanTween.scale(IronPop, new Vector3(0, 0, 0), 0.15f).setEase(outType);
        LeanTween.scale(GoldPop, new Vector3(0, 0, 0), 0.15f).setEase(outType);
    }

    private void Rotate()
    {
        if (!digAmountChanged)
        {
           float numberOfRock1 = ((float)(DataStore.data.digAmount) * (float)51) / (float)100;
           float numberOfIron1 = ((float)(DataStore.data.digAmount) * (float)20) / (float)100;
           float numberOfPuzzle1 = ((float)(DataStore.data.digAmount) * (float)17) / (float)100;
           float numberOfGold1 = ((float)(DataStore.data.digAmount) * (float)12) / (float)100;
           float numberOfDiamond1 = ((float)(DataStore.data.digAmount) * (float)3) / (float)100;
           float maxTurn1 = ((float)(DataStore.data.digAmount) * (float)80) / (float)100;
           float meanTurn1 = ((float)(DataStore.data.digAmount) * (float)54) / (float)100;
           float minTurn1 = ((float)(DataStore.data.digAmount) * (float)17) / (float)100;
            numberOfRock = Mathf.RoundToInt(numberOfRock1);
            numberOfIron = Mathf.RoundToInt(numberOfIron1);
            numberOfGold = Mathf.RoundToInt(numberOfGold1);
            numberOfPuzzle = Mathf.RoundToInt(numberOfPuzzle1);
            numberOfDiamond = Mathf.RoundToInt(numberOfDiamond1);
            minTurn = 10;//Mathf.RoundToInt(minTurn1);
            maxTurn = 30;// Mathf.RoundToInt(maxTurn1);
            meanTurn = 20;// Mathf.RoundToInt(meanTurn1);
            digAmountChanged = !digAmountChanged;
        }
        stoppedSlot = "";
        if (DataStore.data.digAmount > 0)
        {
            if (DataStore.data.digAmount > Manger.DigCapacity)
                Manger.ExtraDigs = Manger.ExtraDigs - 1;
            DataStore.data.digAmount = DataStore.data.digAmount - 1;
        }
        DataStore.data.setData();
        turn = turn + 1;
        Debug.Log("turn: " + turn);

        StartCoroutine("Spin");
    }
    public void streakBasedGoal()
    {
        if (DataStore.data.winstreakAmount == 0)
            Manger.puzzlegoalAmount = Manger.firstStreakgoal;
        else if (DataStore.data.winstreakAmount == 1)
            Manger.puzzlegoalAmount = Manger.secondStreakgoal;
        else if (DataStore.data.winstreakAmount == 2)
            Manger.puzzlegoalAmount = Manger.thirdStreakgoal;
        else if (DataStore.data.winstreakAmount == 3)
            Manger.puzzlegoalAmount = Manger.fourthStreakgoal;
        else if (DataStore.data.winstreakAmount == 4)
            Manger.puzzlegoalAmount = Manger.fifthStreakgoal;
    }
    public void SetData()
    {
        DataStore.data.setData();
    }

    private IEnumerator Spin()
    {
        Manger.digRiward = false;
        StoppedRotate = false;
        SlotMachineStopped = false;
        DigButt.interactable = false;
        timeInterval = 0.000005f * Time.deltaTime;  
        for(int i =0; i < 20; i++)
        {
            Color newColor = new Color(1, 1, 1, 0.3f);
            GetComponent<SpriteRenderer>().material.color = newColor;
            if (transform.position.y <= -8.6f)
                transform.position = new Vector2(transform.position.x, 7.4f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            yield return new WaitForSeconds(timeInterval);
        }
        randomValue = Random.Range(20, 40);
        switch (randomValue % 4)
        {
            case 1:
                randomValue += 3;
                break;
            case 2:
                randomValue += 2;
                break;
            case 3:
                randomValue += 1;
                break;

        }

        for (int i = 0; i < randomValue; i++)
        {
            if (transform.position.y <= -10.6f) // 10.6
                transform.position = new Vector2(transform.position.x, 9.4f); // 9.4

            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);

            if (i > Mathf.RoundToInt(randomValue * 0.4f))
                timeInterval = 0.000001f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomValue * 0.6f))
                timeInterval = 0.00007f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomValue * 0.8f))
                timeInterval = 0.00004f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomValue * 0.9f))
                timeInterval = 0.00001f * Time.deltaTime;

            yield return new WaitForSeconds(timeInterval);
            SlotMachineStopped = true;
            Color newColor1 = new Color(1, 1, 1, 0.4f);
            GetComponent<SpriteRenderer>().material.color = newColor1;
        }
        if (transform.position.y > -8.7f && transform.position.y < -8.5f)
        {
            stoppedSlot = "Rock";
            transform.position = new Vector2(transform.position.x, RockPosition - 0.2f);
            LeanTween.move(gameObject, new Vector2(transform.position.x, RockPosition), 25f * Time.deltaTime).setEase(rollStopAnimation);
            LeanTween.scale(RockPop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
            StoppedRotate = true;
        }
        else if (transform.position.y > -4.7f && transform.position.y < -4.5f)
        {           
            if (puzzleAmount >= numberOfPuzzle)
            {
                if(turn % 2 == 0 && turn < minTurn)
                {
                    stoppedSlot = "Rock";
                    transform.position = new Vector2(transform.position.x, RockPosition - 0.2f);
                    LeanTween.move(gameObject, new Vector2(transform.position.x, RockPosition), 25f * Time.deltaTime).setEase(rollStopAnimation);
                    LeanTween.scale(RockPop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                    StoppedRotate = true;
                }
                else
                {
                    stoppedSlot = "Puzzle";
                    transform.position = new Vector2(transform.position.x, PuzzlePosition - 0.3f);
                    LeanTween.move(gameObject, new Vector2(transform.position.x, PuzzlePosition), 25f * Time.deltaTime).setEase(rollStopAnimation);
                    LeanTween.scale(PuzzlePop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                    StoppedRotate = true;
                }
            }
            else
            {
                stoppedSlot = "Puzzle";
                transform.position = new Vector2(transform.position.x, PuzzlePosition - 0.3f);
                LeanTween.move(gameObject, new Vector2(transform.position.x, PuzzlePosition), 25f * Time.deltaTime).setEase(rollStopAnimation);
                LeanTween.scale(PuzzlePop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                StoppedRotate = true;

            }

        }
        else if (transform.position.y > -0.7f && transform.position.y < -0.5f)
        {
            stoppedSlot = "Diamond";
            if (turn > maxTurn)
            {
                transform.position = new Vector2(transform.position.x, DiamondPosition - 0.3f);
                LeanTween.move(gameObject, new Vector2(transform.position.x, DiamondPosition), 25f * Time.deltaTime).setEase(rollStopAnimation);

                LeanTween.scale(DiamondPop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);

                StoppedRotate = true;
            }

        }
        else if (transform.position.y > 3.3f && transform.position.y < 3.5f)
        {
            stoppedSlot = "Iron";
            if (turn > minTurn)
            {
                transform.position = new Vector2(transform.position.x, IronPosition - 0.3f);
                LeanTween.move(gameObject, new Vector2(transform.position.x, IronPosition), 25f * Time.deltaTime).setEase(rollStopAnimation);

                LeanTween.scale(IronPop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                StoppedRotate = true;
            }                

        }
        else if (transform.position.y > 7.3f && transform.position.y < 7.5f)
        {
            stoppedSlot = "Gold";
            if (turn > meanTurn)
            {
                transform.position = new Vector2(transform.position.x, GoldPosition - 0.3f);
                LeanTween.move(gameObject, new Vector2(transform.position.x, GoldPosition), 25f * Time.deltaTime).setEase(rollStopAnimation);

                LeanTween.scale(GoldPop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                StoppedRotate = true;
            }               

        }
        Color newColor2 = new Color(1, 1, 1, 1f);
        GetComponent<SpriteRenderer>().material.color = newColor2;

       
        SpinLogic();
        if (StoppedRotate == true)
        {
            if (stoppedSlot != "Puzzle")
            {
                timeInterval = 25 * Time.deltaTime;
                yield return new WaitForSeconds(timeInterval);
                //Instantiate(cashFlow, CashFlowPosition, transform.rotation * Quaternion.Euler(CashFlowRotation));
            }
            timeInterval = 40 * Time.deltaTime;
            yield return new WaitForSeconds(timeInterval);
            if(DataStore.dataSet == true)
                DigButt.interactable = true;

            DigFinished();

        }
        else
        {
            
        }
        if (stoppedSlot == "Puzzle")
        {
            gemBar.SetActive(false);
            StartCoroutine("PuzzAnimation");
        }

        
    }

    private void SpinLogic()
    {
        if(turn <= minTurn)
        {
            if (stoppedSlot == "Iron")
            {
                transform.position = new Vector2(transform.position.x, PuzzlePosition - 0.3f);
                LeanTween.move(gameObject, new Vector2(transform.position.x, PuzzlePosition), 25f * Time.deltaTime).setEase(rollStopAnimation);
                stoppedSlot = "Puzzle";
                LeanTween.scale(PuzzlePop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                StoppedRotate = true;
            }
            if (stoppedSlot == "Gold" || stoppedSlot == "Diamond" || stoppedSlot == "Iron")
            {
                transform.position = new Vector2(transform.position.x, RockPosition - 0.2f);
                LeanTween.move(gameObject, new Vector2(transform.position.x, RockPosition), 25f * Time.deltaTime).setEase(rollStopAnimation);
                stoppedSlot = "Rock";
                LeanTween.scale(RockPop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                StoppedRotate = true;
            }
        }
        else if(turn <= meanTurn && turn > minTurn)
        {
            if (stoppedSlot == "Diamond" || stoppedSlot == "Gold")
            {
                transform.position = new Vector2(transform.position.x, RockPosition - 0.2f);
                LeanTween.move(gameObject, new Vector2(transform.position.x, RockPosition), 25f * Time.deltaTime).setEase(rollStopAnimation);
                stoppedSlot = "Rock";
                LeanTween.scale(RockPop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                StoppedRotate = true;
            }
        }
        else if(turn <= maxTurn && turn > meanTurn)
        {
            if (stoppedSlot == "Diamond")
            {
                transform.position = new Vector2(transform.position.x, RockPosition - 0.2f);
                LeanTween.move(gameObject, new Vector2(transform.position.x, RockPosition), 25f * Time.deltaTime).setEase(rollStopAnimation);
                stoppedSlot = "Rock";
                LeanTween.scale(RockPop, new Vector3(1, 1, 1), 20f * Time.deltaTime).setEase(inType);
                StoppedRotate = true;
            }
        }
    }

    private IEnumerator digFinishAnimation()
    {
        float timeInt = 45 * Time.deltaTime;
        yield return new WaitForSeconds(timeInt);
        disablePanel.SetActive(false);
        StoreParentPanel.SetActive(true);
        LeanTween.scale(StorePanel, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.5f).setEase(inType);
        //RunOnlyOnce = false;
    }

    void DigFinished()
    {
        if (DataStore.data.digAmount < 1 && stoppedSlot != "Puzzle")
        {

            StartCoroutine("digFinishAnimation");
        }
    }
    public void closeOnlyStorePanel()
    {
        soundManager.playSound("button");
        LeanTween.scale(StorePanel, new Vector3(0, 0, 0), 20 * Time.deltaTime).setEase(inType);
        StoreParentPanel.SetActive(false);
    }
    public void closeStorePanel()
    {
        soundManager.playSound("button");
        LeanTween.scale(StorePanel, new Vector3(0, 0, 0), 20 * Time.deltaTime).setEase(inType);
        LeanTween.scale(watchPanel, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.5f).setEase(inType);
    } 
    public void closeWatchPanel()
    {
        soundManager.playSound("button");
        LeanTween.scale(watchPanel, new Vector3(0, 0, 0), 20 * Time.deltaTime).setEase(inType);
        if (TMmanger.RewardsAvailable && DataStore.data.digAmount < Manger.DigCapacity)
        {
            LeanTween.scale(DigRiwardsDisplay, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.5f).setEase(inType);
        }
        else
        {
            StoreParentPanel.SetActive(false);
        }
    }
    public void UpgradePanelPop()
    {
        soundManager.playSound("button");
        upgradePanel.SetActive(true);
        LeanTween.scale(upgradeDisplay, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.2f).setEase(inType);
    }
    
    public void CloseUpgradePanel()
    {
        soundManager.playSound("button");
        LeanTween.scale(upgradeDisplay, new Vector3(0, 0, 0), 20 * Time.deltaTime).setEase(inType);
        upgradePanel.SetActive(false);
    }

    private void CheckEveryRoundTillEnd()
    {
        if (rockAmount == numberOfRock && ironAmount == numberOfIron && diamondAmount == numberOfDiamond && goldAmount == numberOfGold && puzzleAmount < numberOfPuzzle)
        {
            transform.position = new Vector2(transform.position.x, PuzzlePosition);
        }
        else if (rockAmount == numberOfRock && ironAmount < numberOfIron && diamondAmount == numberOfDiamond && goldAmount == numberOfGold && puzzleAmount == numberOfPuzzle)
        {
            transform.position = new Vector2(transform.position.x, IronPosition);
        }
        else if (rockAmount == numberOfRock && ironAmount == numberOfIron && diamondAmount == numberOfDiamond && goldAmount < numberOfGold && puzzleAmount == numberOfPuzzle)
        {
            transform.position = new Vector2(transform.position.x, GoldPosition);
        }
        else if (rockAmount == numberOfRock && ironAmount == numberOfIron && diamondAmount < numberOfDiamond && goldAmount == numberOfGold && puzzleAmount == numberOfPuzzle)
        {
            transform.position = new Vector2(transform.position.x, DiamondPosition);
        }
        else if (rockAmount < numberOfRock && ironAmount == numberOfIron && diamondAmount == numberOfDiamond && goldAmount == numberOfGold && puzzleAmount == numberOfPuzzle)
        {
            transform.position = new Vector2(transform.position.x, RockPosition);
        }
        else if (rockAmount < numberOfRock && ironAmount < (numberOfIron - (numberOfIron - 1)) && diamondAmount == numberOfDiamond && goldAmount == numberOfGold && puzzleAmount == numberOfPuzzle)
        {
            transform.position = new Vector2(transform.position.x, RockPosition);
        }
        else if (rockAmount < numberOfRock && ironAmount == numberOfIron && diamondAmount == numberOfDiamond && goldAmount < (numberOfGold - (numberOfGold - 1)) && puzzleAmount == numberOfPuzzle)
        {
            transform.position = new Vector2(transform.position.x, RockPosition);
        }
    }
    private IEnumerator PuzzAnimation()
    {
        puzzleAmount = puzzleAmount + 1;
        DigButt.interactable = false;
        LeanTween.scale(DigButtonPopDown, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        timeInterval = 45 * Time.deltaTime;
        yield return new WaitForSeconds(timeInterval);
        LeanTween.scale(pickAxeUpBut, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(slotPOPdown, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(sliderPanel, new Vector3(0, 0, 0), 20 * Time.deltaTime).setEase(inType);
        RewardShowPanel.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        LeanTween.move(RewardDisplay.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 20 * Time.deltaTime).setEase(inType);
        yield return new WaitForSeconds(2.4f);
        LeanTween.move(RewardDisplay.GetComponent<RectTransform>(), new Vector3(0, -690, 0), 20 * Time.deltaTime).setEase(inType);
        yield return new WaitForSeconds(0.6f);
        RewardShowPanel.SetActive(false);
        LeanTween.scale(PuzzleUiPanel, new Vector3(1, 1, 1), 25 * Time.deltaTime).setDelay(0.2f).setEase(inType);
        LeanTween.scale(puzzleBoard, new Vector3(1, 1, 1), 25 * Time.deltaTime).setDelay(0.4f).setEase(inType);
        DataStore.data.isInGame = 1;
        SetData();
    }


    private void OnDestroy()
    {
        Manger.DigCall -= Rotate;
    }

    void Update()
    {
        if (DataStore.dataRecieve == true && FirstDataRecieve == false)
        {
            LoadingPanel.SetActive(false);
            checkNewPlayer();
            playerID.text = PlayfabFacebookAuthExample.id.ToString();
            if (NewPlayer == false)
            {
                Debug.Log("Its false");
                StartCoroutine("digFinishAnimation");
            }
            else
            {
                disablePanel.SetActive(false);
                //Debug.Log("Its false");
            }
            Debug.Log("still running");
            FirstDataRecieve = true;
        }
        /*if(FirstDataRecieve == true)
        {
            FirstDataRecieve = false;
            checkNewPlayer();
            if (NewPlayer == false)
            {
                Debug.Log("Its false");
                StartCoroutine("digFinishAnimation");
            }
            else
            {
                disablePanel.SetActive(false);
                //Debug.Log("Its false");
            }
        }*/

        if (endPuzzle == true)
        {
            gemBar.SetActive(true);
            endPuzzle = false;
            DigFinished();
        }
    }

}

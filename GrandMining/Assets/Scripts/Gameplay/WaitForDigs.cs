using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaitForDigs : MonoBehaviour
{
    //UI
    public GameObject TmLabel;
    public TextMeshPro timeLabel; //only use if your timer uses a label
    public Button timerButton; //used to disable button when needed
    public Image _progress;
    //TIME ELEMENTS
    public int hours; //to set the hours
    public int minutes; //to set the minutes
    public int seconds; //to set the seconds
    private bool _timerComplete = false;
    private bool _timerIsReady;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
    private TimeSpan _remainingTime;
    //progress filler
    private float _value = 1f;
    //reward to claim
    public int RewardToEarn;
    double totalTime;
    double minuitesss;
    int tempDigs;
    int hourLeft;
    public static int totalDigRiwards;
    float totalTm;
    float FloatMinuites;
    bool timerFinished = true;


    //startup
    void Start()
    {
        finishCheck();
        //PlayerPrefs.GetInt("tempDigs");
        if (PlayerPrefs.GetString("_timer") == "")
        {
            Debug.Log("==> Enableing button");
            //CheckPickAxe();
            enableButton();
            //validateTime();
            //StartCoroutine("CheckTime");
        }
        else
        {
            //CheckPickAxe();
            //disableButton();            
            StartCoroutine("CheckTime");
            //StartCoroutine("CheckPickWait");
            CheckPickAxe();
            if (DataStore.data.digAmount < Manger.DigCapacity)
                _timerIsReady = true;
            else
            {
                timerFinished = true;
                totalDigRiwards = 0;
            }
        }
        //if (!_timerComplete)
            //validateTime();

        //Debug.Log(_remainingTime.Minutes);
    }

    private IEnumerator CheckPickWait()
    {
        yield return new WaitForSeconds(1.5f);
        CheckPickAxe();
    }

    void finishCheck()
    {
        //if (_remainingTime.Hours <= 0 && _remainingTime.Minutes <= 0 && _remainingTime.Seconds <= 0)
            //timerFinished = true;
        if (DataStore.data.digAmount == Manger.DigCapacity)
            timerFinished = true;

        else
            timerFinished = false;
    }

    //update the time information with what we got some the internet
    private void updateTime()
    {
        if (PlayerPrefs.GetString("_timer") == "Standby")
        {
            PlayerPrefs.SetString("_timer", TimeManager.sharedInstance.getCurrentTimeNow());
            PlayerPrefs.SetInt("_date", TimeManager.sharedInstance.getCurrentDateNow());
        }
        else if (PlayerPrefs.GetString("_timer") != "" && PlayerPrefs.GetString("_timer") != "Standby")
        {
            int _old = PlayerPrefs.GetInt("_date");
            int _now = TimeManager.sharedInstance.getCurrentDateNow();

            //check if a day as passed
            if (_now > _old)
            {//day as passed
                Debug.Log("Day has passed");
                enableButton();
                return;
            }
            else if (_now == _old)
            {//same day
                Debug.Log("Same Day - configuring now");
                _configTimerSettings();
                return;
            }
            else
            {
                Debug.Log("error with date");
                return;
            }
        }
        Debug.Log("Day had passed - configuring now");
        _configTimerSettings();
    }

    //setting up and configureing the values
    //update the time information with what we got some the internet
    private void _configTimerSettings()
    {
        _startTime = TimeSpan.Parse(PlayerPrefs.GetString("_timer"));
        _endTime = TimeSpan.Parse(hours + ":" + minutes + ":" + seconds);
        TimeSpan temp = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
        TimeSpan diff = temp.Subtract(_startTime);
        _remainingTime = _endTime.Subtract(diff);
        //start timmer where we left off
        setProgressWhereWeLeftOff();

        if (diff >= _endTime)
        {
            _timerComplete = true;
            enableButton();
        }
        else
        {
            _timerComplete = false;
            //disableButton();
            _timerIsReady = true;
        }
        if (DataStore.data.digAmount < Manger.DigCapacity)
            _timerIsReady = true;
        //else if (DataStore.data.digAmount >= Manger.DigCapacity)
            //_timerComplete = true;
    }

    //initializing the value of the timer
    private void setProgressWhereWeLeftOff()
    {
        float ah = 1f / (float)_endTime.TotalSeconds;
        float bh = 1f / (float)_remainingTime.TotalSeconds;
        _value = ah / bh;
        _progress.fillAmount = _value;
    }



    //enable button function
    private void enableButton()
    {
        //DataStore.data.digAmount = 5;
        timerButton.interactable = true;
        Debug.Log("Enable button called");
        //TmLabel.SetActive(true);
        //timeLabel.text = "CLAIM REWARD";
    }
    public void calcTime()
    {
        hours = (int)totalTm;
        FloatMinuites = (totalTm - hours) * 60;
        Debug.Log("Total Tm: " + totalTm);
        minutes = (int)FloatMinuites;
        hourLeft = hours - _remainingTime.Hours;
        totalDigRiwards = hourLeft * tempDigs;
        /*if(DataStore.data.digAmount == Manger.DigCapacity)
        {
            CheckPickAxe();
            PlayerPrefs.SetString("_timer", "Standby");
        }*/
        //if(_remainingTime.Seconds == 0 && _remainingTime.Minutes == 0) //&& hourLeft != 0
        //totalDigRiwards = hourLeft * tempDigs;
        Debug.Log("Hours Left: " + hourLeft);
        Debug.Log("total dig rewards: " + totalDigRiwards);
        Debug.Log("remaining hours: " + _remainingTime.Hours);
        Debug.Log("total hours: " + hours);
        if (DataStore.data.digAmount < Manger.DigCapacity && _remainingTime.Hours < hours) //&& TimeManager.TmManagerReady && _remainingTime.Minutes == 0 && _remainingTime.Minutes == 0
        {
            if((DataStore.data.digAmount + totalDigRiwards) > Manger.DigCapacity)
            {
                DataStore.data.digAmount = Manger.DigCapacity;
                totalDigRiwards = 0;
            }
            else
            {
                DataStore.data.digAmount = DataStore.data.digAmount + totalDigRiwards;
                totalDigRiwards = 0;
            }              
        }
        //SetData();
    }
    public void CheckPickAxe()
    {
        if (DataStore.data.currentPickaxe == 0)
        {
            tempDigs = 5;
            totalTm = (float)Manger.DigCapacity / (float)tempDigs;
            calcTime();
        }
        else if (DataStore.data.currentPickaxe == 1)
        {
            tempDigs = 6;
            totalTm = (float)Manger.DigCapacity / (float)tempDigs;
            calcTime();
        }
        else if (DataStore.data.currentPickaxe == 2)
        {
            tempDigs = 8;
            totalTm = (float)Manger.DigCapacity /44.0f;
            calcTime();
        }
        else if (DataStore.data.currentPickaxe == 3)
        {
            tempDigs = 11;
            totalTm = (float)Manger.DigCapacity / (float)tempDigs;
            calcTime();
        }
        else if (DataStore.data.currentPickaxe == 4)
        {
            tempDigs = 15;
            totalTm = (float)Manger.DigCapacity / (float)tempDigs;
        }
        else if (DataStore.data.currentPickaxe == 5)
        {
            tempDigs = 21;
            totalTm = (float)Manger.DigCapacity / (float)tempDigs;
            calcTime();
        }
        else if (DataStore.data.currentPickaxe == 6)
        {
            tempDigs = 27;
            totalTm = (float)Manger.DigCapacity / (float)tempDigs;
            calcTime();
        }
        else if (DataStore.data.currentPickaxe == 7)
        {
            tempDigs = 34;
            totalTm = (float)Manger.DigCapacity / (float)tempDigs;
            calcTime();
        }
        else if (DataStore.data.currentPickaxe == 8)
        {
            tempDigs = 42;
            totalTm = (float)Manger.DigCapacity / (float)tempDigs;
            calcTime();
        }
    }

    //disable button function
    private void disableButton()
    {
        timerButton.interactable = false;
        //timeLabel.text = "NOT READY";
    }


    //use to check the current time before completely any task. use this to validate
    private IEnumerator CheckTime()
    {
        //disableButton();
        //timeLabel.text = "Checking the time";
        Debug.Log("==> Checking for new time");
        yield return StartCoroutine(
            TimeManager.sharedInstance.getTime()
        );

        updateTime(); 
        totalTime = _remainingTime.Seconds;
        minuitesss = _remainingTime.Minutes;
        Debug.Log(_remainingTime.Minutes);
        Debug.Log(_remainingTime.Hours);
        Debug.Log("==> Time check complete!");
        //finishCheck();
        //CheckPickAxe();
        //rewardClicked();

    }


    //trggered on button click and resets time
    public void rewardClicked()
    {
        if (timerFinished == true) //DataStore.data.digAmount < Manger.DigCapacity && 
        {
            timerFinished = false;
            TmLabel.SetActive(true);
            Debug.Log("==> Claim Button Clicked");
            claimReward(RewardToEarn);
            hours = (int)totalTm;
            FloatMinuites = (totalTm - hours) * 60;
            minutes = (int)FloatMinuites;
            totalDigRiwards = 0;
            //CheckPickAxe();
            //StartCoroutine("CheckTime");
            PlayerPrefs.SetString("_timer", "Standby");
            //PlayerPrefs.SetInt("tempDigs", tempDigs);
            StartCoroutine("CheckTime");
        }
        else
        {
            Debug.Log("not in");
            return;
        }
    }

    void countDownFormat()
    {
        if(minuitesss < 10 && totalTime < 10)
            timeLabel.text = tempDigs + " digs in 0" + minuitesss.ToString("0") + ":0" + totalTime.ToString("0");
        else if(minuitesss > 9 && totalTime < 10)
            timeLabel.text = tempDigs + " digs in " + minuitesss.ToString("0") + ":0" + totalTime.ToString("0");
        else if (minuitesss < 10 && totalTime > 9)
            timeLabel.text = tempDigs + " digs in 0" + minuitesss.ToString("0") + ":" + totalTime.ToString("0");
        else if (minuitesss > 9 && totalTime > 9)
            timeLabel.text = tempDigs + " digs in " + minuitesss.ToString("0") + ":" + totalTime.ToString("0");
    }

    //update method to make the progress tick
    void Update()
    {
        //finishCheck();
        if (_timerIsReady)
        {
            if (DataStore.data.digAmount >= Manger.DigCapacity)
                timeLabel.text = "+" + Manger.ExtraDigs.ToString() + " digs";
            else
                countDownFormat();
            if (totalTime != 0 && DataStore.data.digAmount < Manger.DigCapacity)
            {
                totalTime -= Time.deltaTime;
                //countDownFormat();
            }
            else if(totalTime == 0 && DataStore.data.digAmount < Manger.DigCapacity)
            {
                //totalTime = 60;
                //countDownFormat();
                minuitesss -= Time.deltaTime;
            }


            //StartCoroutine("waitForDigs");
            /*if(_remainingTime.Minutes > 9)
                timeLabel.text = "25 digs in " + _remainingTime.Minutes.ToString() + ":" + _remainingTime.Seconds.ToString();
            else if(_remainingTime.Minutes < 10)
                timeLabel.text = "25 digs in 0" + _remainingTime.Minutes.ToString() + ":" + _remainingTime.Seconds.ToString();*/

            //StartCoroutine("waitForDigs");
            //Debug.Log(_remainingTime.TotalSeconds);
            if (!_timerComplete && PlayerPrefs.GetString("_timer") != "")
            {
                _value -= Time.deltaTime * 1f / (float)_endTime.TotalSeconds;
                _progress.fillAmount = _value;                
                //this is called once only
                if(totalTime <= 0 && !_timerComplete)
                {                   
                    validateTime();
                    _timerComplete = true;
                }
                if (totalTime == 0 && minuitesss == 0 && !_timerComplete)
                {
                    validateTime();
                    finishCheck();
                    CheckPickAxe();
                    _timerComplete = true;
                }
                if (_remainingTime.Hours == 0 && _remainingTime.Minutes == 0 && _remainingTime.Seconds == 0 && !_timerComplete) //_remainingTime.Hours == 0 && _remainingTime.Minutes == 0 && _remainingTime.Seconds == 0
                {
                    //when the timer hits 0, let do a quick validation to make sure no speed hacks.
                    Debug.Log("Checking and debugging");
                    validateTime();
                    finishCheck();
                    CheckPickAxe();
                    //DataStore.data.digAmount = tempDigs;
                    enableButton();
                    _timerComplete = true;
                }
                else if(DataStore.data.digAmount == Manger.DigCapacity && _remainingTime.Minutes > -1 && !_timerComplete)
                {
                    validateTime();
                    //enableButton();
                    finishCheck();
                    CheckPickAxe();
                    _timerComplete = true;
                }

            }
        }
    }
    void SetData()
    {
        DataStore.data.setData();
    }

    private IEnumerator waitForDigs()
    {
        while (_remainingTime.TotalSeconds > -1)
        {
            yield return new WaitForSeconds(1);
            totalTime = (totalTime - 1);
            timeLabel.text = totalTime.ToString();
            Debug.Log(totalTime); 
        }
        timeLabel.text = "";
        yield break;
    }

    //validator
    private void validateTime()
    {
        //CheckPickAxe();
        Debug.Log("==> Validating time to make sure no speed hack!");
        StartCoroutine("CheckTime");
    }


    private void claimReward(int x)
    {
        Debug.Log("YOU EARN " + x + " REWARDS");
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TMmanger : MonoBehaviour
{
    public float msWait = 3600000f;
    int tempDigs, totalDigRiwards;
    public GameObject storeParentPanel, DigRiwardsDisplay;
    //public RectTransform ClassicBag, LuxuryBag, CollectCoins;
    //public Button store, gift, play, ad, play1v1;
    public static bool RewardsAvailable = false;
    public TextMeshPro timer;

    public LeanTweenType inType;
    //private Button freeBag;
    ulong LastFreeBagOpen;
    // Start is called before the first frame update
    private void Start()
    {
        CheckPickAxe();
        //freeBag = GetComponent<Button>();
        LastFreeBagOpen = ulong.Parse(PlayerPrefs.GetString("LastFreeBagOpenn"));
        //timer = GetComponentInChildren<Text>();
        if (!isFreeBagReady())
            RewardsAvailable = false;
        //freeBag.interactable = false;
    }
    public void resetTimer()
    {
        if(DataStore.data.digAmount == (Manger.DigCapacity - 1))
        {
            LastFreeBagOpen = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastFreeBagOpenn", LastFreeBagOpen.ToString());
            DigRiwardsDisplay.transform.localScale = new Vector3(0, 0, 0);
            RewardsAvailable = false;
            storeParentPanel.SetActive(false);
        }
    }

    void SetData()
    {
        DataStore.data.setData();
    }

    public void CollectBag()
    {
        if ((DataStore.data.digAmount + totalDigRiwards) < Manger.DigCapacity)
            DataStore.data.digAmount = DataStore.data.digAmount + totalDigRiwards;
        else
            DataStore.data.digAmount = Manger.DigCapacity;
        SetData();
        if(DataStore.data.digAmount < Manger.DigCapacity)
        {
            LastFreeBagOpen = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastFreeBagOpenn", LastFreeBagOpen.ToString());
            RewardsAvailable = false;
        }
        DigRiwardsDisplay.transform.localScale = new Vector3(0, 0, 0);
        storeParentPanel.SetActive(false);

    }

    public void CheckPickAxe()
    {
        if (DataStore.data.currentPickaxe == 0)
        {
            tempDigs = 5;
        }
        else if (DataStore.data.currentPickaxe == 1)
        {
            tempDigs = 6;
        }
        else if (DataStore.data.currentPickaxe == 2)
        {
            tempDigs = 8;
        }
        else if (DataStore.data.currentPickaxe == 3)
        {
            tempDigs = 11;
        }
        else if (DataStore.data.currentPickaxe == 4)
        {
            tempDigs = 15;
        }
        else if (DataStore.data.currentPickaxe == 5)
        {
            tempDigs = 21;
        }
        else if (DataStore.data.currentPickaxe == 6)
        {
            tempDigs = 27;
        }
        else if (DataStore.data.currentPickaxe == 7)
        {
            tempDigs = 34;
        }
        else if (DataStore.data.currentPickaxe == 8)
        {
            tempDigs = 42;
        }
    }

    private bool isFreeBagReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - LastFreeBagOpen);
        ulong mls = diff / TimeSpan.TicksPerMillisecond;
        float secondsCount = (float)(msWait - mls) / 1000.0f;
        float timePassed = (float)mls / 1000.0f;

        if (secondsCount < 0)
        {
            if((timePassed / 3600f) > 0) //3600f
            {
                RewardsAvailable = true;
                int digRiwardsAmount = (int)(timePassed / 3600f);
                totalDigRiwards = digRiwardsAmount * tempDigs;
                
            }
            //timer.text = "Ready!";
            return true;
        }
        return false;
    }
    void showCountdown()
    {
        if(DataStore.data.digAmount < Manger.DigCapacity)
        {
            ulong diff = ((ulong)DateTime.Now.Ticks - LastFreeBagOpen);
            ulong mls = diff / TimeSpan.TicksPerMillisecond;
            float secondsCount = (float)(msWait - mls) / 1000.0f;

            string t = "";
            //Hours
            //t += ((int)secondsCount / 3600).ToString() + "h ";
            secondsCount -= ((int)secondsCount / 3600) * 3600;
            //Munites
            t += ((int)secondsCount / 60).ToString("00") + ":";
            //Seconds
            t += (secondsCount % 60).ToString("00");
            timer.text = tempDigs + " digs in " + t;
        }       
    }

    // Update is called once per frame
    private void Update()
    {
        CheckPickAxe();
        if (DataStore.data.digAmount == Manger.DigCapacity)
            timer.text = "Full";
        if (!RewardsAvailable)
        {
            ulong difff = ((ulong)DateTime.Now.Ticks - LastFreeBagOpen);
            ulong mlss = difff / TimeSpan.TicksPerMillisecond;
            float secondsCountt = (float)(msWait - mlss) / 1000.0f;


            if (isFreeBagReady()) //&& (secondsCountt / 3600f) > 0)
            {
                if (DataStore.data.digAmount > 0 && DataStore.data.digAmount < Manger.DigCapacity)
                {
                    storeParentPanel.SetActive(true);
                    LeanTween.scale(DigRiwardsDisplay, new Vector3(1, 1, 1), 20 * Time.deltaTime).setDelay(0.5f).setEase(inType);
                }
                RewardsAvailable = true;
                //int digRiwardsAmount = (int)(secondsCountt / 3600f);
                //totalDigRiwards = digRiwardsAmount * tempDigs;
                //freeBag.interactable = true;
                return;
            }

            showCountdown();

        }
    }
}
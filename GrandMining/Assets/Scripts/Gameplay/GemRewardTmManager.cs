using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GemRewardTmManager : MonoBehaviour
{
    private float msWait = 43200000f;
    //public LeanTweenType inType;
    //public GameObject gem;
    public TextMeshProUGUI timer;
    private bool timeUp;
    ulong LastFreeBagOpen;
    // Start is called before the first frame update
    private void Start()
    {
        LastFreeBagOpen = ulong.Parse(PlayerPrefs.GetString("LastFreeBagOpens"));
        if (!isFreeBagReady())
            timeUp = false;
    }

    public void CollectBag()
    {
        LastFreeBagOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastFreeBagOpens", LastFreeBagOpen.ToString());
        timeUp = false;
    }

    void resetGemState()
    {
        DataStore.data.freeGemState = 0;
        DataStore.data.diamondGathered = 0;
        DataStore.data.goldGathered = 0;
        DataStore.data.ironGathered = 0;
        DataStore.data.rockGathered = 0;
    }

    private bool isFreeBagReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - LastFreeBagOpen);
        ulong mls = diff / TimeSpan.TicksPerMillisecond;
        float secondsCount = (float)(msWait - mls) / 1000.0f;
        if (secondsCount < 0)
        {
            timer.text = "Ready!";
            return true;
        }
        return false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!timeUp)
        {
            if (isFreeBagReady())
            {
                timeUp = true;
                resetGemState();
                CollectBag();
                //LeanTween.scale(gem, new Vector3(1.5f, 1.5f, 1.5f), 20 * Time.deltaTime).setDelay(0.5f).setEase(inType);
                //LeanTween.scale(gem, new Vector3(1f, 1f, 1f), 20 * Time.deltaTime).setDelay(0.5f).setEase(inType);
                DataStore.data.setData();
                return;
            }

            ulong diff = ((ulong)DateTime.Now.Ticks - LastFreeBagOpen);
            ulong mls = diff / TimeSpan.TicksPerMillisecond;
            float secondsCount = (float)(msWait - mls) / 1000.0f;

            string t = "";
            //Hours
            t += ((int)secondsCount / 3600).ToString() + "h ";
            secondsCount -= ((int)secondsCount / 3600) * 3600;
            //Munites
            t += ((int)secondsCount / 60).ToString("00") + "m ";
            //Seconds
            t += (secondsCount % 60).ToString("00") + "s";
            timer.text = t;

        }
    }
}

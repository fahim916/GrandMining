using System.Collections;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class DataStore : MonoBehaviour
{
    public static DataStore data;
    public int done = 0;

    public int cashAmount;
    public int digAmount;
    public int gemAmount;
    public int winstreakAmount;
    public int currentPickaxe;
    public int isInGame;
    public int isSoundOff;
    public int oldPlayer;

    public int freeGemState;

    public int rockGathered;
    public int ironGathered;
    public int goldGathered;
    public int diamondGathered;

    public static bool dataRecieve = false;
    public static bool dataSet = false;

    private WaitForDigs timeScript;
    private void OnEnable()
    {
        if (DataStore.data == null)
        {
            DataStore.data = this;
        }
        else
        {
            if (DataStore.data != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void setData()
    {
        if(dataRecieve == true)
        {
            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
                Statistics = new List<StatisticUpdate> {
            //new StatisticUpdate { StatisticName = "diamondAmount", Value = diamondAmount },
            //new StatisticUpdate { StatisticName = "goldAmount", Value = goldAmount },
            //new StatisticUpdate { StatisticName = "ironAmount", Value = ironAmount },
            //new StatisticUpdate { StatisticName = "rockAmount", Value = rockAmount },
            new StatisticUpdate { StatisticName = "cashAmount", Value = cashAmount },
            new StatisticUpdate { StatisticName = "digAmount", Value = digAmount },
            new StatisticUpdate { StatisticName = "gemAmount", Value = gemAmount },
            new StatisticUpdate { StatisticName = "currentPickAxe", Value = currentPickaxe },
            new StatisticUpdate { StatisticName = "winstreakAmount", Value = winstreakAmount },
            new StatisticUpdate { StatisticName = "isInGame", Value = isInGame },
            new StatisticUpdate { StatisticName = "isSoundOff", Value = isSoundOff },
            new StatisticUpdate { StatisticName = "oldPlayer", Value = oldPlayer },
            new StatisticUpdate { StatisticName = "freeGemState", Value = freeGemState },
            new StatisticUpdate { StatisticName = "rockGathered", Value = rockGathered },
            new StatisticUpdate { StatisticName = "ironGathered", Value = ironGathered },
            new StatisticUpdate { StatisticName = "goldGathered", Value = goldGathered },
            new StatisticUpdate { StatisticName = "diamondGathered", Value = diamondGathered },
            }
            },
        result => { Debug.Log("User statistics updated"); dataSet = true; },
        error => { Debug.LogError(error.GenerateErrorReport()); });
        }       
    }

    public void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        dataRecieve = true;
        foreach (var eachStat in result.Statistics)
        {
            //Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                /*case "diamondAmount":
                    diamondAmount = eachStat.Value;
                    break;
                case "goldAmount":
                    goldAmount = eachStat.Value;
                    break;
                case "ironAmount":
                    ironAmount = eachStat.Value;
                    break;
                case "rockAmount":
                    rockAmount = eachStat.Value;
                    break;*/
                case "cashAmount":
                    cashAmount = eachStat.Value;
                    break;
                case "digAmount":
                    digAmount = eachStat.Value;
                    break;
                case "gemAmount":
                    gemAmount = eachStat.Value;
                    break;
                case "currentPickAxe":
                    currentPickaxe = eachStat.Value;
                    break;
                case "winstreakAmount":
                    winstreakAmount = eachStat.Value;
                    break;
                case "isInGame":
                    isInGame = eachStat.Value;
                    break;
                case "isSoundOff":
                    isSoundOff = eachStat.Value;
                    break;
                case "oldPlayer":
                    oldPlayer = eachStat.Value;
                    break;
                case "freeGemState":
                    freeGemState = eachStat.Value;
                    break;
                case "rockGathered":
                    rockGathered = eachStat.Value;
                    break;
                case "ironGathered":
                    ironGathered = eachStat.Value;
                    break;
                case "goldGathered":
                    goldGathered = eachStat.Value;
                    break;
                case "diamondGathered":
                    diamondGathered = eachStat.Value;
                    break;
            }
            done++;
            //Debug.Log("Data amount: " + done);
            if (data.winstreakAmount == 0)
                Manger.puzzlegoalAmount = Manger.firstStreakgoal;
            if (data.winstreakAmount == 1)
                Manger.puzzlegoalAmount = Manger.secondStreakgoal;
            if (data.winstreakAmount == 2)
                Manger.puzzlegoalAmount = Manger.thirdStreakgoal;
            if (data.winstreakAmount == 3)
                Manger.puzzlegoalAmount = Manger.fourthStreakgoal;
            if (data.winstreakAmount == 4)
                Manger.puzzlegoalAmount = Manger.fifthStreakgoal;
            if (data.winstreakAmount == 0)
                Match3.gemsNeed = 100;
            else if (data.winstreakAmount == 1)
                Match3.gemsNeed = 100;
            else if (data.winstreakAmount == 2)
                Match3.gemsNeed = 100;
            else if (data.winstreakAmount == 3)
                Match3.gemsNeed = 100;
            else if (data.winstreakAmount == 4)
                Match3.gemsNeed = 300;
            //Debug.Log("Puzzle Goal Amount: " + Manger.puzzlegoalAmount);
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

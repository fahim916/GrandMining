using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    int iron2Amount = 50000;
    int iron3Amount = 80000;
    int gold1Amount = 120000;
    int gold2Amount = 150000;
    int gold3Amount = 180000;
    int diamond1Amount = 250000;
    int diamond2Amount = 420000;
    int diamond3Amount = 600000;
    private Manger manager;
    public Button iron2, iron3, gold1, gold2, gold3, diamond1, diamond2, diamond3;
    public GameObject Upiron2, Upiron3, Upgold1, Upgold2, Upgold3, Updiamond1, Updiamond2, Updiamond3;
    public GameObject Iron2, Iron3, Gold1, Gold2, Gold3, Diamond1, Diamond2, Diamond3, notoficationIndication;
    // Start is called before the first frame update
    void Start()
    {
        upgrade();
        checkCurrentPickAxe();
        extraDigCalc();
    }

    // Update is called once per frame
    void Update()
    {
        upgrade();
        checkCurrentPickAxe();
        extraDigCalc();
    }

    void checkCurrentPickAxe()
    {
        if(DataStore.data.currentPickaxe == 0)
        {
            Manger.DigCapacity = 35;
        }
        else if(DataStore.data.currentPickaxe == 1)
        {
            Manger.DigCapacity = 40;
            Iron2.SetActive(false);
            Upiron2.SetActive(true);
        }
        else if(DataStore.data.currentPickaxe == 2)
        {
            Manger.DigCapacity = 45;
            Iron3.SetActive(false);
            Upiron2.SetActive(true);
            Upiron3.SetActive(true);
        }
        else if (DataStore.data.currentPickaxe == 3)
        {
            Manger.DigCapacity = 50;
            Gold1.SetActive(false);
            Upiron2.SetActive(true);
            Upiron3.SetActive(true);
            Upgold1.SetActive(true);
        }
        else if (DataStore.data.currentPickaxe == 4)
        {
            Manger.DigCapacity = 50;
            Gold2.SetActive(false);
            Upiron2.SetActive(true);
            Upiron3.SetActive(true);
            Upgold1.SetActive(true);
            Upgold2.SetActive(true);
        }
        else if (DataStore.data.currentPickaxe == 5)
        {
            Manger.DigCapacity = 50;
            Gold3.SetActive(false);
            Upiron2.SetActive(true);
            Upiron3.SetActive(true);
            Upgold1.SetActive(true);
            Upgold2.SetActive(true);
            Upgold3.SetActive(true);
        }
        else if (DataStore.data.currentPickaxe == 6)
        {
            Manger.DigCapacity = 60;
            Diamond1.SetActive(false);
            Upiron2.SetActive(true);
            Upiron3.SetActive(true);
            Upgold1.SetActive(true);
            Upgold2.SetActive(true);
            Upgold3.SetActive(true);
            Updiamond1.SetActive(true);
        }
        else if (DataStore.data.currentPickaxe == 7)
        {
            Manger.DigCapacity = 65;
            Diamond2.SetActive(false);
            Upiron2.SetActive(true);
            Upiron3.SetActive(true);
            Upgold1.SetActive(true);
            Upgold2.SetActive(true);
            Upgold3.SetActive(true);
            Updiamond1.SetActive(true);
            Updiamond2.SetActive(true);
        }
        else if (DataStore.data.currentPickaxe == 8)
        {
            Manger.DigCapacity = 70;
            Diamond3.SetActive(false);
            Upiron2.SetActive(true);
            Upiron3.SetActive(true);
            Upgold1.SetActive(true);
            Upgold2.SetActive(true);
            Upgold3.SetActive(true);
            Updiamond1.SetActive(true);
            Updiamond2.SetActive(true);
            Updiamond3.SetActive(true);
        }
    }

    void extraDigCalc()
    {
        if (DataStore.data.digAmount > Manger.DigCapacity)
        {
            Manger.ExtraDigs = DataStore.data.digAmount - Manger.DigCapacity;
        }
    }

    void upgrade()
    {
        if(DataStore.data.currentPickaxe == 0 && DataStore.data.cashAmount >= iron2Amount)
        {
            notoficationIndication.SetActive(true);
            iron2.interactable = true;
        }
        else if(DataStore.data.currentPickaxe == 1 && DataStore.data.cashAmount >= iron3Amount)
        {
            notoficationIndication.SetActive(true);
            iron3.interactable = true;
        }
        else if (DataStore.data.currentPickaxe == 2 && DataStore.data.cashAmount >= gold1Amount)
        {
            notoficationIndication.SetActive(true);
            gold1.interactable = true;
        }
        else if (DataStore.data.currentPickaxe == 3 && DataStore.data.cashAmount >= gold2Amount)
        {
            notoficationIndication.SetActive(true);
            gold2.interactable = true;
        }
        else if (DataStore.data.currentPickaxe == 4 && DataStore.data.cashAmount >= gold3Amount)
        {
            notoficationIndication.SetActive(true);
            gold3.interactable = true;
        }
        else if (DataStore.data.currentPickaxe == 5 && DataStore.data.cashAmount >= diamond1Amount)
        {
            notoficationIndication.SetActive(true);
            diamond1.interactable = true;
        }
        else if (DataStore.data.currentPickaxe == 6 && DataStore.data.cashAmount >= diamond2Amount)
        {
            notoficationIndication.SetActive(true);
            diamond2.interactable = true;
        }
        else if (DataStore.data.currentPickaxe == 7 && DataStore.data.cashAmount >= diamond3Amount)
        {
            notoficationIndication.SetActive(true);
            diamond3.interactable = true;
        }
    }

    public void iron2But()
    {
        DataStore.data.currentPickaxe = 1;
        DataStore.data.cashAmount = DataStore.data.cashAmount - iron2Amount;
        Iron2.SetActive(false);
        Upiron2.SetActive(true);
        //manager.SetData();
    }
    public void iron3But()
    {
        DataStore.data.currentPickaxe = 2;
        DataStore.data.cashAmount = DataStore.data.cashAmount - iron3Amount;
        Iron3.SetActive(false);
        Upiron3.SetActive(true);
        //manager.SetData();
    }
    public void gold1But()
    {
        DataStore.data.currentPickaxe = 3;
        DataStore.data.cashAmount = DataStore.data.cashAmount - gold1Amount;
        Gold1.SetActive(false);
        Upgold1.SetActive(true);
        //manager.SetData();
    }
    public void gold2But()
    {
        DataStore.data.currentPickaxe = 4;
        DataStore.data.cashAmount = DataStore.data.cashAmount - gold2Amount;
        Gold2.SetActive(false);
        Upgold2.SetActive(true);
        //manager.SetData();
    }
    public void gold3But()
    {
        DataStore.data.currentPickaxe = 5;
        DataStore.data.cashAmount = DataStore.data.cashAmount - gold3Amount;
        Gold3.SetActive(false);
        Upgold3.SetActive(true);
        //manager.SetData();
    }
    public void diamond1but()
    {
        DataStore.data.currentPickaxe = 6;
        DataStore.data.cashAmount = DataStore.data.cashAmount - diamond1Amount;
        Diamond1.SetActive(false);
        Updiamond1.SetActive(true);
        //manager.SetData();
    }
    public void diamond2But()
    {
        DataStore.data.currentPickaxe = 7;
        DataStore.data.cashAmount = DataStore.data.cashAmount - diamond2Amount;
        Diamond2.SetActive(false);
        Updiamond2.SetActive(true);
        //manager.SetData();
    }
    public void diamond3But()
    {
        DataStore.data.currentPickaxe = 8;
        DataStore.data.cashAmount = DataStore.data.cashAmount - diamond3Amount;
        Diamond3.SetActive(false);
        Updiamond3.SetActive(true);
        //manager.SetData();
    }
}

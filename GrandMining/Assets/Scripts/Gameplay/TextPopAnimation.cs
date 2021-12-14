using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextPopAnimation : MonoBehaviour
{
    private SlotMachine slotMachine;
    private float timeInterval;
    public GameObject DiamondPop, GoldPop, IronPop, RockPop, PuzzlePop;
    public LeanTweenType inType;
    public LeanTweenType outType;
    //public UnityEvent onCompleteCallBack;


    /*public void OnEnable()
    {
        //transform.localScale = new Vector3(0, 0, 0);
        //LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.25f).setDelay(0.1f).setOnComplete(OnComplete).setEase(inType);
        SelectionPopUp();
    }*/
    /*public void OnComplete()
    {
        if(onCompleteCallBack != null)
        {
            onCompleteCallBack.Invoke();
        }
    }*/
    public void OnClose()
    {
        LeanTween.scale(PuzzlePop, new Vector3(0, 0, 0), 0.25f).setEase(outType);
        LeanTween.scale(RockPop, new Vector3(0, 0, 0), 0.25f).setEase(outType);
        LeanTween.scale(DiamondPop, new Vector3(0, 0, 0), 0.25f).setEase(outType);
        LeanTween.scale(IronPop, new Vector3(0, 0, 0), 0.25f).setEase(outType);
        LeanTween.scale(GoldPop, new Vector3(0, 0, 0), 0.25f).setEase(outType);
    }

    /*private void DestroyThis()
    {
        Destroy(gameObject);
    }*/
    private void StartPopUp()
    {
        StartCoroutine("show");
    }
    private IEnumerator show()
    {
        timeInterval = 2f;
        if (slotMachine.stoppedSlot == "Rock")
        {
            //RockPop.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(RockPop, new Vector3(1, 1, 1), 0.25f).setEase(inType);
            Debug.Log("Animation!!!");
        }
        else if (slotMachine.stoppedSlot == "Iron")
        {
            //IronPop.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(IronPop, new Vector3(1, 1, 1), 0.25f).setEase(inType);
        }
        else if (slotMachine.stoppedSlot == "Gold")
        {
            //GoldPop.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(GoldPop, new Vector3(1, 1, 1), 0.25f).setEase(inType);
        }
        else if (slotMachine.stoppedSlot == "Diamond")
        {
            //DiamondPop.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(DiamondPop, new Vector3(1, 1, 1), 0.25f).setEase(inType);
        }
        else if (slotMachine.stoppedSlot == "Puzzle")
        {
           // PuzzlePop.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(PuzzlePop, new Vector3(1, 1, 1), 0.25f).setEase(inType);
            //LeanTween.scale(PuzzlePop, new Vector3(1, 1, 1), 0.25f).setDelay(0.1f).setOnComplete(OnComplete).setEase(inType);
        }
        yield return new WaitForSeconds(timeInterval);

    }
    // Start is called before the first frame update
    void Start()
    {
        PuzzlePop.transform.localScale = new Vector3(0, 0, 0);
        RockPop.transform.localScale = new Vector3(0, 0, 0);
        IronPop.transform.localScale = new Vector3(0, 0, 0);
        GoldPop.transform.localScale = new Vector3(0, 0, 0);
        DiamondPop.transform.localScale = new Vector3(0, 0, 0);
        Manger.ShowPopUp += StartPopUp;
    }

    // Update is called once per frame
    void Update()
    {
        //SelectionPopUp();
    }
}

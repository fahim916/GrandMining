using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Match3 : MonoBehaviour
{
    public GameObject movesBuyPanel, DigrewardsCollectPanel, buy100Gems, shareForGems, watchForGems, winningStreakAnimation;
    public TextMeshProUGUI gemsNeedAmount;
    public Button moveBuyButton;
    public LeanTweenType inType;
    public ArrayLayout boardlayout;
    private Manger manager;
    private SlotMachine slotMachine;
    int moveEndCheck = 0;

    Vector3 starFlowPosition = new Vector3(0f, 10.7f, -3.1f);//(0f, -5.3f, -2f);
    Vector3 starFlowRotation = new Vector3(-90f, 0f, 0f);

    public static int gemsNeed;
    [Header("UI Elements")]
    public Sprite[] peices;
    public RectTransform gameBoard;
    public RectTransform killedBoard;

    [Header("Prefabs")]
    public GameObject NodePeice;
    public GameObject killedPeice;

    public static bool change = false;
    //bool ClickedMoveBUt = false;
    //bool ClickedCloseBUt = false;
    public static int diamondCount;
    int width = 7;
    int height = 12;
    int[] fills;
    node[,] board;
    System.Random random;

    List<NodePeice> update;
    List<FLippedPeices> Flipped;
    List<NodePeice> dead;
    List<KilledPiece> killed;

    void Start()
    {
        DigrewardsCollectPanel.transform.localScale = new Vector3(0, 0, 0);
        movesBuyPanel.transform.localScale = new Vector3(0, 0, 0);
        buy100Gems.transform.localScale = new Vector3(0, 0, 0);
        shareForGems.transform.localScale = new Vector3(0, 0, 0);
        watchForGems.transform.localScale = new Vector3(0, 0, 0);
        changeGemNeedAmount();
        GameStart();
    }
    private void Update()
    {
        gemsNeedAmount.text = gemsNeed.ToString();
        List<NodePeice> finishedUpdating = new List<NodePeice>();
        for(int i = 0; i < update.Count; i++)
        {
            NodePeice peice = update[i];
            bool updating = peice.updatePeiece();
            if (!peice.updatePeiece()) finishedUpdating.Add(peice);
        }
        for (int i = 0; i < finishedUpdating.Count; i++)
        {
            NodePeice piece = finishedUpdating[i];
            FLippedPeices flip = getFliped(piece);
            NodePeice flippedPiece = null;

            int x = (int)piece.index.x;
            fills[x] = Mathf.Clamp(fills[x] - 1, 0, width);
                
            List<positionPoint> connected = isConnected(piece.index, true);
            bool wasFlipped = (flip != null);
            if (wasFlipped) // if we flipped to make this update
            {

                flippedPiece = flip.getOtherPiece(piece);
                AddPoints(ref connected, isConnected(flippedPiece.index, true));
            }
            if(connected.Count == 0) // if we didn't make a match
            {
                if (wasFlipped) // if we  flipped
                    flipPieces(piece.index, flippedPiece.index, false); //flip back
            }
            else // if we made a match
            {
                moveEndCheck = 1;
                //if (Manger.moveAmount >= 0 && Manger.puzzlegoalAmount <= 0 && MovePieces.MoveCheck == true)
                //{
                //DataStore.data.winstreakAmount = DataStore.data.winstreakAmount + 1;
                //MovePieces.MoveCheck = false;
                //}
                if (MovePieces.MoveCheck == true)
                {
                    Manger.moveAmount = Manger.moveAmount - 1;
                    MovePieces.MoveCheck = false;
                }
                foreach (positionPoint pnt in connected) //remove the npde pieces connected
                {
                    int val = getValuePoint(pnt) - 1;
                    if (val == 3)
                    {
                        soundManager.playSound("match3");
                        diamondCount = diamondCount + 1;
                        KillPiece(pnt);
                        Manger.puzzlegoalAmount = Manger.puzzlegoalAmount - diamondCount;
                        if (Manger.puzzlegoalAmount <= 0)
                            Manger.puzzlegoalAmount = 0;
                        
                        //Debug.Log("Diamond capture: "+ diamondCount);
                        diamondCount = 0;
                    }
                    node Node = getNodeAtpoint(pnt);
                    NodePeice nodePiece = Node.getPiece();
                    if (nodePiece != null)
                    {
                        nodePiece.gameObject.SetActive(false);
                        dead.Add(nodePiece);
                    }
                    Node.SetPiece(null);
                }
                if (Manger.moveAmount < 1 || Manger.puzzlegoalAmount < 1)
                {
                    MovePieces.EndCheck = true;
                    if (Manger.moveAmount > -1 && Manger.puzzlegoalAmount < 1)
                    {
                        int noStreak = 0;
                        int firstStreak = 1;
                        int secondStreak = 2;
                        int thirdStreak = 3;
                        int fouthStreak = 4;

                        if (DataStore.data.winstreakAmount == noStreak && change == false)
                        {
                            change = true;
                            Debug.Log("Streak 1");
                            DataStore.data.winstreakAmount = 1;
                            changeGemNeedAmount();
                            Manger.puzzleDone = true;
                        }
                        else if (DataStore.data.winstreakAmount == firstStreak && change == false)
                        {
                            change = true;
                            Debug.Log("Streak 2");
                            DataStore.data.winstreakAmount = 2;
                            changeGemNeedAmount();
                            Manger.puzzleDone = true;
                        }
                        else if (DataStore.data.winstreakAmount == secondStreak && change == false)
                        {
                            change = true;
                            Debug.Log("Streak 3");
                            DataStore.data.winstreakAmount = 3;
                            changeGemNeedAmount();
                            Manger.puzzleDone = true;
                        }
                        else if (DataStore.data.winstreakAmount == thirdStreak && change == false)
                        {
                            change = true;
                            Debug.Log("Streak 4");
                            DataStore.data.winstreakAmount = 4;
                            changeGemNeedAmount();
                            Manger.puzzleDone = true;
                        }
                        else if (DataStore.data.winstreakAmount == fouthStreak && change == false)
                        {
                            change = true;
                            Debug.Log("Streak 5");
                            DataStore.data.winstreakAmount = 5;
                            changeGemNeedAmount();
                            StartCoroutine("waitForTheLastMatchWin");
                        }
                    }
                    else if(Manger.moveAmount <= 0 && Manger.puzzlegoalAmount > 0)
                    {
                        StartCoroutine("waitForTheLastMatchLose");
                    }                                     
                    //StartCoroutine("waitForTheDeadGem");
                }
                applyGravityToBoard();
            }


            Flipped.Remove(flip); // remove the flip after update
            update.Remove(piece);

        }
        if (DataStore.data.gemAmount >= gemsNeed)
            moveBuyButton.interactable = true;
    }
    private IEnumerator waitForTheLastMatchWin()
    {
        float waitTime = 2f;
        yield return new WaitForSeconds(waitTime);
        LeanTween.scale(DigrewardsCollectPanel, new Vector3(1, 1, 1), 25 * Time.deltaTime).setEase(inType);
    }
    private IEnumerator waitForTheLastMatchLose()
    {
        float waitTime = 2f;
        yield return new WaitForSeconds(waitTime);
        moveEndCheck = 0;
        if(moveEndCheck > 0)
            yield return new WaitForSeconds(1);
        moveEndCheck = 0;
        if (Manger.moveAmount > -1 && Manger.puzzlegoalAmount < 1)
            yield break;
        LeanTween.scale(movesBuyPanel, new Vector3(1, 1, 1), 25 * Time.deltaTime).setEase(inType);
        yield return new WaitForSeconds(0.5f);
        LeanTween.scale(buy100Gems, new Vector3(1, 1, 1), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(shareForGems, new Vector3(1, 1, 1), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(watchForGems, new Vector3(1, 1, 1), 25 * Time.deltaTime).setEase(inType);
    }
    public void changeGemNeedAmount()
    {
        if (DataStore.data.winstreakAmount == 0)
            gemsNeed = 100;
        else if (DataStore.data.winstreakAmount == 1)
            gemsNeed = 100;
        else if (DataStore.data.winstreakAmount == 2)
            gemsNeed = 100;
        else if (DataStore.data.winstreakAmount == 3)
            gemsNeed = 100;
        else if (DataStore.data.winstreakAmount == 4)
            gemsNeed = 300;
    }
    public void BuyMoveClicked()
    {
        if (DataStore.data.gemAmount >= gemsNeed)
        {
            MovePieces.EndCheck = false;
            Manger.moveAmount = 5;
            DataStore.data.gemAmount = DataStore.data.gemAmount - gemsNeed;
            LeanTween.scale(movesBuyPanel, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        }
        else
        {
            moveBuyButton.interactable = false;
        }
        //ClickedMoveBUt = true;
    }
    public void CollectDigRewards()
    {
        DataStore.data.digAmount = DataStore.data.digAmount + 20;
        //DataStore.data.gemAmount = DataStore.data.gemAmount + 300;
        DataStore.data.cashAmount = DataStore.data.cashAmount + 5000;
        Manger.collectRiward = true;
        Instantiate(winningStreakAnimation, starFlowPosition, transform.rotation * Quaternion.Euler(starFlowRotation));
        //SlotMachine.turn = 0;
        /*float numberOfPuzzle1 = ((float)(DataStore.data.digAmount) * (float)17) / (float)100;
        float maxTurn1 = ((float)(DataStore.data.digAmount) * (float)80) / (float)100;
        float meanTurn1 = ((float)(DataStore.data.digAmount) * (float)54) / (float)100;
        float minTurn1 = ((float)(DataStore.data.digAmount) * (float)17) / (float)100;
        SlotMachine.numberOfPuzzle = Mathf.RoundToInt(numberOfPuzzle1);
        SlotMachine.minTurn = Mathf.RoundToInt(minTurn1);
        SlotMachine.maxTurn = Mathf.RoundToInt(maxTurn1);
        SlotMachine.meanTurn = Mathf.RoundToInt(meanTurn1);*/
        LeanTween.scale(DigrewardsCollectPanel, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        //Manger.puzzleDone = true;
        if (DataStore.data.winstreakAmount == 5)
            DataStore.data.winstreakAmount = 0;

        //slotMachine.streakBasedGoal();
        changeGemNeedAmount();
    }
    public void closeButClicked()
    {
        int NoStreak = 0;
        int FirstStreak = 1;
        int SecondStreak = 2;
        int ThirdStreak = 3;
        int FourthStreak = 4;
        if (DataStore.data.winstreakAmount == NoStreak)
            DataStore.data.winstreakAmount = 0;
        else if (DataStore.data.winstreakAmount == FirstStreak)
            DataStore.data.winstreakAmount = 0;
        else if (DataStore.data.winstreakAmount == SecondStreak)
            DataStore.data.winstreakAmount = 0;
        else if (DataStore.data.winstreakAmount == ThirdStreak)
            DataStore.data.winstreakAmount = 0;
        else if (DataStore.data.winstreakAmount == FourthStreak)
            DataStore.data.winstreakAmount = 0;
        changeGemNeedAmount();
        //ClickedCloseBUt = false;
        LeanTween.scale(movesBuyPanel, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(buy100Gems, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(shareForGems, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        LeanTween.scale(watchForGems, new Vector3(0, 0, 0), 25 * Time.deltaTime).setEase(inType);
        //ClickedCloseBUt = true;
        //MovePieces.EndCheck = true;
        Manger.puzzleDone = true;
        moveBuyButton.interactable = true;
    }
    /*void streakIncrease()
    {
        int StreakIncrease = 1;
        DataStore.data.winstreakAmount = DataStore.data.winstreakAmount + StreakIncrease;
    }*/

    void applyGravityToBoard()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = (height - 1); y >= 0; y--)
            {
                positionPoint p = new positionPoint(x, y);
                node Node = getNodeAtpoint(p);
                int val = getValuePoint(p);
                if (val != 0) continue; //if it is not a whole, d0 nothing
                for(int ny = (y-1); ny >= -1; ny--)
                {
                    positionPoint next = new positionPoint(x, ny);
                    int nextVal = getValuePoint(next);
                    if (nextVal == 0)
                        continue;
                    if(nextVal != -1) // if we did not hit an end, but its not 0 then use this to fill the current hole
                    {
                        node got = getNodeAtpoint(next);
                        NodePeice piece = got.getPiece();

                        //Set the hole
                        Node.SetPiece(piece);
                        update.Add(piece);

                        //Replace the hole
                        got.SetPiece(null);
                    }
                    else // HIt an end
                    {
                        //Fill in the hole
                        int newVal = fillPiece();
                        NodePeice piecess;
                        positionPoint fallPnt = new positionPoint(x, (-1 - fills[x]));
                        if (dead.Count > 0)
                        {
                            NodePeice revived = dead[0];
                            revived.gameObject.SetActive(true);
                            //revived.rect.anchoredPosition = getPositionFromPoint(fallPnt);
                            piecess = revived;
                            
                            dead.RemoveAt(0);
                        }
                        else
                        {
                            GameObject obj = Instantiate(NodePeice, gameBoard);
                            NodePeice n = obj.GetComponent<NodePeice>();
                            //RectTransform rect = obj.GetComponent<RectTransform>();
                            
                            piecess = n;
                        }

                        piecess.initialize(newVal, p, peices[newVal - 1]);
                        piecess.rect.anchoredPosition = getPositionFromPoint(fallPnt);
                        node hole = getNodeAtpoint(p);
                        hole.SetPiece(piecess);
                        resetPeice(piecess);
                        fills[x]++;
                    }                    
                    break;

                }
            }
        }
    }

    FLippedPeices getFliped(NodePeice p)
    {
        FLippedPeices flip = null;
        for (int i = 0; i < Flipped.Count; i++)
        {
            if (Flipped[i].getOtherPiece(p) != null)
            {
                flip = Flipped[i];
                break;
            }                
        }
        return flip;
    }

    void GameStart()
    {
        //board = new node[width, height];
        fills = new int[width];
        string seed = getRandomSeed();
        random = new System.Random(seed.GetHashCode());
        update = new List<NodePeice>();
        Flipped = new List<FLippedPeices>();
        dead = new List<NodePeice>();
        killed = new List<KilledPiece>();
        InitializeBoard();
        boardVarify();
        instantiateBoard();
    }
    void InitializeBoard()
    {
        board = new node[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                board[x, y] = new node((boardlayout.rows[y].row[x]) ? -1 : fillPiece(), new positionPoint(x, y));
            }
        }
    }
    void boardVarify()
    {
        List<int> remove;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                positionPoint p = new positionPoint(x, y);
                int val = getValuePoint(p);
                if (val <= 0) continue;

                remove = new List<int>();
                while(isConnected(p, true).Count > 0)
                {
                    val = getValuePoint(p);
                    if (!remove.Contains(val))
                        remove.Add(val);
                    SetValuePoint(p, newValue(ref remove));
                }
            }
        }
    }

    void instantiateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                node nd = getNodeAtpoint(new positionPoint(x, y));
                int val = nd.value;
                if (val <= 0) continue;
                GameObject p = Instantiate(NodePeice, gameBoard);
                NodePeice peice = p.GetComponent<NodePeice>();
                RectTransform rect = p.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(32 + (64 * x), -32 - (64 * y));
                peice.initialize(val, new positionPoint(x, y), peices[val - 1]);
                nd.SetPiece(peice);
            }
        }
    }

    public void resetPeice(NodePeice peice)
    {
        peice.resetPos();
        update.Add(peice);
    }

    public void flipPieces(positionPoint one, positionPoint two, bool main)
    {
        if (getValuePoint(one) < 0) return;
        node nodeOne = getNodeAtpoint(one);
        NodePeice pieceOne = nodeOne.getPiece();

        if (getValuePoint(two) > 0)
        {

            node nodeTwo = getNodeAtpoint(two);
            NodePeice pieceTwo = nodeTwo.getPiece();
            nodeOne.SetPiece(pieceTwo);
            nodeTwo.SetPiece(pieceOne);
            if (main)
            {
                Flipped.Add(new FLippedPeices(pieceOne, pieceTwo));
            }
            /*

            pieceOne.flipped = pieceTwo;
            pieceTwo.flipped = pieceOne;
            */

            update.Add(pieceOne);
            update.Add(pieceTwo);
        }
        else
            resetPeice(pieceOne);
    }

    List<positionPoint> isConnected(positionPoint p, bool main)
    {
        List<positionPoint> connected = new List<positionPoint>();
        int val = getValuePoint(p);
        positionPoint[] directions =
        {
            positionPoint.up,
            positionPoint.right,
            positionPoint.down,
            positionPoint.left
        };

        foreach (positionPoint dir in directions) //Checking if there is 2 or more same shapes in the directions
        {
            List<positionPoint> line = new List<positionPoint>();
            int same = 0;           
            for(int i = 1; i < 3; i++)
            {
                positionPoint check = positionPoint.Addition(p, positionPoint.mulTipication(dir, i));
                if(getValuePoint(check) == val)
                {
                    line.Add(check);
                    same++;
                }
            }
            if (same > 1) //if there are more than 1 of the same shape in the direction, then it is a match
                AddPoints(ref connected, line); //Add these points to the overarching connected list
        }
        for(int i = 0; i < 2; i++) //Checking if we are in the middle of the same two shapes
        {
            List<positionPoint> line = new List<positionPoint>();
            int same = 0;
            positionPoint[] check = { positionPoint.Addition(p, directions[i]), positionPoint.Addition(p, directions[i + 2]) };
            foreach (positionPoint next in check) //Check both sides of he piece, if they are the same value, add them to the list.
            {
                if (getValuePoint(next) == val)
                {
                    line.Add(next);
                    same++;
                }
            }
            if (same > 1)
                AddPoints(ref connected, line);
        }

        for(int i = 0; i < 4; i++) //Check for a 2x2
        {
            List<positionPoint> square = new List<positionPoint>();

            int same = 0;
            int next = i + 1;
            if (next >= 4)
                next -= 4;
            positionPoint[] check = { positionPoint.Addition(p, directions[i]), positionPoint.Addition(p, directions[next]), positionPoint.Addition(p, positionPoint.Addition(directions[i], directions[next]))};
            foreach (positionPoint pnt in check) //Check all sides of he piece, if they are the same value, add them to the list.
            {
                if (getValuePoint(pnt) == val)
                {
                    square.Add(pnt);
                    same++;
                }
            }
            if (same > 2)
                AddPoints(ref connected, square);
        }
        if (main) //Checks for other matches along the current match.
        {
            for (int i = 0; i < connected.Count; i++)
                AddPoints(ref connected, isConnected(connected[i], false));
        }
        //if (connected.Count > 0)
            //connected.Add(p);
        return connected;
    }

    void AddPoints(ref List<positionPoint> points, List<positionPoint> add)
    {
        foreach(positionPoint p in add)
        {
            bool DoAdd = true;
            for(int i = 0; i < points.Count; i++)
            {
                if (points[i].Equals(p))
                {
                    DoAdd = false;
                    break;
                }
            }
            if (DoAdd) points.Add(p);
        }
    }

    int fillPiece()
    {
        int val = 1;
        val = (random.Next(0, 100) / (100 / peices.Length)) + 1;
        //val = random.Next(1, peices.Length);
        return val;
    }

    void KillPiece(positionPoint p)
    {
        List<KilledPiece> available = new List<KilledPiece>();
        for (int i = 0; i < killed.Count; i++)
            if (!killed[i].falling) available.Add(killed[i]);
        KilledPiece set = null;

        if (available.Count > 0)
            set = available[0];
            //Debug.Log("Available");
        else
        {

            GameObject kill = GameObject.Instantiate(killedPeice, killedBoard);
            KilledPiece kPiece = kill.GetComponent<KilledPiece>();
            set = kPiece;
            Debug.Log("Not Availavle");
            killed.Add(kPiece);
        }
        int val = getValuePoint(p) - 1;
        if (set != null && val >= 0 && val < peices.Length)
            set.initialize(peices[val], getPositionFromPoint(p));
        //if (set != null && val == 3)
            //set.initialize(peices[val], getPositionFromPoint(p));
    }

    int getValuePoint(positionPoint p)
    {
        if (p.x < 0 || p.x >= width || p.y < 0 || p.y >= height) return -1;
        return board[p.x, p.y].value;
    }

    void SetValuePoint(positionPoint p, int v)
    {
        board[p.x, p.y].value = v;
    }

    node getNodeAtpoint(positionPoint p)
    {
        return board[p.x, p.y];
    }


    int newValue(ref List<int> remove)
    {
        List<int> available = new List<int>();
        for (int i = 0; i < peices.Length; i++)
            available.Add(i + 1);
        foreach (int i in remove)
            available.Remove(i);
        if (available.Count <= 0) return 0;
        return available[random.Next(0, available.Count)];
    }
    string getRandomSeed()
    {
        string seed = "";
        string AccecptableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()";
        for (int i = 0; i < 20; i++)
            seed += AccecptableChars[Random.Range(0, AccecptableChars.Length)];
        return seed;
    }

    public Vector2 getPositionFromPoint(positionPoint p)
    {
        return new Vector2(32 + (64 * p.x), -32 - (64 * p.y));
    }
}

[System.Serializable]
public class node
{
    public int value; // 0 = blank, 1 = rock, 2 = iron, 3 = gold, 4 = diamond, -1 = hole
    public positionPoint index;
     NodePeice piece;

    public node(int v, positionPoint i)
    {
        value = v;
        index = i;
    }

    public void SetPiece(NodePeice p)
    {
        piece = p;
        value = (piece == null) ? 0 : piece.value;
        if (piece == null) return;
        piece.setIndex(index);
    }

    public NodePeice getPiece()
    {
        return piece;
    }
}

[System.Serializable]
public class FLippedPeices
{
    public NodePeice one;
    public NodePeice two;

    public FLippedPeices (NodePeice o, NodePeice t)
    {
        one = o; two = t;
    }

    public NodePeice getOtherPiece(NodePeice p)
    {
        if (p == one)
            return two;
        else if (p == two)
            return one;
        else
            return null;
    }
}

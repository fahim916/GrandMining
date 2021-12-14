using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePieces : MonoBehaviour
{
    public static MovePieces instance;
    Match3 game;
    public static bool MoveCheck = false;
    public static bool EndCheck = false;
    public static bool streakCheck = false;
    NodePeice moving;
    positionPoint newIndex;
    Vector2 mouseStart;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<Match3>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving != null)
        {
            Vector2 dir = ((Vector2)Input.mousePosition - mouseStart);
            Vector2 nDir = dir.normalized;
            Vector2 aDir = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

            newIndex = positionPoint.clone(moving.index);
            positionPoint add = positionPoint.zero;
            if(dir.magnitude > 32)  //if our mouse is 32px away from the starting point of the mouse
            {
                //make add either (1,0) | (-1,0) | (0,-1) depending on the direction of the mouse point
                if (aDir.x > aDir.y)
                    add = (new positionPoint((nDir.x > 0) ? 1 : -1, 0));
                else if (aDir.y > aDir.x)
                    add = (new positionPoint(0, (nDir.y > 0) ? -1 : 1));
            }
            newIndex.Addition(add);

            Vector2 pos = game.getPositionFromPoint(moving.index);
            if (!newIndex.Equals(moving.index))
                pos += positionPoint.mulTipication(new positionPoint(add.x, -add.y), 16).ToVector();
            moving.movePositionTo(pos);
        }
    }

    public void MovePiece(NodePeice peice)
    {
        if (moving != null) return;
        moving = peice;
        mouseStart = Input.mousePosition;
    }


    public void DropPeice()
    {
        if (moving == null) return;
        //Debug.Log("dropped");
        if (EndCheck == false)
        {
            if (!newIndex.Equals(moving.index))
            {
                game.flipPieces(moving.index, newIndex, true);
                MoveCheck = true;
            }
            else
            {
                game.resetPeice(moving);
            }
        }

        moving = null;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodePeice : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int value;
    public positionPoint index;
    public Vector2 pos;
    [HideInInspector]    
    public RectTransform rect;

    bool updating;
    Image img;

    public void initialize(int v, positionPoint p, Sprite piece)
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        setIndex(p);
        value = v;        
        img.sprite = piece;
    }

    public void setIndex(positionPoint p)
    {
        index = p;
        resetPos();
        UpdateName();
    }

    public void resetPos()
    {
        pos = new Vector2(32 + (64 * index.x), -32 - (64 * index.y));
    }

    void UpdateName()
    {
        transform.name = "Node [" + index.x + ", " + index.y + "]";
    }

    public void movePosition(Vector2 move)
    {
        rect.anchoredPosition += move * Time.deltaTime * 16f;
    }

    public void movePositionTo(Vector2 move)
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, move, Time.deltaTime * 10f);
    }

    public bool updatePeiece()
    {
        if(Vector3.Distance(rect.anchoredPosition, pos) > 1)
        {
            movePositionTo(pos);
            updating = true;
            return true;
        }
        else
        {
            rect.anchoredPosition = pos;
            updating = false;
            return false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (updating) return;
        MovePieces.instance.MovePiece(this);
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MovePieces.instance.DropPeice();
        //throw new System.NotImplementedException();
    }
}

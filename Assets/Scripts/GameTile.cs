using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class GameTile : MonoBehaviour
{
    public int Col { get; set; }

    public int Row { get; set; }
    
    public GameTile(int row, int col)
    {
        Row = row;
        Col = col;
    }


    public void UpdatePosition()
    {
        var transformPosition = transform.position;
        transformPosition.x = Row;
        transformPosition.y = Col;
        transform.DOMove(transformPosition, 0.2f);
    }
}
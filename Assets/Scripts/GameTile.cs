using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class GameTile : MonoBehaviour
{
    private PieceMark _pieceMark;

    public PieceMark PieceMark
    {
        get => _pieceMark;
        set
        {
            _pieceMark = value;
            _spriteRenderer.sprite = Resources.Load<Sprite>($"Art/Piece{(int)value}");
            //Debug.Log((int)value);
        }
    }

    private SpriteRenderer _spriteRenderer;
    public int Col { get; set; }
    public int Row { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
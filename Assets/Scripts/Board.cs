using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public GameTile[,] GameTiles { get; private set; }
    public float deathDuration;
    
    private Config _config;
    
    public event Action<int,int> OnDestroiedTile;

    private void Start()
    {
        _config = FindObjectOfType<Config>();
        GameTiles = new GameTile[_config.BoardSize.x, _config.BoardSize.y];
        var main = Camera.main;
        main.orthographicSize = _config.BoardSize.x;
        main.transform.position = new Vector3(_config.BoardSize.x * 0.5f - 0.5f,
            _config.BoardSize.y * 0.5f - 0.5f,
            -10);
        GenerateBoard();
    }

    public void DestroyTile(int x, int y)
    {
        if (GameTiles[x, y] == null)
        {
            return;
        }
        GameTiles[x, y].gameObject.transform.DOScale(1.25f, deathDuration);
        var spriteRenderer = GameTiles[x, y].gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0f, deathDuration)
            .OnComplete(() =>
            {
                Destroy(GameTiles[x, y].gameObject);
                OnDestroiedTile?.Invoke(x,y);
            });

    }

    private void GenerateBoard()
    {
        for (int x = 0; x < _config.BoardSize.x; x++)
        {
            for (int y = 0; y < _config.BoardSize.y; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    public void GenerateTile(int x, int y)
    {
        if (GameTiles[x, y] != null)
        {
            Debug.Log(GameTiles[x, y] );
            //return;
        }
        
        GameTiles[x, y] = Instantiate(_config.PieceMark,
            new Vector3Int(x, y + 1, 0), Quaternion.identity,transform);
        var lengthPieceMarkEnum = Enum.GetValues(typeof(PieceMark)).Length - 1;
        GameTiles[x, y].PieceMark = (PieceMark)Random.Range(0, lengthPieceMarkEnum);
        var spriteRenderer = GameTiles[x, y].GetComponent<SpriteRenderer>();
        var spriteRendererColor = spriteRenderer.color;
        spriteRendererColor.a = 0;
        GameTiles[x, y].GetComponent<SpriteRenderer>().color = spriteRendererColor;
        GameTiles[x, y].GetComponent<SpriteRenderer>().DOFade(1f, deathDuration);
        GameTiles[x, y].Row = x;
        GameTiles[x, y].Col = y;
        GameTiles[x, y].UpdatePosition();
        //Debug.Log($"Generated {x} {y}");
    }
}
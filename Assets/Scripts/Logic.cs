using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class Logic : MonoBehaviour
    {
        private static Board _board;
        private static Config _config;
        private bool isInteractable = true;

        private void Start()
        {
            _config = FindObjectOfType<Config>();
            _board = FindObjectOfType<Board>();
            _board.OnDestroiedTile += BoardOnOnDestroiedTile;
        }

        private void BoardOnOnDestroiedTile(int x, int y)
        {
            isInteractable = false;
            SettleBlocksNew(x,y);
            isInteractable = true;
        }

        private void SettleBlocksNew(int x, int y)
        {
            var tile = _board.GameTiles[x, y];
            tile.DeathAnimation();
            for (; y < _config.BoardSize.y - 1; y++)
            {
                //Debug.Log($"{x}, {y} destroied settle check");
                _board.GameTiles[x, y] = _board.GameTiles[x, y + 1];
                _board.GameTiles[x, y + 1] = null;
                _board.GameTiles[x, y].Col = y;
                _board.GameTiles[x, y].UpdatePosition();
            }
            tile.BornAnimation();
            _board.GameTiles[x, _config.BoardSize.y - 1] = tile;
            _board.GameTiles[x, y].Col = _config.BoardSize.y - 1;
            _board.GameTiles[x, y].ChangePieceMarkRandom();
            _board.GameTiles[x, y].UpdatePosition();
        }
        
        private void SettleBlocks(int x, int y)
        {
            for (; y < _config.BoardSize.y - 1; y++)
            {
                //Debug.Log($"{x}, {y} destroied settle check");
                _board.GameTiles[x, y] = _board.GameTiles[x, y + 1];
                _board.GameTiles[x, y + 1] = null;
                _board.GameTiles[x, y].Col = y;
                _board.GameTiles[x, y].UpdatePosition();
            }
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && isInteractable)
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var xRound = (int)Math.Round(mouseWorldPos.x);
                var yRound = (int)Math.Round(mouseWorldPos.y);
                var tile = _board.GameTiles[xRound,yRound];
                _board.DestroyTile(xRound, yRound);
            }

            if (Input.GetMouseButtonDown(1))
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var xRound = (int)Math.Round(mouseWorldPos.x);
                var yRound = (int)Math.Round(mouseWorldPos.y);
                var tile = _board.GameTiles[xRound,yRound];
                var sb = new StringBuilder();
                var tuple = CoordinatesOf<GameTile>(_board.GameTiles, tile);
                sb.Append($"Name: {tile.GetComponent<SpriteRenderer>().sprite.name}\r\n");
                sb.Append($"Row :{tile.Row}\tCol: {tile.Col}\r\n");
                Debug.Log($"in tile array: {tuple}");
                Debug.Log(sb.ToString());
            }

        }

        private static Tuple<int, int> CoordinatesOf<T>( GameTile[,] matrix, GameTile value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }
        
    }
}
using System;
using System.Text;
using UnityEngine;

namespace DefaultNamespace
{
    public class Logic : MonoBehaviour
    {
        private static Board _board;
        private static Config _config;

        private void Start()
        {
            _config = FindObjectOfType<Config>();
            _board = FindObjectOfType<Board>();
            _board.OnDestroiedTile += BoardOnOnDestroiedTile;
        }

        private void BoardOnOnDestroiedTile(int x, int y)
        {
            if (y != _config.BoardSize.y - 1)
            {
                SettleBlocks(x,y);
                _board.GenerateTile(x, _config.BoardSize.y - 1);
            }
            else
                _board.GenerateTile(x, y);

            //Debug.Log($"{x}, {y} destroied");
        }

        public static void SettleBlocks(int x, int y)
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
            if (Input.GetMouseButtonDown(0))
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
                sb.Append($"Name: {tile}\r\n");
                sb.Append($"Row :{tile.Row}\tCol: {tile.Col}\r\n");
                var tuple = CoordinatesOf<GameTile>(_board.GameTiles, tile);
                Debug.Log(tuple);
                sb.Append($"Board[x,y]: {tuple.Item1}, {tuple.Item2}");
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
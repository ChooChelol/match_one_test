using System;
using System.ComponentModel;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Helpers
{
    public static void ChangePieceMark(this GameTile gameTile, PieceMark pieceMark)
    {
        if (gameTile == null) throw new ArgumentNullException(nameof(gameTile));
        if (!Enum.IsDefined(typeof(PieceMark), pieceMark))
            throw new InvalidEnumArgumentException(nameof(pieceMark), (int) pieceMark, typeof(PieceMark));
        gameTile.PieceMark = pieceMark;
    }
        
    public static void ChangePieceMarkRandom(this GameTile gameTile)
    {
        if (gameTile == null) throw new ArgumentNullException(nameof(gameTile));
        var lengthPieceMarkEnum = Enum.GetValues(typeof(PieceMark)).Length - 1;
        gameTile.PieceMark = (PieceMark)Random.Range(0, lengthPieceMarkEnum);
    }

    public static void DeathAnimation(this GameTile gameTile)
    {
        gameTile.gameObject.transform.DOScale(1.25f, 0.2f);
        var spriteRenderer = gameTile.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0f, 0.2f);
    }

    public static void BornAnimation(this GameTile gameTile)
    {
        var spriteRenderer = gameTile.GetComponent<SpriteRenderer>();
        var spriteRendererColor = spriteRenderer.color;
        spriteRendererColor.a = 0;
        gameTile.GetComponent<SpriteRenderer>().color = spriteRendererColor;
        gameTile.transform.DOScale(1f, 0.2f);
        gameTile.GetComponent<SpriteRenderer>().DOFade(1f, 0.4f);
    }
}
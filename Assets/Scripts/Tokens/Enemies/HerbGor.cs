using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HerbGor : Gor
{
    public static HerbGor Factory(int cellID) {
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Enemies/Gor");
        GameObject go = new GameObject("Gor");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        go.transform.localScale = new Vector3(10, 10, 10);

        HerbGor gor = go.AddComponent<HerbGor>();
        gor.TokenName = Type;
        gor.Cell = Cell.FromId(cellID);

        gor.Will = 4;
        gor.Strength = 2;
        gor.Reward = 2;

        return gor;
    }
    
    protected void OnDestroy() {
        if(quitting) return;
        if(GameManager.instance.CurrentPlayer == GameManager.instance.MainHero) Herb.Factory(Cell.Index);
        base.OnDestroy();
    }
}
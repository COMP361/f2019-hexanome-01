using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour, IEnemy {
    //int dices;
    GameManager gm;
    Token token;
    static Color color = Color.black;

    public static Troll Factory(int cellID) {
        Token token = Token.Factory(Type, color);
        token.Position(Cell.FromId(cellID));
        Troll troll = token.gameObject.AddComponent<Troll>();
        troll.token = token;

        troll.Will = 12;
        troll.Strength = 14;
        troll.Reward = 6;

        return troll;
    }

    void Start() {
        gm = GameManager.instance;
    }

    public int Will { get; set; }

    public int Strength { get; set; }

    public int Reward { get; set; }

    public Token Token { get; set; }

    public static string Type { get => typeof(Troll).ToString(); }
}
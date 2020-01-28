using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skral : MonoBehaviour, IEnemy {
    //int dices;
    GameManager gm;
    Token token;
    static Color color = Color.black;

    public static Skral Factory(int cellID) {
        Token token = Token.Factory(Type, color);
        token.Position(Cell.FromId(cellID));
        Skral skral = token.gameObject.AddComponent<Skral>();
        skral.token = token;

        skral.Will = 6;
        skral.Strength = 6;
        skral.Reward = 4;

        return skral;
    }

    void Start() {
        gm = GameManager.instance;
    }

    public int Will { get; set; }

    public int Strength { get; set; }

    public int Reward { get; set; }

    public Token Token { get; set; }

    public static string Type { get => typeof(Skral).ToString(); }
}

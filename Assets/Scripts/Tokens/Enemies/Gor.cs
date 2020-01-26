using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gor : MonoBehaviour, IEnemy {
    GameManager gm;
    Token token;
    static Color color = Color.black;

    public static Gor Factory(int cellID) {
        Token token = Token.Factory(Type, color);
        token.Position(Cell.FromId(cellID));
        Gor gor = token.gameObject.AddComponent<Gor>();
        gor.token = token;

        gor.Will = 4;
        gor.Strength = 2;
        gor.Reward = 2;

        return gor;
    }

    void Start() {
        gm = GameManager.instance;
    }

    public int Will { get; set; }

    public int Strength { get; set; }

    public int Reward { get; set; }

    public Token Token { get; set; }

    public static string Type { get => typeof(Gor).ToString(); }
}
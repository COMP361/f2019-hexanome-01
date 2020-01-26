using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrak : MonoBehaviour, IEnemy {
    //int dices;
    GameManager gm;
    Token token;
    static Color color = Color.black;

    public static Wardrak Factory(int cellID) {
        Token token = Token.Factory(Type, color);
        token.Position(Cell.FromId(cellID));
        Wardrak wardrak = token.gameObject.AddComponent<Wardrak>();
        wardrak.token = token;

        wardrak.Will = 7;
        wardrak.Strength = 10;
        wardrak.Reward = 6;

        return wardrak;
    }

    void Start() {
        gm = GameManager.instance;
    }

    public int Will { get; set; }

    public int Strength { get; set; }

    public int Reward { get; set; }

    public Token Token { get; set; }

    public static string Type { get => typeof(Wardrak).ToString(); }
}
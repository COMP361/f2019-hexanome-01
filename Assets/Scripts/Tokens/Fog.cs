using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog {
    Dictionary<string, int> qty = new Dictionary<string, int>();
    protected List<int> cellsID = new List<int>(){ 8, 11, 12, 13, 16, 32, 42, 44, 46, 64, 63, 56, 47, 48, 49 };
    protected List<Token> tokens = new List<Token>();
    protected static GameObject fog;
    

    public Fog() {
        // Set the number of tokens per type
        qty["2will"] = 1;
        qty["3will"] = 1;
        qty["strength"] = 1;
        qty["wineSkin"] = 1;
        qty["witchBrew"] = 1;
        qty["gor"] = 2;
        qty["gold"] = 3;
        qty["event"] = 5;   

        foreach(KeyValuePair<string, int> entry in qty) {
            Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/" + entry.Key);

            for (int i = 0; i < entry.Value; i++) {
                fog = GameObject.Instantiate((GameObject) Resources.Load("Prefabs/Tokens/Fog")) as GameObject;
                GameObject token = fog.transform.Find("Token").gameObject;    
                token.GetComponent<SpriteRenderer>().sprite = sprite;
                tokens.Add(Token.Factory(sprite.name + "Fog", fog));
            }
        }

        tokens.Shuffle();

        for (int i = 0; i < tokens.Count; i++) {
            Cell c = Cell.FromId(cellsID[i]);
            tokens[i].Position(c);
        }
    }

    public bool isOnCell(int cellID) {
        if(cellsID.Contains(cellID)) return true;
        return false;
    }
}
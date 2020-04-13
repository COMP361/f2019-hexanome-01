using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using System.Linq;

public class Fog : Token {
    public static List<int> cellsID = new List<int>(){ 8, 11, 12, 13, 16, 32, 42, 44, 46, 64, 63, 56, 47, 48, 49 };
    static List<Token> tokens = new List<Token>();

    public void OnEnable() {
        EventManager.MoveComplete += OnMoveComplete;
        base.OnEnable();
	}

	public void OnDisable() {
		EventManager.MoveComplete -= OnMoveComplete;
    }   
        
    public static void Factory() {
        Fog2Will.Factory();
        Fog3Will.Factory();
        FogStrength.Factory();
        FogWineSkin.Factory();
        FogWitchBrew.Factory();
        FogGor.Factory();
        FogGold.Factory();
        FogEvent.Factory();

        int[] shuffledCellsID = PhotonNetwork.CurrentRoom.CustomProperties["FogCells"] as int[];

        if (tokens.Count > shuffledCellsID.Length) {
            Debug.Log("Wrong number of Fog tokens initialized. Abort.");
            Fog.Destroy();
            return;
        }

        for(int i = 0; i < tokens.Count; i++) {
            tokens[i].Cell = Cell.FromId(shuffledCellsID[i]);
        }
    }

    public static void Init(string id, int qty, Type type) { 
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/" + id);
        if(sprite == null) {
            Debug.Log("Error creating the Fog Token");
            return;
        }

        for(int i = 0; i < qty; i++) {
            GameObject fog = GameObject.Instantiate((GameObject) Resources.Load("Prefabs/Tokens/Fog")) as GameObject;
            GameObject secretToken = fog.transform.Find("Token").gameObject;    
            secretToken.GetComponent<SpriteRenderer>().sprite = sprite;
            
            Fog fogToken = (Fog)fog.AddComponent(type);
            fogToken.TokenName = sprite.name + "Fog";
            tokens.Add(fogToken);
        }
    }

    public static void Load(string id, Type type, int cell)
    {
        Sprite sprite = Resources.Load<Sprite>("Sprites/Tokens/Fog/" + id);
        if (sprite == null)
        {
            Debug.Log("Error creating the Fog Token");
            return;
        }

        GameObject fog = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Tokens/Fog")) as GameObject;
        GameObject secretToken = fog.transform.Find("Token").gameObject;
        secretToken.GetComponent<SpriteRenderer>().sprite = sprite;

        Fog fogToken = (Fog)fog.AddComponent(type);
        fogToken.TokenName = sprite.name + "Fog";
        tokens.Add(fogToken);
        fogToken.Cell = Cell.FromId(cell);
    }

    public static void Destroy() {
        for(int i = 0; i < tokens.Count; i++) {
            Destroy(tokens[i].gameObject);
        }
    }

    public bool isOnCell(int cellID) {
        if(cellsID.Contains(cellID)) return true;
        return false;
    }

    public virtual void Reveal() {
        GetComponent<SpriteRenderer>().enabled = false;    
    }

    void OnMoveComplete(Token token) {
        if(Cell != null && (!typeof(Hero).IsCompatibleWith(token.GetType()) || token.Cell.Index != Cell.Index)) return;
        Reveal();
        StartCoroutine(timer((Hero)token));
    }

    IEnumerator timer(Hero hero) {
        yield return new WaitForSeconds(2.0f);
        ApplyEffect(hero);
        Cell = null;
        Destroy(gameObject);
    }
    
    public virtual void ApplyEffect(Hero hero) {
    }
}
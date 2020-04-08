using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;

public class LoadGame : MonoBehaviour
{
    public GameObject btn;
    public List<Player> players;
    private string[] heroes = { "Archer", "Warrior", "Mage", "Dwarf" };

    // Start is called before the first frame update
    void Start() {
        players = PhotonNetwork.PlayerList.ToList();
        
        string[] games = Directory.GetDirectories(Path.Combine(Application.persistentDataPath, "Games"));
        foreach(string d in games) {
            bool missingHero = false;
            var heroesName = new List<string>(heroes);
            string[] gameData = Directory.GetFiles(d);

            // Filter heroes with files in save folder
            for (int i = heroesName.Count - 1; i >= 0; i--) {
                if(!Array.Exists(gameData, element => Path.GetFileName(element) == heroesName[i] + ".json")) {
                    heroesName.RemoveAt(i);
                }
            }

            foreach (Player p in players) {
                string hero = (string)p.CustomProperties["Class"];

                // if there is no hero.json in the save folder, ignore the save folder
                if(!Array.Exists(gameData, element => Path.GetFileName(element) == hero + ".json")) {
                    missingHero = true;
                    break;
                } else {
                    heroesName.Remove(hero);
                }
            }

            if(!missingHero && heroesName.Count == 0) {
                var dirName = new DirectoryInfo(d).Name;

                GameObject button = Instantiate(btn) as GameObject;
                Text txt = button.transform.Find("Text").GetComponent<Text>();
                txt.text = dirName;

                button.transform.SetParent(gameObject.transform);
            }
        }
    }
}

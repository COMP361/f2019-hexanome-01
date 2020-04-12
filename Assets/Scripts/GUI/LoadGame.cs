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

    void Start() {
        players = PhotonNetwork.PlayerList.ToList();

        string directoryPath = Application.dataPath.Replace("/Assets", "");
        string[] games = Directory.GetDirectories(Path.Combine(directoryPath, "Saves"));
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
                button.GetComponent<Button>().onClick.AddListener(() => { OnClick(dirName); });

                button.transform.SetParent(gameObject.transform);
                button.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        void OnClick(string name)
        {
            ExitGames.Client.Photon.Hashtable SaveTable = new ExitGames.Client.Photon.Hashtable();
            SaveTable.Add("Save", name);
            PhotonNetwork.CurrentRoom.SetCustomProperties(SaveTable);
        }
    }
}

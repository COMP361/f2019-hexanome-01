using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerFightPlayer : MonoBehaviour
{
    public class HeroFighter : MonoBehaviour
    {
        public Text name;
        public Text strength;
        public Text wp;
        public SpriteRenderer spriteRenderer;
        public regularDices[] rd; //= new regularDices[4];
        public bool isPresent = false;

        public HeroFighter(string type)
        {
            this.name = GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/Name").GetComponent<Text>();
            this.strength = GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/Strength").GetComponent<Text>();
            this.wp = GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/WP").GetComponent<Text>();
            this.rd = new regularDices[] {
                GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/regular dice/rd1").GetComponent<regularDices>(),
                GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/regular dice/rd2").GetComponent<regularDices>(),
                GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/regular dice/rd3").GetComponent<regularDices>(),
                GameObject.Find("Multiplayer-Fight/Heroes/" + type + "/regular dice/rd4").GetComponent<regularDices>()
            };
            Hero h = GameManager.instance.heroes.Find(x => x.Type.Equals(type));
            this.spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/heroes/"+ h.getSex() + "_" + h.Type.ToLower());
        }

        public void ActivateDice(HeroFighter hero, int nb_dice)
        {
            for(int i=0; i<nb_dice; i++)
            {
                hero.rd[i].gameObject.SetActive(true);
            }
        }


    }

    private HeroFighter mage = new HeroFighter("Mage");
    private HeroFighter archer = new HeroFighter("Archer");
    private HeroFighter warrior = new HeroFighter("Warrior");
    private HeroFighter dwarf = new HeroFighter("Dwarf");

    public void activateMage()
    {
        mage.isPresent = !mage.isPresent;
    }

    public void activateArcher()
    {
        archer.isPresent = !archer.isPresent;
    }

    public void activateWarrior()
    {
        warrior.isPresent = !warrior.isPresent;
    }

    public void activateDwarf()
    {
        dwarf.isPresent = !dwarf.isPresent;
    }
}

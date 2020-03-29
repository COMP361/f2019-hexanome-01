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
    public Button mage_button;
    private HeroFighter archer = new HeroFighter("Archer");
    public Button archer_button;
    private HeroFighter warrior = new HeroFighter("Warrior");
    public Button warrior_button;
    private HeroFighter dwarf = new HeroFighter("Dwarf");
    public Button dwarf_button;

    public void activateMage()
    {
        mage.isPresent = !mage.isPresent;

        ColorBlock myColorBlock = new ColorBlock();
        if (mage.isPresent)
        {
            myColorBlock.normalColor = new Color(121, 7, 242, 1);
            mage_button.colors = myColorBlock;
        }
        else
        {
            myColorBlock.normalColor = Color.white;
            mage_button.colors = myColorBlock;
        }
    }

    public void activateArcher()
    {
        archer.isPresent = !archer.isPresent;

        ColorBlock myColorBlock = new ColorBlock();
        if (archer.isPresent)
        {
            myColorBlock.normalColor = Color.green;
            archer_button.colors = myColorBlock;
        }
        else
        {
            myColorBlock.normalColor = Color.white;
            archer_button.colors = myColorBlock;
        }
    }

    public void activateWarrior()
    {
        warrior.isPresent = !warrior.isPresent;

        ColorBlock myColorBlock = new ColorBlock();
        if (warrior.isPresent)
        {
            myColorBlock.normalColor = Color.blue;
            warrior_button.colors = myColorBlock;
        }
        else
        {
            myColorBlock.normalColor = Color.white;
            warrior_button.colors = myColorBlock;
        }
    }

    public void activateDwarf()
    {
        dwarf.isPresent = !dwarf.isPresent;

        ColorBlock myColorBlock = new ColorBlock();
        if (dwarf.isPresent)
        {
            myColorBlock.normalColor = Color.yellow;
            dwarf_button.colors = myColorBlock;
        }
        else
        {
            myColorBlock.normalColor = Color.white;
            dwarf_button.colors = myColorBlock;
        }
    }

    private void InTheCellHeroes()
    {
        Cell currentCell = GameManager.instance.CurrentPlayer.Cell;
        List<string> hero_types = new List<string>();
        List<Button> buttons = new List<Button> { warrior_button, mage_button, archer_button, dwarf_button };

        foreach (Hero h in currentCell.Inventory.Heroes)
        {
            hero_types.Add(h.Type.ToLower());
        }

        if (hero_types.Contains("archer"))
        {
            archer_button.enabled = true;
        }
        if (hero_types.Contains("warrior"))
        {
            warrior_button.enabled = true;
        }
        if (hero_types.Contains("mage"))
        {
            mage_button.enabled = true;
        }
        if (hero_types.Contains("dwarf"))
        {
            dwarf_button.enabled = true;
        }
    }
}

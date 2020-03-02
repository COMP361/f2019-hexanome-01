using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]

public class Cell : MonoBehaviour, IComparable<Cell>
{

    #region Fields
    /****** [Variables] ******/
    public static List<Cell> cells = new List<Cell>();
    public static GameObject[] Farmers = new GameObject[4];
    public List<Transform> neighbours = new List<Transform>();
    public Cell enemyPath;
    protected GameManager gm;
    protected SpriteRenderer sprite;

    private Color32 color = new Color(1f, 1f, 1f, 0f);
    private Color32 hoverColor = new Color(1f, 1f, 1f, .2f);

    /****** [Properties] ******/
    public Vector3 HeroesPosition { get; private set; }
    public Vector3 MovablesPosition { get; private set; }
    public Vector3 TokensPosition { get; private set; }
    public Vector3 Position { get; private set; }
    public int Index { get; private set; }
    public bool Active { get; set; }

    ///<summary>
    ///if true, we have to pay willPoints to reach the cell
    ///</summary>
    public bool Extension { get; set; }
    public float Heuristic { get; set; }
    public float Cost { get; set; }
    public Cell Parent { get; set; }

    public float f {
        get { return Cost + Heuristic; }
    }

    public CellState State { get; private set; }

    public int CompareTo(Cell cell) {
        return cell.Index.CompareTo(cell.Index);
    }

    #endregion

    #region Functions [Unity + Constructor]
    protected virtual void Awake() {
        cells.Add(this);
        Active = true;
        Position = transform.position;
        HeroesPosition = transform.Find("positions/heroes").position;
        MovablesPosition = transform.Find("positions/movables").position;
        TokensPosition = transform.Find("positions/tokens").position;
        State = new CellState();
    }

    protected virtual void Start() {
        gm = GameManager.instance;

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;

        Index = int.Parse(this.name);
    }

    protected virtual void OnMouseEnter() {
        if (!Active) return;

        var color = gm.CurrentPlayer.Color;
        color.a = .4f;
        sprite.color = color;
        EventManager.TriggerCellMouseEnter(Index);
        EventManager.TriggerInventoryUICellEnter(State.cellInventory, Index);
    }

    protected virtual void OnMouseExit() {
        sprite.color = color;
        EventManager.TriggerCellMouseLeave(Index);
        EventManager.TriggerInventoryUICellExit();
    }

    void OnMouseDown() {
        if (!Active) return;
        EventManager.TriggerCellClick(Index);
    }

    void OnDrawGizmos() {
        GameObject positions = transform.Find("positions").gameObject;
        Gizmos.color = Color.blue;

        foreach (Transform child in positions.transform) {
            Gizmos.DrawSphere(child.position, 0.6f);
        }
    }

    public void Reset() {
        Active = true;
        Extension = false;
        color = new Color(1f, 1f, 1f, 0f);
        sprite.color = color;
    }

    #endregion

    #region Functions [Cell]

    public void Deactivate() {
        Active = false;
        Extension = false;
        color = new Color(0f, 0f, 0f, 0.7f);
        sprite.color = color;
    }

    ///<summary>
    ///Sets the state of <see cref="Extension"/> to true:
    ///<para> If a cell is an extension of the day, we have to pay willPoints to reach it. </para>
    ///</summary>
    public void Extended() {
        Extension = true;
        Active = true;
        color = new Color(1f, 0f, 0f, 0.3f);
        sprite.color = color;
    }

    /// <summary>
    /// Returns the list of cells between <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    public List<Cell> WithinRange(int min, int max) {
        Queue<Tuple<int, Cell>> queue = new Queue<Tuple<int, Cell>>();
        HashSet<Cell> visited = new HashSet<Cell>();
        List<Cell> cells = new List<Cell>();

        if (min < 0) min = 0;

        queue.Enqueue(new Tuple<int, Cell>(0, this));
        Tuple<int, Cell> t;
        Cell c;
        int i;

        do {
            t = queue.Dequeue();
            i = t.Item1 + 1;

            for (int j = 0; j < t.Item2.neighbours.Count; j++) {
                c = t.Item2.neighbours[j].GetComponent<Cell>();
                if (visited.Add(c)) {
                    if (i >= min) cells.Add(c);
                    queue.Enqueue(new Tuple<int, Cell>(i, c));
                }
            }
        } while (queue.Count > 0 && t.Item1 < max);

        return cells;
    }


    public static Cell FromId(int id) {
        Cell cell = null;
        GameObject go = GameObject.Find("Cells/" + id);
        if (go != null) cell = go.GetComponent<Cell>();

        if (cell == null) {
            var message = string.Format("'{0}' is not a valid cell id.", id);
            throw new ApplicationException(message);
        }

        return cell;
    }

    protected bool setIcon(string spriteName) {

        Sprite spriteIcon=Resources.Load<Sprite>("Sprites/icons/merchant");


        Debug.Log(spriteIcon);
        if (spriteIcon != null) {
            GameObject spriteObject = new GameObject(spriteName);

            spriteObject.transform.parent = this.transform;
            SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
            renderer.sprite = spriteIcon;

            return true;
        }
        return false;
    }

    #endregion

}

public class CellState : ICloneable
{
    #region Fields
    public int numGoldenShields { get; private set; }
    public CellInventory cellInventory { get; private set; }
    /*
    public List<Hero> Heroes { get; private set; }
    public List<Enemy> Enemies { get; private set; }
    public List<Farmer> Farmers { get; private set; }
    public List<Token> Tokens { get; private set; }
    public List<Token> Golds { get; private set; }
    */
    #endregion

    // Pickable
    // Should we have well, fog?
    #region Functions [Constructor]
    public CellState()
    {
      cellInventory =  new CellInventory();
      /*
        Heroes = new List<Hero>();
        Enemies = new List<Enemy>();
        Farmers = new List<Farmer>();
        Tokens = new List<Token>();
        Golds = new List<Token>();
        int numGoldenShields; */
    }
    #endregion

    #region Functions[Constructor + Unity]
    public void initGoldenShields(int numOfPlayers) {
        if (numOfPlayers == 4) { numGoldenShields = 1; }
    }
    public int decrementGoldenShields() {
        if (numGoldenShields > 0) { numGoldenShields--; return 1; } else { return -1; }   // game over
    }

    public void addToken(Token token) {

      cellInventory.addToken(token);
      /*  Type listType;

        listType = Heroes.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            Heroes.Add((Hero)token);
            return;
        }

        listType = Enemies.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            Enemies.Add((Enemy)token);
            return;
        }

        listType = Farmers.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            Farmers.Add((Farmer)token);
            return;
        }
        */
    }

    public void removeToken(Token token) {

      cellInventory.removeToken(token);
      /*  Type listType;

        listType = Heroes.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            Heroes.Remove((Hero)token);
            return;
        }

        listType = Enemies.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            Enemies.Remove((Enemy)token);
            return;
        }

        listType = Farmers.GetListType();
        if (listType.IsCompatibleWith(token.GetType())) {
            Farmers.Remove((Farmer)token);
            return;
        }
        */
    }

    public object Clone() {
        CellState cs = (CellState)this.MemberwiseClone();
        return cs;
    }
    #endregion


}

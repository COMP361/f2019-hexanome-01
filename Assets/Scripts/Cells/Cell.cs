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
    //public static GameObject[] Farmers = new GameObject[4];
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

    public CellInventory Inventory { get; private set; }

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
        Index = int.Parse(this.name);
        Inventory = new CellInventory(Index);
    }

    protected virtual void Start() {
        gm = GameManager.instance;

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    protected virtual void OnMouseEnter() {
        if (!Active) return;

        var color = gm.MainHero.Color;
        color.a = .4f;
        sprite.color = color;
        EventManager.TriggerCellMouseEnter(Index);
        EventManager.TriggerInventoryUICellEnter(Inventory, Index);
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
            Gizmos.DrawSphere(child.position, 3f);
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
                    if (i >= min && i <= max) cells.Add(c);
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

    #endregion
}

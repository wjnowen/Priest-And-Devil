using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    SSDirector my;

    public State state = State.BSTART;
    public enum State { BSTART, BSEMOVING, BESMOVING, BEND, WIN, LOSE };

    GameObject[] boat = new GameObject[2];
    GameObject boat_obj;

    Stack<GameObject> priests_start = new Stack<GameObject>();
    Stack<GameObject> priests_end = new Stack<GameObject>();
    Stack<GameObject> devils_start = new Stack<GameObject>();
    Stack<GameObject> devils_end = new Stack<GameObject>();

    float gap = 1.2f;
    Vector3 priestStartPos = new Vector3(-14f, 0, 0);
    Vector3 priestEndPos = new Vector3(14f, 0, 0);
    Vector3 devilStartPos = new Vector3(-10f, 0, 0);
    Vector3 devilEndPos = new Vector3(10f, 0, 0);

    Vector3 boatStartPos = new Vector3(-3f, 0, 0);
    Vector3 boatEndPos = new Vector3(3f, 0, 0);

    public float speed = 20;

    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }

    public void LoadResources()
    {
        GameObject myGame = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/main"),
            Vector3.zero, Quaternion.identity);
        myGame.name = "main";

        boat_obj = Instantiate(Resources.Load("prefabs/Boat"), new Vector3(-3f, 0f, 0f), Quaternion.identity) as GameObject;
        for (int i = 0; i < 3; i++)
        {
            priests_start.Push(Instantiate(Resources.Load("prefabs/Priest")) as GameObject);
            devils_start.Push(Instantiate(Resources.Load("prefabs/devil")) as GameObject);
        }
    }

    int boatCapacity()
    {
        int capacity = 0;
        for (int i = 0; i < 2; i++)
        {
            if (boat[i] == null) capacity++;
        }
        return capacity;
    }

    void setCharacterPositions(Stack<GameObject> stack, Vector3 pos)
    {
        GameObject[] array = stack.ToArray();
        for (int i = 0; i < stack.Count; ++i)
        {
            array[i].transform.position = new Vector3(pos.x + gap * i, pos.y, pos.z);
        }
    }

    public void priestOnBoat()
    {
        if (priests_start.Count != 0 && boatCapacity() != 0 && this.state == State.BSTART)
            priestOnBoat_(priests_start.Pop());
        if (priests_end.Count != 0 && boatCapacity() != 0 && this.state == State.BEND)
            priestOnBoat_(priests_end.Pop());
    }

    public void priestOnBoat_(GameObject obj)
    {
        if (boatCapacity() != 0)
        {
            obj.transform.parent = boat_obj.transform;
            if (boat[0] == null)
            {
                boat[0] = obj;
                obj.transform.localPosition = new Vector3(-0.2f, 1.2f, 0f);
            }
            else
            {
                boat[1] = obj;
                obj.transform.localPosition = new Vector3(0.2f, 1.2f, 0f);
            }
        }
    }

    public void priestOffBoat()
    {
        for (int i = 0; i < 2; i++)
        {
            if (boat[i] != null)
            {
                if (this.state == State.BEND)
                {
                    if (boat[i].name == "Priest(Clone)")
                    {
                        priests_end.Push(boat[i]);
                        boat[i].transform.parent = null;
                        boat[i] = null;
                        break;
                    }
                }
                else if (this.state == State.BSTART)
                {
                    if (boat[i].name == "Priest(Clone)")
                    {
                        priests_start.Push(boat[i]);
                        boat[i].transform.parent = null;
                        boat[i] = null;
                        break;
                    }
                }
            }
        }
    }

    public void devilOnBoat()
    {
        if (devils_start.Count != 0 && boatCapacity() != 0 && this.state == State.BSTART)
            devilOnBoat_(devils_start.Pop());
        if (devils_end.Count != 0 && boatCapacity() != 0 && this.state == State.BEND)
            devilOnBoat_(devils_end.Pop());
    }

    public void devilOnBoat_(GameObject obj)
    {
        if (boatCapacity() != 0)
        {
            obj.transform.parent = boat_obj.transform;
            if (boat[0] == null)
            {
                boat[0] = obj;
                obj.transform.localPosition = new Vector3(-0.2f, 1.2f, 0f);
            }
            else
            {
                boat[1] = obj;
                obj.transform.localPosition = new Vector3(0.2f, 1.2f, 0f);
            }
        }
    }

    public void devilOffBoat()
    {
        for (int i = 0; i < 2; i++)
        {
            if (boat[i] != null)
            {
                if (this.state == State.BEND)
                {
                    if (boat[i].name == "Devil(Clone)")
                    {
                        devils_end.Push(boat[i]);
                        boat[i].transform.parent = null;
                        boat[i] = null;
                        break;
                    }
                }
                else if (this.state == State.BSTART)
                {
                    if (boat[i].name == "Devil(Clone)")
                    {
                        devils_start.Push(boat[i]);
                        boat[i].transform.parent = null;
                        boat[i] = null;
                        break;
                    }
                }
            }
        }
    }

    public void moveBoat()
    {
        if (boatCapacity() != 2)
        {
            if (this.state == State.BSTART)
            {
                this.state = State.BSEMOVING;
            }
            else if (this.state == State.BEND)
            {
                this.state = State.BESMOVING;
            }
        }
    }
    public void restart()
    {
        Application.LoadLevel(Application.loadedLevelName);
        state = State.BSTART;
    }

    void check()
    {
        int priests_s = 0, devils_s = 0, priests_e = 0, devils_e = 0;
        int pOnBoat = 0, dOnBoat = 0;

        if (priests_end.Count == 3 && devils_end.Count == 3)
        {
            this.state = State.WIN;
            return;
        }

        for (int i = 0; i < 2; ++i)
        {
            if (boat[i] != null && boat[i].name == "Priest(Clone)") pOnBoat++;
            else if (boat[i] != null && boat[i].name == "Devil(Clone)") dOnBoat++;
        }


        if (this.state == State.BSTART)
        {
            priests_s = priests_start.Count;
            devils_s = devils_start.Count ;
            priests_e = priests_end.Count;
            devils_e = devils_end.Count;
        }
        else if (this.state == State.BEND)
        {
            priests_s = priests_start.Count;
            devils_s = devils_start.Count;
            priests_e = priests_end.Count ;
            devils_e = devils_end.Count ;
        }
        if ((priests_s != 0 && priests_s < devils_s) || (priests_e != 0 && priests_e < devils_e))
        {
            this.state = State.LOSE;
        }
    }

    public bool isWin()
    {
        if (this.state == State.WIN) return true;
        return false;
    }

    public bool isLose()
    {
        if (this.state == State.LOSE) return true;
        return false;
    }
    void Start() { }

    void Update()
    {
        setCharacterPositions(priests_start, priestStartPos);
        setCharacterPositions(priests_end, priestEndPos);
        setCharacterPositions(devils_start, devilStartPos);
        setCharacterPositions(devils_end, devilEndPos);

        if (this.state == State.BSEMOVING)
        {
            boat_obj.transform.position = Vector3.MoveTowards(boat_obj.transform.position, boatEndPos, speed * Time.deltaTime);
            if (boat_obj.transform.position == boatEndPos)
            {
                this.state = State.BEND;
            }
        }
        else if (this.state == State.BESMOVING)
        {
            boat_obj.transform.position = Vector3.MoveTowards(boat_obj.transform.position, boatStartPos, speed * Time.deltaTime);
            if (boat_obj.transform.position == boatStartPos)
            {
                this.state = State.BSTART;
            }
        }
        else
        {
            check();
        }
    }
}
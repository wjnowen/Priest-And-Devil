using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{

    private IUserAction action;
    void Start()
    {
        action = SSDirector.getInstance().currentSceneController as IUserAction;
    }


    void OnGUI()
    {
        float width = Screen.width / 6;
        float height = Screen.height / 12;
        if (action.isWin())
        {
            GUI.Button(new Rect(0, Screen.height - 3f * height, Screen.width, height), "游戏胜利！");
            if (GUI.Button(new Rect(0, Screen.height - height, Screen.width, height), "重新开始"))
            {
                action.restart();
            }
        }
        else if (action.isLose())
        {
            GUI.Button(new Rect(0, Screen.height - 3f * height, Screen.width, height), "游戏失败！");
            if (GUI.Button(new Rect(0, Screen.height - height, Screen.width, height), "重新开始"))
            {
                action.restart();
            }
        }
        else
        {
            if (GUI.Button(new Rect(0, 0, width, height), "牧师上船"))
            {
                action.priestOnBoat();
            }

            if (GUI.Button(new Rect(0 + width, 0, width, height), "牧师下船"))
            {
                action.priestOffBoat();
            }

            if (GUI.Button(new Rect(0, 0 + height, width, height), "魔鬼上船"))
            {
                action.devilOnBoat();
            }

            if (GUI.Button(new Rect(0 + width, 0 + height, width, height), "魔鬼下船"))
            {
                action.devilOffBoat();
            }

            if (GUI.Button(new Rect(0 + 2 * width, 0, width, height), "开船"))
            {
                action.moveBoat();
            }
        }
    }
}
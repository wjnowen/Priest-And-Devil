using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    //void priestOnBoat(GameObject obj);
    void priestOnBoat();
    void priestOffBoat();
    void devilOnBoat();
    void devilOffBoat();
    void moveBoat();
    void restart();
    bool isWin();
    bool isLose();
}
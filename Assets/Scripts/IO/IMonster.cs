using UnityEngine;
using System.Collections;

public interface IMonster  {
    void Move();
    void Fly();
    void Die();
    void Normal();
    void InPool();
    void SetSprite();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour, IUnlockable
{
   public void Unlock()
    {
        gameObject.SetActive(true);
    }
}

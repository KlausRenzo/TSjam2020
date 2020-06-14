using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDisabler : MonoBehaviour
{
    public float timeToDisable;
    public string turretTag;
    public Dictionary<GameObject, Coroutine> dic = new Dictionary<GameObject, Coroutine>();

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == turretTag)
        {
            var cor = StartCoroutine(DisableTurretCor(other.gameObject));
            dic.Add(other.gameObject, cor);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == turretTag)
        {
            try
            {
                StopCoroutine(dic[other.gameObject]);
                dic.Remove(other.gameObject);
            }
            catch
            {
                //
            }
        }
    }

    IEnumerator DisableTurretCor(GameObject turret)
    {
        yield return new WaitForSeconds(timeToDisable);
        turret.GetComponent<TurretBehaviour>().EnableTurret(false);
        var thisCor = dic[turret];
        dic.Remove(turret.gameObject);
        StopCoroutine(thisCor);
    }
}

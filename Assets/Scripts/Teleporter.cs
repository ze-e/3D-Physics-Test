using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public Transform twin;
    bool coolDown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !coolDown && twin != null)
        {
            other.GetComponent<Player>().Teleport(twin);
            coolDown = true;
            StartCoroutine(CoolDownCoroutine(3));
        }
    }

    IEnumerator CoolDownCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        coolDown = false;
    }
}

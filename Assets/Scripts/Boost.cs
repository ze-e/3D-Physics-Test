using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Boost(boostForce);
        }
    }
}

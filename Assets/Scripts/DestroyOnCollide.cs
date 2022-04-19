using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<LevelManager>().data.score++;
            GameObject.FindObjectOfType<LevelManager>().data.collected[name] = true;
            gameObject.SetActive(false);
        }
    }
}

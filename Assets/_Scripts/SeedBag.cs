using System.Security.Cryptography;
using UnityEngine;

public class SeedBag : MonoBehaviour
{
    Seed currentSeed = null;
    [SerializeField] Transform seedPos;

    // Start is called before the first frame update
    void Start() { ReplenishSeed(); }

    private void Update() { if (NoSeedAvailable()) { ReplenishSeed(); } }

    private bool NoSeedAvailable() { return currentSeed == null
            || currentSeed.InBag == false; }

    private void ReplenishSeed()
    {
        GameObject newSeed = 
            Instantiate(GameManager.Get().GameData.seed,
            seedPos.position,
            seedPos.rotation);

        newSeed.transform.SetParent(this.transform);
        newSeed.GetComponent<SpriteRenderer>().enabled = false;

        currentSeed = newSeed.GetComponent<Seed>();
    }
}

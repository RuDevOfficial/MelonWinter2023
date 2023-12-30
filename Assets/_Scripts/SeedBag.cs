using UnityEngine;

public class SeedBag : MonoBehaviour
{
    Seed currentSeed = null;

    // Start is called before the first frame update
    void Start() { ReplenishSeed(); }

    private void Update() { if (NoSeedAvailable()) { ReplenishSeed(); } }

    private bool NoSeedAvailable() { return currentSeed == null
            || currentSeed.InBag == false; }

    public void TakeSeed() { currentSeed = null; }

    private void ReplenishSeed()
    {
        GameObject newSeed = 
            Instantiate(GameManager.Get().GameData.seed,
            transform.position,
            transform.rotation);

        newSeed.transform.SetParent(this.transform);

        currentSeed = newSeed.GetComponent<Seed>();
    }
}

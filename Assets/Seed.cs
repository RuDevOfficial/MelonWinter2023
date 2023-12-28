using UnityEngine;

public class Seed : PickableObject
{
    public override void Dropped()
    {
        base.Dropped();

        if (HasPotClose(out Pot closerPot))
        {
            closerPot.TryPlant(out bool success);

            if (success) { Destroy(this.gameObject); }
        }
    }

    private bool HasPotClose(out Pot closerPot)
    {
        closerPot = null;
        float minDistanceRecord = GameManager.Get().GameData.PotDetectionRadius;

        foreach (Pot pot in GameManager.Get().PotList)
        {
            Vector2 pickablePos = this.transform.position;
            Vector2 potPos = pot.transform.position;

            float distance = Vector2.Distance(pickablePos, potPos);
            if (distance <= minDistanceRecord)
            {
                minDistanceRecord = distance;
                closerPot = pot;
            }
        }

        if (closerPot != null) { return true; }

        return false;
    }
}

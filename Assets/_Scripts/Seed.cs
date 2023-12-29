using UnityEngine;

public class Seed : PickableObject
{
    public override void Dropped()
    {
        base.Dropped();

        if (PotManager.Get().GetClosestPot(this.transform.position, out Pot closerPot))
        {
            closerPot.TryPlant(out bool success);

            if (success) { Destroy(this.gameObject); }
        }
    }
}

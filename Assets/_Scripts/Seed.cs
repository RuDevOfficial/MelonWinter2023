using UnityEngine;

public class Seed : PickableObject
{
    public bool InBag => FromBag;
    [SerializeField] bool FromBag = true;

    public override void Picked()
    {
        base.Picked();

        FromBag = false;
    }

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

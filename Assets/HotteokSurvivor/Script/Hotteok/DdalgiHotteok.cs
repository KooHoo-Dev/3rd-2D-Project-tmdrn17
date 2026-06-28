using HotteokSurvivor;
using UnityEngine;

namespace HotteokSurvivor
{
    public class DdalgiHotteok : Hotteok
    {
        protected override void Move()
        {
            transform.Rotate(Vector3.forward * shotSpeed * Time.deltaTime);
        }

        protected override void SendDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}
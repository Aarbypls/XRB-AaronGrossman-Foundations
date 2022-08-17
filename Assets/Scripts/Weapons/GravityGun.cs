using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{
    public class GravityGun : Gun
    {
        [SerializeField] private Projection _projection;
        [SerializeField] private GravityBullet _gravityBulletPrefab;
        
        protected override void Fire(ActivateEventArgs arg0)
        {
            if (!CanFire())
            {
                return;
            }
            
            base.Fire(arg0);

            var bullet = Instantiate(_gravityBulletPrefab, _gunBarrel.position, Quaternion.identity); 
            bullet.Init(_gunBarrel.forward * _ammoClip.bulletSpeed, false);
            // Destroy(bullet.gameObject, 6f);
        }

        private void Update()
        {
            if (_ammoClip == null)
            {
                return;
            }
            
            _projection.SimulateTrajectory(_gravityBulletPrefab, _gunBarrel.position, _gunBarrel.forward * _ammoClip.bulletSpeed);
        }
    }
}

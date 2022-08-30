using UnityEngine;

namespace GalaxyShooter.Core
{
    public class Explosion : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 3.0f);
        }
    }
}
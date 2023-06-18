using UnityEngine;

namespace Tools
{
    public class PortalPair
    {
        private readonly GameObject _portal1;
        private readonly GameObject _portal2;

        public PortalPair(GameObject p1, GameObject p2)
        {
            _portal1 = p1;
            _portal2 = p2;
        }
        /// <summary>
        /// return the target portal position
        /// </summary>
        /// <param name="portal">the portal you entered</param>
        public Vector3 GetTarget(GameObject portal)
        {
            return portal == _portal1 ? _portal2.transform.position : _portal1.transform.position;
        }
    }
}


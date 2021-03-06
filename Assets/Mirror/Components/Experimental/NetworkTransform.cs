using UnityEngine;

namespace Mirror.Experimental
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/Experimental/NetworkTransformExperimental")]
    [HelpURL("https://mirror-networking.com/docs/Articles/Components/NetworkTransform.html")]
    public class NetworkTransform : NetworkTransformBase
    {
        protected override Transform targetTransform => transform;
    }
}

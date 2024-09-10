using UnityEngine;

namespace Coffee.UIExtensions
{
    /// <summary>
    /// Unmask Raycast Filter.
    /// The ray passes through the unmasked rectangles.
    /// </summary>
    [AddComponentMenu("UI/Unmask/UnmaskRaycastFilter", 2)]
    public class UnmaskRaycastFilter : MonoBehaviour, ICanvasRaycastFilter
    {
        //################################
        // Serialize Members.
        //################################
        [Tooltip("Target unmask components. The ray passes through the unmasked rectangles.")]
        [SerializeField] private Unmask[] m_TargetUnmasks;

        //################################
        // Public Members.
        //################################
        /// <summary>
        /// Target unmask components. Ray through the unmasked rectangles.
        /// </summary>
        public Unmask[] targetUnmasks { get { return m_TargetUnmasks; } set { m_TargetUnmasks = value; } }

        /// <summary>
        /// Given a point and a camera is the raycast valid.
        /// </summary>
        /// <returns>Valid.</returns>
        /// <param name="sp">Screen position.</param>
        /// <param name="eventCamera">Raycast camera.</param>
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            // Skip if deactivated or no unmasks.
            if (!isActiveAndEnabled || m_TargetUnmasks == null || m_TargetUnmasks.Length == 0)
            {
                return true;
            }

            foreach (var unmask in m_TargetUnmasks)
            {
                // Skip if unmask is null or inactive
                if (unmask == null || !unmask.isActiveAndEnabled)
                {
                    continue;
                }

                // Check if point is inside unmask rectangle
                bool isInside = eventCamera
                    ? RectTransformUtility.RectangleContainsScreenPoint((unmask.transform as RectTransform), sp, eventCamera)
                    : RectTransformUtility.RectangleContainsScreenPoint((unmask.transform as RectTransform), sp);

                if (isInside)
                {
                    return false; // Ray is inside an unmasked area, so it's valid (not blocked)
                }
            }

            return true; // Ray is not inside any unmasked area, so it's blocked
        }

        //################################
        // Private Members.
        //################################

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
        {
            // Initialization code if needed
        }
    }
}
using UnityEngine;


namespace TowerDefense
{
    public class BackgroundItem : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer icon;
    
        [SerializeField] public AnimationCurve alphaCurve;
        [SerializeField] public AnimationCurve scaleCurve;
        [SerializeField] public AnimationCurve positionCurve;
        [SerializeField] public AnimationCurve rotationCurve;
    
    }
}

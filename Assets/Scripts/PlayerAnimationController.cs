using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public List<AnimationState> states = new List<AnimationState>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[CreateAssetMenu(fileName ="Animation State", menuName ="Custom/Animations/Animation State", order =0)]
public class AnimationState: ScriptableObject
{
    public string animationName = "User friendly description";
    public string animatorStateName = "idle";
    public string animatorParam = "optional param";
    public float duration;

    public bool trigger;
//#if !trigger
    public bool boolean;
    //#endif
    public bool play;

    public UnityEngine.Events.UnityEvent onEnter;
    public UnityEngine.Events.UnityEvent onUpdate;
    public UnityEngine.Events.UnityEvent onFinish;

}

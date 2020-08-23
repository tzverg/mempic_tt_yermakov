using System.Collections;
using UnityEngine;

public class MeshAnim : MonoBehaviour
{
    [SerializeField] private ConfigSO config;

    private AnimationState state;

    private Vector3 currLS;
    private Vector3 incrLS;
    private Vector3 decrLS;
    private Vector3 decrLSM;

    public Vector3 waveVelocity;

    public bool currentMesh;
    public bool prepareToAnim;
    public bool perfectMove;
    public bool anim;

    void Start()
    {
        prepareToAnim = false;
        anim = false;

        waveVelocity = Vector3.zero;
        state = AnimationState.DISABLED;
    }

    private void PrepareToAnimation()
    {
        currLS = transform.localScale;
        incrLS = CalculateIncreaseLSValue();
        decrLS = CalculateDecreaseLSValue();

        //Debug.Log("currLS: " + currLS);
        //Debug.Log("incrLS: " + incrLS);
        //Debug.Log("decrLS: " + decrLS);

        state = AnimationState.ANIMATION_INC;
        prepareToAnim = false;
        anim = true;
    }

    private Vector3 ClampXZ(Vector3 target)
    {
        Vector3 result = new Vector3();
        result.x = Mathf.Clamp(target.x, config.MeshMinScale.x, config.MeshMaxScale.x);
        result.y = target.y;
        result.z = Mathf.Clamp(target.z, config.MeshMinScale.z, config.MeshMaxScale.z);
        return result;
    }

    private Vector3 CalculateIncreaseLSValue()
    {
        Vector3 result = currentMesh ? (currLS + config.WaveFirstStepInc) : (currLS + config.WaveNextStepInc);
        return ClampXZ(result);
    }

    private Vector3 CalculateDecreaseLSValue()
    {
        Vector3 result = currentMesh ? (incrLS - config.WaveFirstStepDec) : (MoultiplyV(incrLS, config.WaveNextStepDecM));
        return ClampXZ(result);
    }

    private Vector3 MoultiplyV(Vector3 targetA, Vector3 targetB)
    {
        return new Vector3((targetA.x * targetB.x), targetA.y, (targetA.z * targetB.z));
    }

    private void AnimateMesh(Vector3 nextLocalScale)
    {
        if (anim)
        {
            //if (!transform.localScale.Equals(nextLocalScale))
            if (transform.localScale != nextLocalScale)
            {
                transform.localScale = Vector3.SmoothDamp(transform.localScale, nextLocalScale, ref waveVelocity, config.WaveSmoothScaleTime);
            }
            else
            {
                transform.localScale = nextLocalScale; //crunch
                switch (state)
                {
                    case AnimationState.ANIMATION_INC:
                        if (perfectMove)
                        {
                            state = AnimationState.ANIM_RESTORE;
                        }
                        else
                        {
                            state = AnimationState.ANIMATION_DEC;
                        }
                        break;
                    case AnimationState.ANIMATION_DEC:
                        state = AnimationState.DISABLED;
                        break;
                    default:
                        state = AnimationState.DISABLED;
                        break;
                }
            }
        }
    }

    void Update()
    {
        if (isActiveAndEnabled)
        {
            if (prepareToAnim)
            {
                state = AnimationState.PREPARE;
            }
            switch (state)
            {
                case AnimationState.PREPARE:
                    PrepareToAnimation();
                    break;
                case AnimationState.ANIMATION_INC:
                    AnimateMesh(incrLS);
                    break;
                case AnimationState.ANIMATION_DEC:
                    AnimateMesh(decrLS);
                    break;
                case AnimationState.ANIM_RESTORE:
                    AnimateMesh(currLS);
                    break;
                case AnimationState.DISABLED:
                    anim = false;
                    break;
                default:
                    break;
            }
        }
    }
}

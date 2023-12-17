using UnityEngine;

/// <summary>接地判定用クラス</summary>
public class GroundJudge : MonoBehaviour
{
    [SerializeField]
    [Tooltip("接地判定用Rayの長さ")]
    float _rayLength = 1.009f;

    [SerializeField]
    [Tooltip("接地判定確認用")]
    bool isGround;

    [SerializeField]
    [Header("接地しているとみなすもの")]
    LayerMask groundLayer;
        
    public bool IsGround => isGround;
    
    // Update is called once per frame
    void Update()
    {
        isGround = IsGroundJudg();
    }

    bool IsGroundJudg()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 1.009f, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * 1.009f, Color.black, 2);
        if (raycastHit.collider == null)
        {
            return false;
        }

        return true;
    }
}
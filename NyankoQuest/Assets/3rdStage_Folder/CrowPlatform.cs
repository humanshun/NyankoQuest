using UnityEngine;

public class CustomTriggerAreaController : MonoBehaviour
{
    public GameObject targetObject; // コライダーを非表示にする対象のゲームオブジェクト
    public Vector3 areaCenter = Vector3.zero; // トリガーエリアの中心
    public Vector3 areaSize = new Vector3(5.0f, 5.0f, 5.0f); // トリガーエリアのサイズ

    private Collider[] targetColliders;

    void Start()
    {
        // targetObject からすべてのコライダーを取得
        targetColliders = targetObject.GetComponents<Collider>();
    }

    void Update()
    {
        Vector3 worldCenter = transform.position + areaCenter;
        Bounds triggerBounds = new Bounds(worldCenter, areaSize);

        // エリア内に "エネミー" タグのオブジェクトが存在するかどうかをチェック
        bool isEnemyInside = false;
        Collider[] hitColliders = Physics.OverlapBox(worldCenter, areaSize / 2);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                isEnemyInside = true;
                break;
            }
        }

        // targetObject のコライダーの表示/非表示を切り替え
        if (targetColliders != null)
        {
            foreach (var collider in targetColliders)
            {
                collider.enabled = isEnemyInside;
            }
        }
    }

    // ギズモでトリガーエリアを可視化
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + areaCenter, areaSize);
    }
}

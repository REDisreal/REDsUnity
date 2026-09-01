using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Vector2 position;
    [SerializeField] float moveSpeed;
    [SerializeField] int ngang;
    [SerializeField] int doc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = new Vector2(0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i=0;i<=ngang;i++){
            for (int j=0;j<=doc;j++){
                Gizmos.DrawWireCube(Vector3.zero,new Vector3(i,j));
            }
        }
    }
}

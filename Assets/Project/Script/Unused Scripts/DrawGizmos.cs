using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    [Header("Grid Config")]
    [SerializeField] float cach = 1f;    // Độ lớn 1 ô (cell size)
    [SerializeField] float space = 0.1f;  // Khoảng cách giữa các ô
    [SerializeField] float rong = 5f;    // Tổng chiều rộng túi
    [SerializeField] float dai = 4f;     // Tổng chiều cao túi

    private void OnDrawGizmos()
    {
        Vector2 totalSize = new Vector2(rong, dai);
        Vector2 cellSize = new Vector2(cach, cach);
        Vector2 spacing = new Vector2(space, space);

        Gizmos.color = Color.red;

        // Tính số cột và số hàng
        int cols = Mathf.FloorToInt((totalSize.x + spacing.x) / (cellSize.x + spacing.x));
        int rows = Mathf.FloorToInt((totalSize.y + spacing.y) / (cellSize.y + spacing.y));

        // Tính tổng kích thước thực tế mà lưới các ô sẽ chiếm
        float actualWidth = cols * cellSize.x + (cols - 1) * spacing.x;
        float actualHeight = rows * cellSize.y + (rows - 1) * spacing.y;

        // Căn gốc origin về góc dưới-trái sao cho transform.position nằm đúng Trung Tâm
        Vector3 centerPosition = transform.position;
        Vector3 origin = new Vector3(
            centerPosition.x - (actualWidth / 2f),
            centerPosition.y - (actualHeight / 2f),
            centerPosition.z
        );

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Vị trí tâm của từng ô dựa trên origin đã dịch chuyển
                float posX = origin.x + x * (cellSize.x + spacing.x) + (cellSize.x / 2f);
                float posY = origin.y + y * (cellSize.y + spacing.y) + (cellSize.y / 2f);

                Vector3 cellCenter = new Vector3(posX, posY, centerPosition.z);
                Vector3 size = new Vector3(cellSize.x, cellSize.y, 0.01f);

                // Vẽ từng ô
                Gizmos.DrawWireCube(cellCenter, size);
            }
        }
    }
}
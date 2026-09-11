using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject hoithoai;
    [SerializeField] private float displayDuration = 5f; // Thời gian hiển thị (5 giây)

    private Coroutine hideDialogueCoroutine;

    private void Start()
    {
        // Ẩn hội thoại khi bắt đầu game
        if (hoithoai != null)
        {
            hoithoai.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (hoithoai != null)
            {
                hoithoai.SetActive(true);

                // Nếu đang có đếm ngược từ lần trước thì hủy đi để đếm lại từ đầu
                if (hideDialogueCoroutine != null)
                {
                    StopCoroutine(hideDialogueCoroutine);
                }

                // Bắt đầu đếm ngược 8 giây để ẩn hội thoại
                hideDialogueCoroutine = StartCoroutine(HideDialogueAfterDelay(displayDuration));
            }
            Debug.Log("Đây là anh Phúc");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Người chơi rời vùng tương tác thì tắt ngay và dừng đếm ngược
            if (hideDialogueCoroutine != null)
            {
                StopCoroutine(hideDialogueCoroutine);
            }

            if (hoithoai != null)
            {
                hoithoai.SetActive(false);
            }
        }
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Chờ 8 giây

        if (hoithoai != null)
        {
            hoithoai.SetActive(false);
        }
    }
}
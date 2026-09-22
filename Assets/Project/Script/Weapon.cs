using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float displayDuration = 1.5f; 
    private Coroutine hideCoroutine;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void TriggerWeapon()
    {
        gameObject.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        gameObject.SetActive(false);
    }
}
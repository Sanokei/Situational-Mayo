using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplanationPanelBehaviour : MonoBehaviour
{
    public TextMeshProUGUI _text;
    [SerializeField] Renderer _renderer;
    void OnEnable()
    {
        // wait 3 seconds before hiding the panel
        StartCoroutine(HidePanel());
    }
    IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}

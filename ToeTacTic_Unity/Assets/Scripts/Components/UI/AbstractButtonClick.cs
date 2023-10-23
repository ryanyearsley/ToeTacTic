using UnityEngine;
using UnityEngine.UI;
public abstract class AbstractButtonClick : MonoBehaviour
{
    protected Button button;
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public virtual void OnClick()
    {
    }
}
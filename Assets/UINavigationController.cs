using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UINavigationController : MonoBehaviour
{
    [Header("Navigation Settings")]
    public Selectable[] selectableElements;
    public float inputDelay = 0.3f;
    public float activeScale = 1.25f;
    public float normalScale = 1.0f;
    public float animationDuration = 0.15f;

    public string submitButton = "Submit";
    public float inputDeadzone = 0.5f;

    private Selectable _currentSelected;
    private float _lastInputTime;
    private bool _isNavigating;
    private bool _inputReleased = true;

    void Start()
    {
        if (selectableElements == null || selectableElements.Length == 0)
        {
            enabled = false;
            return;
        }
        SetSelected(selectableElements[0], true);
    }

    void Update()
    {
        HandleNavigationInput();
        HandleSubmitInput();
        CheckActualSelection();
        HandleBackInput();
    }
    public string backButton = "Cancel"; 
    public UnityEngine.Events.UnityEvent onBackPressed; 
    private void HandleBackInput()
    {
        if (Input.GetButtonDown(backButton))
        {
            onBackPressed.Invoke();
        }
    }

    private void HandleNavigationInput()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        if (input.magnitude < inputDeadzone)
        {
            _inputReleased = true;
            _isNavigating = false;
            return;
        }
        if (!_isNavigating && !_inputReleased) return;
        if (Time.time - _lastInputTime < inputDelay && _isNavigating) return;
        int direction = GetPrimaryDirection(input);
        Navigate(direction);

        _lastInputTime = Time.time;
        _isNavigating = true;
        _inputReleased = false;
    }

    private void HandleSubmitInput()
    {
        if (Input.GetButtonDown(submitButton))
        {
            SubmitCurrent();
        }
    }

    private void CheckActualSelection()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(_currentSelected.gameObject);
            return;
        }

        if (EventSystem.current.currentSelectedGameObject != _currentSelected.gameObject)
        {
            Selectable newSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
            if (newSelected != null && System.Array.IndexOf(selectableElements, newSelected) >= 0)
            {
                SetSelected(newSelected, false);
            }
        }
    }

    private int GetPrimaryDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return input.x > 0 ? 1 : -1;
        else
            return input.y > 0 ? -1 : 1;
    }

    private void Navigate(int direction)
    {
        int currentIdx = System.Array.IndexOf(selectableElements, _currentSelected);
        if (currentIdx < 0) currentIdx = 0;
        int newIdx = (currentIdx + direction + selectableElements.Length) % selectableElements.Length;
        SetSelected(selectableElements[newIdx], true);
    }

    private void SetSelected(Selectable newSelected, bool updateEventSystem)
    {
        if (_currentSelected == newSelected) return;
        if (_currentSelected != null)
        {
            StartCoroutine(ScaleElement(_currentSelected.transform, normalScale));
        }
        _currentSelected = newSelected;
        StartCoroutine(ScaleElement(_currentSelected.transform, activeScale));

        if (updateEventSystem) EventSystem.current.SetSelectedGameObject(_currentSelected.gameObject);
    }

    public void SubmitCurrent()
    {
        if (_currentSelected == null) return;

        var button = _currentSelected.GetComponent<Button>();
        if (button != null && button.interactable)
        {
            button.onClick.Invoke();
        }
    }

    private IEnumerator ScaleElement(Transform element, float targetScale)
    {
        float startScale = element.localScale.x;
        float time = 0;

        while (time < animationDuration)
        {
            float scale = Mathf.Lerp(startScale, targetScale, time / animationDuration);
            element.localScale = Vector3.one * scale;
            time += Time.deltaTime;
            yield return null;
        }

        element.localScale = Vector3.one * targetScale;
    }

    private void OnEnable()
    {
        if (selectableElements != null && selectableElements.Length > 0)
        {
            SetSelected(selectableElements[0], true);
        }
    }
}
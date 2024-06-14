using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelScrollRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public ScrollRect scroll;
    private Gamepad gamepad;
    private float[] pagePosition = new float[2] { 0, 0.5f};
    private float targetPosition = 0;
    public int label => isLeft?0:1;
    public bool open=>Panel.activeSelf;

    public bool doubleInstrument;

    public Image toRight;//右箭头

    private bool isLeft=true;
    
    public GameObject Panel;
    void Start()
    {
        scroll = GetComponent<ScrollRect>();
        gamepad = Gamepad.current;

        if (doubleInstrument) toRight.enabled = true;
    }

    void Update()   
    {
        HandleInput();
        
        if(Input.GetKeyDown(KeyCode.A))Debug.Log(label);
        //open = Panel.activeSelf;
    }

    private void HandleInput()
    {
        if (gamepad != null&&open==false&& doubleInstrument) // Check if Gamepad instance is not null
        {
            // Get the input from the gamepad
            float horizontalInput = gamepad.dpad.x.ReadValue();

            if (Mathf.Abs(horizontalInput) > 0.5f)
            {
                // Check if the input is towards the left
                if (horizontalInput < 0)
                {
                    MoveToPreviousPage();
                }
                // Check if the input is towards the right
                else if (horizontalInput > 0)
                {
                    MoveToNextPage();
                }
            }
        }
    }


    private void MoveToPreviousPage()
    {
        int currentIndex = GetCurrentPageIndex();
        int previousIndex = Mathf.Clamp(currentIndex - 1, 0, pagePosition.Length - 1);
        isLeft = true;
        targetPosition = pagePosition[previousIndex];
        StartCoroutine(SmoothScroll());
    }

    private void MoveToNextPage()
    {
        int currentIndex = GetCurrentPageIndex();
        int nextIndex = Mathf.Clamp(currentIndex + 1, 0, pagePosition.Length - 1);
        isLeft = false;
        targetPosition = pagePosition[nextIndex];
        StartCoroutine(SmoothScroll());
    }

    private int GetCurrentPageIndex()
    {

        return label;
        
        float currentNormalizedPosition = scroll.horizontalNormalizedPosition;
        for (int i = 0; i < pagePosition.Length; i++)
        {
            if (Mathf.Approximately(currentNormalizedPosition, pagePosition[i]))
            {
                return i;
            }
        }
        return 0;
    }


    private IEnumerator SmoothScroll()
    {
        float duration = 0.3f; // Adjust the duration as needed
        float elapsedTime = 0;

        float initialPosition = scroll.horizontalNormalizedPosition;

        while (elapsedTime < duration)
        {
            scroll.horizontalNormalizedPosition = Mathf.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scroll.horizontalNormalizedPosition = targetPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // You can add custom behavior if needed
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // You can add custom behavior if needed
    }
}

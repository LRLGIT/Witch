using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FuWenManager : Singleton<FuWenManager>
{

    public KeyCode key;
    public Image up, down;

    public GameObject Panel;
    public GameObject icon;
    public InputActions input;
    private Gamepad gamepad;

    public Image plante;
    public Image element;
    public Image skill;

    [System.Serializable]
    public struct FuWenData
    {
        public GameObject FuWenIcon;
        public Sprite FuWenSprite;
        public Sprite FuWenSpriteCommon;
        public Sprite FuWenSpriteHighlight;

        private Image fuwenImage;
        public Image FuWenImage
        {
            get
            {
                if (fuwenImage != null) return fuwenImage;
                fuwenImage = FuWenIcon.GetComponent<Image>();
                return fuwenImage;
            }
        }
    }


    public FuWenData sun;
    public FuWenData moon;
    public FuWenData mercury;
    public FuWenData venus;
    public FuWenData mars;
    public FuWenData jupiter;
    public FuWenData saturn;


    public FuWenData fire;
    public FuWenData water;
    public FuWenData wind;
    public FuWenData soid;


    public FuWenData leftUpSkill;
    public FuWenData leftDownSkill;
    public FuWenData rightUpSkill;
    public FuWenData rightDownSkill;

    public EnableType enableState;

    
    // public bool planteEnable = true;
    // public bool elementEnable = false;
    // public bool skillEnable = false;


    public Image PlanteDisk;
    public Image ElementDisk;
    public Image SkillRunes;
    public Material replacementMaterial;
    private Material originalMaterial; 

    bool complete = false;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        input = new InputActions();
        
        //if(!StaticData.haveFuWen)transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        input.Enable();

        input.gameplay.plante.performed += plante_performed;
    }

    public void ReplaceSpriteByItemData(ItemData_So itemData)
    {

        string itemName = itemData.description;

        Debug.Log(itemName);

        FieldInfo field = typeof(FuWenManager).GetField(itemName, BindingFlags.Public | BindingFlags.Instance);
        Debug.Log(field);
        if (field != null && field.FieldType == typeof(FuWenData))
        {
  
            Debug.Log(field);
            
            FuWenData fuWenData = (FuWenData)field.GetValue(this);

            if (fuWenData.FuWenIcon != null)
            {
                fuWenData.FuWenIcon.SetActive(true);
            }


            fuWenData.FuWenSprite = itemData.itemSprite;


            field.SetValue(this, fuWenData);
        }
    }

    private void ResetFuWenSprite(FuWenData fuWen)
    {
        if (fuWen.FuWenImage != null && fuWen.FuWenSpriteCommon != null)
        {
            fuWen.FuWenImage.sprite = fuWen.FuWenSpriteCommon;
            fuWen.FuWenImage.SetNativeSize();
        }
    }
    
    private void HighLightFuWenSprite(FuWenData fuWen)
    {
        ResetAllSprite();
        if (fuWen.FuWenImage != null && fuWen.FuWenSpriteHighlight != null)
        {
            fuWen.FuWenImage.sprite = fuWen.FuWenSpriteHighlight;
            fuWen.FuWenImage.SetNativeSize();
        }
    }

    private void ResetAllSprite()
    {
        ResetFuWenSprite(sun);
        ResetFuWenSprite(moon);
        ResetFuWenSprite(mercury);
        ResetFuWenSprite(venus);
        ResetFuWenSprite(mars);
        ResetFuWenSprite(jupiter);
        ResetFuWenSprite(saturn);
        ResetFuWenSprite(fire);
        ResetFuWenSprite(water);
        ResetFuWenSprite(wind);
        ResetFuWenSprite(soid);
        ResetFuWenSprite(leftUpSkill);
        ResetFuWenSprite(leftDownSkill);
        ResetFuWenSprite(rightUpSkill);
        ResetFuWenSprite(rightDownSkill);
    }
    
    private void plante_performed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = input.FindAction("plante").ReadValue<Vector2>();

        
        if (moveInput.x < 0.3 && moveInput.x > -0.3 && moveInput.y > 0.3)
        {
            switch (enableState)
            {
                case EnableType.Planet:
                {
                    up.transform.rotation = Quaternion.Euler(0, 0, 0);

                    Color color = plante.color;
                    color.a = 1f;
                    plante.color = color;
                    plante.sprite = sun.FuWenSprite;
                    HighLightFuWenSprite(sun);
                }
                    break;
                case EnableType.Element:
                {                
                    down.transform.rotation = Quaternion.Euler(0, 0, 90);

                    Color color = element.color;
                    color.a = 1f;
                    element.color = color;
                    element.sprite = fire.FuWenSprite;
                    HighLightFuWenSprite(fire);
                }
                    break;
                case EnableType.Skill:
                    break;
                case EnableType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            //
            // if (planteEnable == true)
            // {
            //     up.transform.rotation = Quaternion.Euler(0, 0, 0);
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     plante.color = color;
            //     plante.sprite = sun.FuWenSprite;
            // }
            // else if (elementEnable == true)
            // {
            //     down.transform.rotation = Quaternion.Euler(0, 0, 90);
            //
            //     Color color = element.color;
            //     color.a = 1f;
            //     element.color = color;
            //     element.sprite = fire.FuWenSprite;
            // }
        }


        else if (moveInput.x > 0.3 && moveInput.y < 0.3 && moveInput.y > -0.3)
        {
            
            switch (enableState)
            {
                case EnableType.Planet:
                {
                    up.transform.rotation = Quaternion.Euler(0, 0, -102);

                    Color color = plante.color;
                    color.a = 1f;
                    plante.color = color;
                    plante.sprite = mercury.FuWenSprite;
                    HighLightFuWenSprite(moon);
                }
                    break;
                case EnableType.Element:
                {                
                    down.transform.rotation = Quaternion.Euler(0, 0, 0);

                    Color color = element.color;
                    color.a = 1f;
                    element.color = color;
                    element.sprite = soid.FuWenSprite;
                    HighLightFuWenSprite(soid);
                }
                    break;
                case EnableType.Skill:
                    break;
                case EnableType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            // if (planteEnable == true)
            // {
            //     up.transform.rotation = Quaternion.Euler(0, 0, -102);
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     plante.color = color;
            //     plante.sprite = mercury.FuWenSprite;
            // }
            // else if (elementEnable == true)
            // {
            //     down.transform.rotation = Quaternion.Euler(0, 0, 0);
            //
            //     Color color = element.color;
            //     color.a = 1f;
            //     element.color = color;
            //     element.sprite = soid.FuWenSprite;
            // }
        }


        else if (moveInput.x < -0.3 && moveInput.y < 0.3 && moveInput.y > -0.3)
        {
            
            switch (enableState)
            {
                case EnableType.Planet:
                {
                    up.transform.rotation = Quaternion.Euler(0, 0, 100);

                    Color color = plante.color;
                    color.a = 1f;
                    plante.color = color;
                    plante.sprite = jupiter.FuWenSprite;
                    HighLightFuWenSprite(jupiter);
                }
                    break;
                case EnableType.Element:
                {                
                    down.transform.rotation = Quaternion.Euler(0, 0, -180);

                    Color color = element.color;
                    color.a = 1f;
                    element.color = color;
                    element.sprite = wind.FuWenSprite;
                    HighLightFuWenSprite(wind);
                }
                    break;
                case EnableType.Skill:
                    break;
                case EnableType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
            // if (planteEnable == true)
            // {
            //     up.transform.rotation = Quaternion.Euler(0, 0, 100);
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     plante.color = color;
            //     plante.sprite = jupiter.FuWenSprite;
            // }
            // else if (elementEnable == true)
            // {
            //     down.transform.rotation = Quaternion.Euler(0, 0, -180);
            //
            //     Color color = element.color;
            //     color.a = 1f;
            //     element.color = color;
            //     element.sprite = wind.FuWenSprite;
            // }
        }


        else if (moveInput.x < 0.3 && moveInput.x > -0.3 && moveInput.y < -0.3)
        {
            switch (enableState)
            {
                case EnableType.Planet:
                {
                    // up.transform.rotation = Quaternion.Euler(0, 0, 100);
                    //
                    // Color color = plante.color;
                    // color.a = 1f;
                    // plante.color = color;
                    // plante.sprite = jupiter.FuWenSprite;
                }
                    break;
                case EnableType.Element:
                {                
                    down.transform.rotation = Quaternion.Euler(0, 0, -90);

                    Color color = element.color;
                    color.a = 1f;
                    element.color = color;
                    element.sprite = water.FuWenSprite;
                    HighLightFuWenSprite(water);
                }
                    break;
                case EnableType.Skill:
                    break;
                case EnableType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            //
            // if (elementEnable == true)
            // {
            //     down.transform.rotation = Quaternion.Euler(0, 0, -90);
            //
            //     Color color = element.color;
            //     color.a = 1f;
            //     element.color = color;
            //     element.sprite = water.FuWenSprite;
            // }
        }


        else if (moveInput.x < -0.3 && moveInput.y > 0.3)
        {
            switch (enableState)
            {
                case EnableType.Planet:
                {
                    up.transform.rotation = Quaternion.Euler(0, 0, 51);

                    Color color = plante.color;
                    color.a = 1f;
                    plante.color = color;
                    plante.sprite = saturn.FuWenSprite;
                    HighLightFuWenSprite(saturn);
                }
                    break;
                case EnableType.Element:
                {                

                    
                }
                    break;
                case EnableType.Skill:
                {
                    Color color = plante.color;
                    color.a = 1f;
                    skill.color = color;
                    skill.sprite = leftUpSkill.FuWenSprite;
                    HighLightFuWenSprite(leftUpSkill);
                }
                    break;
                case EnableType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // if (planteEnable == true)
            // {
            //     up.transform.rotation = Quaternion.Euler(0, 0, 51);
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     plante.color = color;
            //     plante.sprite = saturn.FuWenSprite;
            // }
            // else if (skillEnable == true)
            // {
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     skill.color = color;
            //     skill.sprite = leftUpSkill.FuWenSprite;
            // }
        }

        else if (moveInput.x < -0.3 && moveInput.y < -0.3)
        {
            switch (enableState)
            {
                case EnableType.Planet:
                {
                    up.transform.rotation = Quaternion.Euler(0, 0, 151);

                    Color color = plante.color;
                    color.a = 1f;
                    plante.color = color;
                    plante.sprite = mars.FuWenSprite;
                    HighLightFuWenSprite(mars);
                }
                    break;
                case EnableType.Element:
                {                
                    // Color color = plante.color;
                    // color.a = 1f;
                    // skill.color = color;
                    // skill.sprite = leftUpSkill.FuWenSprite;
                    
                }
                    break;
                case EnableType.Skill:
                {
                    Color color = plante.color;
                    color.a = 1f;
                    skill.color = color;
                    skill.sprite = leftDownSkill.FuWenSprite;
                    
                    HighLightFuWenSprite(leftDownSkill);
                }
                    break;
                case EnableType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // if (planteEnable == true)
            // {
            //     up.transform.rotation = Quaternion.Euler(0, 0, 151);
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     plante.color = color;
            //     plante.sprite = mars.FuWenSprite;
            // }
            // else if (skillEnable == true)
            // {
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     skill.color = color;
            //     skill.sprite = leftDownSkill.FuWenSprite;
            // }
        }

 
        else if (moveInput.x > 0.3 && moveInput.y < -0.3)
        {
            switch (enableState)
            {
                case EnableType.Planet:
                {
                    up.transform.rotation = Quaternion.Euler(0, 0, -155);

                    Color color = plante.color;
                    color.a = 1f;
                    plante.color = color;
                    HighLightFuWenSprite(venus);
                }
                    break;
                case EnableType.Element:
                {                
                    // Color color = plante.color;
                    // color.a = 1f;
                    // skill.color = color;
                    // skill.sprite = leftUpSkill.FuWenSprite;
                    
                }
                    break;
                case EnableType.Skill:
                {

                    Color color = plante.color;
                    color.a = 1f;
                    skill.color = color;
                    skill.sprite = rightDownSkill.FuWenSprite;
                    HighLightFuWenSprite(rightDownSkill);
                }
                    break;
                case EnableType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // if (planteEnable == true)
            // {
            //     up.transform.rotation = Quaternion.Euler(0, 0, -155);
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     plante.color = color;
            //     plante.sprite = venus.FuWenSprite;
            // }
            // else if (skillEnable == true)
            // {
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     skill.color = color;
            //     skill.sprite = rightDownSkill.FuWenSprite;
            // }
        }


        else if (moveInput.x > 0.3 && moveInput.y > 0.3)
        {
            switch (enableState)
            {
                case EnableType.Planet:
                {
                    up.transform.rotation = Quaternion.Euler(0, 0, -52);

                    Color color = plante.color;
                    color.a = 1f;
                    plante.color = color;
                    plante.sprite = moon.FuWenSprite;
                    HighLightFuWenSprite(moon);
                }
                    break;
                case EnableType.Element:
                {                
                    // Color color = plante.color;
                    // color.a = 1f;
                    // skill.color = color;
                    // skill.sprite = leftUpSkill.FuWenSprite;
                    
                }
                    break;
                case EnableType.Skill:
                {

     
                    Color color = plante.color;
                    color.a = 1f;
                    skill.color = color;
                    skill.sprite = rightUpSkill.FuWenSprite;
                    
                    HighLightFuWenSprite(rightUpSkill);
                }
                    break;
                case EnableType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // if (planteEnable == true)
            // {
            //     up.transform.rotation = Quaternion.Euler(0, 0, -52);
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     plante.color = color;
            //     plante.sprite = moon.FuWenSprite;
            // }
            // else if (skillEnable == true)
            // {
            //
            //     Color color = plante.color;
            //     color.a = 1f;
            //     skill.color = color;
            //     skill.sprite = rightUpSkill.FuWenSprite;
            // }
        }
    }

    private void OnDisable()
    {
        if(input!=null)input.gameplay.plante.performed -= plante_performed;

       if(input!=null) input.Disable();
    }

    void Start()
    {
        animator = Panel.GetComponent<Animator>();
        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (RandomObjectMover.Instance!=null&&RandomObjectMover.Instance.RightPanel.activeSelf)
        {
            icon.SetActive(false);
        }
        else icon.SetActive(true);
        if (FindObjectOfType<MusicChange>())
        {
            if(FindObjectOfType<MusicChange>().panel.activeSelf == true)
                icon.SetActive(false);
            else icon.SetActive(true);
        }
        if (Panel.activeSelf)
        {
            Time.timeScale =0f; 
        }
        else
        {
            Time.timeScale = 1f; // ª÷∏¥”Œœ∑
        }
        //if(input.FindAction("switchControl").ReadValue<float>() > 0.5f && input.FindAction("switch").ReadValue<Vector2>().y > 0.5f)
        if (input.FindAction("switch").ReadValue<Vector2>().y > 0.5f)
        {
            if (!complete)
            {
                switch (enableState)
                {
                    case EnableType.Planet:
                    {
                        // planteEnable = false;
                        // skillEnable = true;

                        enableState = EnableType.Skill;
                    }
                        break;
                    case EnableType.Element:
                    {
                        // elementEnable = false;
                        // planteEnable = true;
                        
                        enableState = EnableType.Planet;
                    }
                        break;
                    case EnableType.Skill:
                    {
                        // skillEnable = false;
                        // elementEnable = true;
                        
                        enableState = EnableType.Element;
                    }
                        break;
                    case EnableType.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                
                // if (planteEnable == true)
                // {
                //     planteEnable = false;
                //     skillEnable = true;
                // }
                // else if (elementEnable == true)
                // {
                //
                //     elementEnable = false;
                //     planteEnable = true;
                // }
                // else if (skillEnable == true)
                // {
                //
                //     skillEnable = false;
                //     elementEnable = true;
                // }

                complete = true;
            }
        }
        else if (input.FindAction("switch").ReadValue<Vector2>().y < -0.5f)
        {
            if (!complete)
            {
                // if (planteEnable == true)
                // {
                //
                //     elementEnable = true;
                //     planteEnable = false;
                // }
                // else if (elementEnable == true)
                // {
                //     elementEnable = false;
                //     skillEnable = true;
                // }
                // else if (skillEnable == true)
                // {
                //     skillEnable = false;
                //     planteEnable = true;
                // }

                switch (enableState)
                {
                    case EnableType.Planet:
                    {
                        // elementEnable = true;
                        // planteEnable = false;
                        
                        enableState = EnableType.Element;
                    }
                        break;
                    case EnableType.Element:
                    {
                        // elementEnable = false;
                        // skillEnable = true;
                        
                        enableState = EnableType.Skill;
                    }
                        break;
                    case EnableType.Skill:
                    {
                        // skillEnable = false;
                        // planteEnable = true;
                        
                        enableState = EnableType.Planet;
                    }
                        break;
                    case EnableType.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                complete = true;
            }
        }
        else
        {
            complete = false;
        }
        if (enableState==EnableType.Planet|| enableState==EnableType.Element)
        {
            plante.color = new Color(plante.color.r, plante.color.g, plante.color.b, 1f); 
            element.color = new Color(element.color.r, element.color.g, element.color.b, 1f); 
            skill.color = new Color(skill.color.r, skill.color.g, skill.color.b, 0f);
        }
        else if (enableState==EnableType.Skill)
        {
            plante.color = new Color(plante.color.r, plante.color.g, plante.color.b, 0f);
            element.color = new Color(element.color.r, element.color.g, element.color.b, 0f); 
            skill.color = new Color(skill.color.r, skill.color.g, skill.color.b, 1f); 
        }
        // if (planteEnable || elementEnable)
        // {
        //     plante.color = new Color(plante.color.r, plante.color.g, plante.color.b, 1f); 
        //     element.color = new Color(element.color.r, element.color.g, element.color.b, 1f); 
        //     skill.color = new Color(skill.color.r, skill.color.g, skill.color.b, 0f);
        // }
        // else if (skillEnable)
        // {
        //     plante.color = new Color(plante.color.r, plante.color.g, plante.color.b, 0f);
        //     element.color = new Color(element.color.r, element.color.g, element.color.b, 0f); 
        //     skill.color = new Color(skill.color.r, skill.color.g, skill.color.b, 1f); 
        // }
        if (Input.GetKeyDown(key) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Panel.SetActive(!Panel.activeSelf);
            icon.SetActive(!Panel.activeSelf);
            
            var obj = GameObject.FindGameObjectWithTag("Player");
            
            if(obj!=null)obj.GetComponent<PlayerController>().RegisterPanel(Panel);
        }
        if(Panel.activeSelf)
        {
            if(animator!=null)animator.Play("Play");
        }

        switch (enableState)
        {
            case EnableType.Planet:
            {
                PlanteDisk.material = replacementMaterial;
                ElementDisk.material = null;
                SkillRunes.material = null;
                
                //Debug.Log("Plant");
            }
                break;
            case EnableType.Element:
            {
                ElementDisk.material = replacementMaterial;
                PlanteDisk.material = null;
                SkillRunes.material = null;
                //Debug.Log("Element");
            }
                break;
            case EnableType.Skill:
            {
                SkillRunes.material = replacementMaterial;
                PlanteDisk.material = null;
                ElementDisk.material = null;
            }
                break;
            case EnableType.None:
            {
                PlanteDisk.material = null;
                ElementDisk.material = null;
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
        // if (planteEnable)
        // {
        //     PlanteDisk.material = replacementMaterial;
        //     ElementDisk.material = null;
        //     SkillRunes.material = null;
        // }
        // else if (elementEnable)
        // {
        //     ElementDisk.material = replacementMaterial;
        //     PlanteDisk.material = null;
        //     SkillRunes.material = null;
        // }
        // else if (skillEnable)
        // {
        //     SkillRunes.material = replacementMaterial;
        //     PlanteDisk.material = null;
        //     ElementDisk.material = null;
        // }
        // else
        // {
        //     PlanteDisk.material = null;
        //     ElementDisk.material = null;
        // }
    }
    
}

public enum EnableType
{
    Planet,
    Element,
    Skill,
    None
}

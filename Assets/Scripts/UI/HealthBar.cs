using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Mover mover;
    public Slider healthBar;
    public Text healthText;
    public Color color;
    public Color backgroundColor;

    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = mover.maxHealth;
        healthBar.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        healthBar.transform.Find("Background").GetComponent<Image>().color = backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = mover.currentHealth;
        healthText.text = $"{mover.currentHealth:F1}";
    }
}
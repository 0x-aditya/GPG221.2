using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int wood = 0;
    public int stone = 0;
    public int food = 0;
    public int maxWood = 75;
    public int maxStone = 95;
    public int maxFood = 45;
    
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI foodText;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        UpdateResourceTexts();
    }

    public bool HasResources(int requiredWood, int requiredStone, int requiredFood)
    {
        return wood >= requiredWood && stone >= requiredStone && food >= requiredFood;
    }

    public void UseResources(int usedWood, int usedStone, int usedFood)
    {
        if (!HasResources(usedWood, usedStone, usedFood))
        {
            Debug.Log("Not enough resources");
            return;
        }

        wood -= usedWood;
        stone -= usedStone;
        food -= usedFood;

        UpdateResourceTexts();
    }

    public void AddWood(int amount)
    {
        wood += amount;
        if (wood > maxWood)
        {
            wood = maxWood;
        }
        UpdateResourceTexts();
    }

    public void AddStone(int amount)
    {
        stone += amount;
        if (stone > maxStone)
        {
            stone = maxStone;
        }
        UpdateResourceTexts();
    }
    
    public void AddFood(int amount)
    {
        food += amount;
        if (food > maxFood)
        {
            food = maxFood;
        }
        UpdateResourceTexts();
    }
    private void UpdateResourceTexts()
    {
        woodText.text = $"Wood: {wood}";
        stoneText.text = $"Stone: {stone}";
        foodText.text = $"Food: {food}";
    }
}


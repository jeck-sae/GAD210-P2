using TMPro;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [Header("Resources")]
    [SerializeField] int food;
    [SerializeField] int energy;
    [SerializeField] int oxygen;

    [Header("Starting Resources")]
    [SerializeField] int startingFood = 25;
    [SerializeField] int startingEnergy = 25;
    [SerializeField] int startingOxygen = 25;

    [Header("Decay")]
    [SerializeField]public int decayAmount = 1;
    [SerializeField] float decayInterval = 1f;
    private float decayTimer;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI foodText;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI oxygenText;

    private void Start()
    {
        food = startingFood;
        energy = startingEnergy;
        oxygen = startingOxygen;
        UpdateResourceUI();
    }
    private void Update()
    {
        decayTimer += Time.deltaTime;
        if (decayTimer >= decayInterval)
        {
            decayTimer = 0f;
            ApplyDecay();
        }
    }
    private void ApplyDecay()
    {
        food = Mathf.Max(0, food - decayAmount);
        energy = Mathf.Max(0, energy - decayAmount);
        oxygen = Mathf.Max(0, oxygen - decayAmount);

        if (food == 0 || energy == 0 || oxygen == 0)
        {
            EndScreen.Instance.GAMEOVER();
            Debug.Log("YOU LOSE");
        }

        UpdateResourceUI();
    }
    public void AddResource(ResourceType type, int amount)
    {
        Debug.Log("Added resources");
        switch (type)
        {
            case ResourceType.Food:
                food += amount;
                break;
            case ResourceType.Energy:
                energy += amount;
                break;
            case ResourceType.Oxygen:
                oxygen += amount;
                break;
        }
        UpdateResourceUI();
    }
    public void UpdateResourceUI()
    {
        foodText.text = food.ToString();
        energyText.text = energy.ToString();
        oxygenText.text = oxygen.ToString();
    }
}
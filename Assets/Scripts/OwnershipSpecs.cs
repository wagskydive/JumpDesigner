using UnityEngine;

public class OwnershipSpecs
{
    public int Age{ get; private set; }

    public int Size { get; private set; }

    public float DamagePercentage { get; private set; }

    public bool IsDamaged { get; private set; }
    public Color[] CustomColors { get; private set; }

    public OwnershipSpecs(int _age, int _size, Color[] _customColors, float _damagePercentage)
    {
        Age = _age;
        Size = _size;
        CustomColors = _customColors;
        DamagePercentage = _damagePercentage;
        IsDamaged = DamagePercentage >= 1;
    }   


    public void AgeUp()
    {
        Age++;
    }

    public void SetCustomColors(Color[] _customColors)
    {
        CustomColors = _customColors;
    }

    public void AddDamage(float _damage)
    {
        DamagePercentage += _damage;
        if(DamagePercentage > 1)
        {
            DamagePercentage = 1;

        }
    }

    void SetAsDamaged()
    {
        IsDamaged = true;
    }


    void Repair()
    {
        DamagePercentage = 0;
        IsDamaged = false;
    }


    
}

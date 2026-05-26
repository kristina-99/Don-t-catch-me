public class WeaponData
{
    private const int SwordDamageBuff = 0;

    public static readonly WeaponData Sword = new WeaponData("Sword", WeaponType.Sword, SwordDamageBuff);

    public string DisplayName { get; }
    public WeaponType Type { get; }
    public int DamageBuff { get; }

    public WeaponData(string displayName, WeaponType type, int damageBuff)
    {
        DisplayName = displayName;
        Type = type;
        DamageBuff = damageBuff;
    }
}

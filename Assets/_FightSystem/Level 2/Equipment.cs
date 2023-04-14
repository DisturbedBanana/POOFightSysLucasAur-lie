
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Défintion simple d'un équipement apportant des boost de stats
    /// </summary>

    public class Equipment
    {
        int _bonusHealth;
        int _bonusAttack;
        int _bonusDefense;
        int _bonusSpeed;
        bool _isPriority;

        public Equipment(int bonusHealth, int bonusAttack, int bonusDefense, int bonusSpeed, bool isPriority)
        {
            _bonusHealth = bonusHealth;
            _bonusAttack = bonusAttack;
            _bonusDefense = bonusDefense;
            _bonusSpeed = bonusSpeed;
            _isPriority = isPriority;
        }

        public int BonusHealth { get => _bonusHealth; }
        public int BonusAttack { get => _bonusAttack; }
        public int BonusDefense { get => _bonusDefense; }
        public int BonusSpeed { get => _bonusSpeed; }
        public bool IsPriority { get => _isPriority; }

    }
}

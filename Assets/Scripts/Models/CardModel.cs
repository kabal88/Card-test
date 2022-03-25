using Data;

namespace Models
{
    public class CardModel
    {
        public int Health { get; private set; }
        public int Mana{ get; private set;}
        public int Attack{ get; private set;}

        public void SetHealth(int value)
        {
            Health = value;
        }
        
        public void SetMana(int value)
        {
            Mana = value;
        }
        
        public void SetAttack(int value)
        {
            Attack = value;
        }

        public CardModel(Stats stats)
        {
            Health = stats.Health;
            Mana = stats.Mana;
            Attack = stats.Attack;
        }
    }
}
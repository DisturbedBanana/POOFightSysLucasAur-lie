
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            if (character1 == null || character2 == null)
            {
                throw new ArgumentNullException();
            }
            Character1 = character1;
            Character2 = character2;
        }

        public Character Character1 { get; private set; }
        public Character Character2 { get; private set; }
        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished
        {
            get
            {
                if (!Character1.IsAlive || !Character2.IsAlive)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            //skillFromCharacter1.Power += Character1.Attack;
            //skillFromCharacter2.Power += Character2.Attack;

            switch(Character1.Speed > Character2.Speed)
            {
                case true:
                    skillFromCharacter1.Power = Character1.Attack;
                    Character2.ReceiveAttack(skillFromCharacter1);
                    if (Character2.IsAlive)
                        Character1.ReceiveAttack(skillFromCharacter2);
                    break;
                case false:
                    skillFromCharacter2.Power = Character2.Attack;
                    Character1.ReceiveAttack(skillFromCharacter2);
                    if (Character1.IsAlive)
                        Character2.ReceiveAttack(skillFromCharacter1);
                    break;
            }
        }

    }
}

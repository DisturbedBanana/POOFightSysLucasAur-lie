using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer les TU sur le reste et de les implémenter

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // - un heal ne régénère pas plus que les HP Max
        // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type

        [Test]
        public void HealDoesntGoOverMax()
        {
            Character c = new Character(100, 10, 10, 10, TYPE.NORMAL);
            Punch punch = new Punch();
            c.ReceiveAttack(punch);
            c.Heal(1000);
            Assert.That(c.CurrentHealth, Is.EqualTo(100));
        }

        [Test]
        public void ReduceMaxHPNoOverflow()
        {
            Character c = new Character(100, 10, 10, 10, TYPE.NORMAL);
            c.ReduceMaxHealth(50);
            Assert.That(c.CurrentHealth, Is.EqualTo(c.MaxHealth));
        }

        [Test]
        //Pour implémenter ca j'ai simplement rajouté un boolean
        //dans certains de tes tests qui appelent new Equipment.
        //Le Int WhoAttackedFirst si = 0 -> aucun des 2 character n'avaient d'equipement prioritaire
        //si = 1 -> le character 1 a un equipement prioritaire
        //si = 2 -> le character 2 a un equipement prioritaire
        public void PriorityEquipment()
        {
            Character c1 = new Character(100, 10, 10, 10, TYPE.NORMAL);
            Character c2 = new Character(80, 20, 5, 15, TYPE.NORMAL);
            Equipment prioEquipment = new Equipment(0, 0, 0, 0, true);
            c2.Equip(prioEquipment);
            Fight f1 = new Fight(c1, c2);
            Punch p = new Punch();
            f1.ExecuteTurn(p, p);
            Assert.That(f1.WhoAttackedFirst, Is.EqualTo(2));

            c2.Unequip();
            c1.Equip(prioEquipment);
            f1.ExecuteTurn(p, p);
            Assert.That(f1.WhoAttackedFirst, Is.EqualTo(1));

            c1.Unequip();
            f1.ExecuteTurn(p, p);
            Assert.That(f1.WhoAttackedFirst, Is.EqualTo(0));
        }

        [Test]
        public void EquipmentIsntNullCheck()
        {
            Character c1 = new Character(100, 10, 10, 10, TYPE.NORMAL);
            Character c2 = new Character(80, 20, 5, 15, TYPE.NORMAL);
            Fight f1 = new Fight(c1, c2);
            Punch p1 = new Punch();
            Punch p2 = null;

            Assert.Throws<ArgumentNullException>(() => f1.ExecuteTurn(p1, p2));
        }

        [Test]
        public void StrengthsWeaknesses()
        {
            Character cNormal = new Character(100, 30, 10, 40, TYPE.NORMAL);
            Character cFire = new Character(100, 30, 10, 10, TYPE.FIRE);
            Character cWater = new Character(100, 30, 10, 20, TYPE.WATER);
            Character cGrass = new Character(100, 30, 10, 10, TYPE.GRASS);

            FireBall fb = new FireBall();
            WaterBlouBlou wbb = new WaterBlouBlou();
            Punch p = new Punch();
            MagicalGrass mg = new MagicalGrass();

            //Fire vs Water 
            Fight f = new Fight(cFire, cWater);
            f.ExecuteTurn(fb, wbb);
            Assert.That(cFire.CurrentHealth, Is.EqualTo(50));
            Assert.That(cWater.CurrentHealth, Is.EqualTo(95));
            cFire.Heal(1000);
            cWater.Heal(1000);
                
            //Grass vs Fire
            Fight f1 = new Fight(cFire, cGrass);
            f1.ExecuteTurn(fb, mg);
            Assert.That(cFire.CurrentHealth, Is.EqualTo(95));
            Assert.That(cGrass.CurrentHealth, Is.EqualTo(50));
            cFire.Heal(1000);
            cGrass.Heal(1000);

            //Grass vs Water
            Fight f2 = new Fight(cGrass, cWater);
            f2.ExecuteTurn(mg, wbb);
            Assert.That(cGrass.CurrentHealth, Is.EqualTo(95));
            Assert.That(cWater.CurrentHealth, Is.EqualTo(50));
        }
    }
}

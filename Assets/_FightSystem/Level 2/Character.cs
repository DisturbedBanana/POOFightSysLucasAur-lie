﻿using System;
using System.Linq.Expressions;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        int _maxHealth;
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _maxHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { 
            get
            {
                return _baseHealth;
            }
            private set
            {
                _baseHealth = value;
            }
        }
        public TYPE BaseType { get => _baseType;}
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return _maxHealth;
            }
            private set
            {
                _maxHealth = value;
            }
        }
        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get
            {
                return _baseAttack;
            }
            private set
            {
                _baseAttack = value;
            }
        }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get
            {
                return _baseDefense;
            }
            private set
            {
                _baseDefense = value;
            }
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get
            {
                return _baseSpeed;
            }
            private set
            {
                _baseSpeed = value;
            }
        }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive
        {
            get
            {
                if (CurrentHealth <= 0)
                    return false;
                else
                    return true;
            }
        }


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s)
        {
            switch (s.Type)
            {
                case TYPE.NORMAL:
                    CurrentHealth -= (s.Power - Defense);
                    break;
                case TYPE.FIRE:
                    switch (BaseType)
                    {
                        case TYPE.FIRE:
                            CurrentHealth -= (s.Power - Defense);
                            break;
                        case TYPE.WATER:
                            CurrentHealth -= (s.Power / 2) - Defense;
                            break;
                        case TYPE.GRASS:
                            CurrentHealth -= (s.Power * 2) - Defense;
                            break;
                    }
                    break;
                case TYPE.WATER:
                    switch (BaseType)
                    {
                        case TYPE.FIRE:
                            CurrentHealth -= (s.Power * 2) - Defense;
                            break;
                        case TYPE.WATER:
                            CurrentHealth -= (s.Power - Defense);
                            break;
                        case TYPE.GRASS:
                            CurrentHealth -= (s.Power / 2) - Defense;
                            break;
                    }
                    break;
                case TYPE.GRASS:
                    switch (BaseType)
                    {
                        case TYPE.FIRE:
                            CurrentHealth -= (s.Power / 2) - Defense;
                            break;
                        case TYPE.WATER:
                            CurrentHealth -= (s.Power * 2) - Defense;
                            break;
                        case TYPE.GRASS:
                            CurrentHealth -= (s.Power - Defense);
                            break;
                    }
                    break;
            }
            
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
                
            CurrentStatus = StatusEffect.GetNewStatusEffect(s.Status);
        }
        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null)
            {
                throw new ArgumentNullException();
            }
            CurrentEquipment = newEquipment;
            MaxHealth += newEquipment.BonusHealth;
            Attack += newEquipment.BonusAttack;
            Defense += newEquipment.BonusDefense;
            Speed += newEquipment.BonusSpeed;
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            MaxHealth -= CurrentEquipment.BonusHealth;
            Attack -= CurrentEquipment.BonusAttack;
            Defense -= CurrentEquipment.BonusDefense;
            Speed -= CurrentEquipment.BonusSpeed;
            CurrentEquipment = null;
        }

        public void Heal(int value)
        {
            if (CurrentHealth + value > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            else
            {
                CurrentHealth += value;
            }
        }

        public void ReduceMaxHealth(int value)
        {
            MaxHealth -= value;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }
}

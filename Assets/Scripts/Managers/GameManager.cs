﻿using System;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers {
    /// <summary>
    /// Общая логика управления игрой
    /// </summary>
    public class GameManager : MonoBehaviour {
        public static GameManager Instance;

        public Image Background;
        public Sprite PoorHouseSprite;
        public Sprite CommonHouseSprite;
        public Sprite ExpensiveHouseSprite;

        private void Awake() {
            Instance = this;
            SetHouseSprite();
        }

        /// <summary>
        /// Устанавливает бэкграунд дома игрока
        /// </summary>
        public void SetHouseSprite() {
            var house = PlayerManager.GetProperty().House;
            Background.sprite = GetSprite(house);
        }

        /// <summary>
        /// Возвращает спрайт дома
        /// </summary>
        private Sprite GetSprite(HouseType house) {
            switch (house) {
                case HouseType.Poor:
                    return PoorHouseSprite;
                case HouseType.Common:
                    return CommonHouseSprite;
                case HouseType.Expensive:
                    return ExpensiveHouseSprite;
                default:
                    throw new ArgumentException();
            }
        }
    }
}

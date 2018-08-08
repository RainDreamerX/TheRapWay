using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Extentions;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Actions {
    /// <summary>
    /// Магазин
    /// </summary>
    public class Shop : BaseAction {
        private const int AUTHOTUNE_PRICE = 2000;

        public GameObject Micro;
        public GameObject Launchpad;
        public GameObject Autotune;
        public GameObject Houses;

        /// <summary>
        /// Цены микрофонов
        /// </summary>
        private static readonly Dictionary<PropertyLevel, int> _microPrices = new Dictionary<PropertyLevel, int> {
            [PropertyLevel.Cheapest] = 100,
            [PropertyLevel.Cheap] = 1000,
            [PropertyLevel.Middle] = 5000,
            [PropertyLevel.Expensive] = 10000,
            [PropertyLevel.MostExpensive] = 20000
        };

        /// <summary>
        /// Цены лаучпадов
        /// </summary>
        private static readonly Dictionary<PropertyLevel, int> _launchpadPrices = new Dictionary<PropertyLevel, int> {
            [PropertyLevel.Cheapest] = 5000,
            [PropertyLevel.Cheap] = 12000,
            [PropertyLevel.Middle] = 20000,
            [PropertyLevel.Expensive] = 30000,
            [PropertyLevel.MostExpensive] = 40000
        };

        /// <summary>
        /// Требования для покупки домов
        /// </summary>
        private static readonly Dictionary<HouseType, Tuple<int, int>> _housesRequirements = new Dictionary<HouseType, Tuple<int, int>> {
            [HouseType.Common] = new Tuple<int, int>(800000, 70000),
            [HouseType.Expensive] = new Tuple<int, int>(2500000, 1500000)
        };

        /// <summary>
        /// Инициализация дочернего компонента
        /// </summary>
        public override void ChildAwake() {
            Micro.GetComponentInChildren<Button>().onClick.AddListener(() => OnBuy(OnBuyMicro));
            Launchpad.GetComponentInChildren<Button>().onClick.AddListener(() => OnBuy(OnBuyLaunchpad));
            Autotune.GetComponentInChildren<Button>().onClick.AddListener(() => OnBuy(OnBuyAutotune));
            Houses.GetComponentInChildren<Button>().onClick.AddListener(() => OnBuy(OnBuyHouse));
        }

        /// <summary>
        /// Открывает страницу
        /// </summary>
        public override void OpenPage() {
            var playerProperty = PlayerManager.GetProperty();
            ShowPropertyGood(Micro, playerProperty.Micro, _microPrices);
            ShowPropertyGood(Launchpad, playerProperty.Launchpad, _launchpadPrices);
            if (playerProperty.HasAutotune) HideBuyButton(Autotune);
            ShowHouseProperty(playerProperty);
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Выводит информацию о товаре
        /// </summary>
        private static void ShowPropertyGood(GameObject component, PropertyLevel playerProperty, IReadOnlyDictionary<PropertyLevel, int> prices) {
            component.GetComponentsInChildren<Text>(true).First(e => e.name == "Level").text = $"Тек. ур. {(int)playerProperty}";
            if (playerProperty != PropertyLevel.MostExpensive) {
                var next = playerProperty + 1;
                component.GetComponentsInChildren<Text>(true).First(e => e.name == "Price").text = $"{prices[next]} $";
            }
            else {
                HideBuyButton(component);
            }
        }

        /// <summary>
        /// Выводит информацию о покупке дома
        /// </summary>
        private void ShowHouseProperty(PlayerProperty playerProperty) {
            Houses.GetComponentsInChildren<Text>(true).First(e => e.name == "Level").text = $"Текущий: {playerProperty.House.GetDescription()}";
            if (playerProperty.House != HouseType.Expensive) {
                var nextHouse = playerProperty.House + 1;
                var requirements = _housesRequirements[nextHouse];
                Houses.GetComponentsInChildren<Text>(true).First(e => e.name == "Price").text = $"{NumberFormatter.FormatValue(requirements.Item1)} $";
                Houses.GetComponentsInChildren<Text>(true).First(e => e.name == "Fans").text = $"{NumberFormatter.FormatValue(requirements.Item1)}";
            }
            else {
                HideBuyButton(Houses);
            }
        }

        /// <summary>
        /// Скрывает кнопку покупки
        /// </summary>
        private static void HideBuyButton(GameObject component) {
            component.GetComponentInChildren<Button>(true).gameObject.SetActive(false);
            component.GetComponentsInChildren<Text>(true).First(e => e.name == "Price").gameObject.SetActive(false);
            component.GetComponentsInChildren<Text>(true).First(e => e.name == "AlreadyExist").gameObject.SetActive(true);
            if (component.name == "Houses") {
                component.GetComponentsInChildren<Text>(true).First(e => e.name == "Fans").gameObject.SetActive(false);
                component.GetComponentsInChildren<Image>(true).First(e => e.name == "FansIcon").gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Обработчик покупки
        /// </summary>
        private void OnBuy(Action<PlayerInfo> action) {
            var playerInfo = PlayerManager.GetInfo();
            action(playerInfo);
            StatsManager.UpdateStats();
            gameObject.SetActive(false);
            gameObject.GetComponentInParent<ActionsMenu>().TriggerChildVisible();
            SaveManager.Instance.Save();
        }

        /// <summary>
        /// Обработчик покупки микрофона
        /// </summary>
        private void OnBuyMicro(PlayerInfo playerInfo) {
            var nextMicro = playerInfo.PlayerProperty.Micro + 1;
            if (!EnoughMoney(playerInfo.Money, _microPrices[nextMicro])) return;
            playerInfo.Money -= _microPrices[nextMicro];
            playerInfo.PlayerProperty.Micro = nextMicro;
        }

        /// <summary>
        /// Обработчик покупки лаунчпада
        /// </summary>
        private void OnBuyLaunchpad(PlayerInfo playerInfo) {
            var nextPad = playerInfo.PlayerProperty.Launchpad + 1;
            if (!EnoughMoney(playerInfo.Money, _launchpadPrices[nextPad])) return;
            playerInfo.Money -= _launchpadPrices[nextPad];
            playerInfo.PlayerProperty.Launchpad = nextPad;
        }

        /// <summary>
        /// Обработчик покупки автотюна
        /// </summary>
        private void OnBuyAutotune(PlayerInfo playerInfo) {
            if (!EnoughMoney(playerInfo.Money, AUTHOTUNE_PRICE)) return;
            playerInfo.Money -= AUTHOTUNE_PRICE;
            playerInfo.PlayerProperty.HasAutotune = true;
        }

        /// <summary>
        /// Обработчик покупки автотюна
        /// </summary>
        private void OnBuyHouse(PlayerInfo playerInfo) {
            var requirements = _housesRequirements[playerInfo.PlayerProperty.House + 1];
            if (!EnoughMoney(playerInfo.Money, requirements.Item1)) return;
            if (playerInfo.Fans < requirements.Item2) {
                AlertManager.ShowMessage("Недостаточное количество фанатов");
                return;
            }
            playerInfo.Money -= requirements.Item1;
            playerInfo.PlayerProperty.House += 1;
            GameManager.Instance.SetHouseSprite();
        }

        /// <summary>
        /// Проверяет, достаточно ли денег
        /// </summary>
        private bool EnoughMoney(int money, int price) {
            if (money < price) {
                AlertManager.ShowMessage("Недостаточно денег для покупки");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Игнорируем
        /// </summary>
        protected override void ParseActionModel() {}
        protected override void CalculateDurationAndPrice() {}
    }
}
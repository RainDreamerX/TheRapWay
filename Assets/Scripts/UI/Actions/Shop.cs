using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
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

        /// <summary>
        /// Цены микрофонов
        /// </summary>
        private static readonly Dictionary<PropertyLevel, int> microPrices = new Dictionary<PropertyLevel, int> {
            [PropertyLevel.Cheapest] = 100,
            [PropertyLevel.Cheap] = 1000,
            [PropertyLevel.Middle] = 5000,
            [PropertyLevel.Expensive] = 10000,
            [PropertyLevel.MostExpensive] = 20000
        };

        /// <summary>
        /// Цены лаучпадов
        /// </summary>
        private static readonly Dictionary<PropertyLevel, int> launchpadPrices = new Dictionary<PropertyLevel, int> {
            [PropertyLevel.Cheapest] = 5000,
            [PropertyLevel.Cheap] = 12000,
            [PropertyLevel.Middle] = 20000,
            [PropertyLevel.Expensive] = 30000,
            [PropertyLevel.MostExpensive] = 40000
        };

        /// <summary>
        /// Инициализация дочернего компонента
        /// </summary>
        public override void ChildAwake() {
            Micro.GetComponentInChildren<Button>().onClick.AddListener(() => OnBuy(OnBuyMicro));
            Launchpad.GetComponentInChildren<Button>().onClick.AddListener(() => OnBuy(OnBuyLaunchpad));
            Autotune.GetComponentInChildren<Button>().onClick.AddListener(() => OnBuy(OnBuyAutotune));
        }

        /// <summary>
        /// Открывает страницу
        /// </summary>
        public override void OpenPage() {
            var playerProperty = PlayerManager.GetProperty();
            ShowPropertyGood(Micro, playerProperty.Micro, microPrices);
            ShowPropertyGood(Launchpad, playerProperty.Launchpad, launchpadPrices);
            if (playerProperty.HasAutotune) HideBuyButton(Autotune);
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Выводит информацию о товаре
        /// </summary>
        private static void ShowPropertyGood(GameObject component, PropertyLevel playerProperty, IReadOnlyDictionary<PropertyLevel, int> prices) {
            if (playerProperty != PropertyLevel.MostExpensive) {
                component.GetComponentsInChildren<Text>(true).First(e => e.name == "Level").text = $"Тек. ур. {(int) playerProperty}";
                var next = playerProperty + 1;
                component.GetComponentsInChildren<Text>(true).First(e => e.name == "Price").text = $"{prices[next]} $";
            }
            else {
                HideBuyButton(component);
            }
        }

        /// <summary>
        /// Скрывает кнопку покупки
        /// </summary>
        private static void HideBuyButton(GameObject component) {
            component.GetComponentInChildren<Button>(true).gameObject.SetActive(false);
            component.GetComponentsInChildren<Text>(true).First(e => e.name == "Price").gameObject.SetActive(false);
            component.GetComponentsInChildren<Text>(true).First(e => e.name == "AlreadyExist").gameObject.SetActive(true);
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
        }

        /// <summary>
        /// Обработчик покупки микрофона
        /// </summary>
        private void OnBuyMicro(PlayerInfo playerInfo) {
            var nextMicro = playerInfo.PlayerProperty.Micro + 1;
            if (!EnoughMoney(playerInfo.Money, microPrices[nextMicro])) return;
            playerInfo.Money -= microPrices[nextMicro];
            playerInfo.PlayerProperty.Micro = nextMicro;
        }

        /// <summary>
        /// Обработчик покупки лаунчпада
        /// </summary>
        private void OnBuyLaunchpad(PlayerInfo playerInfo) {
            var nextPad = playerInfo.PlayerProperty.Launchpad + 1;
            if (!EnoughMoney(playerInfo.Money, launchpadPrices[nextPad])) return;
            playerInfo.Money -= launchpadPrices[nextPad];
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
        protected override void ParseActionModel() { }
        protected override void CalculateDurationAndPrice() { }
    }
}
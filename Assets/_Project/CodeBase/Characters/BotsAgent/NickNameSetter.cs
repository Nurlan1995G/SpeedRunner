using Assets.Project.AssetProviders;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Project.CodeBase.SharkEnemy
{
    public class NickNameSetter : MonoBehaviour
    {
        [SerializeField] private List<BotView> _bots;
        [SerializeField] private List<BotController> _botControllers;
        [ReadOnly]
        [SerializeField] private string[] _ruNames;
        [ReadOnly]
        [SerializeField] private string[] _enNames;

        [Button]
        private void Initialize()
        {
            _ruNames = File.ReadAllLines(AssetAdress.RuNamePath);
            _enNames = File.ReadAllLines(AssetAdress.EnNamePath);
        }

        private void Start()
        {
            if (Localization.CurrentLanguage == AssetAdress.RU)
                SetRandomNicks(_ruNames);
            else
                SetRandomNicks(_enNames);
        }

        private void SetRandomNicks(string[] names)
        {
            foreach (BotView bot in _bots)
                bot.Nickname.Set(names[Random.Range(0, names.Length)]);

            foreach (BotController botController in _botControllers)
                botController.BotNickName.Set(names[Random.Range(0, names.Length)]);
        }

        [Button(), GUIColor("Green")]
        private void FindBots()
        {
            _bots = GetComponentsInChildren<BotView>().ToList();
            _botControllers = GetComponentsInChildren<BotController>().ToList();
        }

        [Button(), GUIColor("Red")]
        private void ClearList()
        {
            _bots.Clear();
            _botControllers.Clear();
        }
    }
}

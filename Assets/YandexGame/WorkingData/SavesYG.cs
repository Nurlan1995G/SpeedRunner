using System;
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        //Кастомные сохранения

        public int CurrentLevel;
        public int CurrentLevelIndex;
        public int Coin;
        public bool IsMute;
        public List<int> OpenItemsInfoId;

        public int SelectedSkin;
        public int SelectedObject;
        public int SelectedTrail;
        public int SelectedAnimal;

        public SavesYG()
        {
            Coin = 0;
            IsMute = false;

            SelectedSkin = 0;
            SelectedObject = 0;
            SelectedTrail = 0;
            SelectedAnimal = 0;

            OpenItemsInfoId = new List<int>
        {
            SelectedSkin,
            SelectedObject,
            SelectedTrail,
            SelectedAnimal,
        };
        }

        public void OpenItem(int id)
        {
            if (OpenItemsInfoId.Contains(id))
                throw new ArithmeticException(nameof(id));

            OpenItemsInfoId.Add(id);
        }

        public void SelectItem(ItemInfo itemInfo)
        {
            switch (itemInfo)
            {
                case SkinItemInfo:
                    SelectedSkin = itemInfo.Id;
                    break;
                case ObjectItemInfo:
                    SelectedObject = itemInfo.Id;
                    break;
                case TrailItemInfo:
                    SelectedTrail = itemInfo.Id;
                    break;
                case AnimalItemInfo:
                    SelectedAnimal = itemInfo.Id;
                    break;
            }
        }
    }
}
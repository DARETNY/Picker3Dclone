using scritableObject.Skıns;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Button[] skinButtons;
        public Myskın[] skins;
        [SerializeField] private TextMeshProUGUI balance;
        private int _vault;

        private void Start()
        {
            _vault = GameManager.Instance.saveManager.score;
            ShowMoney();

            for (int i = 0; i < skinButtons.Length; i++)
            {
                int index = i;

                skinButtons[i].onClick.AddListener(() =>
                {
                    if (_vault >= skins[index].price && !skins[index].isUnlock)
                    {
                        BuySkin(skins[index]);

                    }
                });
            }
        }

        void ShowMoney()
        {
            balance.text = "GOLD :" + _vault;
        }

        private void BuySkin(Myskın skin)
        {
            skin.isUnlock = true;
            _vault -= skin.price;
            ShowMoney();
            GameManager.Instance.saveManager.score = _vault;
            GameManager.Instance.saveManager.Save();
            if (skin.isUnlock)
            {
                
            }

        }
    }
}
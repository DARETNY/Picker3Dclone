using Manager;
using scritableObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Market.items
{
    public class MarketSystem : MonoBehaviour
    {

        [SerializeField] private İtemsSo _items;
        [SerializeField] private Button buybuButton;
        [SerializeField] private Button equiqButton;
        [SerializeField] private Button unequiqButton;
        [SerializeField] private Transform pos;

        [HideInInspector] public static GameObject spawnedObject;
        [SerializeField] private UIManager _uıManager;
        private int value;
        [SerializeField] private TextMeshProUGUI pricetext;
        [SerializeField] private Image _ımage;
        

        private void Start()
        {
            _ımage.sprite = _items.Image;
           

            LoadItem();
        }


        private void LoadItem()
        {
            if (_items.isPurchaed)
            {
                equiqButton.gameObject.SetActive(true);
                unequiqButton.gameObject.SetActive(false);
                
                pricetext.text = _items.name;
               
                


            }
            else
            {
                pricetext.text =  _items.price.ToString();
                buybuButton.gameObject.SetActive(true);
                equiqButton.gameObject.SetActive(false);
                unequiqButton.gameObject.SetActive(false);
            }
        }


        public void BuyItem()
        {
            if (!_items.isPurchaed && _items.price <= _uıManager.UpdateScoreUI())
            {
               

                _items.isPurchaed = true;
                _uıManager.UpdateMoney(-_items.price);

                equiqButton.gameObject.SetActive(true);
                unequiqButton.gameObject.SetActive(false);
                buybuButton.gameObject.SetActive(false);

                spawnedObject = Instantiate(_items.prefab, pos.position, pos.rotation, pos);
                spawnedObject.SetActive(false);

                SaveItem();
            }
        }
        private void SaveItem()
        {
            GameManager.Instance.saveManager.Save();
        }

        public void EquiqItem()
        {

            equiqButton.gameObject.SetActive(false);
            unequiqButton.gameObject.SetActive(true);
            spawnedObject = Instantiate(_items.prefab, pos.position, pos.rotation, pos);
            spawnedObject.SetActive(true);
            SaveItem();

        }


        public void UnEquiqİtem()
        {

            equiqButton.gameObject.SetActive(true);
            unequiqButton.gameObject.SetActive(false);
            Destroy(spawnedObject);


        }
    }
}
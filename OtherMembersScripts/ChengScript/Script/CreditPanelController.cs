using System;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// クレジットのパネルを管理するクラス
    /// </summary>
    public class CreditPanelController : MonoBehaviour
    {
        /// <summary>
        /// メンバークレジットのパネル
        /// </summary>
        [SerializeField] private Image _membersPanelImage;

        /// <summary>
        /// リソースクレジットのパネル
        /// </summary>
        [SerializeField] private Image _resourcePanelImage;

        private void Update()
        {
            if (TitleInputController.Instance.GetChangeMembersPanelKeyDown())
            {
                if(!_membersPanelImage.IsActive())
                {
                    SePlayer.Instance.Play("SE_Decide");
                    
                    _membersPanelImage.gameObject.SetActive(true);
                    _resourcePanelImage.gameObject.SetActive(false);
                }
                else
                {
                    SePlayer.Instance.Play("SE_Cancel");
                    _membersPanelImage.gameObject.SetActive(false);
                }
                
            }

            if(TitleInputController.Instance.GetChangeResourcePanelKeyDown())
            {
                if (!_resourcePanelImage.IsActive())
                {
                    SePlayer.Instance.Play("SE_Decide");
                    _membersPanelImage.gameObject.SetActive(false);
                    _resourcePanelImage.gameObject.SetActive(true);
                }
                else
                {
                    SePlayer.Instance.Play("SE_Cancel");
                    _resourcePanelImage.gameObject.SetActive(false);
                }
            }
        }
    }
}

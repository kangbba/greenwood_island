// using System.Collections;
// using UnityEngine;

// public class TrustLoss : Element
// {
//     private string _characterID;
//     private int _amount;

//     public TrustLoss(string characterID, int amount)
//     {
//         _characterID = characterID;
//         _amount = amount;
//     }

//     // 신뢰도를 감소시키는 코루틴
//     public override IEnumerator ExecuteRoutine()
//     {
//         GameDataManager.RemoveTrust(_characterID, _amount);
//         yield return null;
//     }
// }

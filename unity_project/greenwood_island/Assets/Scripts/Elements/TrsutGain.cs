// using System.Collections;
// using UnityEngine;

// public class TrustGain : Element
// {
//     private string _characterID;
//     private int _amount;

//     public TrustGain(string characterID, int amount)
//     {
//         _characterID = characterID;
//         _amount = amount;
//     }

//     // 신뢰도를 증가시키는 코루틴
//     public override IEnumerator ExecuteRoutine()
//     {
//         GameDataManager.AddTrust(_characterID, _amount);
//         yield return null;
//     }
// }
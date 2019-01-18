using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Gameplay
{
    public class LW_BoardsManager : MonoBehaviour
    {
        private List<GameObject> _Boards = new List<GameObject>();

        private void Start()
        {
            for(int i = 0; i < this.transform.childCount; i++)
            {
                Transform go = this.transform.GetChild(i);
                if(go.gameObject.CompareTag("Board"))
                {
                    _Boards.Add(go.gameObject);
                }
            }

            //InvokeRepeating("RemoveBoard", 2.0f, 2.0f);
        }

        public void AddBoard()
        {

        }

        public void RemoveBoard()
        {
            if(_Boards.Count != 0)
            {
                Destroy(_Boards[_Boards.Count - 1]);
                _Boards.RemoveAt(_Boards.Count - 1);
            }
            else
            {
                Debug.Log(">>>>> Trying to remove boards, but the list is empty");
            }
        }

        public int GetBoardsCount()
        {
            return _Boards.Count;
        }
    }
}


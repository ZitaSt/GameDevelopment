using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Gameplay
{
    public class LW_BoardsManager : MonoBehaviour
    {
        private List<GameObject> _Boards = new List<GameObject>();
        private float _LastTimeABoardIsRemoved = 0.0f;
        public float boardsAllowedToRemovedPerMinute = 4;
        private float _RemovedBoardsPerSecond;

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

            _RemovedBoardsPerSecond = 60 / boardsAllowedToRemovedPerMinute;

            //InvokeRepeating("RemoveBoard", 2.0f, 2.0f);
        }

        private void Update()
        {
            if(_Boards.Count != 0)
            {
                _LastTimeABoardIsRemoved += Time.deltaTime;
            }
        }

        public void AddBoard()
        {

        }

        public void RemoveBoard()
        {
            if(_Boards.Count != 0 &&
                _LastTimeABoardIsRemoved >= _RemovedBoardsPerSecond)
            {
                Destroy(_Boards[_Boards.Count - 1]);
                _Boards.RemoveAt(_Boards.Count - 1);
                _LastTimeABoardIsRemoved = 0;
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


using RPGM.Core;
using RPGM.Gameplay;
using UnityEngine;

namespace RPGM.UI
{
    /// <summary>
    /// Sends user input to the correct control systems.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        public float stepSize = 0.1f;
        GameModel model = Schedule.GetModel<GameModel>();

        public enum State
        {
            CharacterControl,
            DialogControl,
            Pause
        }

        State state;

        public void ChangeState(State state) => this.state = state;

        void Update()
        {
            switch (state)
            {
                case State.CharacterControl:
                    CharacterControl();
                    break;
                case State.DialogControl:
                    DialogControl();
                    break;
            }
        }

        void DialogControl()
        {
            model.player.nextMoveCommand = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                model.dialog.FocusButton(-1);
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                model.dialog.FocusButton(+1);
            if (Input.GetKeyDown(KeyCode.Space))
                model.dialog.SelectActiveButton();
        }

        private Vector3 GoalVector;
        void CharacterControl()
        {
            bool moveY = true;
            bool moveX = true;
            GoalVector = Vector3.zero;
            
            if (Input.GetKey(KeyCode.UpArrow))
                GoalVector += Vector3.up * stepSize;
            else if (Input.GetKey(KeyCode.DownArrow))
                GoalVector += Vector3.down * stepSize;
            else
                moveY = false;
            
            if (Input.GetKey(KeyCode.LeftArrow))
                GoalVector += Vector3.left * stepSize;
            else if (Input.GetKey(KeyCode.RightArrow))
                GoalVector += Vector3.right * stepSize;
            else
                moveX = false;
            
            if(moveY == false && moveX == false)
                GoalVector = Vector3.zero;

            model.player.nextMoveCommand = GoalVector;
        }
    }
}
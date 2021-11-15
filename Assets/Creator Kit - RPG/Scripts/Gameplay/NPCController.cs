using RPGM.Core;
using RPGM.Gameplay;
using UnityEngine;

namespace RPGM.Gameplay
{
    /// <summary>
    /// Main class for implementing NPC game objects.
    /// </summary>
    public class NPCController : MonoBehaviour
    {
        public ConversationScript[] conversations;

        Quest activeQuest = null;

        Quest[] quests;

        GameModel model = Schedule.GetModel<GameModel>();

        private Animator _npcAnimator;
        private Animator npcAnimator
        {
            get
            {
                if (_npcAnimator == null)
                    _npcAnimator = GetComponent<Animator>();
                return _npcAnimator;
            }
        }
        
        private SpriteRenderer _npcSpriteRenderer;
        private SpriteRenderer npcSpriteRenderer
        {
            get
            {
                if (_npcSpriteRenderer == null)
                    _npcSpriteRenderer = GetComponent<SpriteRenderer>();
                return _npcSpriteRenderer;
            }
        }

        
        void OnEnable()
        {
            quests = gameObject.GetComponentsInChildren<Quest>();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var c = GetConversation();
            if (c != null)
            {
                var ev = Schedule.Add<Events.ShowConversation>();
                ev.conversation = c;
                ev.npc = this;
                ev.gameObject = gameObject;
                ev.conversationItemKey = "";
            }
        }

        public void CompleteQuest(Quest q)
        {
            if (activeQuest != q) throw new System.Exception("Completed quest is not the active quest.");
            foreach (var i in activeQuest.requiredItems)
            {
                model.RemoveInventoryItem(i.item, i.count);
            }
            activeQuest.RewardItemsToPlayer();
            activeQuest.OnFinishQuest();
            activeQuest = null;
        }

        public void StartQuest(Quest q)
        {
            if (activeQuest != null) throw new System.Exception("Only one quest should be active.");
            activeQuest = q;
        }

        ConversationScript GetConversation()
        {
            if (activeQuest == null)
                return conversations[0];
            foreach (var q in quests)
            {
                if (q == activeQuest)
                {
                    if (q.IsQuestComplete())
                    {
                        CompleteQuest(q);
                        return q.questCompletedConversation;
                    }
                    return q.questInProgressConversation;
                }
            }
            return null;
        }

        public void ConversationAction(Sprite sprite, float time)
        {
            CancelInvoke();
            
            npcAnimator.enabled = false;
            npcSpriteRenderer.sprite = sprite;
            
            Invoke("AnimatorResume", time == 0 ? 1.5f : time);
        }

        void AnimatorResume()
        {
            npcAnimator.enabled = true;
        }
    }
}
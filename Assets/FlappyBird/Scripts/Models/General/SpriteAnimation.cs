using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.FlappyBird
{
    public class SpriteAnimation : MonoBehaviour
    {
        public float maxSpriteAnimationTime;
        public Sprite[] spritesToAnimate;
        public SpriteRenderer spriteHolder;
        public bool isAnimationLooping;
        [SerializeField] float currentTime;
        [SerializeField] float animationTime;
        [SerializeField] int spriteIndex;

        private void Start()
        {
            spriteHolder.sprite = spritesToAnimate[0];
            animationTime = maxSpriteAnimationTime / spritesToAnimate.Length;
        }
        public void StartAnimate()
        {
            ResetAnimation();
        }
        private void Update()
        {
            if (currentTime < maxSpriteAnimationTime)
            {
                if (currentTime < animationTime)
                {
                    currentTime += Time.deltaTime;
                }
                else
                {
                    animationTime = animationTime + currentTime;
                    spriteIndex++;
                    spriteHolder.sprite = spritesToAnimate[spriteIndex];
                }
            }
            else
            {
                if (isAnimationLooping)
                {
                    ResetAnimation();
                }
            }
        }
        private void ResetAnimation()
        {
            currentTime = 0;
            spriteIndex = 0;
            spriteHolder.sprite = spritesToAnimate[spriteIndex];
            animationTime = maxSpriteAnimationTime / spritesToAnimate.Length;
        }
    }
}
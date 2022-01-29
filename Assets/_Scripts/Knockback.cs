using _Scripts;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts
{
    public class Knockback : MonoBehaviour
    {
        CharacterController characterController;
        PlayerMovement playerMovement;

        public float knockbackTime = 0.5f;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        public void DoKnockback()
        {
            StartCoroutine(nameof(KnockbackSequence));
        }

        IEnumerator KnockbackSequence()
        {
            var totalTimeForDirection = knockbackTime / 2;
            var time = 0f;

            while (time < totalTimeForDirection)
            {
                time += Time.deltaTime;
                //playerMovement.Idle();
                characterController.Move(transform.forward * -1 * 10 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            time = 0;
            /*while (time < totalTimeForDirection)
            {
                time += Time.deltaTime;
                characterController.Move(transform.forward * 3 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }*/
        }
    }
}
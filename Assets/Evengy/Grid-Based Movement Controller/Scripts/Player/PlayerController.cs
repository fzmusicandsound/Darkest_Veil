using System.Numerics;
using Evengy.GridBasedMovementController.Navigation;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

namespace Evengy.GridBasedMovementController.Player
{
    public partial class PlayerController : MonoBehaviour
    {
        [SerializeField] bool smoothTransition = false;
        [SerializeField] float movementSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] float precision;


        bool defaultTransition;
        float defaultMovementSpeed;

        [SerializeField] TileController grid;
        TileController targetTile;
        Direction currentDirection;
        Vector3 targetRotation;

        public float range = 5;

        public bool IsBusy => Vector3.Distance(transform.position, targetTile.transform.position) > precision
            || Vector3.Distance(transform.rotation.eulerAngles, targetRotation) > precision;

        private Direction localDirection(Direction direction)
            => (Direction)(((int)currentDirection + (int)direction) % (int)Direction.Round);

        private void Start()
        {
            defaultTransition = smoothTransition;
            defaultMovementSpeed = movementSpeed;

            currentDirection = Direction.Forward;
            targetTile = grid;
            targetRotation = Vector3.up * (int)currentDirection;
        }

        private void Update()
        {
            if (!IsBusy) return;
            Move();
            Rotate();
        }


        private void FixedUpdate()
        {
            Vector3 direction = Vector3.forward;
            Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));

            if (Physics.Raycast(theRay, out RaycastHit hit, range))
            {
                if (hit.collider.tag == "StoneTile")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SoundEffects/Player/Footsteps/StoneFootsteps");
                }
                if (hit.collider.tag == "GrassTile")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SoundEffects/Player/Footsteps/GrassDirtFootsteps");
                }
                if (hit.collider.tag == "CarpetTile")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SoundEffects/Player/Footsteps/CarpetFootsteps");
                }
                if (hit.collider.tag == "WoodTile")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SoundEffects/Player/Footsteps/WoodFootsteps");
                }
                if (hit.collider.tag == "WaterTile")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SoundEffects/Player/Footsteps/WaterFootsteps");
                }
                if (hit.collider.tag == "SludgeTile")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SoundEffects/Player/Footsteps/SludgeFootsteps");
                }
                if (hit.collider.tag == "GravelTile")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SoundEffects/Player/Footsteps/GravelFootsteps");
                }
            }
        }
       
        private void Move() => transform.position = smoothTransition ?
            Vector3.MoveTowards(transform.position, targetTile.transform.position, Time.deltaTime * movementSpeed)
            : targetTile.transform.position;

        private void Rotate() => transform.rotation = smoothTransition ?
            Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationSpeed)
            : Quaternion.Euler(targetRotation);

        public void MoveTowards(Direction direction)
        {
            targetTile = IsBusy ? targetTile : targetTile.GetTile(localDirection(direction));
            transform.parent = targetTile.transform;
        }
        public void RotateTowards(Direction direction)
        {
            if (IsBusy) return;
            currentDirection = localDirection(direction);
            targetRotation = Vector3.up * (int)currentDirection;
        }

    }    
}
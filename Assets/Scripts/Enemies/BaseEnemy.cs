using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField]
        protected float _speed;

        protected Vector2 _minMaxZ = new Vector2(-10f, 40f);
        protected Vector2 _minMaxX = new Vector2(-33.5f, 33.5f);

        protected float _randomX;

        protected virtual void Update()
        {
            CalculateMovement();
        }

        protected virtual void CalculateMovement()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;

            if (transform.position.z < _minMaxZ.x)
            {
                _randomX = Random.Range(_minMaxX.x, _minMaxX.y);

                transform.position = new Vector3(_randomX, 0, _minMaxZ.y);
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var tag = other.tag;

            switch (tag)
            {
                case "Player":
                    Destroy(this.gameObject);
                    break;

                case "Harpoon":
                    other.gameObject.SetActive(false);
                    Destroy(this.gameObject);
                    break;

                default:
                    break;
            }
        }
    }
}


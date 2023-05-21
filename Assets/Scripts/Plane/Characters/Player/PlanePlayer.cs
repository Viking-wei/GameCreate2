using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

[RequireComponent(typeof(Rigidbody2D))]
public class PlanePlayer : Character
{
    [SerializeField] StatsBar_HUD startsBar_HUD;
    [SerializeField] bool regenerateHealth = true;
    [SerializeField] float healthRegenerateTime;
    [SerializeField,Range(0f,1f)] float healthRegeneratePercent;
    [Header("----INPUT----")]
    [SerializeField] PlayerInput_2D input;
    [Header("----MOVE----")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float accelerationTime = 3f;
    [SerializeField] float decelerationTime = 3f;
    [SerializeField] float moveRotationAngle = 50f;
    [SerializeField] float paddingX = 0.8f;
    [SerializeField] float paddingY = 0.22f;
    [Header("----FIRE----")]
    [SerializeField] GameObject projectile1;
    [SerializeField] GameObject projectile2;
    [SerializeField] GameObject projectile3;
    [SerializeField] Transform muzzleTop;
    [SerializeField] Transform muzzleMiddle;
    [SerializeField] Transform muzzleBottom;
    [SerializeField, Range(0, 2)] int weaponPower = 0;
    [SerializeField] float fireInterval=0.2f;
    WaitForSeconds waitForFireInterval;
    WaitForSeconds waitHealthRegenerateTime;

    new Rigidbody2D rigidbody;

    Coroutine moveCoroutine;
    Coroutine healthRegenerateCotourine;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onFire += Fire;
        input.onStopFire += StopFire;

    }

    private void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
        input.onFire -= Fire;
        input.onStopFire -= StopFire;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.gravityScale = 0f;
        waitForFireInterval = new WaitForSeconds(fireInterval);
        waitHealthRegenerateTime = new WaitForSeconds(healthRegenerateTime);
        startsBar_HUD.Initialize(health, maxHealth);
        input.EnableGameplayInput();
        
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        startsBar_HUD.UpdateStats(health,maxHealth);

        if(gameObject.activeSelf)
        {
            if(regenerateHealth)
            {
                if(healthRegenerateCotourine!=null)
                {
                    StopCoroutine(healthRegenerateCotourine);
                }
               healthRegenerateCotourine= StartCoroutine(HealthRegenerateCoroutine(waitHealthRegenerateTime, healthRegeneratePercent));
            }
        }
    }
    public override void RestoreHealth(float value)
    {
        base.RestoreHealth(value);
        startsBar_HUD.UpdateStats(health, maxHealth);
    }
    public override void Die()
    {
        startsBar_HUD.UpdateStats(0f, maxHealth);
        base.Die();
    }
    // Update is called once per frame
    #region MOVE
    void Move(Vector2 moveInput)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        
        moveCoroutine=StartCoroutine(MoveCoroutine(accelerationTime,moveInput.normalized * moveSpeed, 
        Quaternion.AngleAxis(moveRotationAngle * moveInput.y, Vector3.right)));
        StartCoroutine(MovePositionLimitCoroutine());
    }
    void StopMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine=StartCoroutine(MoveCoroutine(decelerationTime, Vector2.zero,Quaternion.identity));
        StopCoroutine(MovePositionLimitCoroutine());
    }

    IEnumerator MoveCoroutine(float time,Vector2 moveVelocity,Quaternion moveRotation)
    {
        float t = 0f;
        while(t<1f)
        {
            t += Time.fixedDeltaTime / time;
            rigidbody.velocity=Vector2.Lerp(rigidbody.velocity, moveVelocity,t);
            transform.rotation=Quaternion.Lerp(transform.rotation,moveRotation,t);

            yield return null;
        }
    }
    
    IEnumerator MovePositionLimitCoroutine()
    {
        while(true) 
        {
            transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position,paddingX,paddingY);
            yield return null;
        }
    }
    #endregion
    #region FIRE
    void Fire()
    {
        StartCoroutine(nameof(FireCoroutine));
    }

    void StopFire()
    {
        StopCoroutine(nameof(FireCoroutine));
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            
            switch (weaponPower)
            {
                case 0:
                    PoolManager.Release(projectile1,muzzleMiddle.position);
                    break;
                case 1:
                    PoolManager.Release(projectile1, muzzleTop.position);
                    PoolManager.Release(projectile1, muzzleBottom.position);
                    break;
                case 2:
                    PoolManager.Release(projectile1, muzzleMiddle.position);
                    PoolManager.Release(projectile2, muzzleTop.position);
                    PoolManager.Release(projectile3, muzzleBottom.position);
                    
                    break;
                default:
                    break;

            }
            yield return waitForFireInterval;
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀을 사용하는 오브젝트의 종류
/// </summary>
public enum PoolObjectType
{
    PlayerBullet = 0,   // 플레이어의 총알
    HitEffect,          // 총알이 터지는 이펙트
    ExplosionEffect,    // 적이 터지는 이펙트
    Enemy,              // 적
    Asteroid
}

public class Factory : Singleton<Factory>
{
    // 오브젝트 풀들
    BulletPool bullet;
    HitEffectPool hit;
    ExplosionEffectPool explosion;
    EnemyPool enemy;
    AsteroidPool asteroid;

    /// <summary>
    /// 씬이 로딩 완료될 때마다 실행되는 초기화 함수
    /// </summary>
    protected override void OnInitialize()
    {
        base.OnInitialize();

        // GetComponentInChildren : 나와 내 자식 오브젝트에서 컴포넌트 찾음

        // 풀 컴포넌트 찾고, 찾으면 초기화하기
        bullet = GetComponentInChildren<BulletPool>();  
        if (bullet != null)
            bullet.Initialize();

        hit = GetComponentInChildren<HitEffectPool>();
        if ( hit != null )
            hit.Initialize();
        
        explosion = GetComponentInChildren<ExplosionEffectPool>();
        if(explosion != null )
            explosion.Initialize();
        
        enemy = GetComponentInChildren<EnemyPool>();
        if(enemy != null)
            enemy.Initialize();

        asteroid = GetComponentInChildren<AsteroidPool>();
        if (asteroid != null) asteroid.Initialize();
    }

    /// <summary>
    /// 풀에 있는 게임 오브젝트 하나 가져오기
    /// </summary>
    /// <param name="type">가져올 오브젝트의 종류</param>
    /// <returns>활성화된 오브젝트</returns>
    public GameObject GetObject(PoolObjectType type)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.PlayerBullet:
                result = bullet.GetObject().gameObject;
                break;
            case PoolObjectType.HitEffect:
                result = hit.GetObject().gameObject;
                break;
            case PoolObjectType.ExplosionEffect:
                result = explosion.GetObject().gameObject;
                break;
            case PoolObjectType.Enemy:
                result = enemy.GetObject().gameObject;
                break;
            case PoolObjectType.Asteroid:
                result = asteroid.GetObject().gameObject;
                break;
        }

        return result;
    }

    /// <summary>
    /// 풀에 있는 게임 오브젝트 하나 가져와서 특정 위치에 배치하기
    /// </summary>
    /// <param name="type">가져올 오브젝트의 종류</param>
    /// <param name="position">오브젝트가 배치될 위치</param>
    /// <returns>활성화된 오브젝트</returns>
    public GameObject GetObject(PoolObjectType type, Vector3 position) 
    {
        GameObject obj = GetObject(type);       // 가져와서 
        obj.transform.position = position;      // 위치 적용

        // 개별적으로 추가 처리가 필요한 오브젝트들
        switch (type)    
        {
            case PoolObjectType.Enemy:
                Enemy enemy = obj.GetComponent<Enemy>();
                enemy.SetStartPosition(position);   // 적의 spawnY 지정
                break;
        }

        return obj;
    }

    /// <summary>
    /// 총알 하나 가져오는 함수
    /// </summary>
    /// <returns>활성화된 총알</returns>
    public Bullet GetBullet()
    {
        return bullet.GetObject();
    }

    /// <summary>
    /// 총알 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position">배치될 위치</param>
    /// <returns>활성화된 총알</returns>
    public Bullet GetBullet(Vector3 position)
    {
        Bullet bulletComp = bullet.GetObject();
        bulletComp.transform.position = position; 
        return bulletComp;
    }

    public Explosion GetHitEffect()
    {
        return hit.GetObject();
    }

    public Explosion GetHitEffect(Vector3 position)
    {
        Explosion comp = hit.GetObject();
        comp.transform.position = position;
        return comp;
    }

    public Explosion GetExplosionEffect()
    {
        return explosion.GetObject();
    }

    public Explosion GetExplosionEffect(Vector3 position)
    {
        Explosion comp = explosion.GetObject();
        comp.transform.position = position;
        return comp;
    }

    public Enemy GetEnemy()
    {
        return enemy.GetObject();
    }

    public Enemy GetEnemy(Vector3 position)
    {
        Enemy comp = enemy.GetObject();
        comp.SetStartPosition(position);    // spawnY 지정하기 위한 용도
        return comp;
    }

    public Asteroid GetAsteroid()
    {
        return asteroid.GetObject();
    }

    public Asteroid GetAsteroid(Vector3 position)
    {
        Asteroid comp = asteroid.GetObject();
        comp.transform.position = position;
        return comp;
    }

}

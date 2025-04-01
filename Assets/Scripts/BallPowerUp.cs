using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class BallPowerUp : MonoBehaviour
{
    private PowerUp power;
    public bool powered { get; set; } = false;

    [SerializeField] private CircleCollider2D trigger;
    [SerializeField] private float MultiballExplodeForce = 4f;
    [SerializeField] private float fireBallExpansion = 0.2f;

    private void Start()
    {
        trigger.enabled = false;
    }

    public void Activate()
    {
        if (powered)
        {
            switch(PowerUpVaraible.powers[perks.origin])
            {
                case 0: //Multiball
                    if (PowerUpVaraible.powers[perks.exploding] != 0) power = new ExplodingBall(this, MultiballExplodeForce);
                    else power = new MultiBall(this);
                    break;
                case 1: //Fireball
                    power = new FireBall(this);
                    gameObject.layer = default;
                    trigger.enabled = true;
                    transform.localScale *= 1f + (fireBallExpansion * PowerUpVaraible.powers[perks.fireballSize]);
                    break;
            }
        }
    }

    private bool isPowered()
    {
        return power != null && powered;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPowered()) power.OnCollide();
    }

    /// <summary>
    /// Method <c>TryDestroy</c> trys to destroy ball and returns true if destroyed
    /// </summary>
    /// <returns>true on successful destroy</returns>
    public bool TryDestroy()
    {
        if (isPowered()) return power.TryDestroy();
        else
        {
            Destroy(gameObject);
            return true;
        }
    }
}

abstract class PowerUp
{
    protected Dictionary<perks, int> currentPower = PowerUpVaraible.powers;
    protected BallPowerUp ball;
    public PowerUp(BallPowerUp b)
    {
        currentPower = PowerUpVaraible.powers;
        ball = b;
    }

    public abstract void OnCollide();
    public abstract bool TryDestroy();
}
class MultiBall : PowerUp
{
    protected int spookies;
    public MultiBall (BallPowerUp b) : base(b) 
    {
        spookies = currentPower[perks.spooky];
    }

    override
    public void OnCollide() { }
    override
    public bool TryDestroy() { 
        if (spookies > 0)
        {
            //place at top of screen
            spookies--;
            return false;
        } else
        {
            GameObject.Destroy(ball.gameObject);
            return true;
        }
    }
}
class ExplodingBall : MultiBall
{
    private float explodeForce = 1;
    private bool exploded = false;

    public ExplodingBall(BallPowerUp b, float eForce) : base(b) 
    {
        explodeForce = eForce;
        exploded = false;
    }

    override
    public void OnCollide()
    {
        exploded = true;
        if (!exploded)
        {
            for (int i = 0; i < currentPower[perks.exploding] * (currentPower[perks.multiCount] + 1); i++)
            {
                GameObject newBall = GameObject.Instantiate(ball.gameObject, ball.transform.position, ball.transform.rotation);
                GameManager.game.ballActive += 1;

                Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    //apply force
                    rb.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * explodeForce, ForceMode2D.Impulse);
                }
            }
            GameObject.Destroy(ball.gameObject);
            GameManager.game.ballActive -= 1;
        }
    }
}
class FireBall : PowerUp
{
    private float wispTime;
    private float wispInterval;
    public FireBall(BallPowerUp b) : base(b) 
    {
        wispTime = currentPower[perks.wispDur];
        wispInterval = currentPower[perks.wispInterval];
        if (currentPower[perks.fireWisp] != 0) ball.StartCoroutine(wispGenerator());
    }

    override
    public void OnCollide() { }
    override
    public bool TryDestroy()
    { 
        GameObject.Destroy(ball.gameObject); 
        return true; 
    }

    IEnumerator wispGenerator ()
    {
        yield return new WaitForSeconds(wispInterval);
        //generate wisp
    }
}
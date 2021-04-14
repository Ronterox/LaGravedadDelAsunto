namespace Combat
{
    public class EnemyHealth : CharacterHealth
    {
        public override void Die()
        {
            base.Die();

            Destroy(gameObject);
        }
    }
}

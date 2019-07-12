
namespace SYHX.AbnormalStatus
{
    public class PowerUpSource : AbnormalStatusSource<PowerUp> { }
    [System.Serializable]
    public class PowerUp : AbnormalStatusContent
    {
        [CloneField] public float attackRate;
        [CustomDesc("damageRate")] public string dAttackRate { get => (attackRate * 100).ToString() + "%"; }
        [CustomDesc("turn")] public string dTurn { get => count.ToString(); }
        public override void OnGenerate(params object[] args)
        {
            owner.attackRate += attackRate;
        }
        public override void OnClear()
        {
            owner.attackRate -= attackRate;
        }
        public override void OnRemove()
        {
            owner.attackRate -= attackRate;
        }
    }
}

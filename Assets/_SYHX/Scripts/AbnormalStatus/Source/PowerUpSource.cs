
namespace SYHX.AbnormalStatus
{
    public class PowerUpSource : AbnormalStatusSource<PowerUp>
    {
    }
    [System.Serializable]
    public class PowerUp : AbnormalStatusContent
    {
        [CloneField] public float attackRate;
        [CustomDesc("damageRate")]public string dAttackRate { get => (attackRate*100).ToString() + "%";}
        [CustomDesc("turn")]public string dTurn{get=>count.ToString();}
        public override void OnGenerate()
        {
            owner.ChangeAttackRate(attackRate);
        }
        public override void OnClear()
        {
            owner.ChangeAttackRate(-attackRate);
        }
        public override void OnRemove()
        {
            owner.ChangeAttackRate(-attackRate);
        }
    }
}

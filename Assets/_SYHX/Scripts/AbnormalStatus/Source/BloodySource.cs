
namespace SYHX.AbnormalStatus
{
    public class BloodySource : AbnormalStatusSource<Bloody> { }
    [System.Serializable]
    public class Bloody : AbnormalStatusContent
    {
        [CloneField] public float damageRate;
        [CustomDesc("damageRate")] public string dReceiveRate { get => ((int)(attack * damageRate)).ToString(); }
        [CustomDesc("turn")] public string dTurn { get => count.ToString(); }

        private int attack;
        public override void OnGenerate(params object[] args)
        {
            attack = (int)args[0];
            AddEndAction(ReduceHP);
        }
        public override void OnClear()
        {
            RemoveEndAction(ReduceHP);
        }
        public override void OnRemove()
        {
            RemoveEndAction(ReduceHP);
        }

        private void ReduceHP()
        {
            owner.DecreaseHp((int)(attack * damageRate));
            this.Decrease(1);
        }
    }
}

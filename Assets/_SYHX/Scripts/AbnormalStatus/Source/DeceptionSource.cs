
namespace SYHX.AbnormalStatus
{
    public class DeceptionSource : AbnormalStatusSource<Deception> { }
    [System.Serializable]
    public class Deception : AbnormalStatusContent
    {
        [CloneField] public float increaseRate;
        [CustomDesc("increaseRate")] public string dReceiveRate { get => (increaseRate * 100).ToString() + "%"; }
        public override void OnGenerate(params object[] args)
        {
            BattleManager.Hero.onAddAStatus += IncreaseATK;
        }

        private void IncreaseATK()
        {
            owner.optionAttack += (int)(owner.attack * increaseRate);
        }

        public override void Release()
        {
            base.Release();
            BattleManager.Hero.onAddAStatus -= IncreaseATK;
        }
    }
}

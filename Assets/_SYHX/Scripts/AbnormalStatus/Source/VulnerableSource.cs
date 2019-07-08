
namespace SYHX.AbnormalStatus
{
    public class VulnerableSource : AbnormalStatusSource<Vulnerable>{}
    [System.Serializable]
    public class Vulnerable : AbnormalStatusContent
    {
        [CloneField] public float receiveRate;
        [CustomDesc("receiveRate")]public string dReceiveRate { get => (receiveRate*100).ToString() + "%";}
        [CustomDesc("turn")]public string dTurn{get=>count.ToString();}
        public override void OnGenerate()
        {
            owner.receiveRate += receiveRate;
        }
        public override void OnClear()
        {
            owner.attackRate -= receiveRate;
        }
        public override void OnRemove()
        {
            owner.attackRate -= receiveRate;
        }
    }
}

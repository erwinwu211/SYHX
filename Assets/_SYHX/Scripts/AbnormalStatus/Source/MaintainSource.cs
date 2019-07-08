
namespace SYHX.AbnormalStatus
{
    public class MaintainSource : AbnormalStatusSource<Maintain>{}
    [System.Serializable]
    public class Maintain : AbnormalStatusContent
    {
        private ConnectionSignal signal;
        [CustomDesc("turn")]public string dTurn{get=>count.ToString();}
        public override void OnGenerate()
        {
            signal = BattleManager.Ins.signals.GetSignal<ConnectionSignal>("connection");
            ChangeReset(1);
        }
        public override void OnClear()
        {
            ChangeReset(-1);
        }
        public override void OnRemove()
        {
            ChangeReset(-1);
        }

        private void ChangeReset(int count)
        {
            if(signal != null)
            {
                signal.stopReset += count;
            }
        }
    }
}

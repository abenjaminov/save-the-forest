namespace _Scripts.State
{
    namespace State
    {
        public interface IState
        {
            void Tick();
            void OnEnter();
            void OnExit();
        }
    }
}
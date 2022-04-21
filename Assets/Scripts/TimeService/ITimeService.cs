namespace TimeService
{
    public interface ITimeService
    {
        void SlowDownTo(float scale);

        void ReturnToDefault(float time);
    }
}
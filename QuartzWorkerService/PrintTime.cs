namespace QuartzWorkerService
{
    internal class PrintTime : IPrintTime
    {
        public string GetCurrentTime()
        {
            return DateTime.Now.ToString();
        }
    }
}

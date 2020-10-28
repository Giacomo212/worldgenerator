namespace Game.Runners{
    public interface ICheckableProcess{
        
        //returns values form 0 to 100
        public int CheckPercentDone();
        public void Run();
    }
}
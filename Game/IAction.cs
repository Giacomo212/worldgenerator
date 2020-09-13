namespace Game{
    public interface IAction{
        public Context ReturnNewContext();
    }
    public class ChangeToMainUi : IAction{
        public Context ReturnNewContext(){
            return new MainUiContext();
        }
    }
}


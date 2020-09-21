namespace Game{
    public interface IChangeContext{
        public Context ReturnNewContext();
    }
    public class ChangeToMainUi : IChangeContext{
        public Context ReturnNewContext(){
            return new MainUiContext();
        }
    }
}


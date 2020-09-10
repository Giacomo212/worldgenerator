namespace Game{
    public class ChangeToMainUi : IAction{
        public Context ReturnNewContext(){
            return new MainUiContext();
        }
    }
}
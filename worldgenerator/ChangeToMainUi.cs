namespace worldgenerator{
    public class ChangeToMainUi : IAction{
        public Context ReturnNewContext(){
            return new MainUiContext();
        }
    }
}
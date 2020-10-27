using Types;

namespace Game.WorldMap{
    public class MapImageGenerationRunner : ICheckableProcess{
        
        private readonly ImageGenerator _imageGenerator;
        public MapImageGenerationRunner(Map map){
            _imageGenerator = new ImageGenerator(map);
        }
        public int CheckPercentDone(){
            return _imageGenerator.PercentDone;
        }

        public void Run(){
            _imageGenerator.GenerateImage();
        }
    }
}
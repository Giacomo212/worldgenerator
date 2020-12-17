// using System.Collections.Generic;
// using WorldGenerator.MapElements;
//
// namespace WorldGenerator.Utils{
//     public class Grid<T>{
//         private List<List<T>> _columns;
//
//         public Grid(Position capacity){
//             _columns = new List<List<T>>(capacity.X);
//             for (int i = 0; i < capacity.X; i++){
//                 _columns[i] = new List<T>(_columns.Capacity);
//             }
//         }
//
//         public void MoveLeft(Chunk chunk){
//             for (int i = 0; i < _columns.Count; i++){
//                 _columns[i].RemoveAt(0);
//                 _columns[i].Insert(0,chunk);
//             }
//         }
//
//         public void RemoveLastRow(){
//             
//         }
//
//         public void RemoveFirstColumn(){
//             
//         }
//
//         public void RemoveLastColumn(){
//             
//         }
//
//     }
// }
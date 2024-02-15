using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Api.Model {
    public class Game {
        [NotMapped]
        public const int BoardSize = 8;
        
        [NotMapped]
        public char[,] Board { 
            get { return StrToBoard(_boardString); }
            set { _boardString = BoardToStr(value);}
        }

        private string _boardString { get; set; } = "[        ],[        ],[        ],[        ],[        ],[        ],[        ],[        ]";
        
        public bool? Turn { get; set; }
        
        private static string BoardToStr(char[,] board) {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < BoardSize; i++) {
                sb.Append("[");
                for (int j = 0; j < BoardSize; j++)
                    sb.Append(board[i, j]);
                sb.Append("]");

                if (i < BoardSize - 1)
                    sb.Append(",");
            }

            return sb.ToString();
        }

        private static char[,] StrToBoard(string boardStr) {
            string[] rowStrings = boardStr.Split(',');

            char[,] board = new char[BoardSize, BoardSize];

            for (int i = 0; i < BoardSize; i++){
                string row = rowStrings[i].Trim('[', ']');
                  
                for (int j = 0; j < row.Length; j++)
                    board[i, j] = row[j];
            }

            return board;
        }
    }
}

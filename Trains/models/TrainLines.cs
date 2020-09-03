namespace Trains.models
{
    public static class TrainLines
    {
        public const int TriageLineNumber = -1;
        public static string GetTrainLineWagons(string trainLine)
        {
            return trainLine.Replace("0", "");
        }
        
        public static string[] ApplyMove(string[] trainLines, Move move)
        {
            trainLines[move.From] = RemoveWagonsFromTrainLine(trainLines[move.From], move.Wagons);
            if (move.To != TriageLineNumber)
            {
                trainLines[move.To] = AddWagonsToTrainLine(trainLines[move.To], move.Wagons);
            }

            return trainLines;
        }
        
        public static string RemoveWagonsFromTrainLine(string trainLine, string wagonsToRemove)
        {
            return trainLine; // TODO
        }

        public static string AddWagonsToTrainLine(string trainLine, string wagonsToAdd)
        {
            return trainLine; // TODO
        }
    }
}
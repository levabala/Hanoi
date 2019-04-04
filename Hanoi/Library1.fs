namespace HanoiModel

module private Model =
  let move (f: int) (t: int) (moves: List<int>) : List<int> =
    moves @ [(f * 10 + t)]  

  let rec hanoi n orig dest tmp (moves: List<int>): List<int> = 
    if n = 0 then 
      moves
    else
      hanoi (n - 1) orig tmp dest moves 
      |> move orig dest    
      |> hanoi (n - 1) tmp dest orig  

module public Hanoi =
  let doHanoi count = Model.hanoi count 0 1 2 []

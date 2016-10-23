#pragma strict

var gameModel : GameModel;

function Start () {
gameModel.grid = [
[new Cell(true,false,false,false),new Cell(false,false,false,false),new Cell(false,false,false,false)],
[new Cell(false,false,false,false),new Cell(false,false,false,false),new Cell(false,false,false,false)],
[new Cell(false,false,false,false),new Cell(false,false,false,false),new Cell(false,false,false,false)]
];
gameModel.playerPosition = new Position(0,0);
}

function Update () {
//Debug.Log("ww");
}
#pragma strict
var speed : float = 1;
var direction : Vector3;
function Start () {

}

function Update () 
{
	transform.Rotate(direction * speed * Time.deltaTime);

}
 var dir:Vector3;
var style:GUIStyle;
function Awake()
{
style=new GUIStyle();
style.alignment=TextAnchor.MiddleCenter;
style.normal.textColor=Color.red;
}
function Update()
{
dir=Input.acceleration;
}
function OnGUI()
{
GUI.Label(Rect(Screen.width/2-100,Screen.height/2-100,200,200),dir+"",style);
}
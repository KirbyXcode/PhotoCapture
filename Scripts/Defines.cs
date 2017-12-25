using UnityEngine;

#region Global delegate 委托
public delegate void OnTouchEventHandle(GameObject _listener, object _args, object _param);
#endregion

public enum EnumTouchEventType
{
	OnClick,
	OnDoubleClick,
	OnDown,
	OnUp,
	OnEnter,
	OnExit,
	OnSelect,  
	OnUpdateSelect,  
	OnDeSelect, 
	OnDrag, 
	OnDragEnd,
	OnDrop,
	OnScroll, 
	OnMove,
}

public enum ReturnCode
{
    None,
    Success,
    Failure
}

public enum Background
{
    None,
    NewLook,
    Riverside,
    Surface,
    MorningSun,
    Buildings,
    Bridge,
    River,
    Happyness
}
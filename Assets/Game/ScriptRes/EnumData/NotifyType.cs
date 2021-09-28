public enum NotifyType
{
    [EnumName("无事件")]
    Msg_None=0,
    [EnumName("Progress观察者")]
    Msg_ProgressObserve=1,
    [EnumName("摄像机")]
    Msg_Camera=2,
    [EnumName("触发器消息")]
    Msg_ItemTriggerEvent=3,
    [EnumName("UI消息")]
    Msg_GameUiCmd=4,
    [EnumName("显示物体")]
    Msg_AcitveCmd_Show=5,
    [EnumName("隐藏物体")]
    Msg_AcitveCmd_Hide=6,
    [EnumName("替换物体")]
    Msg_GameObject_Replace=7,
    [EnumName("游戏指令")]
    Msg_GameCmd=8,
    [EnumName("播放角色动画")]
    Msg_PlayerAni=9,
    [EnumName("摄像机控制器")]
    Msg_CameraController=10,
    [EnumName("资源加载")]
    Msg_Resouce_Load=11,
}

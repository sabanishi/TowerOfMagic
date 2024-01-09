using System;
public class FloorChip
{
    public FloorEnum FloorEnum { get; set; }
    public int RoomNum { get; set; }
    public int X, Y;
    public bool IsOnMap;
    public bool IsMonsterHouse { get; set; }

    public FloorChip(FloorEnum floorEnum,int roomNum,int x,int y)
    {
        FloorEnum = floorEnum;
        RoomNum = roomNum;
        X = x;
        Y = y;
        IsOnMap = false;
        IsMonsterHouse = false;
    }
}

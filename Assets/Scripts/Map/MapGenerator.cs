using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    private const int MINIMUM_RANGE_WIDTH = 6;

    private int mapSizeX;
    private int mapSizeY;
    private int maxRoom;

    private FloorChip[,] map;
    private List<Range> roomList = new List<Range>();
    private List<Range> rangeList = new List<Range>();
    private List<Range> passList = new List<Range>();
    private List<Range> roomPassList = new List<Range>();
    private List<FloorChip> destinations = new List<FloorChip>();

    public List<FloorChip>[] Rooms;

    public FloorChip[,] GenerateMap(int mapSizeX, int mapSizeY, int maxRoom)
    {
        this.mapSizeX = mapSizeX;
        this.mapSizeY = mapSizeY;
        map = new FloorChip[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                map[x, y] = new FloorChip(FloorEnum.Wall, -1, x, y);
            }
        }

        CreateRange(maxRoom);
        CreateRoom();

        foreach (Range pass in passList)
        {
            for (int x = pass.Start.X; x <= pass.End.X; x++)
            {
                for (int y = pass.Start.Y; y <= pass.End.Y; y++)
                {
                    map[x, y].FloorEnum = FloorEnum.AISLE;
                }
            }
        }
        foreach (Range roomPass in roomPassList)
        {
            for (int x = roomPass.Start.X; x <= roomPass.End.X; x++)
            {
                for (int y = roomPass.Start.Y; y <= roomPass.End.Y; y++)
                {
                    map[x, y].FloorEnum = FloorEnum.AISLE;
                }
            }
        }

        Rooms = new List<FloorChip>[roomList.Count];
        bool isCreateMonsterHouse=false;
        float rate = 0;
        if (GameManager.instance.GetLevel() >= 20)
        {
            rate = 0.15f;
        }
        else if(GameManager.instance.GetLevel()>=10)
        {
            rate = 0.1f;
        }
        else
        {
            rate = 0.05f;
        }
        for (int i=0;i<roomList.Count;i++)
        {
            Range room = roomList[i];
            List<FloorChip> chipList = new List<FloorChip>();
            if (!isCreateMonsterHouse && Utils.RandomJadge(rate))
            {
                //モンスターハウス作成
                isCreateMonsterHouse = true;
                for (int x = room.Start.X; x <= room.End.X; x++)
                {
                    for (int y = room.Start.Y; y <= room.End.Y; y++)
                    {
                        map[x, y].FloorEnum = FloorEnum.ROOM;
                        map[x, y].RoomNum = i;
                        chipList.Add(map[x, y]);
                        map[x, y].IsMonsterHouse = true;
                    }
                }
            }
            else
            {
                for (int x = room.Start.X; x <= room.End.X; x++)
                {
                    for (int y = room.Start.Y; y <= room.End.Y; y++)
                    {
                        map[x, y].FloorEnum = FloorEnum.ROOM;
                        map[x, y].RoomNum = i;
                        chipList.Add(map[x, y]);
                    }
                }
            }
            Rooms[i] = chipList;
        }

        TrimPassList(ref map);

        foreach (var chip in destinations)
        {
            if (chip.FloorEnum == FloorEnum.AISLE)
            {
                map[chip.X, chip.Y].FloorEnum = FloorEnum.ENTRANCE;
                Rooms[map[chip.X, chip.Y].RoomNum].Add(chip);
            }
        }

        
        return map;
    }

    private void CreateRange(int maxRoom)
    {
        rangeList.Add(new Range(0, 0, mapSizeX - 1, mapSizeY - 1));

        bool isDevided;
        do
        {
            isDevided = DevideRange(false);
            isDevided = DevideRange(true) || isDevided;
            if (rangeList.Count >= maxRoom)
            {
                break;
            }
        } while (isDevided);
    }

    private bool DevideRange(bool isVertical)
    {
        bool isDevided = false;

        //区画ごとに切るかどうか判定する
        List<Range> newRangeList = new List<Range>();
        foreach (Range range in rangeList)
        {
            if (isVertical && range.GetWidthY() < MINIMUM_RANGE_WIDTH * 2 + 1)
            {
                continue;
            }
            else if (!isVertical && range.GetWidthX() < MINIMUM_RANGE_WIDTH * 2 + 1)
            {
                continue;
            }
            //40%の可能性で分割しない、ただし区画の数が一つの時は必ず分割する
            if(rangeList.Count > 1 && Utils.RandomJadge(0.4f))
            {
                continue;
            }

            int length = isVertical ? range.GetWidthY() : range.GetWidthX();//isVetrical（垂直）ならY、水平ならX
            int margin = length - MINIMUM_RANGE_WIDTH * 2;
            int baseIndex = isVertical ? range.Start.Y : range.Start.X;
            int devideIndex = baseIndex + MINIMUM_RANGE_WIDTH + Utils.GetRandomInt(1, margin) - 1;

            //分割された区画の大きさを変更し、新しい区画を追加リストに追加する
            //同時に分割した境界を通路として保存しておく
            Range newRange = new Range();
            if (isVertical)
            {
                passList.Add(new Range(range.Start.X, devideIndex, range.End.X, devideIndex));
                newRange = new Range(range.Start.X, devideIndex + 1, range.End.X, range.End.Y);
                range.End.Y = devideIndex - 1;
            }
            else
            {
                passList.Add(new Range(devideIndex, range.Start.Y, devideIndex, range.End.Y));
                newRange = new Range(devideIndex + 1, range.Start.Y, range.End.X, range.End.Y);
                range.End.X = devideIndex - 1;
            }
            newRangeList.Add(newRange);
            isDevided = true;
        }

        rangeList.AddRange(newRangeList);

        return isDevided;
    }

    private void CreateRoom()
    {
        //rangeListのシャッフル
        rangeList.Sort((a, b) => Utils.GetRandomInt(0, 1) - 1);

        //1区画あたり一部屋を作る
        int i = 0;
        foreach (Range range in rangeList)
        {
            //猶予を計算
            int marginX = range.GetWidthX() - MINIMUM_RANGE_WIDTH + 1;
            int marginY = range.GetWidthY() - MINIMUM_RANGE_WIDTH + 1;

            //開始位置を決定
            int randomX = Utils.GetRandomInt(1, marginX);
            int randomY = Utils.GetRandomInt(1, marginY);

            //座標を算出
            int startX = range.Start.X + randomX;
            int endX = range.End.X - Utils.GetRandomInt(0, (marginX - randomX)) - 1;
            int startY = range.Start.Y + randomY;
            int endY = range.End.Y - Utils.GetRandomInt(0, (marginY - randomY)) - 1;

            //部屋リストへ追加
            Range room = new Range(startX, startY, endX, endY);
            roomList.Add(room);

            //通路を作る
            CreatePass(range, room,i);
            i++;
        }
    }

    private void CreatePass(Range range, Range room,int i)
    {
        List<int> directionList = new List<int>();
        //xマイナス
        if (range.Start.X != 0)
        {
            directionList.Add(0);
        }
        //xプラス
        if (range.End.X != mapSizeX - 1)
        {
            directionList.Add(1);
        }
        //yマイナス
        if (range.Start.Y != 0)
        {
            directionList.Add(2);
        }
        //yプラス
        if (range.End.Y != mapSizeY - 1)
        {
            directionList.Add(3);
        }

        //directionListのシャッフル
        directionList.Sort((a, b) => Utils.GetRandomInt(0, 1) - 1);

        //bool isFirst = true;
        foreach (int direction in directionList)
        {
            //向きの判定
            int random;
            switch (direction)
            {
                case 0://xマイナス
                    random = room.Start.Y + Utils.GetRandomInt(1, room.GetWidthY() - 1);
                    map[room.Start.X - 1, random].RoomNum = i;
                    destinations.Add(map[room.Start.X-1,random]);
                    roomPassList.Add(new Range(range.Start.X, random, room.Start.X - 1, random));
                    break;
                case 1://xプラス
                    random = room.Start.Y + Utils.GetRandomInt(1, room.GetWidthY() - 1);
                    map[room.End.X + 1, random].RoomNum = i;
                    destinations.Add(map[room.End.X+1, random]);
                    roomPassList.Add(new Range(room.End.X + 1, random, range.End.X, random));
                    break;
                case 2://yマイナス
                    random = room.Start.X + Utils.GetRandomInt(1, room.GetWidthX() - 1);
                    map[random, room.Start.Y - 1].RoomNum = i;
                    destinations.Add(map[random, room.Start.Y-1]);
                    roomPassList.Add(new Range(random, range.Start.Y, random, room.Start.Y - 1));
                    break;
                case 3://yプラス
                    random = room.Start.X + Utils.GetRandomInt(1, room.GetWidthX() - 1);
                    map[random, room.End.Y + 1].RoomNum = i;
                    destinations.Add(map[random, room.End.Y+1]);
                    roomPassList.Add(new Range(random, room.End.Y + 1, random, range.End.Y));
                    break;
            }
        }
    }

    private void TrimPassList(ref FloorChip[,] map)
    {
        //どの通路とも接続されていない通路の削除
        for (int i = passList.Count - 1; i >= 0; i--)
        {
            Range pass = passList[i];
            bool isVertical = pass.GetWidthY() > 1;

            //通路が部屋通路から接続されているか判定
            bool isTrimTarget = true;
            if (isVertical)
            {
                int x = pass.Start.X;
                for (int y = pass.Start.Y; y <= pass.End.Y; y++)
                {
                    if (!(map[x - 1, y].FloorEnum == FloorEnum.Wall&&map[x + 1, y].FloorEnum==FloorEnum.Wall))
                    {
                        isTrimTarget = false;
                        break;
                    }
                }
            }
            else
            {
                int y = pass.Start.Y;
                for (int x = pass.Start.X; x <= pass.End.X; x++)
                {
                    if (!(map[x, y - 1].FloorEnum==FloorEnum.Wall&& map[x, y + 1].FloorEnum == FloorEnum.Wall))
                    {
                        isTrimTarget = false;
                        break;
                    }
                }
            }

            //削除対象になった通路の削除
            if (isTrimTarget)
            {
                passList.Remove(pass);
                if (isVertical)
                {
                    int x = pass.Start.X;
                    for (int y = pass.Start.Y; y <= pass.End.Y; y++)
                    {
                        map[x, y].FloorEnum = FloorEnum.Wall;
                    }
                }
                else
                {
                    int y = pass.Start.Y;
                    for (int x = pass.Start.X; x <= pass.End.X; x++)
                    {
                        map[x, y].FloorEnum = FloorEnum.Wall;
                    }
                }
            }
        }

        //外周に接している通路を他の通路との接続点まで削除する
        for (int x = 0; x < mapSizeX - 1; x++)//上下
        {
            if (map[x, 0].FloorEnum !=FloorEnum.Wall)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    if (map[x - 1, y].FloorEnum !=FloorEnum.Wall || map[x + 1, y].FloorEnum != FloorEnum.Wall)
                    {
                        break;
                    }
                    map[x, y].FloorEnum = FloorEnum.Wall ;
                }
            }
            if (map[x, mapSizeY - 1].FloorEnum !=FloorEnum.Wall)
            {
                for (int y = mapSizeY - 1; y >= 0; y--)
                {
                    if (map[x - 1, y].FloorEnum != FloorEnum.Wall || map[x + 1, y].FloorEnum != FloorEnum.Wall)
                    {
                        break;
                    }
                    map[x, y].FloorEnum =FloorEnum.Wall ;
                }
            }
        }

        for (int y = 0; y < mapSizeY - 1; y++)
        {
            if (map[0, y].FloorEnum != FloorEnum.Wall)
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    if (map[x, y - 1].FloorEnum != FloorEnum.Wall || map[x, y + 1].FloorEnum != FloorEnum.Wall)
                    {
                        break;
                    }
                    map[x, y].FloorEnum =FloorEnum.Wall ;
                }
            }
            if (map[mapSizeX - 1, y].FloorEnum != FloorEnum.Wall)
            {
                for (int x = mapSizeX - 1; x >= 0; x--)
                {
                    if (map[x, y - 1].FloorEnum != FloorEnum.Wall || map[x, y + 1].FloorEnum !=FloorEnum.Wall)
                    {
                        break;
                    }
                    map[x, y].FloorEnum =FloorEnum.Wall ;
                }
            }
        }
    }

}


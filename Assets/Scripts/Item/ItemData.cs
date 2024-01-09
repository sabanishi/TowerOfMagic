using System;
public class ItemData
{
    public ItemEnum ID { get; private set; }
    public string Name { get; private set; }
    public string ExplainText { get; private set; }

    public ItemData(ItemEnum id,string name,string explainData)
    {
        ID = id;
        ExplainText = explainData;
        Name = name;
    }
}

using UnityEngine;

public class App : Process
{
    public class Data
    {
        public int appID;
        public string companyName;
        public string name;

        public GameObject contents;

        public Data(int appID, string companyName, string name)
        {
            this.appID = appID;
            this.companyName = companyName;
            this.name = name;
            this.contents = contents;
        }

        public Data(Data data)
        {
            appID = data.appID;
            companyName = data.companyName;
            name = data.name;
        }
    }

    private Data data;

    public Data GenericData
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
        }
    }

    // æ€ø° ¥Î«— ¡¯¿‘¡° ±∏«ˆ
    protected override void Main()
    {
        base.Main();

        print($"{data.appID}:{data.companyName}.{data.name}");
    }

    protected override void Release()
    {
        base.Release();

        print($"{data.appID}:{data.companyName}.{data.name} æ€ ¡æ∑· µ .");
    }
}

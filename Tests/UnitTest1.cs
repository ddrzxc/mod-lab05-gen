namespace generator;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        BigrGenerator bigr = new BigrGenerator();
        bigr.Load("../../../test1.txt");
        string sym = bigr.GetSym();
        string[] syms = {"аа", "аб", "ав", "аг", "ад", "ае"};
        Assert.IsTrue(syms.Contains(sym));
    }
    [TestMethod]
    public void TestMethod2()
    {
        BigrGenerator bigr = new BigrGenerator();
        Dictionary<string, int> map = new Dictionary<string, int>
        {
            {"aa", 10},
            {"bb", 20},
            {"cc", 30}
        };
        bigr.Load(map);
        string sym = bigr.GetSym();
        Assert.IsTrue(sym == "aa" | sym == "bb" | sym == "cc");
    }
    [TestMethod]
    public void TestMethod3()
    {
        BigrGenerator bigr = new BigrGenerator();
        bigr.Load("../../../test1.txt");
        string text = bigr.GetString(10);
        Assert.IsTrue(text.Length == 20);
    }
    [TestMethod]
    public void TestMethod4()
    {
        WordGenerator word = new WordGenerator();
        word.Load("../../../test2.txt");
        string sym = word.GetWord();
        string[] syms = {"и", "в", "не", "на", "я"};
        Assert.IsTrue(syms.Contains(sym));
    }
    [TestMethod]
    public void TestMethod5()
    {
        WordGenerator word = new WordGenerator();
        Dictionary<string, int> map = new Dictionary<string, int>
        {
            {"aa", 10},
            {"bb", 20},
            {"cc", 30}
        };
        word.Load(map);
        string w = word.GetWord();
        Assert.IsTrue(w == "aa" | w == "bb" | w == "cc");
    }
    [TestMethod]
    public void TestMethod6()
    {
        WordGenerator word = new WordGenerator();
        word.Load("../../../test2.txt");
        string text = word.GetString(10);
        Assert.IsTrue(text.Split(" ", StringSplitOptions.RemoveEmptyEntries).Count() == 10);
    }
}
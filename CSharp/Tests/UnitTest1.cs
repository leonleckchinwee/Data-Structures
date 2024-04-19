using DSA.LinkedList;

namespace DSA.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public void Node_Test01()
    {
        LLNode<int> node = new LLNode<int>(1);

        Assert.That(node.Value, Is.EqualTo(1));
    }

    [Test]
    public void EmptyList_Test01()
    {
        LList<int> list = new LList<int>();

        Assert.That(list.Count, Is.EqualTo(0));
    }

    [Test]
    public void AddFirst_Test01()
    {
        LList<int> list = new LList<int>();

        list.AddFirst(1);

        Assert.That(list.Count, Is.EqualTo(1));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(1));
        Assert.That(list.Last.Value, Is.EqualTo(1));
        Assert.That(list.First, Is.EqualTo(list.Last));
    }

    [Test]
    public void AddFirst_Test02()
    {
        LList<int> list = new LList<int>();

        list.AddFirst(1);
        list.AddFirst(2);

        Assert.That(list.Count, Is.EqualTo(2));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(2));
        Assert.That(list.Last.Value, Is.EqualTo(1));
        Assert.That(list.First, !Is.EqualTo(list.Last));
    }

    [Test]
    public void AddFirst_Test03()
    {
        LList<int> list = new LList<int>();

        for (int i = 5; i > 0; --i)
        {
            list.AddFirst(i);
        }

        Assert.That(list.Count, Is.EqualTo(5));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(1));
        Assert.That(list.Last.Value, Is.EqualTo(5));
        Assert.That(list.First, !Is.EqualTo(list.Last));

        Assert.NotNull(list.First.Next);
        Assert.That(list.First.Next.Value, Is.EqualTo(2));

        Assert.NotNull(list.First.Next.Next);
        Assert.That(list.First.Next.Next.Value, Is.EqualTo(3));

        Assert.NotNull(list.First.Next.Previous);
        Assert.That(list.First.Next.Previous.Value, Is.EqualTo(1));

        Assert.NotNull(list.Last.Previous);
        Assert.That(list.Last.Previous.Value, Is.EqualTo(4));
    }

    [Test]
    public void AddFirst_Node_Test01()
    {
        LList<int> list = new LList<int>();
        LLNode<int> node = new LLNode<int>(1);

        list.AddFirst(node);

        Assert.That(list.Count, Is.EqualTo(1));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(1));
        Assert.That(list.Last.Value, Is.EqualTo(1));
        Assert.That(list.First, Is.EqualTo(list.Last));
    }

    [Test]
    public void AddFirst_Node_Test02()
    {
        LList<int> list = new LList<int>();
        LLNode<int> node = new LLNode<int>(1);
        LLNode<int> node2 = new LLNode<int>(2);

        list.AddFirst(node);
        list.AddFirst(node2);

        Assert.That(list.Count, Is.EqualTo(2));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(2));
        Assert.That(list.Last.Value, Is.EqualTo(1));
        Assert.That(list.First, !Is.EqualTo(list.Last));
    }

    [Test]
    public void AddFirst_Duplicate_Test01()
    {
        LList<int> list = new LList<int>();
        LLNode<int> node = new LLNode<int>(1);

        list.AddFirst(node);

        Assert.Throws<ArgumentException>(() => list.AddFirst(node));
    }

    [Test]
    public void AddFirst_Duplicate_Test02()
    {
        LList<int> list = new LList<int>();
        LList<int> list2 = new LList<int>();

        LLNode<int> node = new LLNode<int>(1);

        list.AddFirst(node);

        Assert.Throws<ArgumentException>(() => list2.AddFirst(node));
    }

    [Test]
    public void AddLast_Test01()
    {
        LList<int> list = new LList<int>();

        list.AddLast(1);

        Assert.That(list.Count, Is.EqualTo(1));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(1));
        Assert.That(list.Last.Value, Is.EqualTo(1));
        Assert.That(list.First, Is.EqualTo(list.Last));
    }

    [Test]
    public void AddLast_Test02()
    {
        LList<int> list = new LList<int>();

        list.AddLast(1);
        list.AddLast(2);

        Assert.That(list.Count, Is.EqualTo(2));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(1));
        Assert.That(list.Last.Value, Is.EqualTo(2));
        Assert.That(list.First.Next?.Value, Is.EqualTo(2));
        Assert.That(list.Last.Previous?.Value, Is.EqualTo(1));
    }

    [Test]
    public void AddLast_Node1_Test01()
    {
        LList<int> list = new LList<int>();
        LLNode<int> node = new LLNode<int>(1);

        list.AddLast(node);

        Assert.That(list.Count, Is.EqualTo(1));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(1));
        Assert.That(list.Last.Value, Is.EqualTo(1));
        Assert.That(list.First, Is.EqualTo(list.Last));
    }

    [Test]
    public void AddLast_Node_Test02()
    {
        LList<int> list = new LList<int>();
        LLNode<int> node = new LLNode<int>(1);
        LLNode<int> node2 = new LLNode<int>(2);

        list.AddLast(node);
        list.AddLast(node2);

        Assert.That(list.Count, Is.EqualTo(2));
        Assert.NotNull(list.First);
        Assert.NotNull(list.Last);

        Assert.That(list.First.Value, Is.EqualTo(1));
        Assert.That(list.Last.Value, Is.EqualTo(2));
        Assert.That(list.First.Next?.Value, Is.EqualTo(2));
        Assert.That(list.Last.Previous?.Value, Is.EqualTo(1));
    }
}
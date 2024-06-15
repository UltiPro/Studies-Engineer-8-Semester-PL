namespace SPA.Common;

public static class Lists
{
    public static bool AreListsEqual(List<string> list1, List<string> list2)
    {
        if (list1 == null && list2 == null) return true;
        if (list1 == null || list2 == null) return false;
        if (list1.Count != list2.Count) return false;

        var sortedList1 = list1.OrderBy(x => x).ToList();
        var sortedList2 = list2.OrderBy(x => x).ToList();

        return sortedList1.SequenceEqual(sortedList2);
    }
}